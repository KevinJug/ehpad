using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
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

    public class AfterVaccineDate : ValidationAttribute
    {
        public string VaccineDate { get; set; }


        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            PropertyInfo vaccineDateProperty = validationContext.ObjectType.GetProperty(VaccineDate);

            DateTime vaccineDate = (DateTime)vaccineDateProperty.GetValue(validationContext.ObjectInstance, null);

            DateTime reminderDate = Convert.ToDateTime(value);

            if (reminderDate > vaccineDate)
            {
                return ValidationResult.Success;
            } else
            {
                return new ValidationResult(ErrorMessage);
            }
        }

    }

    public class SexValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (Convert.ToString(value) == "Homme" || Convert.ToString(value) == "Femme")
                return ValidationResult.Success;
            else
                return new ValidationResult(ErrorMessage);
        }
    }

    public class ConditionValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (Convert.ToString(value) == "Résident" || Convert.ToString(value) == "Personnel")
                return ValidationResult.Success;
            else
                return new ValidationResult(ErrorMessage);
        }
    }

    public class UniqueValue : ValidationAttribute
    {
        public string VaccineId { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            PropertyInfo vaccineIdProperty = validationContext.ObjectType.GetProperty(VaccineId);

            int vaccineId = (int)vaccineIdProperty.GetValue(validationContext.ObjectInstance, null);

            Context ct = new Context();

             var injection = ct.Injections.FirstOrDefault(m => m.PeopleId == (int)value && m.VaccineId == vaccineId);
          
            
            if (injection == null)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult(ErrorMessage);
            }

        }
    }
}
