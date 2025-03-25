using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;

using LabManagementApp;
using System.Net.Http.Json;
using LabManagementApp.Models;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

using var http = new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) };
var config = await http.GetFromJsonAsync<AppSettings>("appsettings.json");

if(config != null)
{
    builder.Services.AddSingleton(config);
}

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
//builder.Services.AddScoped(sp => new ApiService(new HttpClient { BaseAddress = new Uri("http://localhost:5000/") }));
builder.Services.AddScoped(sp => new ApiService(new HttpClient { BaseAddress = new Uri(config.BaseApiUrl) }));
builder.Services.AddScoped(sp => new SupervisorService(new HttpClient { BaseAddress = new Uri(config.BaseApiUrl) }));
builder.Services.AddScoped(sp => new LaboratoryService(new HttpClient { BaseAddress = new Uri(config.BaseApiUrl) }));
builder.Services.AddScoped(sp => new RoomService(new HttpClient { BaseAddress = new Uri(config.BaseApiUrl) }));
builder.Services.AddScoped(sp => new ComputerService(new HttpClient { BaseAddress = new Uri(config.BaseApiUrl) }));

/*
builder.Services.AddScoped<ApiService>();
builder.Services.AddScoped<SupervisorService>();
builder.Services.AddScoped<LaboratoryService>();
builder.Services.AddScoped<RoomService>();
builder.Services.AddScoped<ComputerService>();
*/
await builder.Build().RunAsync();
