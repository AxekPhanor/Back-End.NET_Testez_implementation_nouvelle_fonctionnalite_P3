using System;
using System.ComponentModel.DataAnnotations;

namespace P3AddNewFunctionalityDotNetCore.Models.ViewModels.Attribute
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class PositiveAttribute : RequiredAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is null)
            {
                return false;
            }

            decimal number;
            if (decimal.TryParse(value.ToString(), out number))
            {
                return number > 0;
            }
            return true;
        }
    }
}
