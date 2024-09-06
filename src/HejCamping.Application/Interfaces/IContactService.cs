using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HejCamping.Application.Interfaces
{
    public interface IContactService {
        Task SendContactEmailAsync(string fromEmail, string orderNumber, string subject, string body);
    }
}