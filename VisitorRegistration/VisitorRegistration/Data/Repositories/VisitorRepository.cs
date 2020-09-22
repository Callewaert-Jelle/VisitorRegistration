using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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
            Thread logOutThread = new Thread(
                () =>
                {
                    Thread.Sleep(10 * 1000);
                    visitor.LogOut();
                    SaveChanges();
                });
            // logOutThread.Start();
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
            return _visitors.Where(v => v.Entered.Date == DateTime.Now.Date).Where(v => v.Left.Date == DateTime.MinValue.Date);
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}
