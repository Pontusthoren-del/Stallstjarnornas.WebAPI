using Stallstjarnornas.Library.Models;
using System.ComponentModel.DataAnnotations;

namespace Stallstjarnornas.WebAPI.DTOs.OperatingDayDTOs
{

    public record UpdateOpeningHoursRequest
    {
        public DayOfWeek Day { get; set; }
        public TimeOnly? Opens { get; set; }//nullable eftersom isclosed kan va true.
        public TimeOnly? Closes { get; set; }//nullable eftersom isclosed kan va true.
        public bool IsClosed { get; set; }

    }

   

}
