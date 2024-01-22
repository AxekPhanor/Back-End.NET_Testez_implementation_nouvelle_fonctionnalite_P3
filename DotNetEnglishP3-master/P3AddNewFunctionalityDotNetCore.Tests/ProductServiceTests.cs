using P3AddNewFunctionalityDotNetCore.Models.ViewModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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
        public void CreateNewProductWithEmptyName_ShouldHaveMissingNameError()
        {
            // Arrange
            var product = new ProductViewModel()
            {
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
            Assert.Contains("ErrorMissingName", results.Select(r => r.ErrorMessage));
        }

        [Fact]
        public void CreateNewProductWithAlphabetPrice_ShouldHavePriceNotANumberError()
        {
            // Arrange
            var product = new ProductViewModel()
            {
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
            Assert.Contains("ErrorPriceNotANumber", results.Select(r => r.ErrorMessage));
        }

        [Fact]
        public void CreateNewProductWithPriceLowerThanZero_ShouldHavePriceNotGreaterThanZeroError()
        {
            // Arrange
            var product = new ProductViewModel()
            {
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
            Assert.Contains("ErrorPriceNotGreaterThanZero", results.Select(r => r.ErrorMessage));
        }

        [Fact]
        public void CreateNewProductWithDecimalPrice_ShouldHaveModelValid()
        {
            // Arrange
            var product = new ProductViewModel()
            {
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
            Assert.Contains("ErrorMissingQuantity", results.Select(r => r.ErrorMessage));
        }

        [Fact]
        public void CreateNewProductWithAlphabetQuantity_ShouldHaveQuantiteNotAnIntegerError()
        {
            // Arrange
            var product = new ProductViewModel()
            {
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
            Assert.Contains("ErrorQuantityNotAnInteger", results.Select(r => r.ErrorMessage));
        }

        [Fact]
        public void CreateNewProductWithDecimalQuantity_ShouldHaveQuantiteNotAnIntegerError()
        {
            // Arrange
            var product = new ProductViewModel()
            {
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
            Assert.Contains("ErrorQuantityNotAnInteger", results.Select(r => r.ErrorMessage));
        }

        [Fact]
        public void CreateNewProductWithQuantityLowerThanZero_ShouldHaveQuantityNotGreaterThanZeroError()
        {
            // Arrange
            var product = new ProductViewModel()
            {
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
            Assert.Contains("ErrorQuantityNotGreaterThanZero", results.Select(r => r.ErrorMessage));
        }
    }
}