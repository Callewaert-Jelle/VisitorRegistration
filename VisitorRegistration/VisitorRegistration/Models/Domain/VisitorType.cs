using System.ComponentModel.DataAnnotations;

namespace VisitorRegistration.Models.Domain
{
    public enum VisitorType
    {
        [Display(Name = "Supplier")]
        Supplier,
        [Display(Name = "Applicant")]
        Applicant,
        [Display(Name = "Visitor")]
        Visitor
    }
}
