using Microsoft.EntityFrameworkCore;
using HyggyBackend.BLL.Infrastructure;
using HyggyBackend.DAL.EF;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using HyggyBackend.BLL.Interfaces;
using HyggyBackend.BLL.Services;


var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
string? connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddControllers();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddHyggyContext(connection);
builder.Services.AddScoped<IOrderService, OrderService>();

builder.Services.AddIdentity<IdentityUser, IdentityRole>(opt =>
{
    opt.Password.RequiredLength = 6;
    opt.Password.RequireNonAlphanumeric = false;
    opt.Password.RequireDigit = false;
    opt.Password.RequireLowercase = false;
    opt.Password.RequireUppercase = false;
}).AddEntityFrameworkStores<HyggyContext>().AddDefaultTokenProviders();

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(opt =>
{
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

builder.Services.AddAuthorization(opt =>
{
    opt.AddPolicy("StoreManagerPolicy", policy => policy.RequireRole("Store manager"));
    opt.AddPolicy("StorekeeperPolicy", policy => policy.RequireRole("Storekeeper"));
    opt.AddPolicy("SellerPolicy", policy => policy.RequireRole("Seller"));
    opt.AddPolicy("AccountantPolicy", policy => policy.RequireRole("Accountant"));
    opt.AddPolicy("MainAccountantPolicy", policy => policy.RequireRole("Main Accountant"));
    opt.AddPolicy("CustomerPolicy", policy => policy.RequireRole("Customer"));
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
//app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
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
