using API.EndPoints;
using Application.Observations;
using Domain.Common;
using Domain.Observations;
using Microsoft.EntityFrameworkCore;
using Persistence.Database;
using Persistence.Repositories;
using Persistence.UnitOfWork;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ObservationsService>();

builder.Services.AddScoped<IObservationsRepository, ObservationsRepository>();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseInMemoryDatabase("ArchitectureConcepts");
});
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapObservationsEndpoints();
app.UseHttpsRedirection();

app.Run();