using BuildingBlocks.Behaviors;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCarter(null, config =>
{
    var modules = typeof(Program).Assembly.GetTypes().Where(t => t.IsAssignableTo(typeof(ICarterModule))).ToArray();
    config.WithModules(modules);
});

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
});


builder.Services.AddMarten(opt =>
{
    opt.Connection(builder.Configuration.GetConnectionString("CatalogConnection")!);
}).UseLightweightSessions();


builder.Services.AddExceptionHandler<CustomExceptionHandler>();



var app = builder.Build();


app.MapCarter();
app.UseExceptionHandler(options => { });
app.Run();

