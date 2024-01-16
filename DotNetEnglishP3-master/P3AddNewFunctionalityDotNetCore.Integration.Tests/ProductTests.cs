using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using P3AddNewFunctionalityDotNetCore.Controllers;
using P3AddNewFunctionalityDotNetCore.Models;
using P3AddNewFunctionalityDotNetCore.Models.Repositories;
using P3AddNewFunctionalityDotNetCore.Models.Services;
using P3AddNewFunctionalityDotNetCore.Models.ViewModels;

namespace P3AddNewFunctionalityDotNetCore.Integration.Tests
{
    public class ProductTests : IClassFixture<DbFixture>
    {
        private readonly DbFixture _dbFixture;

        public ProductTests(DbFixture dbFixture)
        {
            _dbFixture = dbFixture;
        }

        [Fact]
        public async Task CreateAction_ShouldHaveNewProductsInDatabase()
        {
            // Arrange
            var orderRepository = _dbFixture.GetOrderRepository();
            var productRepository = _dbFixture.GetProductRepository();
            var localizer = new Mock<IStringLocalizer<ProductService>>().Object;
            var languageService = new LanguageService();
            var cart = new Cart();
            var productService = new ProductService(
                cart,
                productRepository,
                orderRepository,
                localizer);
            var productController = new ProductController(productService, languageService);

            // Act
            var product = new ProductViewModel()
            {
                Name = "Test",
                Description = "Test",
                Details = "Vide",
                Price = "10",
                Stock = "5",
            };
            productController.Create(product);
            var result = await productService.GetProduct(6);


            // Assert
            Assert.NotNull(result);
            Assert.Equal("Test", result.Name);
            Assert.Equal("Test", result.Description);
            Assert.Equal("Vide", result.Details);
            Assert.Equal(10, result.Price);
            Assert.Equal(5, result.Quantity);
        }

        [Fact]
        public void DeleteProductsAction_ShouldDeleteProductWithId1()
        {
            // Arrange
            var orderRepository = _dbFixture.GetOrderRepository();
            var productRepository = _dbFixture.GetProductRepository();
            var localizer = new Mock<IStringLocalizer<ProductService>>().Object;
            var languageService = new LanguageService();
            var cart = new Cart();
            var productService = new ProductService(
                cart,
                productRepository,
                orderRepository,
                localizer);
            var productController = new ProductController(productService, languageService);

            // Act
            productController.DeleteProduct(1);
            var result = productService.GetAllProducts();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(4, result.Count);
            Assert.Null(result.FirstOrDefault(p => p.Id == 1));
        }

        [Fact]
        public void CreateView_ShouldReturnView()
        {
            // Arrange
            var orderRepository = _dbFixture.GetOrderRepository();
            var productRepository = _dbFixture.GetProductRepository();
            var localizer = new Mock<IStringLocalizer<ProductService>>().Object;
            var languageService = new LanguageService();
            var cart = new Cart();
            var productService = new ProductService(
                cart,
                productRepository,
                orderRepository,
                localizer);
            var productController = new ProductController(productService, languageService);

            // Act
            var view = productController.Create();

            // Assert
            Assert.NotNull(view);
        }
    }
}