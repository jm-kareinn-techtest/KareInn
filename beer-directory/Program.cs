using BeerDirectory.Core.Repositories;
using BeerDirectory.Core.Repositories.Client;
using BeerDirectory.Core.Repositories.Interfaces;
using BeerDirectory.Core.Services;
using BeerDirectory.Core.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();

// connection
builder.Services.AddSingleton<BeerDirectoryMongoClient, BeerDirectoryMongoClient>(_ =>
	new BeerDirectoryMongoClient(builder.Configuration["Data:MongoClient:ConnectionString"],
		builder.Configuration["Data:MongoClient:DbName"]));

// repositories
builder.Services.AddScoped<IBeerRepository, BeerRepository>();
			
// services
builder.Services.AddTransient<IBeerService, BeerService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
	name: "default",
	pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");
;

app.Run();