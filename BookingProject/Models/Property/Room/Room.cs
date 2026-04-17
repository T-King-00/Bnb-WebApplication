namespace BookingProject;

public abstract class Room
{
    public int Id{get;set;}
    public List<Bed>? Beds { get; set; } = new ();
    public double BasePricePerDay{get;set;}
    
}

