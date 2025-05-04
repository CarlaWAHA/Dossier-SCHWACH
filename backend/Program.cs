using Microsoft.EntityFrameworkCore;
using backend.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// 🔐 Configuration JWT
var jwtKey = builder.Configuration["Jwt:Key"];
var key = Encoding.ASCII.GetBytes(jwtKey);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
        ClockSkew = TimeSpan.Zero
    };
});

// 📦 Services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("AllowAll", p =>
        p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

var app = builder.Build();

// 🔧 Middleware
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCors("AllowAll");

app.UseAuthentication(); // Auth avant Authorization
app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

// 🎬 Injection initiale si pas encore de films
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
            new() { Name = "Carla", Bio = "Actrice et développeuse Web" },
            new() { Name = "Loïc", Bio = "Acteur et développeur Web" },
            new() { Name = "Julie", Bio = "Actrice, développeuse Web" },
            new() { Name = "Louna", Bio = "Actrice, communication" },
            new() { Name = "Loane", Bio = "Actrice, animatrice 2D/3D" },
            new() { Name = "Lyna", Bio = "Actrice, perchman" },
            new() { Name = "Anas", Bio = "Développeur Web" },
            new() { Name = "Karl", Bio = "Acteur, audiovisuel" },
            new() { Name = "Julien", Bio = "Acteur, animateur 3D/2D" }
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
