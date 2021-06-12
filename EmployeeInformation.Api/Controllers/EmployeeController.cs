using System;
using Serilog;
using AutoMapper;
using System.Net;
using System.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using EmployeeInformation.Api.ViewModels;
using EmployeeInformation.Infrastracture.Models;
using EmployeeInformation.Core.Repositories.Contract;

namespace EmployeeInformation.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(IEmployeeService employeeService,
                                  IMapper mapper,
                                  ILogger<EmployeeController> logger)
        {
            _employeeService = employeeService;
            _mapper = mapper;
            _logger = logger;
        }

        [Route("GetAllEmployee")]
        [HttpGet]
        public async Task<IActionResult> GetAllEmployee()
        {
            var employeeListViewModel = new List<EmployeeViewModel>();

            try
            {
                _logger.LogInformation("GetAllEmployee endpoint was invoked.");

                var employeeList = await _employeeService.GetAllAsync();
                employeeListViewModel = _mapper.Map<List<EmployeeViewModel>>(employeeList);

                _logger.LogInformation($"Returning {employeeListViewModel.Count} records.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
                throw;
            }

            return Ok(employeeListViewModel);
        }

        [Route("GetEmployeeByUniqueId")]
        [HttpPost]
        public async Task<IActionResult> GetEmployeeByUniqueId(Guid guid)
        {
            var employeeViewModel = new EmployeeViewModel();
            try
            {
                _logger.LogInformation($"GetEmployeeByUniqueId endpoint was invoked using Id {guid}");

                var employee = await _employeeService.GetAsync(guid);
                employeeViewModel = _mapper.Map<EmployeeViewModel>(employee);

                if (employee != null)
                {
                    _logger.LogInformation($"Succesfully fetched employee with the following details: {JsonConvert.SerializeObject(employeeViewModel)}");
                    return Ok(employeeViewModel);
                }
                else
                {
                    _logger.LogInformation("Employee not found. Please check the unique Id.");
                    return BadRequest("Employee not found. Please check the unique Id.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
                throw;
            }

        }

        [Route("CreateEmployee")]
        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] EmployeeViewModel employee)
        {
            try
            {
                _logger.LogInformation($"CreateEmployee endpoint was invoked.");

                var status = await _employeeService.CreateEmployee(_mapper.Map<Employee>(employee));

                if (status)
                {
                    _logger.LogInformation("Successfully saved new employee.");
                    return Ok("Successfully saved.");
                }
                else
                {
                    _logger.LogInformation("Employee unique Id already exists.");
                    return BadRequest("Employee unique Id already exists.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [Route("UpdateEmployee")]
        [HttpPut]
        public async Task<IActionResult> UpdateEmployee(EmployeeViewModel employee)
        {
            try
            {
                _logger.LogInformation($"UpdateEmployee endpoint was invoked.");

                var status = await _employeeService.UpdateEmployee(_mapper.Map<Employee>(employee));

                if (status)
                {
                    _logger.LogInformation($"Successfully updated employee with Id: {employee.Id}");
                    return Ok("Successfully updated.");
                }
                else
                {
                    _logger.LogInformation("Employee not found. Please check the unique Id.");
                    return BadRequest("Employee not found. Please check the unique Id.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [Route("DeleteEmployee")]
        [HttpDelete]
        public async Task<IActionResult> DeleteEmployee(Guid guid)
        {
            try
            {
                _logger.LogInformation($"UpdateEmployee endpoint was invoked.");

                var status = await _employeeService.DeleteEmployee(guid);

                if (status)
                {
                    _logger.LogInformation($"Successfully deleted employee with Id: {guid}");
                    return Ok("Successfully deleted.");
                }
                else
                {
                    _logger.LogInformation("Employee not found. Please check the unique Id.");
                    return BadRequest("Employee not found. Please check the unique Id.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

    }
}
