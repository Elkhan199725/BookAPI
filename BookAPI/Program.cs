using BookAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using BookAPI.Helpers;
using BookAPI;

var builder = WebApplication.CreateBuilder(args);

// Register IHttpContextAccessor
builder.Services.AddHttpContextAccessor();


// Register FileManager as a scoped service
builder.Services.AddScoped<FileManager>();

// Add services to the container.
builder.Services.AddDbContext<BookDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddControllers();
builder.Services.AddRepositories();

// CORS configuration
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.WithOrigins("https://localhost:7134") // Specify the allowed origin
                          .AllowAnyMethod() // Allows all methods
                          .AllowAnyHeader()); // Allows all headers
});

// Swagger configuration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); // Provides detailed error responses in development
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Error"); // A generic error handler in production
    app.UseHsts(); // Enforces HTTPS
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Apply CORS policy
app.UseCors("AllowSpecificOrigin");

app.UseAuthorization();
app.MapControllers();

app.Run();
