using EmailAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmailAPI.Services
{
    public interface IEmailService
    {
        Task<SendStatus> SendEmailAsync(Message message);

        EmailConfiguration GetConfiguration();

        Task<IEnumerable<SentMessage>> GetSentMessagesAsync();
    }
}
