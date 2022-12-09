using AutoMapper;
using BKS.Task.BL.Interfaces;
using BKS.Task.BL.Mapping;
using BKS.Task.BL.Services;
using BKS.Task.DL;
using BKS.Task.DL.Interfaces;
using BKS.Task.DL.Repositories;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//DI
builder.Services.AddScoped<IUserMessageRepository, UserMessageRepository>();
builder.Services.AddScoped<IUserMessageService, UserMessageService>();

//AutoMapper
var mapperConfig = new MapperConfiguration((v) =>
{
    v.AddProfile(new MappingProfile());
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

//DB Connection
var connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<BKSContext>(options => options.UseSqlServer(connection));

//Logger configs
Log.Logger = new LoggerConfiguration()
    .CreateLogger();

var environment =
    Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{environment}.json", true)
    .AddEnvironmentVariables().Build();


builder.Host.UseSerilog((hostingContext, loggerConfiguration) =>

    loggerConfiguration.ReadFrom.Configuration(config, "Serilog"));
try
{
    var app = builder.Build();
    Log.Information($"Web Api started on {DateTime.UtcNow}");

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
}
catch (Exception e)
{
    Console.WriteLine(e);
    throw;
}

