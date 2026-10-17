using Employee_API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Employee_API.Configurations
{
    public class EmplyeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(e => e.Salary)
                .HasPrecision(18,2);
        }
    }
}
