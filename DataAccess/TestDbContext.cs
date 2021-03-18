using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess
{
    public class TestDbContext:DbContext
    {
        private readonly string _connectionString;
        public DbSet<Device> Devices { get; set; }
        public DbSet<Gateway> Gateways { get; set; }
        public TestDbContext(DbContextOptions<TestDbContext> options):base(options)
        {
        }
        public TestDbContext(string connectionString):base()
        {
            _connectionString = connectionString;
        }

    }
}
