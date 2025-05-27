using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Model;

public class Budget
{
    [Key]
    public int Id { get; set; }
    [ForeignKey(nameof(TransactionType))] 
    public int TransactionTypeId { get; set; }
    public DateTime StartRange { get; set; }
    public DateTime EndRange { get; set; } 
    public decimal GoalAmount { get; set; }

    [InverseProperty(nameof(TransactionType.Budgets))]
    public virtual TransactionType TransactionType { get; set; } = null!;
}
