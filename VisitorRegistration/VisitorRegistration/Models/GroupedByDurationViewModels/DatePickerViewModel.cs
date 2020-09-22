using System;
using System.ComponentModel.DataAnnotations;

namespace VisitorRegistration.Models.GroupedByDurationViewModels
{
    public class DatePickerViewModel
    {
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime Date { get; set; }
    }
}
