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
        private readonly ILogger<EmployeeController> _logger;   

        public EmployeeController(IEmployeeService employeeService , ILogger<EmployeeController>logger)
        {
            _employeeService = employeeService;
            _logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllEmployees(CancellationToken cancellationToken )
        {
            _logger.LogInformation("Retrieving all employees from database.");
            var employees = await _employeeService.GetAllEmployeesAsync(cancellationToken);
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(int id,CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fetching employee with ID: {EmployeeId}", id);
            var employee = await _employeeService.GetEmployeeByIdAsync(id , cancellationToken);
            if (employee is null)
            {
                _logger.LogWarning("Employee with ID: {EmployeeId} was not found", id);
                return NotFound($"Employee With {id} is not Found.");
            }
                
            return Ok(employee);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EmployeeDto employeeDto, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Attempting to create employee: {EmployeeName}", employeeDto.Name);
            var createdEmployee = await _employeeService.CreateEmployeeAsync(employeeDto, cancellationToken);
            _logger.LogInformation("Employee created successfully with ID: {EmployeeId}", createdEmployee.Id);

            return CreatedAtAction(nameof(GetEmployeeById), new { id = createdEmployee.Id }, createdEmployee);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] EmployeeDto employeeDto, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Attempting to create employee: {EmployeeName}", employeeDto.Name);
            var isUpdated = await _employeeService.UpdateEmployeeAsync(id, employeeDto, cancellationToken);
            if (!isUpdated)
            {

                _logger.LogWarning("Failed to update. Employee with ID: {EmployeeId} does not exist", id);
                return NotFound($"Employee with Id {id} was not found.");

            }
                

            return NoContent();
        }

        // 5. DELETE: api/employees/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Attempting to delete employee with ID: {EmployeeId}", id);
            var isDeleted = await _employeeService.DeleteEmployeeAsync(id, cancellationToken);
            if (!isDeleted)
            {
                _logger.LogWarning("Failed to delete. Employee with ID: {EmployeeId} does not exist", id);
                return NotFound($"Employee with Id {id} was not found.");
            }
            _logger.LogInformation("Employee with ID: {EmployeeId} deleted successfully", id);
            return NoContent();
        }


        [HttpGet("performance-test")]
        public async Task<IActionResult> PerformanceTest()
        {
            _logger.LogInformation("Starting performance benchmark test (Sequential vs Parallel)...");

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
            _logger.LogInformation("Benchmark finished. Sequential: {Sequential}ms | Parallel: {Parallel}ms", sequentialTime, parallelTime);
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
