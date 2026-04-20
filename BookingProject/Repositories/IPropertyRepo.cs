namespace BookingProject;

public interface IPropertyRepo
{
    public List<Property> GetAllProperties();
    public Property GetPropertyById(int id);
    public void AddProperty(Property property);
    public void UpdateProperty(Property property);
    public void DeleteProperty(int id);
    
}