using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Accounting.WS.Models.DB;

namespace Accounting.WS.Repositories
{
    public abstract class Repository<T> : IRepository<T>
        where T : class
    {
        protected SystemDbContext _context { get; set; }

        public async Task<T> Get(int id)
        {
            return await _context.FindAsync<T>(id);
        }

        public IQueryable<T> Get()
        {
            return _context.Set<T>().AsQueryable();
        }

        public async Task Create(T entity)
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var entity = _context.Find<T>(id);
            _context.Entry(entity).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }
    }

    public class ExpenditureRepository : Repository<Expenditure>, IExpenditureRepository
    {
        public ExpenditureRepository(SystemDbContext context)
        {
            _context = context;
        }
    }

    public class ReFundRepository : Repository<ExpenditureReFund>, IReFundRepository
    {
        public ReFundRepository(SystemDbContext context)
        {
            _context = context;
        }
    }
}