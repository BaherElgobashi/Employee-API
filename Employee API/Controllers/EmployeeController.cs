using Employee_API.DTOS;
using Employee_API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Employee_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _employeeService.GetAllEmployeesAsync();
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            if (employee is null)
                return NotFound($"Employee With {id} is not Found.");
            return Ok(employee);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] EmployeeDto employeeDto, CancellationToken cancellationToken)
        {
            var isUpdated = await _employeeService.UpdateEmployeeAsync(id, employeeDto, cancellationToken);
            if (!isUpdated)
                return NotFound($"Employee with Id {id} was not found.");

            return NoContent();
        }

        // 5. DELETE: api/employees/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var isDeleted = await _employeeService.DeleteEmployeeAsync(id, cancellationToken);
            if (!isDeleted)
                return NotFound($"Employee with Id {id} was not found.");

            return NoContent();
        }


        [HttpGet("performance-test")]
        public async Task<IActionResult> PerformanceTest()
        {
            
            async Task SimulateTaskAsync(int delayMs) => await Task.Delay(delayMs);

            var stopwatch = new Stopwatch();

            
            stopwatch.Start();
            await SimulateTaskAsync(1000);
            await SimulateTaskAsync(1500);
            stopwatch.Stop();
            long sequentialTime = stopwatch.ElapsedMilliseconds;

            
            stopwatch.Restart();
            var task1 = SimulateTaskAsync(1000);
            var task2 = SimulateTaskAsync(1500);
            await Task.WhenAll(task1, task2);
            stopwatch.Stop();
            long parallelTime = stopwatch.ElapsedMilliseconds;

            return Ok(new
            {
                Sequential_Time_Ms = sequentialTime,
                Parallel_Time_Ms = parallelTime,
                Saved_Time_Ms = sequentialTime - parallelTime,
                IsParallelFaster = parallelTime < sequentialTime,
                Conclusion = "Parallel Time is less than Sequential Time."
            });
        }

    }
}
