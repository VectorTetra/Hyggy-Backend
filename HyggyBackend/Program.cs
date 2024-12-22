using HyggyBackend.BLL.Infrastructure;
using HyggyBackend.BLL.Services.EmailService;
using HyggyBackend.DAL.EF;
using HyggyBackend.DAL.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
//using Microsoft.Extensions.DependencyInjection; // Додано для правильного підключення AddNewtonsoftJson
//using Newtonsoft.Json;
//using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
//using Newtonsoft.Json.Serialization; // Додано для підтримки NewtonsoftJson

var builder = WebApplication.CreateBuilder(args);

//var cultureInfo = new CultureInfo("en-US");
//CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
//CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

var emailConfig = builder.Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>();
builder.Services.AddCors();
builder.Services.AddSingleton(emailConfig!);
// Add services to the container.
string? connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddUnitOfWorkService();
//builder.Services.AddControllers().AddJsonOptions(options =>
//{
//    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
//});

builder.Services.AddControllers();

builder.Services.AddDistributedMemoryCache();


builder.Services.AddHyggyContext(connection);

builder.Services
    .AddIdentity<User, IdentityRole>(
        opt =>
        {
            opt.Password.RequiredLength = 6;
            opt.Password.RequireNonAlphanumeric = false;
            opt.Password.RequireDigit = false;
            opt.Password.RequireLowercase = false;
            opt.Password.RequireUppercase = false;
            opt.SignIn.RequireConfirmedEmail = true;
        })
    .AddEntityFrameworkStores<HyggyContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<DataProtectionTokenProviderOptions>(opt => opt.TokenLifespan = TimeSpan.FromHours(4));

builder.Services
    .AddAuthentication(
        options =>
        {
            options.DefaultAuthenticateScheme =
    options.DefaultChallengeScheme =
                options.DefaultForbidScheme =
                options.DefaultScheme =
                options.DefaultSignInScheme =
                options.DefaultSignOutScheme =
                JwtBearerDefaults.AuthenticationScheme;
        })
    .AddJwtBearer(
        opt =>
        {
            opt.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidateAudience = true,
                ValidAudience = builder.Configuration["JWT:Audience"],
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
            };
        });

builder.Services
    .AddAuthorization(
        opt =>
        {
            opt.AddPolicy("StoreManagerPolicy", policy => policy.RequireRole("Store manager"));
            opt.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
            opt.AddPolicy("StorekeeperPolicy", policy => policy.RequireRole("Storekeeper"));
            opt.AddPolicy("SellerPolicy", policy => policy.RequireRole("Seller"));
            opt.AddPolicy("AccountantPolicy", policy => policy.RequireRole("Accountant"));
            opt.AddPolicy("MainAccountantPolicy", policy => policy.RequireRole("Main Accountant"));
            opt.AddPolicy("CustomerPolicy", policy => policy.RequireRole("Customer"));
        });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddSwaggerGen(
        option =>
        {
            option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
            option.AddSecurityDefinition(
                "Bearer",
                new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
            option.AddSecurityRequirement(
                new OpenApiSecurityRequirement
                {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                    },
                    new string[] { }
                }
                });
        });

var app = builder.Build();
app.UseCors(
    builder => builder
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod());
// Configure the HTTP request pipeline.
if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.UseDefaultFiles();
app.UseStaticFiles();
app.UseHttpsRedirection();
app.MapControllers();

app.Run();
