using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly AppDbContext context;
        protected readonly DbSet<T> set;

        public Repository(AppDbContext context)
        {
            this.context = context;
            this.set = context.Set<T>();
        }

        public async Task AddAsync(T item)
        {
            await set.AddAsync(item);
        }

        public async Task DeleteAsync(int id)
        {
            T? item = await set.FindAsync(id);
            if(item is not null)
            {
                set.Remove(item);
            }
        }

        public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> predicate)
        {
            return await set.Where(predicate).ToListAsync();
        }

        public async Task<PagedResponse<T>> FindAllPaginatedAsync(PagedRequest request, Expression<Func<T, bool>> predicate)
        {
            var query = set.Where(predicate);
            int totalCount = await query.CountAsync();

            var items = await query.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize).ToListAsync();

            return new PagedResponse<T>()
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };
        }

        public async Task<PagedResponse<T>> FindAllPaginatedAsync<TKey>(PagedRequest request, Expression<Func<T, bool>> predicate, Expression<Func<T, TKey>> orderBy, bool descending = false, Func<IQueryable<T>, IQueryable<T>>? include = null)
        {
            IQueryable<T> query = set;
            if(include is not null)
            {
                query = include(query);
            }
            query = query.Where(predicate);

            query = descending ? query.OrderByDescending(orderBy) : query.OrderBy(orderBy);

            int totalCount = await query.CountAsync();

            var items = await query.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize).ToListAsync();

            return new PagedResponse<T>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };
        }

        public async Task<T?> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await set.FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await set.ToListAsync();
        }

        public async Task<PagedResponse<T>> GetAllPaginated(PagedRequest request)
        {
            var query = set.AsQueryable();
            int totalCount = await query.CountAsync();

            var items = await query.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize).ToListAsync();

            return new PagedResponse<T>()
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };

        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await set.FindAsync(id);
        }

        public async Task UpdateAsync(T item)
        {
            set.Update(item);
        }
    }
}
