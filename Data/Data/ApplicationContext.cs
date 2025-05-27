using Microsoft.EntityFrameworkCore;
using Data.Model;

namespace Data.Data;

public class ApplicationContext : DbContext
{
    public virtual DbSet<Account> Accounts { get; set; }
    public virtual DbSet<Budget> Budgets { get; set; }
    public virtual DbSet<Transaction> Transactions { get; set; }
    public virtual DbSet<TransactionType> TransactionTypes { get; set; } 

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(@"Server=localhost;Port=5432;Database=Budget;User Id=postgres;Password=root");
        base.OnConfiguring(optionsBuilder);
    }
}