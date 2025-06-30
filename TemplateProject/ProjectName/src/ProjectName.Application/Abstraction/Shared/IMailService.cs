using ProjectName.Application.DTOs.Mail;
using System.Threading.Tasks;

namespace ProjectName.Application.Abstraction.Shared;

public interface IMailService
{
    Task SendAsync(MailRequest request);
}