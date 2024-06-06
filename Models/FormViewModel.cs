using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace turnstile.Models;

public class FormViewModel
{
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [ModelBinder(Name = "cf-turnstile-response")]
    public string CfTurnstileResponse { get; set; } = string.Empty;
}