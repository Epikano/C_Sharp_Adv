using BankLibrary;
using System.Data.Entity;

namespace BankApp
{
    class AccountContext : DbContext
    {
        public AccountContext()
                : base("DbConnection")
        { }

        public DbSet<AccountDB> Accounts { get; set; }
    }
}
