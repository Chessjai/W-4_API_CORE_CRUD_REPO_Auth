using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using W_4_API_CORE_CRUD_REPO;
using W_4_API_CORE_CRUD_REPO.Repository;

var builder = WebApplication.CreateBuilder(args);
var TokenValidationParameters = new TokenValidationParameters
{
    ValidIssuer = "https://fbi-demo.com",
    ValidAudience = "https://fbi-demo.com",
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SXkSqsKyNUyvGbnHs7ke2NCq8zQzNLW7mPmHbnZZ")),
    ClockSkew = TimeSpan.Zero // remove delay of token when expire
};
builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(cfg =>
    {
        cfg.TokenValidationParameters = TokenValidationParameters;
    });
//builder.Services.AddAuthorization(cfg =>
//{
//    cfg.AddPolicy("Admin", policy => policy.RequireClaim("type", "Admin"));
//    cfg.AddPolicy("Agent", policy => policy.RequireClaim("type", "Agent"));
//    cfg.AddPolicy("ClearanceLevel1", policy => policy.RequireClaim("ClearanceLevel", "1", "2"));
//    cfg.AddPolicy("ClearanceLevel2", policy => policy.RequireClaim("ClearanceLevel", "2"));
//});
// Add services to the container.


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "FBI", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter JWT with Bearer into field",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
    { new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer"}
            },
        new string[] {}
    }
    });
});
builder.Services.AddScoped<Iproduct, ProductRepository>();


builder.Services.AddDbContext<CoreDbcontext>(options =>
options.UseSqlServer(builder.Configuration.GetSection("ConnectionStrings:Database").Value));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseDefaultFiles();
app.UseStaticFiles();

app.UseHttpsRedirection();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
