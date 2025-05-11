using Quotes.Domain;
using Quotes.MoexProvider;
using Quotes.Provider.Db;
using Quotes.Services;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
var services = builder.Services;

var assemblies = AppDomain.CurrentDomain.GetAssemblies();

services
    .AddDatabase(configuration)
    .AddDomain(assemblies)
    .AddServices()
    .AddMoexProvider(configuration);

services
    .AddSwaggerGen();

services
    .AddControllers();

var app = builder.Build();

app
    .UseSwagger()
    .UseSwaggerUI(x =>
    {
        x.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    });

app.MapControllers();

app.Run();
