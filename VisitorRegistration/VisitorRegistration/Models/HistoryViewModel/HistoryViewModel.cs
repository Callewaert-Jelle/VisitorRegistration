using System.Collections.Generic;
using VisitorRegistration.Models.Domain;
using VisitorRegistration.Models.GroupedByDurationViewModels;

namespace VisitorRegistration.Models.HistoryViewModel
{
    public class HistoryViewModel
    {
        public IEnumerable<Visitor> Visitors { get; set; }
        public DatePickerViewModel DatePickerViewModel { get; set; }
    }
}
