using Cursos.Application.Helpers;
using Cursos.CrossCutting;
using Cursos.IoC.ServiceCollectionExtensions;
using Cursos.WebApp.Extensions;
using Microsoft.Extensions.Hosting;

LoggingSingleton.InitializeLog();
Logging log = LoggingSingleton.GetLogging();
var builder = WebApplication.CreateBuilder(args);
builder.Host.ConfigureAppSettings();

// Add services to the container.
builder.Host.ConfigureServices((context, services) =>
{
    services.RegisterServices(context.Configuration, log).AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
});
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
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
