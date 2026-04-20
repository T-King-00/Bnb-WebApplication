using System.Collections;
using BookingProject;

namespace BookingTestProject;

public class PropertyObjectForTest:IEnumerable<object[]>
{

    

    public IEnumerator<object[]> GetEnumerator()
    {
        Property property = new Property("Danish Hotel","coastal town Skagen");
        property.Rooms = new List<Room>();
        
        Bed bed=new Bed(BedType.Single,true,1);
        List<Bed> beds = new List<Bed>();
        beds.Add(bed);
        
        Room room1=new Room(11,RoomType.SingleRoom,beds,500);
        
        Bed bed2=new Bed(BedType.Double,true,1);
        List<Bed> beds2 = new List<Bed>();
        beds.Add(bed2);
        
        Room room2=new (14,RoomType.DoubleRoom,beds2,600);
        property.Rooms.Add(room1);
        property.Rooms.Add(room2);
        property.Id=11;
        yield return new object[] {property,room1,room2};
 
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}