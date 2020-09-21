using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VisitorRegistration.Models.Domain
{
    public interface IVisitorRepository
    {
        Visitor GetBy(int visitorId);
        IEnumerable<Visitor> GetAll();
        IEnumerable<Visitor> GetAllByDate(DateTime date);
        void Add(Visitor visitor);
        void SaveChanges();
    }
}
