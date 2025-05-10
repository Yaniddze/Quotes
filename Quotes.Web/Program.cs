using Quotes.Domain;
using Quotes.MoexProvider;
using Quotes.Provider.Db;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
var services = builder.Services;

var assemblies = AppDomain.CurrentDomain.GetAssemblies();

services
    .AddDatabase(configuration)
    .AddDomain(assemblies)
    .AddMoexProvider(configuration);

services
    .AddControllers();

var app = builder.Build();

app.MapControllers();

app.Run();
