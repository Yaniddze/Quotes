using Quotes.Db;
using Quotes.Domain;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
var services = builder.Services;

var assemblies = AppDomain.CurrentDomain.GetAssemblies();

services
    .AddDatabase(configuration)
    .AddDomain(assemblies);

services
    .AddControllers();

var app = builder.Build();

app.MapControllers();

app.Run();
