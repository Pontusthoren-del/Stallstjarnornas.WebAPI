using Stallstjarnornas.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stallstjarnornas.WebAPI.Interfaces
{
    public interface IMailLogService
    {
        Task LogMailAsync(string toEmail, int bookingId);
        Task<IEnumerable<MailLog>> GetMailLogAsync();
    }
}
