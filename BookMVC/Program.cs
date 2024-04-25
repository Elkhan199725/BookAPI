var builder = WebApplication.CreateBuilder(args);

// Register IHttpClientFactory
builder.Services.AddHttpClient("BookAPI", client =>
{
    client.BaseAddress = new Uri("https://localhost:7291/api"); // Adjust the port accordingly
    // Optional: Configure default headers or other settings
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
