using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.WebEncoders.Testing;
using Microsoft.IdentityModel.Tokens;
using Stallstjarnornas.Library.Models;
using Stallstjarnornas.WebAPI.Data;
using Stallstjarnornas.WebAPI.DTOs.Booking;
using Stallstjarnornas.WebAPI.DTOs.TableAssignment;
using Stallstjarnornas.WebAPI.Exceptions;
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

        public async Task<TableAssignmentResponseDto> CreateTableAssignmentAsync(CreateTableAssignmentDto dto)
        {


            var booking = await _ctx.Bookings.Include(b=>b.Guest)
            .FirstOrDefaultAsync(b => b.BookingNumber == dto.bookingNumber);

            if (booking == null)
            {
                
                throw new NotFoundException("Booking not found");
            }
            if (booking.Status == "Cancelled" || booking.Status == "Confirmed")
            {
                throw new ConflictException("Booking not valid");
            }//Skriv test för detta

            var noOfGuests = booking.NoOfGuests;
            int tablesNeeded;
            if (noOfGuests % 2 == 0)
            {
                tablesNeeded = noOfGuests / 2;
            }
            else
            {
                tablesNeeded = (noOfGuests + 1) / 2;
            }
            //logik för att se om alla platser är bokade ligger i booking och hanteras inte här. 

            var tablesToAssign = _ctx.Tables.Where(t => dto.TableIds.Contains(t.Id)).ToList();//Hämta alla bord som admin har lagt in i sitt anrop för att se att dom existerar

            if (dto.TableIds.Distinct().Count() != dto.TableIds.Count())//om bord ej är unika
            {
                throw new ConflictException("You assigned one or more tables more than once");
            }
            if (tablesToAssign.Count != dto.TableIds.Count)
            {
                throw new NotFoundException("One or more tables where not found");
            }


            if (tablesToAssign.IsNullOrEmpty())
            {
                throw new NotFoundException("No tables found");
            }

            foreach (var table in tablesToAssign)
            {
                if (_ctx.TableAssignments.Any(ta => ta.Table.Id == table.Id && ta.Booking.BookingDate == booking.BookingDate && ta.Booking.SittingId == booking.SittingId))//Kollar ifall det finns någon assignment inlagd med samma bord.
                {
                    throw new ConflictException("Table is allready being used");
                }
                if (tablesToAssign.Count() < tablesNeeded)
                {
                    throw new ValidationException("You need to assign more tables");
                }

            }

            var response = new TableAssignmentResponseDto(
                  dto.TableIds,
                    //booking.Id,
                    booking.BookingNumber,
                    booking.Guest.Name,
                    booking.NoOfGuests,
                    DateOnly.FromDateTime(booking.BookingDate),
                    booking.SittingId
            );

            foreach (var tableId in dto.TableIds)// Eftersom en bokning kan ha flera bord behöver vi skapa en TableAssignment för varje bord.
            {
                var assignment = new TableAssignment
                {
                    TableId = tableId,
                    BookingId = booking.Id
                };

                _ctx.TableAssignments.Add(assignment);
            }
            booking.Status = "Confirmed";
            
            await _ctx.SaveChangesAsync();

            return response;

        }

        public async Task<GetAvailableTablesResponseDto> GetAvailableTablesAsync(GetAvailableTablesDto dto)
        {
            if (dto.sittingid != 1 && dto.sittingid != 2)
            {
                throw new ValidationException("You must assign sitting Id 1 or 2");
            }
            var bookedTables = await _ctx.TableAssignments
                .Where(ta => DateOnly.FromDateTime(ta.Booking.BookingDate) == dto.bookingDate && ta.Booking.SittingId == dto.sittingid)
                .Select(x => x.TableId)
                .ToListAsync();

            var availableTables = await _ctx.Tables
               .Where(t => !bookedTables.Contains(t.Id))
               .Select(t => t.Id)
               .ToListAsync();

            return new GetAvailableTablesResponseDto(
                dto.bookingDate,
                dto.sittingid,
                availableTables);
        }

        public async Task DeleteAssignedTablesAsync(DeleteAssignedTablesDTO dto)  
        {

            var activeTableassignments = await _ctx.TableAssignments.Include(ta=>ta.Booking).Where(ta => ta.Booking.BookingNumber == dto.bookingNumber).ToListAsync();//HÄMTA FLERA ASSIGNMENTS?!
            if (activeTableassignments.IsNullOrEmpty())
            {
                throw new NotFoundException("No assignments found for booking");
            }
            foreach (var assignment in activeTableassignments)
            {
                _ctx.TableAssignments.Remove(assignment);
                assignment.Booking.Status = "Pending";
            }
            
            await _ctx.SaveChangesAsync();
            
        }



    }
}
