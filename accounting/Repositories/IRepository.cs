using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Collections.Generic;
using Accounting.WS.Models.DB;

namespace Accounting.WS.Repositories
{
    public interface IRepository<T>
    {
        Task<T> Get(int id);

        IQueryable<T> Get();

        Task Create(T entity);

        Task Update(T entity);

        Task Delete(int id);
    }

    public interface IExpenditureRepository : IRepository<Expenditure>
    {
    }

    public interface IReFundRepository : IRepository<ExpenditureReFund>
    {
    }
}