using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Model;

public class Account
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = null!;
    public decimal Balance { get; set; }
    public bool IsCredit { get; set; } = false;

    [InverseProperty(nameof(Transaction.Account))]
    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

    public AccountDto ToDto()
    {
        return new AccountDto
        {
            Id = Id,
            Name = Name,
            Balance = Balance,
            IsCredit = IsCredit
        };
    }
}
