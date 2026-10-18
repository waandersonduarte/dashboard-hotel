using HotelDashboard.Api.Converters;
using HotelDashboard.Api.Filters;
using HotelDashboard.Application;
using HotelDashboard.Infrastructure;
using HotelDashboard.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new StringConverter()));;
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddMvc(options => options.Filters.Add<FilterException>());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.UseHttpsRedirection();

await ExecuteMigrationsAsync();

app.Run();

async Task ExecuteMigrationsAsync()
{
    await using var scope = app.Services.CreateAsyncScope();
    DatabaseMigration.ExecuteMigrations(scope.ServiceProvider);
}