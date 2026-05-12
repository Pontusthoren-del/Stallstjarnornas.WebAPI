using Stallstjarnornas.Library.Models;
using System.ComponentModel.DataAnnotations;

namespace Stallstjarnornas.WebAPI.DTOs.OperatingDayDTOs
{

    public record UpdateOpeningHoursRequest
    {
         public DayOfWeek Day { get; init; }
         public TimeOnly? Opens { get; init; }//nullable eftersom isclosed kan va true.
        public TimeOnly? Closes { get; init; }//nullable eftersom isclosed kan va true.
        public bool IsClosed { get; init; }

    }

   

}
