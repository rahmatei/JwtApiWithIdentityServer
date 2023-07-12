using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Student.Identity.Data;

var builder = WebApplication.CreateBuilder(args);

var assembly=typeof(Program).Assembly.GetName().Name;

var defaultConnectionString= builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AspIdentityDbContext>(options =>
    options.UseSqlServer(defaultConnectionString,
        b=>b.MigrationsAssembly(assembly)));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AspIdentityDbContext>().AddDefaultTokenProviders();

builder.Services.AddIdentityServer()
    .AddAspNetIdentity<IdentityUser>()
    .AddConfigurationStore(options =>
    {
        options.ConfigureDbContext=b=>b.UseSqlServer(defaultConnectionString,opt=>opt.MigrationsAssembly(assembly));
    })
    .AddOperationalStore(options=> {
        options.ConfigureDbContext = b => b.UseSqlServer(defaultConnectionString, opt => opt.MigrationsAssembly(assembly));
    })
    .AddDeveloperSigningCredential();


var app = builder.Build();

app.UseIdentityServer();

app.Run();
