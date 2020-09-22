using System.Collections.Generic;
using System.Linq;
using VisitorRegistration.Models.Domain;

namespace VisitorRegistration.Models.GroupedByDurationViewModels
{
    public class GroupedByDurationViewModel
    {
        public DatePickerViewModel datePickerViewModel { get; set; }
        public IEnumerable<IGrouping<double, Visitor>> resultSetModel { get; set; }
    }
}
