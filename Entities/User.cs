using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Work_with_orders.Enums;

namespace Work_with_orders.Entities;

public class User : EntityBase<long>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
    public Role Role { get; set; }
    public string Email { get; set; }
    public bool IsVerified { get; set; }
    public string Password { get; set; }

    public UserState State { get; set; }
    [Column(TypeName = "decimal(18,2)")] public decimal Balance { get; set; }
    [DataType(DataType.Date)] public DateTime DateCreated { get; set; }

    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }

    public ICollection<Order> Orders { get; set; }
    public Basket Basket { get; set; }


    public User()
    {
        DateCreated = DateTime.UtcNow.Date;
    }
}