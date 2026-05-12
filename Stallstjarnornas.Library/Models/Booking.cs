using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stallstjarnornas.Library.Models
{
    public class Booking
    {
        public int Id { get; set; }

        public int GuestId { get; set; }

        public int SittingId { get; set; }

        public DateTime BookingDate { get; set; }

        public int NoOfGuests { get; set; }

        public string Status { get; set; }

        public int BookingNumber { get; set; }

        public DateTime CreatedDate { get; set; }

        public Guest Guest { get; set; }
        public Sitting Sitting { get; set; }

        public string? Message { get; set; }
        public ICollection<TableAssignment> TableAssignments { get; set; } = new List<TableAssignment>();

    }
}
