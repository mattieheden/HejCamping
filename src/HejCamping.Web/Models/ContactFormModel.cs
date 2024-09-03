using System.ComponentModel.DataAnnotations;

namespace HejCamping.Web.Models;
public class ContactFormModel
{

    [Required]
    [EmailAddress]
    public string? FromEmail { get; set; }

    [Required]
    public string? Message { get; set; }
    public string? Subject { get; set; }
    public string? OrderNumber { get; set; }
}