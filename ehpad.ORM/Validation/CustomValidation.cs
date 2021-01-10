using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ehpad.ORM.Validation
{
    class CustomValidation
    {
    }
    public class ValidBirthdayDate : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            DateTime date = Convert.ToDateTime(value);
            DateTime minDate = Convert.ToDateTime(DateTime.Now.AddYears(-60).ToShortDateString());
            DateTime maxDate = Convert.ToDateTime(DateTime.Now.AddYears(-140).ToShortDateString());

            if (date <= minDate && date >= maxDate)
                return ValidationResult.Success;
            else
                return new ValidationResult(ErrorMessage);
        }
    }

    public class ValidVaccineDate : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            DateTime date = Convert.ToDateTime(value);
            DateTime minDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            DateTime maxDate = Convert.ToDateTime(DateTime.Now.AddDays(-30).ToShortDateString());

            if (date <= minDate && date >= maxDate)
                return ValidationResult.Success;
            else
                return new ValidationResult(ErrorMessage);
        }
    }

    public class SexValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (Convert.ToString(value) == "H" || Convert.ToString(value) == "F")
                return ValidationResult.Success;
            else
                return new ValidationResult(ErrorMessage);
        }
    }

    public class ConditionValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (Convert.ToString(value) == "R" || Convert.ToString(value) == "P")
                return ValidationResult.Success;
            else
                return new ValidationResult(ErrorMessage);
        }
    }
}
