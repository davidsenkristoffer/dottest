using Microsoft.AspNetCore.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddRateLimiter(x => 
{
    x.AddFixedWindowLimiter("fixed", options => 
    {
        options.PermitLimit = 1;
        options.AutoReplenishment = true;
        options.QueueProcessingOrder = System.Threading.RateLimiting.QueueProcessingOrder.OldestFirst;
        options.Window = TimeSpan.FromMinutes(1);
    });
    x.AddSlidingWindowLimiter("sliding", options =>
    {
       options.PermitLimit = 1;
       options.Window = TimeSpan.FromMinutes(1);
       options.QueueProcessingOrder = System.Threading.RateLimiting.QueueProcessingOrder.OldestFirst;
       options.AutoReplenishment = true; 
    });
    x.AddTokenBucketLimiter("bucket", options =>
    {
        options.TokenLimit = 10;
        options.QueueProcessingOrder = System.Threading.RateLimiting.QueueProcessingOrder.OldestFirst;
        options.QueueLimit = 5;
        options.ReplenishmentPeriod = TimeSpan.FromMinutes(1);
        options.TokensPerPeriod = 5;
        options.AutoReplenishment = true;
    });
});

var app = builder.Build();
app.UseRateLimiter();

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
