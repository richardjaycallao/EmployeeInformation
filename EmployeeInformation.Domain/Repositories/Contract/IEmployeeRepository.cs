using System;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using EmployeeInformation.Infrastracture.Models;

namespace EmployeeInformation.Domain.Repositories.Contract
{
    public interface IEmployeeRepository : IBaseRepository<Employee>
    {
        Task<Employee> GetAsync(Guid guid);
        Task<IEnumerable<Employee>> GetAllAsync();
        Task<bool> Create(Employee employee);
        Task<bool> Update(Employee employee);
        Task<bool> Delete(Guid guid);
    }
}
