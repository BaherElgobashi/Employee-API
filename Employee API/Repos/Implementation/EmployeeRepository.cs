using Employee_API.Contexts;
using Employee_API.Models;
using Employee_API.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Employee_API.Repos.Implementation
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;

        public EmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync(CancellationToken cancellationToken = default)
        {
            var employees = await _context.Employees.AsNoTracking().ToListAsync(cancellationToken);
            return employees;
        }

        public async Task<Employee> GetEmployeeByIdAsync(int id , CancellationToken cancellationToken = default)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == id , cancellationToken);
            return employee;
        }
        public async Task AddEmployeeAsync(Employee employee ,  CancellationToken cancellationToken = default)
        {
            await _context.Employees.AddAsync(employee , cancellationToken);
        }

        public void UpdateEmployee(Employee employee)
        {
            _context.Employees.Update(employee);
        }

        public void DeleteEmployee(Employee employee)
        {
            _context.Employees.Remove(employee);
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
