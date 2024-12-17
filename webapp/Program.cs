using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using webapp;
using webapp.Service;
using webapp.Service.LoginService;
using Blazored.LocalStorage;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Use relative path for API calls
// Vi prøver lige noget andet nu
var baseAddress = builder.HostEnvironment.IsDevelopment() 
    ? "http://localhost:5267/" 
    : "/api/";
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(baseAddress) });
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITaskService, TaskServiceServer>();
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddScoped<ILoginService, LoginServiceClientSide>();

// Add Blazored LocalStorage
builder.Services.AddBlazoredLocalStorage();



await builder.Build().RunAsync();
