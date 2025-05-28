using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Model;

public class Budget
{
    [Key]
    public int Id { get; set; }
    [ForeignKey(nameof(TransactionType))] 
    public int TransactionTypeId { get; set; }
    [Required]
    public DateTime StartRange { get; set; }
    [Required]
    public DateTime EndRange { get; set; } 
    [Required]
    public decimal GoalAmount { get; set; }

    [InverseProperty(nameof(TransactionType.Budgets))]
    public virtual TransactionType TransactionType { get; set; } = null!;
}
