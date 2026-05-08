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
        public required string Day { get; set; }

        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }

        public int MaxGuest { get; set; }

        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
