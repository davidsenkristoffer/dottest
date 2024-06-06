using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using turnstile.Models;

namespace turnstile.Controllers;

[EnableRateLimiting("sliding")]
public class FormController(IConfiguration configuration) : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Submit([FromForm] FormViewModel formViewModel)
    {
        var cf = formViewModel.CfTurnstileResponse;
        var ip = Request.HttpContext.Connection.RemoteIpAddress;
        
        using var httpClient = new HttpClient();
        using StringContent jsonContent = new(
            JsonSerializer.Serialize(new
            {
                secret = configuration.GetValue<string>("cf-turnstile-secret"),
                response = cf,
                remoteip = ip?.ToString() ?? "127.0.0.1"
            }),
            Encoding.UTF8,
            "application/json");

        using HttpResponseMessage responseMessage = await httpClient.PostAsync(
            "https://challenges.cloudflare.com/turnstile/v0/siteverify", 
            jsonContent);
        
        responseMessage.EnsureSuccessStatusCode();

        var jsonResponse = await responseMessage.Content.ReadAsStringAsync();
        Console.WriteLine($"Result from Cloudflare: {jsonResponse}");

        return Redirect("/");
    }
}