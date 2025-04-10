using Microsoft.EntityFrameworkCore;
using backend.Data; // 👈 Ce namespace doit correspondre à là où est AppDbContext

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer(); // nécessaire
builder.Services.AddSwaggerGen();           // pour Swagger
builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("AllowAll", p =>
        p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

var app = builder.Build();

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.UseCors("AllowAll");
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();

// Injecter automatiquement un film et ses acteurs au démarrage
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

   if (!await context.Movies.AnyAsync(m => m.Title == "Inception"))
    {
        var movie = new backend.Models.Movie
        {
            Title = "Inception",
            Summary = "Un film de science-fiction sur les rêves partagés.",
            PosterUrl = "/posters/inception.jpg",
            TrailerUrl = "/trailers/inception.mp4",                  
            Actors = new List<backend.Models.Actor>
        {
            new() { Name = "Léa", Bio = "Léa, née le 4 août 2003, actrice montante dans le cinéma français..." },
            new() { Name = "A", Bio = "A est un acteur célèbre dans les films d’action modernes." },
            new() { Name = "B", Bio = "B est connu pour ses rôles comiques dans le cinéma français." },
            new() { Name = "C", Bio = "C est une actrice internationale reconnue pour son charisme." },
            new() { Name = "D", Bio = "D a été nommé à plusieurs prix dans le cinéma indépendant." },
            new() { Name = "E", Bio = "E est réputé pour ses performances intenses et dramatiques." },
            new() { Name = "F", Bio = "F est aussi chanteur, il combine musique et cinéma." },
            new() { Name = "G", Bio = "G est un jeune prodige ayant débuté dans le théâtre." }
        }
        };

        context.Movies.Add(movie);
        await context.SaveChangesAsync();
    }
}

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
