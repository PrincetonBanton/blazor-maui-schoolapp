using Microsoft.AspNetCore.ResponseCompression;
using SchoolApp.Server.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models; //Added for Swagger


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

//SQLITE
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Swagger Services
builder.Services.AddEndpointsApiExplorer(); 
builder.Services.AddSwaggerGen(c =>         
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "SchoolApp API", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.cd s
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();

    // Swagger Middleware
    app.UseSwagger();      
    app.UseSwaggerUI(c => 
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "SchoolApp API v1");
    });
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
app.UseRouting();
app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
