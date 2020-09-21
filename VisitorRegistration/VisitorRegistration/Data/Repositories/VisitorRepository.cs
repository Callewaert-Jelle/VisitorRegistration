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
            throw new NotImplementedException();
        }

        public IEnumerable<Visitor> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Visitor> GetAllByDate(DateTime date)
        {
            throw new NotImplementedException();
        }

        public Visitor GetBy(int visitorId)
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
