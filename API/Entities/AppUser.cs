using API.Extensions;

namespace API.Entities;

public class AppUser
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string KnownAs { get; set; }
    public DateTime Created { get; set; } = DateTime.UtcNow;
    public DateTime LastActive {get; set; } = DateTime.UtcNow;
    public string Gender { get; set; }
    public string Introduction { get; set; }
    public string LookingFor { get; set; } 
    public string Interests { get; set; }    
    public string City { get; set; }
    public string Country { get; set; }
    public List<Photo> Photos { get; set; } = new(); 
    //easier to implement it as an empty list than as null, new() is the same as new List<photo>
    //when we have an entity/type added to another class, its referred to as a "navigation property"

    // public int GetAge() //causes automapper some problems
    // {
    //     return DateOfBirth.CalculateAge();
    // }
    

}
