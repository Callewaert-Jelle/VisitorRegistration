using System;
using System.ComponentModel.DataAnnotations;

namespace VisitorRegistration.Models.GroupedByDurationViewModels
{
    public class DatePickerViewModel
    {
        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
    }
}
