using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.Common.Enums;
using Sprout.Exam.WebApp.Data;
using Sprout.Exam.Business.Models;

namespace Sprout.Exam.WebApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    
    public class EmployeesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<EmployeesController> _logger;
        public EmployeesController(ApplicationDbContext context, ILogger<EmployeesController> logger)
        {
            _context = context;
            _logger = logger;
        }
        /// <summary>
        /// Refactor this method to go through proper layers and fetch from the DB.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var employees = await _context.Employees.Where(e => !e.IsDeleted).ToListAsync();
            return Ok(employees);
        }

        /// <summary>
        /// Refactor this method to go through proper layers and fetch from the DB.
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == id);
            if (employee == null) return NotFound();
            return Ok(employee);
        }

        /// <summary>
        /// Refactor this method to go through proper layers and update changes to the DB.
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, EditEmployeeDto request)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == id);
            if (employee == null) return NotFound();
            employee.FullName = request.FullName;
            employee.Tin = request.Tin;
            employee.Birthdate = request.Birthdate;
            employee.EmployeeTypeId = request.EmployeeTypeId;

            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();

            return Ok(employee);
        }

        /// <summary>
        /// Refactor this method to go through proper layers and insert employees to the DB.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post(CreateEmployeeDto request)
        {
            var employee = new Employee
            {
                FullName = request.FullName,
                Tin = request.Tin,
                Birthdate = request.Birthdate, // Convert string to DateTime
                EmployeeTypeId = request.EmployeeTypeId,
            };

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = employee.Id }, employee);
        }


        /// <summary>
        /// Refactor this method to go through proper layers and perform soft deletion of an employee to the DB.
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == id);
            if (employee == null) return NotFound();

            employee.IsDeleted = true;
            await _context.SaveChangesAsync();

            return Ok(id);
        }



        /// <summary>
        /// Refactor this method to go through proper layers and use Factory pattern
        /// </summary>
        /// <param name="id"></param>
        /// <param name="absentDays"></param>
        /// <param name="workedDays"></param>
        /// <returns></returns>
        [HttpPost("{id}/calculate")]
        public async Task<IActionResult> Calculate(int id, SalaryCalculationDto request)
        {
            _logger.LogInformation($"Employee id: {id}, Absent Days: {request.AbsentDays}, Worked Days: {request.WorkedDays}");
            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == id);
            if (employee == null) return NotFound();
            
            var type = (EmployeeType)employee.EmployeeTypeId;
            decimal salary;
            switch (type)
            {
                case EmployeeType.Regular:
                    decimal basicSalary = 20000;
                    decimal tax = 0.12m;
                    decimal absentDeduction = basicSalary / 22 * request.AbsentDays;
                    salary = basicSalary - absentDeduction - (basicSalary * tax);
                    _logger.LogInformation($"Regular employee salary calculated: {salary}, Tax: {tax}, Absent Deduction: {absentDeduction}, Absent Days: {request.AbsentDays}");
                    return Ok(decimal.Round(salary, 2));

                case EmployeeType.Contractual:
                    decimal rate = 500;
                    salary = rate * request.WorkedDays;
                    _logger.LogInformation($"Contractual employee salary calculated: {salary}, Rate: {rate}, Worked Days: {request.WorkedDays}");
                    return Ok(decimal.Round(salary, 2));

                default:
                    return NotFound("Employee Type not found");
            }
        }
    }
}
