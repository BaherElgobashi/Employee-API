using Employee_API.DTOS;
using Employee_API.Models;
using Employee_API.Repos.Interfaces;
using Employee_API.Services.Interfaces;

namespace Employee_API.Services.Implementation
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public async Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync(CancellationToken cancellationToken = default)
        {
            var employees = await _employeeRepository.GetAllEmployeesAsync(cancellationToken);
            return employees.Select(e => new EmployeeDto()
            {
                Id = e.Id,
                Name = e.Name,
                Salary = e.Salary
            });
        }

        public async Task<EmployeeDto> GetEmployeeByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(id, cancellationToken);
            if (employee is null)
                return null;
            var employeeDto = new EmployeeDto()
            {
                Id = employee.Id,
                Name = employee.Name,
                Salary = employee.Salary

            };
            return employeeDto;
        }

        public async Task<EmployeeDto> CreateEmployeeAsync(EmployeeDto employeeDto, CancellationToken cancellationToken = default)
        {
            var employee = new Employee()
            {
                Name = employeeDto.Name,
                Salary = employeeDto.Salary

            };

            await _employeeRepository.AddEmployeeAsync(employee, cancellationToken);
            await _employeeRepository.SaveChangesAsync(cancellationToken);

            return new EmployeeDto
            {
                Id = employee.Id,
                Name = employee.Name,
                Salary = employee.Salary
            };
        }

        public async Task<bool> DeleteEmployeeAsync(int id, CancellationToken cancellationToken = default)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(id, cancellationToken);

          
            if (employee is null)
                return false;

            
            _employeeRepository.DeleteEmployee(employee);
            await _employeeRepository.SaveChangesAsync(cancellationToken);

            return true;

        }

       
        public async Task<bool> UpdateEmployeeAsync(int id, EmployeeDto employeeDto, CancellationToken cancellationToken = default)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(id, cancellationToken);

            if(employee is null)
                return false;

            employee.Name = employeeDto.Name;
            employee.Salary = employeeDto.Salary;

            _employeeRepository.UpdateEmployee(employee);

            await _employeeRepository.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
