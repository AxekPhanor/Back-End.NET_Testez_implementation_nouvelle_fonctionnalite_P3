using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using P3AddNewFunctionalityDotNetCore.Data;

namespace P3AddNewFunctionalityDotNetCore.Integration.Tests
{
    public static class TestHelpers
    {
        private static P3Referential? _context;

        public static P3Referential GetContext()
        {
            var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.tests.json", true, true)
            .Build();
            var optionsBuilder = new DbContextOptionsBuilder<P3Referential>();
            var connectionString = config.GetConnectionString("P3Referential");
            optionsBuilder.UseSqlServer(connectionString);
            _context = new P3Referential(optionsBuilder.Options, config);
            return _context;
        }
    }
}
