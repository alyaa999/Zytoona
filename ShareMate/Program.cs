using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ShareMate.DbContext;
using ShareMate.IdentityRepo;
using ShareMate.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


// Other configurations...

builder.Services.AddDbContext<DbContextApplication>(Options => Options
.UseSqlServer(builder.Configuration.GetConnectionString("Data")));


builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<DbContextApplication>();


builder.Services.AddCors(option => option
            .AddDefaultPolicy(build => build.AllowAnyOrigin()
            .AllowAnyMethod().AllowAnyHeader()));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignOutScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
.AddCookie(options =>
{
    options.LoginPath = "/api/Account/login";
    options.AccessDeniedPath = "/api/account/accessdenied";
    options.ExpireTimeSpan = TimeSpan.FromDays(30); // Adjust as needed
});


builder.Services.AddScoped<UserManager<User>, UserManager<User>>();
builder.Services.AddTransient<IIdentityInterface, IdentityRepository>();



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
