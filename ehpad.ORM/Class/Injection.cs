using ehpad.ORM.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ehpad.ORM
{
    public class Injection
    {
        [Display(Name = "Date de vaccination")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Vous devez ajouter une date de vaccination.")]
        [ValidVaccineDate(ErrorMessage = "La date est n'est pas dans les bornes (moins 30 jours à aujourd'hui).")]
        public DateTime VaccineDate { get; set; }

        [Display(Name = "Date de rappel")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Vous devez ajouter une date de rappel.")]
        [AfterVaccineDate(VaccineDate= "VaccineDate", ErrorMessage = "La date de rappel doit être supérieur à la date de vaccination.")]
        public DateTime ReminderDate { get; set; }

        [Display(Name = "Vaccin")]
        [Required(ErrorMessage = "Vous devez ajouter un vaccin.")]
        public int VaccineId { get; set; }

        [Display(Name = "Vaccin")]
        public Vaccine Vaccine { get; set; }
        
        [UniqueValue(VaccineId="VaccineId", ErrorMessage = "Cette personne a déjà reçu ce vaccin.")]
        [Display(Name = "Personne")]
        [Required(ErrorMessage = "Vous devez ajouter une personne.")]
        public int PeopleId { get; set; }

        
        [Display(Name = "Personne")]
        public People People { get; set; }
    }
}
