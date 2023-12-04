using LoadBalancer.DAL.Interfaces;
using LoadBalancer.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadBalancer.DAL.Repositories
{
    public class MatrixRepository : IBaseRepository<Matrix>
    {
        private readonly ApplicationDbContext _db;

        public MatrixRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task Create(Matrix entity)
        {
            await _db.Matrix.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(Matrix entity)
        {
            _db.Matrix.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public IQueryable<Matrix> GetAll()
        {
            return _db.Matrix;
        }

        public async Task<Matrix> Update(Matrix entity)
        {
            _db.Matrix.Update(entity);
            await _db.SaveChangesAsync();

            return entity;
        }
    }
}
