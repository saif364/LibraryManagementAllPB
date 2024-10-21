using LibraryManagementRepository.DbConfigure;
using LibraryManagementRepository.InterfaceRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementRepository.Repository
{
    public class AuditTrialBaseRepository<T> : IAuditTrialBaseRepository<T> where T : class
    {
        private readonly LibraryDbContext _context;
        private readonly DbSet<T> _dbSet;

        public AuditTrialBaseRepository(LibraryDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public IQueryable<T> GetAllAsync()
        {
            return _dbSet;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
         
        //public async Task UpdateAsync(T entity)
        //{
        //    _dbSet.Update(entity);
        //    await _context.SaveChangesAsync();
        //}

        //public async Task DeleteAsync(int id)
        //{
        //    var entity = await _dbSet.FindAsync(id);
        //    if (entity != null)
        //    {
        //        _dbSet.Remove(entity);
        //        await _context.SaveChangesAsync();
        //    }
        //}
    }

}
