using FlatFileGenerator.Core.Models;
using FlatFileGenerator.DataAccess.Models;
using FlatFileGenerator.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<FlatFileContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly("FlatFileGenerator.Web")));
//builder.Services.AddDbContext<FlatFileContext>(options =>
//    options.UseInMemoryDatabase("FlatFileContext")
//    .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning)));


builder.Services.AddScoped<ReceiptDetailRepository>();
builder.Services.AddScoped<LogModelRepository>();
builder.Services.AddScoped<InputFileRepository>();
//builder.Services.AddDatabaseDeveloper

var app = builder.Build();

// Configure the HTTP request pipeline.½½
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Admin}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
