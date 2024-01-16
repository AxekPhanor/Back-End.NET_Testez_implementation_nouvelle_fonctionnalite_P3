using Microsoft.Extensions.Localization;
using P3AddNewFunctionalityDotNetCore.Models.Repositories;
using P3AddNewFunctionalityDotNetCore.Models.Services;

namespace P3AddNewFunctionalityDotNetCore.Integration.Tests
{
    public class DbFixture : IDisposable
    {
        private static bool _databaseInitialized;

        public DbFixture()
        {
            if (!_databaseInitialized)
            {
                using (var context = TestHelpers.GetContext())
                {
                    context.Database.EnsureCreated();
                    if (!context.Product.Any())
                    {
                        context.Product.AddRange(
                        new Product
                        {
                            Name = "Echo Dot",
                            Description = "(2nd Generation) - Black",
                            Quantity = 10,
                            Price = 92.50
                        },
                        new Product
                        {
                            Name = "Anker 3ft / 0.9m Nylon Braided",
                            Description = "Tangle-Free Micro USB Cable",
                            Quantity = 20,
                            Price = 9.99
                        },
                        new Product
                        {
                            Name = "JVC HAFX8R Headphone",
                            Description = "Riptidz, In-Ear",
                            Quantity = 30,
                            Price = 69.99
                        },
                        new Product
                        {
                            Name = "VTech CS6114 DECT 6.0",
                            Description = "Cordless Phone",
                            Quantity = 40,
                            Price = 32.50
                        },
                        new Product
                        {
                            Name = "NOKIA OEM BL-5J",
                            Description = "Cell Phone",
                            Quantity = 50,
                            Price = 895.00
                        });
                        context.SaveChanges();
                    }
                }
                _databaseInitialized = true;
            }
        }

        public ProductRepository GetProductRepository() =>
            new ProductRepository(TestHelpers.GetContext());

        public OrderRepository GetOrderRepository() =>
            new OrderRepository(TestHelpers.GetContext());

        public void Dispose()
        {
            using (var context = TestHelpers.GetContext())
            {
                context.Database.EnsureDeleted();
            }
        }
    }
}
