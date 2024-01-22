using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using P3AddNewFunctionalityDotNetCore.Models.ViewModels.Attribute;

namespace P3AddNewFunctionalityDotNetCore.Models.ViewModels
{
    public class ProductViewModel
    {
        [BindNever]
        public int Id { get; set; }

        [Required(ErrorMessage = "ErrorMissingName")]
        public string Name { get; set; }

        public string Description { get; set; }

        public string Details { get; set; }

        [Required(ErrorMessage = "ErrorMissingQuantity")]
        [Integer(ErrorMessage = "ErrorQuantityNotAnInteger")]
        [Positive(ErrorMessage = "ErrorQuantityNotGreaterThanZero")]
        public string Stock { get; set; }

        [Required(ErrorMessage = "ErrorMissingPrice")]
        [Decimal(ErrorMessage = "ErrorPriceNotANumber")]
        [Positive(ErrorMessage = "ErrorPriceNotGreaterThanZero")]
        public string Price { get; set; }
    }
}
