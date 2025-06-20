using AT_TourManager.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Conectando meu DB ao meu context
builder.Services.AddDbContext<TourManagerContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("AzureSQLConnection")));

builder.Services.AddDbContext<IdentidadeContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("AzureSQLConnection")));

builder.Services.AddDefaultIdentity<IdentityUser>(
    options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<IdentidadeContext>();

//builder.Services.AddDefaultIdentity<IdentityUser>(
//    options => options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<IdentidadeContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
