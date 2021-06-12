using System;
using System.Linq;
using System.Linq.Expressions;

namespace EmployeeInformation.Domain.Repositories.Contract
{
    public interface IBaseRepository<T>
    {
        IQueryable<T> FindAll();
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
    }
}
