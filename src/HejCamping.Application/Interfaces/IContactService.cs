using System.Threading.Tasks;

namespace HejCamping.Application.Interfaces
{
    public interface IContactService {
        Task SendContactEmailAsync(string fromEmail, string orderNumber, string subject, string body);
    }
}