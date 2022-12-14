using DocumentService.Controllers;
using DocumentService.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using DocumentService.AsyncDataServices;
using DocumentService.EventProcessing;

var builder = WebApplication.CreateBuilder(args);


IConfiguration configuration = builder.Configuration;
IWebHostEnvironment environment = builder.Environment;

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSingleton<IMongoClient>(s => new MongoClient(configuration.GetConnectionString("MongoDb")));
builder.Services.AddScoped(s => new AppDbContext(s.GetRequiredService<IMongoClient>(), configuration["DbName"]));
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("Document"));
builder.Services.AddScoped<IDocumentRepo, DocumentRepo>();
builder.Services.AddHttpClient<DocumentController>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddHostedService<MessageBusSubscriber>();
builder.Services.AddTransient<IEventProcessor, EventProcessor>(); 


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// app.MapRazorPages();

app.MapControllers();

app.Run();