using E_Commerce.Context;
using E_Commerce.MiddleWare;
using E_Commerce.Model;
using E_Commerce.Service;
using E_Commerce.Service.Interface;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);


//Add SQL-Server Connection
builder.Services.AddDbContext<ECommerceDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("E-Commerce")));


////Add Services
builder.Services.AddScoped(typeof(IService<>), typeof(ServiceRepo<>));






builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{   
    app.UseSwagger();
    app.UseSwaggerUI();

    //Add MiddleWares
    app.UseMiddleware<ErrorHandler>();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
