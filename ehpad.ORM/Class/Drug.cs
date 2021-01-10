using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ehpad.ORM
{
    public class Drug
    {
        public int Id { get; set; }

        [Display(Name = "Type de médicament")]
        [MaxLength(40)]
        [Required(ErrorMessage = "Vous devez ajouter une type de médicament.")]
        [RegularExpression(@"^[\da-zA-Zàáâãäåçèéêëìíîïðòóôõöùúûüýÿ0\s-']{1,40}$",
         ErrorMessage = "Le type est incorrecte.")]
        public String Name { get; set; }


        public List<Vaccine> Vaccinnes { get; } = new List<Vaccine>();
    }
}
