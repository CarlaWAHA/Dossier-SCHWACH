using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// 🔐 JWT Configuration
var jwtKey = builder.Configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT key is not configured.");
var key = Encoding.ASCII.GetBytes(jwtKey);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false; // ❗ À mettre à true en prod
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

// ✅ Force le port 5000 (même si PORT n'est pas défini)
builder.WebHost.ConfigureKestrel(options =>
{
    var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
    options.ListenAnyIP(int.Parse(port));
});

var app = builder.Build();

// ⚙️ Migrations automatiques + données de démo
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
            TrailerUrl = "/trailers/inception.mp4",
            Actors = new List<Actor>
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
        });
        await db.SaveChangesAsync();
    }
}

// 🔧 Middleware
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

// 🧠 Pour apps SPA (Vue)
app.MapFallbackToFile("index.html");

app.Run();

// 🧪 Test only (non utilisé)
record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
