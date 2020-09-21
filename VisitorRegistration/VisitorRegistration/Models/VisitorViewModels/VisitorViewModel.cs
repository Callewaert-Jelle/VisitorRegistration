using System.ComponentModel.DataAnnotations;
using VisitorRegistration.Models.Domain;

namespace VisitorRegistration.Models.VisitorViewModels
{
    public class VisitorViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Last name")]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "Type")]
        public VisitorType VisitorType { get; set; }
        public string Company { get; set; }
        [Display(Name = "License plate")]
        public string LicensePlate { get; set; }

        public VisitorViewModel()
        {

        }
    }
}
