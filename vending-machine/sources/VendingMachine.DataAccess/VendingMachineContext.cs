using Microsoft.EntityFrameworkCore;
using Nagarro.VendingMachine.Domain.Report;

namespace Nagarro.VendingMachine.DataAccess
{
    public class VendingMachineContext : DbContext
    {
        public DbSet<Domain.Product> Products { get; set; }

        public DbSet<Sale> Sales { get; set; }

        private readonly string _connectionString;

        public VendingMachineContext() { }

        public VendingMachineContext(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString));
            }

            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={_connectionString}");
        }
    }
}
