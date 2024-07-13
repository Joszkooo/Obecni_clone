global using Obecni_clone.Server.Models;
global using Microsoft.AspNetCore.Mvc;
global using Obecni_clone.Server.Services.PracownikService;
global using AutoMapper;
global using Microsoft.EntityFrameworkCore;
global using Obecni_clone.Server.Data;
global using System.Text.Json.Serialization;
global using Obecni_clone.Server.Services.KlientService;
global using Obecni_clone.Server.Services.DniWolneService;
global using Obecni_clone.Server.Services.UrlopService;

using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<DataContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program).Assembly); 
builder.Services.AddScoped<IPracownikService, PracownikService>();
builder.Services.AddScoped<IKlientService, KlientService>();
builder.Services.AddScoped<IDniWolneService, DniWolneService>();
builder.Services.AddScoped<IUrlopService, UrlopService>();

// JSON serializer
builder.Services.AddControllers().AddNewtonsoftJson(options=>
options.SerializerSettings.ReferenceLoopHandling=Newtonsoft.Json.ReferenceLoopHandling.Ignore).AddNewtonsoftJson(
    options=>options.SerializerSettings.ContractResolver=new DefaultContractResolver());


var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
