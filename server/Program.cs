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

// Check MongoDB password before starting server
var password = Environment.GetEnvironmentVariable("MONGODB_PASSWORD");
if (string.IsNullOrEmpty(password))
{
    Console.WriteLine("Please enter your MongoDB password:");
    password = Console.ReadLine();
    Environment.SetEnvironmentVariable("MONGODB_PASSWORD", password);
}

// Validate MongoDB connection immediately
try 
{
    var mongoContext = new MongoDbContext(builder.Configuration);
    // If we get here, the connection was successful
    builder.Services.AddSingleton(mongoContext);
}
catch (Exception ex)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("Failed to connect to MongoDB during startup. Shutting down.");
    Console.WriteLine($"Error: {ex.Message}");
    Console.ResetColor();
    Environment.Exit(1);
}

// Register repositories
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<ILocationRepository, LocationRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();

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
