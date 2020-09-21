using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VisitorRegistration.Models.Domain;

namespace VisitorRegistration.Data.Repositories
{
    public class VisitorRepository : IVisitorRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<Visitor> _visitors;

        public VisitorRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _visitors = _dbContext.Visitors;
        }

        public void Add(Visitor visitor)
        {
            _visitors.Add(visitor);
        }

        public IEnumerable<Visitor> GetAll()
        {
            return _visitors.ToList();
        }

        public IEnumerable<Visitor> GetAllByDate(DateTime date)
        {
            return _visitors.Where(v => v.Entered.Date == DateTime.Now.Date);
        }

        public Visitor GetBy(int visitorId)
        {
            return _visitors.FirstOrDefault(v => v.VisitorId == visitorId);
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}
