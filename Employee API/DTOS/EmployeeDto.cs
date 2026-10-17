namespace Employee_API.DTOS
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Salary { get; set; }
    }
}
