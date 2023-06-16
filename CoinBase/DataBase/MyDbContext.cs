using System.Data.Entity;

namespace CoinBase.DataBase
{
    class MyDbContext : DbContext
    {
        public MyDbContext() : base("DbConnectionString")
        {

        }
        public DbSet<Portfolio> Portfolio { get; set; }
        public DbSet<Transaction> Transactions { get; set; }  
    }
}
