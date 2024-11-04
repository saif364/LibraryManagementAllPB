using AutoMapper;
using LibraryManagementModels.BusinessModels;
using LibraryManagementModels.Entities;
using LibraryManagementRepository.InterfaceRepository;
using LibraryManagementRepository.Repository;
using LibraryManagementService.InterfaceService;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementService.Service
{
    public class BaseService<T> : IBaseService<T> where T : class
    {
        private readonly IRepository<T> _repository;

        public BaseService(IRepository<T> repository)
        {
            _repository = repository;

        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task AddAsync(T entity)
        {
            await _repository.AddAsync(entity);
        }

        public async Task UpdateAsync(T entity)
        {
            await _repository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task SaveChangesAsyncWithTransaction()
        {
            await _repository.SaveChangesAsyncWithTransaction();
        }
        public async Task BeginTransactionAsync()
        {
            await _repository.BeginTransactionAsync();
        }
        public async Task CommitTransactionAsync()
        {
            await _repository.CommitTransactionAsync();
        }
        public async Task RollbackTransactionAsync()
        {
            await _repository.RollbackTransactionAsync();
        }

        public async Task<PaginatedList<T>> GetPaginatedDataAsync<T>(
    int start = 0,
    int length = 10,
    string searchValue = "",
    string sortColumn = "",
    string sortDirection = "asc",
    params string[] searchableColumns)
        {
            var query = _repository.GetAllAsyncQuery();

            // Apply search filter to all specified searchable columns
            if (!string.IsNullOrEmpty(searchValue) && searchableColumns.Any())
            {
                var searchPredicate = string.Join(" OR ", searchableColumns.Select(col => $"{col}.Contains(@0)"));
                query = query.Where(searchPredicate, searchValue);
            }

            // Apply sorting dynamically based on the requested column
            if (!string.IsNullOrEmpty(sortColumn))
            {
                var ordering = $"{sortColumn} {sortDirection}";
                query = query.OrderBy(ordering);
            }

            // Get total count after filtering
            var totalRecords = await query.CountAsync();

            // Apply pagination and retrieve the data
            var paginatedData = await query
                .Skip(start)
                .Take(length)
                .ToListAsync();

            return new PaginatedList<T>
            {
                Items = new List<T>(),
                TotalItems = totalRecords,
                PageSize = length,
                CurrentPage = (start / length) + 1
            };
        }


    }


}
