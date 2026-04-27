using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PIZZAORDERING.Data;
using PIZZAORDERING.Dtos;
using PIZZAORDERING.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var JwtSetting = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,           // ← added
        ValidateAudience = true,         // ← added
        ValidateLifetime = true,         // ← added
        ValidateIssuerSigningKey = true,
        ValidIssuer = JwtSetting.Issuer,
        ValidAudience = JwtSetting.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(JwtSetting.SecretKey))
    };
});

builder.Services.AddAuthorization();
builder.Services.AddScoped<AuthServices>();


var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthentication();   // ← must be before UseAuthorization
app.UseAuthorization();
app.MapControllers();
app.Run();