using server.Repositories;
using server.Repositories.Login;
using server.Data;
using webapp.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

// Configure the CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        builder => builder
            .WithOrigins(
                "https://kantinen-frontend.azurewebsites.net",
                "http://localhost:5117",  // Local development URL
                "https://localhost:7112"  // Local development HTTPS URL
            )
            .AllowAnyMethod()
            .AllowAnyHeader());
});

// Validate MongoDB connection immediately yes yes
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
builder.Services.AddScoped<ILoginRepository, LoginRepositoryMongoDB>();


// Register HTTP client services
builder.Services.AddHttpClient();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "MadMatrix API V1");
    c.RoutePrefix = string.Empty; // Serve Swagger UI at the root
});

app.UseCors("AllowFrontend");

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
