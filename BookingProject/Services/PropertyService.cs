namespace BookingProject.Services;

public class PropertyService 
{
    private readonly IPropertyRepo _propertyRepo;
    public PropertyService(IPropertyRepo propertyRepo)
    {
        _propertyRepo = propertyRepo;
    }
    public List<Property> GetAllProperties()
    {
        return _propertyRepo.GetAllProperties();
    }
    public Property GetPropertyById(int id)
    {
        return _propertyRepo.GetPropertyById(id);
    }
    public void AddProperty(Property property)
    {
        _propertyRepo.AddProperty(property);
    }
    public void UpdateProperty(Property property)
    {
        _propertyRepo.UpdateProperty(property);
    }
    public void DeletePropertyById(int id)
    {
        _propertyRepo.DeleteProperty(id);
    }
}