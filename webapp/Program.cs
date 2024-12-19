using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using webapp;
using webapp.Service;
using webapp.Service.LoginService;
using Blazored.LocalStorage;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Listening on the server port (5267)
builder.Services.AddScoped(sp =>
{
    string baseAddress;
    if (builder.HostEnvironment.IsDevelopment())
    {
        baseAddress = "http://localhost:5267/";
    }
    else
    {
        baseAddress = "kantinen-server.azurewebsites.net"
                      ?? throw new InvalidOperationException("API_BASE_URL environment variable not configured");
    }
    Console.WriteLine($"API_BASE_URL: {baseAddress}");
    return new HttpClient { BaseAddress = new Uri(baseAddress) };
});

// builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5267/") });


builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITaskService, TaskServiceServer>();
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddScoped<ILoginService, LoginServiceClientSide>();

// Add Blazored LocalStorage
builder.Services.AddBlazoredLocalStorage();



await builder.Build().RunAsync();
