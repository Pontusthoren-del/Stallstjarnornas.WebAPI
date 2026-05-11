using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stallstjarnornas.Library.Models
{
    public class Guest
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public required string Phone { get; set; }

        public required string Email { get; set; }

        public string? Message { get; set; }
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    }
}
