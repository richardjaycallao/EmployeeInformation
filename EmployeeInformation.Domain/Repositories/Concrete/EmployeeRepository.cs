using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using EmployeeInformation.Infrastracture.Models;
using EmployeeInformation.Domain.Repositories.Contract;

namespace EmployeeInformation.Domain.Repositories.Concrete
{
    public sealed class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(EmployeeRecordsContext employeeRecordsContext) : base(employeeRecordsContext)
        {

        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            try
            {
                return await FindAll()
                    .ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Employee> GetAsync(Guid guid)
        {
            try
            {
                return await FindByCondition(employee => employee.Id == guid)
                    .FirstOrDefaultAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<bool> Create(Employee employee)
        {
            var status = true;

            try
            {
                if (await _employeeRecordsContext.Employees.AnyAsync(m => m.Id == employee.Id))
                    return false;

                _employeeRecordsContext.Employees.Add(employee);
                var response = await _employeeRecordsContext.SaveChangesAsync();

                if (response <= 0)
                {
                    status = false;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return status;
        }

        public async Task<bool> Update(Employee employee)
        {
            var status = true;

            try
            {
                var employeeData = await _employeeRecordsContext.Employees
                                                                .AsNoTracking()
                                                                .FirstOrDefaultAsync(m => m.Id == employee.Id);

                if (employeeData != null)
                {
                    _employeeRecordsContext.Employees.Update(employee);
                    var response = await _employeeRecordsContext.SaveChangesAsync();

                    if (response <= 0)
                    {
                        status = false;
                    }
                }
                else
                {
                    status = false;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return status;
        }

        public async Task<bool> Delete(Guid guid)
        {
            var status = true;

            try
            {
                var employeeData = await _employeeRecordsContext.Employees
                                                                .AsNoTracking()
                                                                .FirstOrDefaultAsync(m => m.Id == guid);

                if (employeeData != null)
                {
                    _employeeRecordsContext.Employees.Remove(employeeData);
                    var response = await _employeeRecordsContext.SaveChangesAsync();

                    if (response <= 0)
                    {
                        status = false;
                    }
                }
                else
                {
                    status = false;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return status;
        }
    }
}
