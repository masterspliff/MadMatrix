using MongoDB.Driver;
using MongoDB.Bson;
using server.Repositories;
using server.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:5172")
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

// Configure MongoDB
static IMongoClient ConfigureMongoDB(IConfiguration configuration)
{
    var password = Environment.GetEnvironmentVariable("MONGODB_PASSWORD");
    if (string.IsNullOrEmpty(password))
    {
        Console.WriteLine("Please enter your MongoDB password:");
        password = Console.ReadLine();
    }

    var baseConnectionString = configuration.GetConnectionString("MongoDB");
    var connectionString = baseConnectionString?.Replace("{password}", password);
    
    var settings = MongoClientSettings.FromConnectionString(connectionString);
    settings.ServerApi = new ServerApi(ServerApiVersion.V1);
    
    return new MongoClient(settings);
}

// Setup MongoDB client and test connection
var mongoClient = ConfigureMongoDB(builder.Configuration);
builder.Services.AddSingleton<IMongoClient>(mongoClient);

// Register MongoDbContext
builder.Services.AddSingleton<MongoDbContext>(sp => 
{
    var connectionString = builder.Configuration.GetConnectionString("MongoDB")
        ?.Replace("{password}", Environment.GetEnvironmentVariable("MONGODB_PASSWORD") ?? "");
    return new MongoDbContext(connectionString!, "MadMatrix");
});

// Register repositories
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<ILocationRepository, LocationRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();

try 
{
    var database = mongoClient.GetDatabase("MadMatrix");
    database.RunCommand<BsonDocument>(new BsonDocument("ping", 1));
    Console.WriteLine("Successfully connected to MongoDB MadMatrix database!");
} 
catch (Exception ex) 
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine($"MongoDB connection failed: {ex.Message}");
    Console.ResetColor();
    throw;
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "MadMatrix API V1");
        c.RoutePrefix = string.Empty; // Serve Swagger UI at the root
    });
}

app.UseAuthentication();
app.UseAuthorization();
app.UseCors();

app.MapControllers();

app.Run();
