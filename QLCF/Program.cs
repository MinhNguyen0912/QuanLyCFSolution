using BlazorBootstrap;
using Blazored.SessionStorage;
using GemBox.Spreadsheet;
using Havit.Blazor.Components.Web;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using QLCF;
using QLCF.Repositories;
using QLCF.Repositories.Interfaces;
using QLCF.Repositories.Services;
using Syncfusion.Blazor;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient("examapi",
    client => { client.BaseAddress = new Uri("https://localhost:7085"); });
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7085") });

builder.Services.AddTransient<IUserRepos, UserRepos>();

builder.Services.AddTransient<IBillInfoRepos, BillInfoRepos>();
builder.Services.AddTransient<IBillRepos, BillRepos>();
builder.Services.AddTransient<ITableRepos, TableRepos>();
builder.Services.AddTransient<ICategoryRepos, CategoryRepos>();
builder.Services.AddTransient<IFoodRepos, FoodRepos>();



builder.Services.AddScoped<BlazorSchoolAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(sp => sp.GetRequiredService<BlazorSchoolAuthenticationStateProvider>());

SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");


builder.Services.AddAuthentication();
builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();


builder.Services.AddBlazoredSessionStorage();
builder.Services.AddSyncfusionBlazor();


builder.Services.AddHxServices();        // <------ ADD THIS LINE

builder.Services.AddBlazorBootstrap(); // Add this line



await builder.Build().RunAsync();
