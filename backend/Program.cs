using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// 🔐 JWT
var jwtKey = Environment.GetEnvironmentVariable("JWT__KEY") 
             ?? builder.Configuration["Jwt:Key"];

if (string.IsNullOrEmpty(jwtKey))
    throw new InvalidOperationException("JWT key is not configured.");
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

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DB
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlite("Data Source=app.db");
});

// CORS
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("AllowAll", p =>
        p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

// Port pour Scalingo
builder.WebHost.ConfigureKestrel(options =>
{
    var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
    options.ListenAnyIP(int.Parse(port));
});

var app = builder.Build();

// DB seeding
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();

    if (!await db.Movies.AnyAsync(m => m.Title == "Inception"))
    {
        db.Movies.Add(new Movie
        {
            Title = "Inception",
            Summary = "Un film de science-fiction sur les rêves partagés.",
            PosterUrl = "/posters/inception.jpg",
            TrailerUrl = "https://www.youtube.com/embed/CSSDgm0LlOw",
            Actors = new List<Actor>
            {
                new() { Name = "Carla", Bio = "Actrice et développeuse Web" },
                new() { Name = "Loïc", Bio = "Acteur et développeur Web" },
                new() { Name = "Julie", Bio = "Actrice, développeuse Web" },
                new() { Name = "Louna", Bio = "Actrice, communication" },
                new() { Name = "Loane", Bio = "Actrice, animatrice 2D/3D" },
                new() { Name = "Lyna", Bio = "Actrice, perchman" },
                new() { Name = "Zoé", Bio = "Actrice, productrice" },
                new() { Name = "Anas", Bio = "Développeur Web" },
                new() { Name = "Karl", Bio = "Acteur, audiovisuel" },
                new() { Name = "Julien", Bio = "Acteur, animateur 3D/2D" },
                new() { Name = "Victor", Bio = "Acteur et audiovisuel" }
            }
        });
        await db.SaveChangesAsync();
    }
}

// Middleware
if (Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")))
{
    app.UseStaticFiles();
}

app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.MapFallbackToFile("index.html");
Console.WriteLine(" Application lancée");

try
{
    app.Run();
}
catch (Exception ex)
{
    Console.WriteLine("Exception au démarrage : " + ex.Message);
    throw;
}
