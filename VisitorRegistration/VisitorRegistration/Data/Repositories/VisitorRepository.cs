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
            return _visitors.Where(v => v.Entered.Date == date.Date);
        }

        public Visitor GetBy(int visitorId)
        {
            return _visitors.FirstOrDefault(v => v.VisitorId == visitorId);
        }

        public IEnumerable<Visitor> GetCurrentVisitors()
        {
            var vs = _visitors;
            var vsNow = vs.Where(v => v.Entered.Date == DateTime.Now.Date);
            var vsNotLeft = vs.Where(v => v.Left.Date == DateTime.MinValue.Date);
            var vsNowNotLeft = _visitors.Where(v => v.Entered.Date == DateTime.Now.Date).Where(v => v.Left.Date == DateTime.MinValue.Date);
            return _visitors.Where(v => v.Entered.Date == DateTime.Now.Date).Where(v => v.Left.Date == DateTime.MinValue.Date);
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}
