using WebAPI_Levinci.Models;
using WebAPI_Levinci.Services.UserService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.JsonWebTokens;
using WebAPI_Levinci.Data;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<LevinciContext>( options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("LevinciConnection")));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();

// Token
//builder.Services.AddAuthentication()
//    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme);
//builder.Services.AddIdentity<Users>();
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer(options =>
//    {
//        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
//        {
//            ValidateIssuer = true,
//            ValidateAudience = true,
//            ValidateLifetime = true,
//            ValidateIssuerSigningKey = true
//        }
//    });

var app = builder.Build();

//app.MapGet("/jwt", () =>
//{
//    var handler = new JsonWebTokenHandler();
//    var token = handler.CreateToken(new SecurityTokenDescriptor()
//    {
//        Issuer = "https//localhost:5000",
//        Subject = new ClaimsIdentity(new[]
//        {
//            new Claim("sub", Guid.NewGuid().ToString()),
//            new Claim("name", "Anton")
//        }),
//        SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.RsaSha256)
//    });
//});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseAuthentication();

app.MapControllers();

app.Run();
