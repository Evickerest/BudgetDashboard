namespace Data.Model;

public class TransactionDto
{
    public int Id { get; set; }
    public decimal Value {  get; set; }
    public string AccountName { get; set; } = null!;
    public string TransactionType { get; set; } = null!;
    public DateTime DateEntered { get; set; }
    public DateTime? TransactionDate { get; set;  } 
}