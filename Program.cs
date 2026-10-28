using DSW_3P_PR01.Repositories;
using DSW_3P_PR01.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddSingleton<IMisionRepository>(_ =>
    new MisionJsonRepository(Path.Combine(builder.Environment.ContentRootPath, "misiones.json")));
builder.Services.AddScoped<IMisionService, MisionService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
