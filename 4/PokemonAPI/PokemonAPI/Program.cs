using System.Reflection;
using PokemonAPI.Common.Mappings;
using PokemonAPI.DAL;
using PokemonAPI.DAL.Seeds;
using PokemonAPI.Interfaces;
using PokemonAPI.Middlewares;
using PokemonAPI.Services;
using DbContext = PokemonAPI.DAL.DbContext;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(config =>
{
    config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
    config.AddProfile(new AssemblyMappingProfile(typeof(DbContext).Assembly));
});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Services.AddSingleton<ExceptionMiddleware>();
builder.Services.AddScoped<IPokeApiService, PokeApiService>();

builder.Services.AddDataAccessLayer(builder.Configuration);
builder.Services.AddLogging();

builder.Services.AddHttpClient();

builder.Services.AddCors(opt
    => opt.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
        policy.AllowAnyOrigin();
    })
);

var app = builder.Build();

//var seeder = app.Services.CreateScope().ServiceProvider.GetRequiredService<ISeeder>();
//await seeder.SeedAsync();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.MapControllers();

app.UseCors(opt => opt.AllowAnyOrigin());

app.UseMiddleware<ExceptionMiddleware>();

app.Run();