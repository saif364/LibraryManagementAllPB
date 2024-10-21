using LibraryManagementModels.Entities;
using LibraryManagementRepository.DbConfigure;
using LibraryManagementRepository.InterfaceRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementRepository.Repository
{
    public class LibraryRepository : ILibraryRepository 
    {
         
        private readonly LibraryDbContext _context;
        public LibraryRepository(LibraryDbContext context)
        {
            _context = context;
        }
        
        public async Task<IEnumerable<Library>> GetAllAsync()
        {
            var result=  _context.Librarys;
            return result;
        }

        public async Task<Library> GetByIdAsync(int id)
        {
            return await _context.Librarys.FindAsync(id);
        }

        public async Task AddAsync(Library entity)
        {
             _context.Librarys.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Library entity)
        {
            _context.Librarys.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Librarys.FindAsync(id);
            if (entity != null)
            {
                _context.Librarys.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }


        

    }

}
