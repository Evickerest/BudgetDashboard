namespace Data.Model;

public class BudgetDto
{
    public int Id { get; set; }
    public int TransactionTypeId { get; set; }
    public DateTime StartRange { get; set; }
    public DateTime EndRange { get; set; } 
    public decimal GoalAmount { get; set; } 
}
