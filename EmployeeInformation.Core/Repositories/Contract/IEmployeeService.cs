using System;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using EmployeeInformation.Infrastracture.Models;

namespace EmployeeInformation.Core.Repositories.Contract
{
    public interface IEmployeeService
    {
        Task<bool> CreateEmployee(Employee employee);
        Task<bool> UpdateEmployee(Employee employee);
        Task<bool> DeleteEmployee(Guid guid);
        Task<Employee> GetAsync(Guid guid);
        Task<List<Employee>> GetAllAsync();
    }
}
