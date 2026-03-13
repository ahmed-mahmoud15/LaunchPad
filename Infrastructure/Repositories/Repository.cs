using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
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

        public async Task<T?> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await set.FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await set.ToListAsync();
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
