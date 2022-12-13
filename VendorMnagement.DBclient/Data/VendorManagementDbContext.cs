using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendorManagement.DBclient.Models;

namespace VendorMnagement.DBclient.Data
{
    public class VendorManagementDbContext : DbContext
    {
        public VendorManagementDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<PartnerType> PartnerTypes { get; set; }

        public DbSet<ContractType> ContractTypes { get; set; }

        public DbSet<ContractStatus> ContractStatus { get; set; }

        public DbSet<CommissionMethod> CommissionMethods { get; set; }

        public DbSet<Contract> Contracts { get; set; }

        public DbSet<Partner> Partners { get; set; }

        public DbSet<Sales> Sales { get; set; }

        public DbSet<Statement> Statements { get; set; }

        public DbSet<User> Users { get; set; }
    }
}
