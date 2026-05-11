using Sample01.Application.Core.Models;

namespace Sample01.Application.Core.Services
{
    public interface IEmailService
    {
        void SendEmail(Email email);
    }
}