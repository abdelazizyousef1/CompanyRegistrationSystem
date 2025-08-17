using Company.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.Data;
using Company.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Company.Repository.Repositories
{
    
        public class GenericRepository<T> : IGenericRepository<T> where T : class
        {
            private readonly ApplicationDbContext _context;
            private readonly DbSet<T> _dbSet;

            public GenericRepository(ApplicationDbContext context)
            {
                _context = context;
                _dbSet = _context.Set<T>();
            }

            public async Task<T?> GetByIdAsync(Guid id)
            {
                return await _dbSet.FindAsync(id);
            }

            public async Task<IEnumerable<T>> GetAllAsync()
            {
                return await _dbSet.ToListAsync();
            }

            public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
            {
                return await _dbSet.Where(predicate).ToListAsync();
            }

            public async Task AddAsync(T entity)
            {
                await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

            
            public async Task UpdateAsync(T entity)
            {
                _dbSet.Update(entity);
                await _context.SaveChangesAsync();
            }

            public void Delete(T entity)
            {
                _dbSet.Remove(entity);
            _context.SaveChangesAsync();
        }

            public async Task SaveChangesAsync()
            {
                await _context.SaveChangesAsync();
            }
            
            public async Task<T?> GetAsync(Expression<Func<T, bool>> predicate)
            {
                return await _dbSet.FirstOrDefaultAsync(predicate);
            }
    }
    

}
