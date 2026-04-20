namespace BookingProject;

public  class Property
{
    //Data members
    public int Id{get;set;}
    public string Name { get; set; }
    public string Address { get; set; } 
    // public string? Country { get; set; }
    // public string? City { get; set; }
    //
    // public string? Description { get; set; }
    //aggregate members
    public PropertyType Type { get; set; }
    public List<Room> Rooms{get;set;}
    
    
    public Property(string name,string address)
    {
        this.Name = name;
        this.Address = address;
      
    }
    public void setRoom(List<Room> rooms)
    {
        this.Rooms = rooms;
    }
    

    
}
public enum PropertyType
{
    Hotel,
    Apartment,
    Villa   
}