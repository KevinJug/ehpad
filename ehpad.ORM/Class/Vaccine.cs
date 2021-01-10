using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ehpad.ORM
{
    public class Vaccine
    {
        public int Id { get; set; }

        [Display(Name = "Lot")]
        [MaxLength(50)]
        [Required(ErrorMessage = "Vous devez ajouter un numéro de lot.")]
        [RegularExpression(@"^[\da-zA-Z]{1,50}$",
         ErrorMessage = "Le lot est incorrect.")]
        public String Lot { get; set; }

        [Display(Name = "Type médicament")]
        [Required(ErrorMessage = "Vous devez ajouter un type de médicament.")]
        public int DrugId { get; set; }
        public Drug Drug { get; set; }

        [Display(Name = "Marque")]
        [Required(ErrorMessage = "Vous devez ajouter une marque.")]
        public int BrandId { get; set; }
        public Brand Brand { get; set; }

        public List<Injection> Injections { get; } = new List<Injection>();
    }
}
