namespace Domain.Model; 

public class Address : Entity {

    public string Street { get; set; }
    public string City { get; set; }
    public string Country { get; set; }

    public virtual User User { get; set; }
}
