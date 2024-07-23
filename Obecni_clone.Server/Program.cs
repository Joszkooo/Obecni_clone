global using Microsoft.AspNetCore.Mvc;
global using AutoMapper;
global using Microsoft.EntityFrameworkCore;
global using Obecni_clone.Server.Data;
global using System.Text.Json.Serialization;
global using Obecni_clone.Server.Models;
global using Obecni_clone.Server.Services.PracownikService;
global using Obecni_clone.Server.Services.KlientService;
global using Obecni_clone.Server.Services.DniWolneService;
global using Obecni_clone.Server.Services.UrlopService;
global using Obecni_clone.Server.Services.HDService;
global using Obecni_clone.Server.Services.RejestrService;
global using System.ComponentModel.DataAnnotations;
global using Obecni_clone.Server.Dtos.HD;
global using Obecni_clone.Server.Dtos.Klient;
global using Obecni_clone.Server.Dtos.Rejestr;


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
builder.Services.AddScoped<IRejestrService, RejestrService>();
builder.Services.AddScoped<IListaHDService, ListaHDService>();


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
    app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyAPI"); c.InjectStylesheet("/swagger-ui/SwaggerDark.css"); });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
