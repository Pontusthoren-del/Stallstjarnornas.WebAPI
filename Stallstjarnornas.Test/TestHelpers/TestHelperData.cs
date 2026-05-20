using Stallstjarnornas.Library.Models;
using Stallstjarnornas.WebAPI.Data;

namespace Stallstjarnornas.Test.TestHelpers;

public static class TestDataHelper
{
    public static async Task SeedBasicDataAsync(StallstjarnornasDbContext ctx)
    {
        ctx.Guests.Add(new Guest { Id = 1, Name = "Anna Lindqvist", Phone = "070-123 45 67", Email = "anna@test.com" });
        ctx.Guests.Add(new Guest { Id = 2, Name = "Erik Johansson", Phone = "073-234 56 78", Email = "erik@test.com" });

        ctx.Sittings.Add(new Sitting { Id = 1, OperatingDayId = 1, StartTime = new TimeOnly(17, 0), EndTime = new TimeOnly(19, 0), MaxGuests = 50 });
        ctx.Sittings.Add(new Sitting { Id = 2, OperatingDayId = 1, StartTime = new TimeOnly(19, 0), EndTime = new TimeOnly(21, 0), MaxGuests = 50 });

        ctx.Tables.Add(new Table { Id = 1, Seats = 2 });
        ctx.Tables.Add(new Table { Id = 2, Seats = 2 });

        await ctx.SaveChangesAsync();
    }
}