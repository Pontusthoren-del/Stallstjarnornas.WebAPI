using Microsoft.EntityFrameworkCore;
using Stallstjarnornas.Library.Models;
using Stallstjarnornas.WebAPI.Data;
using Stallstjarnornas.WebAPI.DTOs.Booking;
using Stallstjarnornas.WebAPI.DTOs.TableAssignment;
using Stallstjarnornas.WebAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stallstjarnornas.WebAPI.Services
{
    public class TableAssignmentService : ITableAssignmentService
    {
        private readonly StallstjarnornasDbContext _ctx;

        public TableAssignmentService(StallstjarnornasDbContext ctx)
        {
            _ctx = ctx;
        }

        //public async Task<TableAssignmentResponseDto> AssignTableAsync(CreateTableAssignmentDto dto)
        //{
        //    var booking = await _ctx.Bookings.FirstOrDefaultAsync(b => b.Id==dto.BookingId);

        //    if (booking == null)
        //    {
        //        throw new Exception("Booking not found");
        //    }

        //    var noOfGuests = booking.NoOfGuests;
        //    int tablesNeeded;
        //    if (noOfGuests % 2 == 0)
        //    {
        //        tablesNeeded = noOfGuests / 2;
        //    }
        //    else
        //    {
        //        tablesNeeded = (noOfGuests + 1) / 2;
        //    }
        //    //logik för att se om alla platser är bokade ligger i booking. 
           
            
        //}


    }
}
