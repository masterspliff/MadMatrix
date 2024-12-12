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
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5267/") });
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITaskService, TaskServiceServer>();
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<ILocationService, LocationService>();

// Add Blazored LocalStorage
builder.Services.AddBlazoredLocalStorage();

// Register LoginService with mode configuration
// Get LoginMode from configuration

builder.Services.AddScoped<ILoginService>(sp => 
    new LoginServiceClientSide(
        sp.GetRequiredService<ILocalStorageService>(),
        sp.GetRequiredService<HttpClient>(),
        LoginMode.Online // Using Demo mode for offline testing
        ));



await builder.Build().RunAsync();
