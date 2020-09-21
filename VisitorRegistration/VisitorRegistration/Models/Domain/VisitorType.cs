using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VisitorRegistration.Models.Domain
{
    public sealed class VisitorType
    {
        private readonly int _value;
        private readonly string _name;

        public static readonly VisitorType SUPPLIER = new VisitorType(1, "Supplier");
        public static readonly VisitorType APPLICANT = new VisitorType(2, "Applicant");
        public static readonly VisitorType VISITOR = new VisitorType(3, "Visitor");

        private VisitorType(int value, string name)
        {
            _name = name;
            _value = value;
        }

        public override string ToString()
        {
            return _name;
        }
    }
}
