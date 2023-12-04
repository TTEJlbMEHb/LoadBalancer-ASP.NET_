using LoadBalancer.DAL.Interfaces;
using LoadBalancer.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace LoadBalancer.DAL.Implementations
{
    public class CalculatorRepository : IBaseRepository<Calculator>
    {
        private readonly ApplicationDbContext _db;

        public CalculatorRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task Create(Calculator entity)
        {
            await _db.Calculator.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(Calculator entity)
        {
            _db.Calculator.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public IQueryable<Calculator> GetAll()
        {
            return _db.Calculator;
        }

        public async Task<Calculator> Update(Calculator entity)
        {
            _db.Calculator.Update(entity);
            await _db.SaveChangesAsync();

            return entity;
        }
    }
}
