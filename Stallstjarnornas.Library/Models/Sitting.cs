using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stallstjarnornas.Library.Models
{
    internal class Sitting
    {
        public int Id { get; set; }

        public int OpeningDaysId { get; set; }

        public OpeningDays OpeningDays { get; set; }

        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }

        public int MaxGuests { get; set; }

        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
