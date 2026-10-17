using Employee_API.Models;
using Employee_API.Repos.Interfaces;

namespace Employee_API.Repos.Implementation
{
    public class EmployeeRepository : IEmployeeRepository
    {
        public Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Employee> GetEmployeeByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
        public void AddEmployee(Employee employee)
        {
            throw new NotImplementedException();
        }

        public void UpdateEmployee(Employee employee)
        {
            throw new NotImplementedException();
        }

        public void DeleteEmployee(Employee employee)
        {
            throw new NotImplementedException();
        }

      

       
    }
}
