using Microsoft.EntityFrameworkCore;
using Stallstjarnornas.WebAPI.Data;

namespace Stallstjarnornas.Test.TestHelpers;

public static class DbContextFactory
{
    public static StallstjarnornasDbContext CreateInMemoryContext()
    {
        var options = new DbContextOptionsBuilder<StallstjarnornasDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new StallstjarnornasDbContext(options);
    }
}