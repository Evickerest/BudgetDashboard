using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Model;

public class Transaction
{
    [Key]
    public int Id { get; set; }
    [ForeignKey(nameof(Account))]
    public int AccountId { get; set; }
    [ForeignKey(nameof(TransactionType))]
    public int TransactionTypeId { get; set; }
    [Required]
    public decimal Value {  get; set; }
    public DateTime DateEntered { get; set; }
    public DateTime? TransactionDate { get; set;  }

    [InverseProperty(nameof(Account.Transactions))]
    public virtual Account Account { get; set; } = null!;

    [InverseProperty(nameof(TransactionType.Transactions))]
    public virtual TransactionType TransactionType { get; set; } = null!;
}