using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingProject;

public  class Room
{
    [Key]
    public int Id{get;set;}
    
    public int size{get;set;}
    public RoomType RoomType{get;set;}
    public List<Bed> Beds { get; set; } = new ();
    
    [ForeignKey("PropertyId")]
    public int PropertyId{get;set;}
    public Property Property{get;set;}
    
    //Navigation property
    public Price Price{get;set;}
    
    
    
    public Room()
    {
        
    }
    public Room(int size, RoomType roomType, List<Bed> beds ,Price basePricePerDay)
    {
        this.size = size;
        this.RoomType = roomType;
        this.Beds = beds;
        this.Price = basePricePerDay;
    }
    
}

public class Price
{
    public int id { get; set; }
    public double BasePrice { get; set; }


    public Price(double basePrice)
    {
        BasePrice = basePrice;
    }


}

public enum  RoomType
{
    SingleRoom,
    DoubleRoom,
    SuiteRoom,
    FamilyRoom
}
