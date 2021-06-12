using Moq;
using AutoMapper;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using EmployeeInformation.Api.ViewModels;
using EmployeeInformation.Api.Controllers;
using EmployeeInformation.Infrastracture.Models;
using EmployeeInformation.Core.Repositories.Contract;

namespace EmployeeInformation.Tests
{
    public class EmployeeServiceTest
    {
        private EmployeeController employeeController;
        private Mock<IEmployeeService> employeeServiceMock;
        private Mock<ILogger<EmployeeController>> logger;
        private Mock<IMapper> mapper;
        private Mock<Employee> employeeMock;
        private Mock<EmployeeViewModel> employeeViewModelMock;

        [SetUp]
        public void Setup()
        {
            employeeServiceMock = new Mock<IEmployeeService>();
            logger = new Mock<ILogger<EmployeeController>>();
            mapper = new Mock<IMapper>();

            employeeController = new EmployeeController(employeeServiceMock.Object, mapper.Object, logger.Object);

            employeeMock = new Mock<Employee>();
            employeeViewModelMock = new Mock<EmployeeViewModel>();
        }

        [Test]
        public void CreateEmployee_Then_Return_Success_Message()
        {
            //Arrange
            employeeServiceMock.Setup(p => p.CreateEmployee(It.IsAny<Employee>()))
                                                            .Returns(Task.FromResult(true))
                                                            .Verifiable();

            //Act
            var result = employeeController.CreateEmployee(employeeViewModelMock.Object).Result;


            //Assert
            var okResult = result as OkObjectResult;
            employeeServiceMock.Verify(s => s.CreateEmployee(It.IsAny<Employee>()), Times.AtLeastOnce());
            Assert.Pass(okResult.Value.ToString(), "Successfully saved.");
        }

        [Test]
        public void UpdateEmployee_Then_Return_Success_Message()
        {
            //Arrange
            employeeServiceMock.Setup(p => p.UpdateEmployee(It.IsAny<Employee>()))
                                                            .Returns(Task.FromResult(true))
                                                            .Verifiable();

            //Act
            var result = employeeController.UpdateEmployee(employeeViewModelMock.Object).Result;


            //Assert
            var okResult = result as OkObjectResult;
            employeeServiceMock.Verify(s => s.UpdateEmployee(It.IsAny<Employee>()), Times.AtLeastOnce());
            Assert.Pass(okResult.Value.ToString(), "Successfully updated.");
        }
    }
}