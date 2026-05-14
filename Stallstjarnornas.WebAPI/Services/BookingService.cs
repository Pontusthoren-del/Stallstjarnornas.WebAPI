using Microsoft.EntityFrameworkCore;
using Stallstjarnornas.WebAPI.Data;
using Stallstjarnornas.WebAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stallstjarnornas.WebAPI.Services
{
    public class BookingService : IBookingService
    {

        private readonly StallstjarnornasDbContext _ctx;

        public BookingService(StallstjarnornasDbContext ctx)
        {
            _ctx = ctx;
        }

        
        

    }
}
