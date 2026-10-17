using Employee_API.DTOS;

namespace Employee_API.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync(CancellationToken cancellationToken = default);

        Task<EmployeeDto> GetEmployeeByIdAsync(int id  , CancellationToken cancellationToken = default);

        Task<EmployeeDto> CreateEmployeeAsync(EmployeeDto employeeDto, CancellationToken cancellationToken = default);

        Task<bool> UpdateEmployeeAsync(int id, EmployeeDto employeeDto, CancellationToken cancellationToken = default);

        Task<bool> DeleteEmployeeAsync(int id, CancellationToken cancellationToken = default);
    }
}
