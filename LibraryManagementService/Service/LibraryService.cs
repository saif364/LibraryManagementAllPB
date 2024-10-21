using LibraryManagementModels.Entities;
using LibraryManagementRepository.DbConfigure;
using LibraryManagementRepository.InterfaceRepository;
using LibraryManagementRepository.Repository;
using LibraryManagementService.InterfaceService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementService.Service
{
    public class LibraryService : ILibraryService
    {
        private readonly LibraryDbContext _context;
        private ILibraryRepository _LibraryRepository;

        private readonly ILogger<LibraryService> _logger;

        
        public LibraryService(ILogger<LibraryService> logger,LibraryDbContext context,ILibraryRepository LibraryRepository)
             
        {
            _LibraryRepository = LibraryRepository;
            _context = context;
            _logger = logger;
        }

        public Task AddAsync(Library entity)
        {
           return _LibraryRepository.AddAsync(entity);
        }

        public Task DeleteAsync(int id)
        {
               return (_LibraryRepository.DeleteAsync(id));
        }

        public Task<IEnumerable<Library>> GetAllAsync()
        {
            var result= _LibraryRepository.GetAllAsync();
            return result;
        }

        //public Task<Library> GetByIdAsync(int id)
        //{
        //    return _LibraryRepository.GetByIdAsync(id);
        //}

        public async Task<Library> GetByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation("Starting to fetch Library with ID: {OrderId}", id);
                var order = await _LibraryRepository.GetByIdAsync(id);

                if (order == null)
                {
                    _logger.LogWarning("Library with ID {OrderId} not found", id);
                    return null;
                }

                _logger.LogDebug("Library with ID {OrderId} successfully retrieved", id); // Debug level, might be turned off in production
                return order;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching Library with ID {OrderId}", id);
                throw;
            }
        }

        public Task UpdateAsync(Library entity)
        {
           return _LibraryRepository.UpdateAsync(entity);
        }

        public async Task AddLibraryWithTransactionAsync(Library library)
        {
            // Start the transaction
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // Step 1: Insert the Library entity
                await _context.Librarys.AddAsync(library);
                await _context.SaveChangesAsync();

                // Step 2: Simulate another database operation
                // You could add or update other entities here as part of the same transaction
                //var book = new Student {  = "New Book", LibraryId = library.Id };
                //await _context.Books.AddAsync(book);
                //await _context.SaveChangesAsync();

                // Commit the transaction if everything is successful
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                // Roll back the transaction if there is an error
                await transaction.RollbackAsync();
                throw; // Re-throw the exception to be handled elsewhere
            }
        }

        // Additional methods specific to LibraryService can go here, if needed
    }

}
