using ehpad.ORM.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ehpad.ORM
{
    public class People
    {
        [Display(Name = "Personne")]
        public int Id { get; set; }


        [Display(Name = "Nom")]
        [MaxLength(50)]
        [Required(ErrorMessage = "Vous devez ajouter un nom.")]
        [RegularExpression(@"^[a-zA-Zàáâãäåçèéêëìíîïðòóôõöùúûüýÿ0\s-']{1,50}$",
         ErrorMessage = "Le nom est incorrect.")]
        public String Name { get; set; }

        [Display(Name = "Prénom")]
        [MaxLength(50)]
        [Required(ErrorMessage = "Vous devez ajouter un prénom.")]
        [RegularExpression(@"^[a-zA-Zàáâãäåçèéêëìíîïðòóôõöùúûüýÿ0\s-']{1,50}$",
         ErrorMessage = "Le prénom est incorrect.")]
        public String Firstname { get; set; }

        [Display(Name = "Sexe")]
        [Required(ErrorMessage = "Vous devez ajouter le sexe de la personne.")]
        [SexValidation(ErrorMessage = "Choisissez votre sexe (Femme ou Homme).")]
        public String Sex { get; set; }

        [Display(Name = "Date de naissance")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Vous devez ajouter une date de naissance.")]
        [ValidBirthdayDate(ErrorMessage = "La date est n'est pas dans les bornes. Vous devez avoir entre 60 et 140 ans.")]
        public DateTime Birth { get; set; }

        [Display(Name = "Etat")]
        [Required(ErrorMessage = "Vous devez ajouter une état.")]
        [ConditionValidation(ErrorMessage = "Choisissez votre état (Résident ou Personnel).")]
        public String Condition { get; set; }

        public List<Injection> Injections { get; } = new List<Injection>();
    }
}
