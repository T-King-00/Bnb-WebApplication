namespace BookingProject.Services;

public class PropertyService 
{
    private readonly IPropertyRepo _propertyRepo;
    public PropertyService(IPropertyRepo propertyRepo)
    {
        _propertyRepo = propertyRepo;
    }
    public List<BaseProperty> GetAllProperties()
    {
        return _propertyRepo.GetAllProperties();
    }
    public BaseProperty GetPropertyById(int id)
    {
        return _propertyRepo.GetPropertyById(id);
    }
    public void AddProperty(BaseProperty property)
    {
        _propertyRepo.AddProperty(property);
    }
    public void AddHotel(Hotel property)
    {
        _propertyRepo.AddProperty(property);    }
    public void UpdateProperty(BaseProperty property)
    {
        _propertyRepo.UpdateProperty(property);
    }
    public void DeletePropertyById(int id)
    {
        _propertyRepo.DeleteProperty(id);
    }

  
}