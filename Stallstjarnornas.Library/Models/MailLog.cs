using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stallstjarnornas.Library.Models
{
    public class MailLog
    {
        public int Id { get; set; }

        public int BookingId { get; set; }

        public string SentTo { get; set; }

        public DateTime SentDate { get; set; }

        public bool Status { get; set; }

        public Booking Booking { get; set; }
    }
}
