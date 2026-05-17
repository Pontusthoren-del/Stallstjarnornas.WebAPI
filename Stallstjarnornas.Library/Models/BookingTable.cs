using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stallstjarnornas.Library.Models
{
    internal class BookingTable
    {
        public int BookingId { get; set; }
        public Booking Booking { get; set; }

        public int TableId { get; set; }
        public Table Table { get; set; }
    }
}

