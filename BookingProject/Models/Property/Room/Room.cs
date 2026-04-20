namespace BookingProject;

public  class Room
{
    public int Id{get;set;}

    public double BasePricePerDay{get;set;}
    public int size{get;set;}
    
    public RoomType RoomType{get;set;}
    public List<Bed> Beds { get; set; } = new ();

    public Room()
    {
        
    }
    public Room(int size, RoomType roomType, List<Bed> beds ,double basePricePerDay)
    {
        this.size = size;
        this.RoomType = roomType;
        this.Beds = beds;
        this.BasePricePerDay = basePricePerDay;
    }
    
}

public enum  RoomType
{
    SingleRoom,
    DoubleRoom,
    SuiteRoom,
    FamilyRoom
}
