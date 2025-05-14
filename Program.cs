using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Student_Internship_Tracker.Data;
using Student_Internship_Tracker.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Configure database context
builder.Services.AddDbContext<InternTrackContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("InternTrackContext") ?? 
                     "Data Source=InternTrack.db"));

// Add logging
builder.Logging.AddConsole();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

// Configure static files with caching disabled for development
app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = ctx =>
    {
        if (app.Environment.IsDevelopment())
        {
            var path = ctx.Context.Request.Path;
            app.Logger.LogInformation($"Static file requested: {path}");
            ctx.Context.Response.Headers["Cache-Control"] = "no-cache, no-store";
        }
    }
});

app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();

// Initialize database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<InternTrackContext>();
        context.Database.EnsureCreated(); // Ensure database is created
        SeedData.Initialize(services);
        app.Logger.LogInformation("Database initialized successfully.");
    }
    catch (Exception ex)
    {
        app.Logger.LogError(ex, "An error occurred while initializing the database.");
        throw; // Rethrow to fail fast in development
    }
}

app.Run();
