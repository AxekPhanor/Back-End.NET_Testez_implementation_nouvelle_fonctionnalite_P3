
using Microsoft.Extensions.Localization;
using Moq;
using P3AddNewFunctionalityDotNetCore.Models;
using P3AddNewFunctionalityDotNetCore.Models.Entities;
using P3AddNewFunctionalityDotNetCore.Models.Repositories;
using P3AddNewFunctionalityDotNetCore.Models.Services;
using P3AddNewFunctionalityDotNetCore.Models.ViewModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using Xunit;

namespace P3AddNewFunctionalityDotNetCore.Tests
{
    public class ProductServiceTests
    {
        /// <summary>
        /// Take this test method as a template to write your test method.
        /// A test method must check if a definite method does its job:
        /// returns an expected value from a particular set of parameters
        /// </summary>
        [Fact]
        public void CreateNewProduct_ShouldBeCreated()
        {
            // Arrange
            var mockCart = new Mock<ICart>();
            var mockProductRepository = new Mock<IProductRepository>();
            mockProductRepository.Setup(productRepository => productRepository.SaveProduct(It.IsAny<Product>()));
            var mockOrderRepository = new Mock<IOrderRepository>();
            var mockStringLocalizer = new Mock<IStringLocalizer<ProductService>>();
            var product = new ProductViewModel()
            {
                Id = 1,
                Name = "Test",
                Description = "Test",
                Details = "",
                Price = "20",
                Stock = "3"
            };
            var productService = new ProductService(
                mockCart.Object,
                mockProductRepository.Object,
                mockOrderRepository.Object,
                mockStringLocalizer.Object);

            // Act
            productService.SaveProduct(product);

            // Assert
            mockProductRepository.Verify(productRepository => productRepository.SaveProduct(It.IsAny<Product>()), Times.Once);
        }

        [Fact]
        public void CreateNewProductWithEmptyName_ShouldHaveMissingNameError()
        {
            // Arrange
            var product = new ProductViewModel()
            {
                Id = 1,
                Name = "",
                Description = "test",
                Details = "",
                Price = "20",
                Stock = "3"
            };
            var context = new ValidationContext(product, null, null);
            var results = new List<ValidationResult>();

            // Act
            var isModelStateValid = Validator.TryValidateObject(product, context, results, true);

            // Assert
            Assert.False(isModelStateValid);
            Assert.Contains("MissingName", results.Select(r => r.ErrorMessage));
        }

        [Fact]
        public void CreateNewProductWithAlphabetPrice_ShouldHavePriceNotANumberError()
        {
            // Arrange
            var product = new ProductViewModel()
            {
                Id = 1,
                Name = "test",
                Description = "test",
                Details = "",
                Price = "abc",
                Stock = "3"
            };
            var context = new ValidationContext(product, null, null);
            var results = new List<ValidationResult>();

            // Act
            var isModelStateValid = Validator.TryValidateObject(product, context, results, true);

            // Assert
            Assert.False(isModelStateValid);
            Assert.Contains("PriceNotANumber", results.Select(r => r.ErrorMessage));
        }

        [Fact]
        public void CreateNewProductWithPriceLowerThanZero_ShouldHavePriceNotGreaterThanZeroError()
        {
            // Arrange
            var product = new ProductViewModel()
            {
                Id = 1,
                Name = "test",
                Description = "test",
                Details = "",
                Price = "-5",
                Stock = "3"
            };
            var context = new ValidationContext(product, null, null);
            var results = new List<ValidationResult>();

            // Act
            var isModelStateValid = Validator.TryValidateObject(product, context, results, true);

            // Assert
            Assert.False(isModelStateValid);
            Assert.Contains("NotGreaterThanZero", results.Select(r => r.ErrorMessage));
        }

        [Fact]
        public void CreateNewProductWithDecimalPrice_ShouldHaveModelValid()
        {
            // Arrange
            var product = new ProductViewModel()
            {
                Id = 1,
                Name = "test",
                Description = "test",
                Details = "",
                Price = "5,8",
                Stock = "3"
            };
            var context = new ValidationContext(product, null, null);
            var results = new List<ValidationResult>();

            // Act
            var isModelStateValid = Validator.TryValidateObject(product, context, results, true);

            // Assert
            Assert.True(isModelStateValid);
        }

        [Fact]
        public void CreateNewProductWithoutQuantity_ShouldHaveMissingQuantityError()
        {
            // Arrange
            var product = new ProductViewModel()
            {
                Id = 1,
                Name = "test",
                Description = "test",
                Details = "",
                Price = "5",
                Stock = ""
            };
            var context = new ValidationContext(product, null, null);
            var results = new List<ValidationResult>();

            // Act
            var isModelStateValid = Validator.TryValidateObject(product, context, results, true);

            // Assert
            Assert.False(isModelStateValid);
            Assert.Contains("MissingQuantity", results.Select(r => r.ErrorMessage));
        }

        [Fact]
        public void CreateNewProductWithAlphabetQuantity_ShouldHaveQuantiteNotAnIntegerError()
        {
            // Arrange
            var product = new ProductViewModel()
            {
                Id = 1,
                Name = "test",
                Description = "test",
                Details = "",
                Price = "5",
                Stock = "abc"
            };
            var context = new ValidationContext(product, null, null);
            var results = new List<ValidationResult>();

            // Act
            var isModelStateValid = Validator.TryValidateObject(product, context, results, true);

            // Assert
            Assert.False(isModelStateValid);
            Assert.Contains("QuantiteNotAnInteger", results.Select(r => r.ErrorMessage));
        }

        [Fact]
        public void CreateNewProductWithDecimalQuantity_ShouldHaveQuantiteNotAnIntegerError()
        {
            // Arrange
            var product = new ProductViewModel()
            {
                Id = 1,
                Name = "test",
                Description = "test",
                Details = "",
                Price = "5",
                Stock = "5.5"
            };
            var context = new ValidationContext(product, null, null);
            var results = new List<ValidationResult>();

            // Act
            var isModelStateValid = Validator.TryValidateObject(product, context, results, true);

            // Assert
            Assert.False(isModelStateValid);
            Assert.Contains("QuantiteNotAnInteger", results.Select(r => r.ErrorMessage));
        }

        [Fact]
        public void CreateNewProductWithQuantityLowerThanZero_ShouldHaveQuantityNotGreaterThanZeroError()
        {
            // Arrange
            var product = new ProductViewModel()
            {
                Id = 1,
                Name = "test",
                Description = "test",
                Details = "",
                Price = "5",
                Stock = "-1"
            };
            var context = new ValidationContext(product, null, null);
            var results = new List<ValidationResult>();

            // Act
            var isModelStateValid = Validator.TryValidateObject(product, context, results, true);

            // Assert
            Assert.False(isModelStateValid);
            Assert.Contains("QuantityNotGreaterThanZero", results.Select(r => r.ErrorMessage));
        }
    }
}