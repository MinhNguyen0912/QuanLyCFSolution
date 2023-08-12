using AutoMapper.Extensions.ExpressionMapping;
using GemBox.Spreadsheet;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using QuanLyCF.BLL.Services.Implements;
using QuanLyCF.BLL.Services.Interfaces;
using QuanLyCF.DAL.DBContext;
using QuanLyCF.DAL.Entities;
using System.Reflection;
using System.Text;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(config => { config.AddExpressionMapping(); }, Assembly.GetExecutingAssembly(),
    Assembly.GetEntryAssembly());

//DI
builder.Services.AddTransient<UserManager<User>, UserManager<User>>();
builder.Services.AddTransient<RoleManager<Role>, RoleManager<Role>>();
builder.Services.AddTransient<SignInManager<User>, SignInManager<User>>();

builder.Services.AddTransient<IBillServices, BillServices>();
builder.Services.AddTransient<ICategoryServices, CategoryServices>();
builder.Services.AddTransient<IFoodServices, FoodServices>();
builder.Services.AddTransient<ITableServices, TableServices>();
builder.Services.AddTransient<IBillInfoServices, BillInfoServices>();

builder.Services.AddTransient<IUserServices, UserServices>();

SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");


builder.Services.AddIdentity<User, Role>(option => option.Password = new PasswordOptions()
{
    RequireDigit = false,
    RequiredLength = 1,
    RequireLowercase = false,
    RequireUppercase = false,
    RequireNonAlphanumeric = false,
})
    .AddEntityFrameworkStores<QuanLyCafeDBContext>()
    .AddDefaultTokenProviders();

builder.Services.AddDbContext<QuanLyCafeDBContext>(options =>
             options.UseSqlServer(builder.Configuration.GetConnectionString("QuanLyCafeDB")));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

builder.Services.AddCors(policy =>
{
    policy.AddPolicy("_myAllowSpecificOrigins", builder =>
     builder.WithOrigins("https://localhost:7085")
      .SetIsOriginAllowed((host) => true) // this for using localhost address
      .AllowAnyMethod()
      .AllowAnyHeader()
      .AllowCredentials());
});
//


var app = builder.Build();
app.UseCors("_myAllowSpecificOrigins");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();
app.MapControllers();

app.Run();
