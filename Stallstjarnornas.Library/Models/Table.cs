using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stallstjarnornas.Library.Models
{
    public class Table
    {
        public int Id { get; set; }
        public int Seats { get; set; } = 2;
        public ICollection<TableAssignment> TableAssignments { get; set; } = new List<TableAssignment>();


    }
}
