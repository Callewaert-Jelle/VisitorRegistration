using Microsoft.Extensions.Localization;
using System.ComponentModel.DataAnnotations;
using VisitorRegistration.Models.Domain;

namespace VisitorRegistration.Models.VisitorViewModels
{
    public class VisitorViewModel
    {
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "LastName")]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "Type")]
        public VisitorType VisitorType { get; set; }
        [Display(Name = "Company")]
        public string Company { get; set; }
        [Display(Name = "LicensePlate")]
        public string LicensePlate { get; set; }
    }
}
