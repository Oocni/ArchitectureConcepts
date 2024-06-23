using ArchitectureConcepts.Clean.Core.Application.Observations.Add;
using ArchitectureConcepts.Clean.Core.Application.Observations.GetAll;
using ArchitectureConcepts.Clean.Core.Domain.Observations;
using ArchitectureConcepts.Clean.External.Persistence.Repositories;
using ArchitectureConcepts.Common.Core.Domain.Common;
using ArchitectureConcepts.Common.External.Persistance.Database;
using ArchitectureConcepts.Common.External.Persistance.UnitOfWork;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IObservationsRepository, ObservationsRepository>();
builder.Services.AddMediatR(conf
    => conf.RegisterServicesFromAssemblies(typeof(AddObservationCommand).Assembly,
        typeof(GetAllObservationsQuery).Assembly));

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

app.UseHttpsRedirection();

app.Run();