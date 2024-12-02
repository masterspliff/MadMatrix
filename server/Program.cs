using MongoDB.Driver;
using MongoDB.Bson;
using server.Repositories;
using server.Data;
using server.Repositories.Base;       
using core.Models;

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

// Check MongoDB password before starting server
var password = Environment.GetEnvironmentVariable("MONGODB_PASSWORD");
if (string.IsNullOrEmpty(password))
{
    Console.WriteLine("Please enter your MongoDB password:");
    password = Console.ReadLine();
    Environment.SetEnvironmentVariable("MONGODB_PASSWORD", password);
    builder.Services.AddSingleton<RepositoryFactory>();
}


// Validate MongoDB connection immediately
try 
{
    var mongoContext = new MongoDbContext(builder.Configuration);
    // If we get here, the connection was successful
    builder.Services.AddSingleton<MongoDbContext>(mongoContext);
}
catch (Exception ex)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("Failed to connect to MongoDB during startup. Shutting down.");
    Console.WriteLine($"Error: {ex.Message}");
    Console.ResetColor();
    Environment.Exit(1);
}

// Configure repositoryType inside appsettings.json
// Value: 'MongoDB' for online db
var repositoryType = builder.Configuration.GetValue<string>("RepositoryType");

// !! IMPORTANT !!
// The use of a singleton for IMongoCollection in the context of dependency injection in a .NET application
// is primarily for efficiency and consistency. Here are the key reasons:                            

// 1 Resource Management: MongoDB connections are resource-intensive. By using a singleton, you ensure that
// only one instance of the IMongoCollection is created and shared across the application, reducing 
// the overhead of creating multiple connections.                                                                                                                                                          
// 2 Consistency: A singleton ensures that all parts of the application use the same instance of the IMongoCollection,
// which can help prevent issues related to data consistency and state management.       
// 3 Performance: Creating a new instance of a collection for every request can be costly in terms of performance.
// A singleton allows the application to reuse the same instance, improving performance by   
// reducing the time and resources needed to establish new connections.                                                                                                                                    
// 4 Configuration: It centralizes the configuration of the MongoDB connection, making it easier to manage and update if needed.
// 5 Thread Safety: MongoDB's driver is designed to be thread-safe, so using a singleton is safe and recommended for managing database connections.        

if (repositoryType == "MongoDB") 
{
    // Register MongoDB repositories
    var collections = new Dictionary<Type, Func<MongoDbContext, object>>
    {
        { typeof(Event), context => context.Events },
        { typeof(Location), context => context.Locations },
        { typeof(User), context => context.Users },
        { typeof(TaskItem), context => context.Tasks }
    };

    foreach (var collection in collections)
    {
        builder.Services.AddSingleton(typeof(IMongoCollection<>).MakeGenericType(collection.Key), sp =>
        {
            var context = sp.GetRequiredService<MongoDbContext>();
            return collection.Value(context);
        });
    }
}
else
{
    // Register local repositories
    builder.Services.AddScoped<IEventRepository, LocalEventRepository>();
    builder.Services.AddScoped<ILocationRepository, LocalLocationRepository>();
    builder.Services.AddScoped<IUserRepository, LocalUserRepository>();
    builder.Services.AddScoped<ITaskRepository, LocalTaskRepository>();
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
