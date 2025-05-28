using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Model;

public class TransactionType
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Type { get; set; } = null!;

    [InverseProperty(nameof(Budget.TransactionType))]
    public virtual ICollection<Budget> Budgets { get; set; } = new List<Budget>();

    [InverseProperty(nameof(Transaction.TransactionType))]
    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
