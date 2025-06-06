namespace Data.Model;

public class AccountDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public decimal Balance { get; set; }
    public bool IsCredit { get; set; } = false; 
}
