using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HejCamping.ApplicationServices
{
    public interface IContactService {
        Task SendContactEmailAsync(string fromEmail, string orderNumber, string subject, string body);
    }
}