using Employee_API.Models;

namespace Employee_API.Repos.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllEmployeesAsync();

        Task<Employee> GetEmployeeByIdAsync(int id);

        void AddEmployee(Employee employee);

        void UpdateEmployee(Employee employee);

        void DeleteEmployee(Employee employee);
    }
}
