using Microsoft.EntityFrameworkCore;
using Student_Internship_Tracker.Models;
using Microsoft.Extensions.DependencyInjection;
using Student_Internship_Tracker.Data;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
// Bring in database context with dependency injection.
builder.Services.AddDbContext<InternTrackContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("InternTrackConnection") ?? 
    "Data Source=InternTrack.db"));

var app = builder.Build();

// Seed Data
using (var scope = app.Services.CreateScope())
{
    SeedData.Initialize(scope.ServiceProvider);
}

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
