using Microsoft.EntityFrameworkCore;
using Stallstjarnornas.Library.Models;
using Stallstjarnornas.WebAPI.Data;
using Stallstjarnornas.WebAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stallstjarnornas.WebAPI.Services
{
    public class MailLogService : IMailLogService
    {
        private readonly StallstjarnornasDbContext _ctx;

        public MailLogService(StallstjarnornasDbContext ctx)
        {
            _ctx = ctx;
        }
        public async Task<IEnumerable<MailLog>> GetMailLogAsync()
        {
            return await _ctx.MailLogs.ToListAsync();
        }

        public async Task LogMailAsync(string toEmail, int bookingId)
        {
            var log = new MailLog
            {
                BookingId = bookingId,
                SentTo = toEmail,
                SentDate = DateTime.Now,
                Status = "Sent"
            };
            _ctx.MailLogs.Add(log);
            await _ctx.SaveChangesAsync();
        }
    }
}
