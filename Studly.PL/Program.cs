using System.Runtime.Loader;
using Microsoft.EntityFrameworkCore;
using Ninject;
using Ninject.Modules;
using Studly;
using Studly.BLL.Infrastructure;
using Studly.BLL.Services;
using Studly.Interfaces;
using Studly.PL.Util;
using Studly.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

NinjectModule customerModule = new CustomerModule();
NinjectModule serviceModule = new ServiceModule("DefaultConnection");
var kernel = new StandardKernel(customerModule, serviceModule);
GlobalConfiguration.Configuration.DependencyResolver = new NinjectDependencyResolver(kernel);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
