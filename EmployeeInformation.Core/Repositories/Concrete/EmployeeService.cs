using System;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using EmployeeInformation.Infrastracture.Models;
using EmployeeInformation.Domain.Repositories.Contract;
using EmployeeInformation.Core.Repositories.Contract;

namespace EmployeeInformation.Core.Repositories.Concrete
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<bool> CreateEmployee(Employee employee)
        {
            var transaction = true;
            try
            {
                transaction = await _employeeRepository.Create(employee);
            }
            catch (Exception)
            {

                throw;
            }

            return transaction;
        }

        public async Task<bool> UpdateEmployee(Employee employee)
        {
            var transaction = true;
            try
            {
                transaction = await _employeeRepository.Update(employee);
            }
            catch (Exception)
            {
                throw;
            }

            return transaction;
        }

        public async Task<bool> DeleteEmployee(Guid guid)
        {
            var transaction = true;
            try
            {
                transaction = await _employeeRepository.Delete(guid);
            }
            catch (Exception)
            {
                throw;
            }

            return transaction;
        }

        public async Task<Employee> GetAsync(Guid guid)
        {
            var employee = new Employee();

            try
            {
               employee = await _employeeRepository.GetAsync(guid);
            }
            catch (Exception)
            {

                throw;
            }

            return employee;
        }

        public async Task<List<Employee>> GetAllAsync()
        {
            IEnumerable<Employee> employeeList = new List<Employee>();

            try
            {
                employeeList = await _employeeRepository.GetAllAsync();

            }
            catch (Exception)
            {

                throw;
            }

            return employeeList.ToList();
        }
    }
}
