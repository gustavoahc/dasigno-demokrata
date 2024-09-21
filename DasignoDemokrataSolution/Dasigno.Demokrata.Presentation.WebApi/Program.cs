using AutoMapper;
using Dasigno.Demokrata.Core.Application;
using Dasigno.Demokrata.Infrastructure.DataAccess;
using Dasigno.Demokrata.Infrastructure.DataAccess.Persistence;
using Dasigno.Demokrata.Presentation.WebApi.Helpers.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

var builder = WebApplication.CreateBuilder(args);

//Mapping configuration
var mappingConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingConfiguration());
});
IMapper mapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

string connectionString = builder.Configuration.GetConnectionString("DemokrataDatabase")!;

// Add services to the container.
builder.Services.AddDataAccessServices(connectionString)
    .AddApplicationServices();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var serviceScope = app.Services.CreateScope())
{
    var context = serviceScope.ServiceProvider.GetRequiredService<DemokrataContext>();
    await context.Database.EnsureCreatedAsync();
    RelationalDatabaseCreator databaseCreator = (RelationalDatabaseCreator)context.Database.GetService<IDatabaseCreator>();
    bool tableExists = await databaseCreator.HasTablesAsync();
    if (!tableExists)
    {
        await databaseCreator.CreateTablesAsync();
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers().WithOpenApi();

await app.RunAsync();
