using Employee_API.Models;

namespace Employee_API.Repos.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllEmployeesAsync(CancellationToken cancellationToken = default);

        Task<Employee> GetEmployeeByIdAsync(int id , CancellationToken cancellationToken = default);

        Task AddEmployeeAsync(Employee employee , CancellationToken cancellationToken = default);

        void UpdateEmployee(Employee employee);

        void DeleteEmployee(Employee employee);

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
