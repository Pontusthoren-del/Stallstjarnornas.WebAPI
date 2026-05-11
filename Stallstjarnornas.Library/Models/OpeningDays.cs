using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stallstjarnornas.Library.Models
{
    internal class OpeningDays
    {
        public int Id { get; set; }

        public DayOfWeek Day { get; set; }

        public TimeOnly Opens { get; set; }
        public TimeOnly Closes { get; set; }

        public bool IsClosed { get; set; } = false;

        public ICollection<Sitting> Sittings { get; set; } = new List<Sitting>();
    }
}
