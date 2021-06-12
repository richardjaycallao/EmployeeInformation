using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using EmployeeInformation.Infrastracture.Models;
using EmployeeInformation.Domain.Repositories.Contract;

namespace EmployeeInformation.Domain.Repositories.Concrete
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected EmployeeRecordsContext _employeeRecordsContext;

        public BaseRepository(EmployeeRecordsContext employeeRecordsContext) =>
            _employeeRecordsContext = employeeRecordsContext;

        public virtual IQueryable<T> FindAll() =>
            _employeeRecordsContext.Set<T>().AsNoTracking();

        public virtual IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression) =>
            _employeeRecordsContext.Set<T>().Where(expression).AsNoTracking();
    }
}
