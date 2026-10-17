using Employee_API.DTOS;
using Employee_API.Services.Interfaces;

namespace Employee_API.Services.Implementation
{
    public class EmployeeService : IEmployeeService
    {
        public Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<EmployeeDto> GetEmployeeByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<EmployeeDto> CreateEmployeeAsync(EmployeeDto employeeDto, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteEmployeeAsync(int id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

       
        public Task<bool> UpdateEmployeeAsync(int id, EmployeeDto employeeDto, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
