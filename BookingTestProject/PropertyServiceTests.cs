using BookingProject;
using BookingProject.Database;
using BookingProject.Services;

namespace BookingTestProject;

public class PropertyServiceTests
{
    private PropertyService? _propertyService;
    
    AppDbContext _db = new AppDbContext();
    
    // Test Description: checks if the property with rooms and beds is added to the database 
    // on success: returns true
    [ClassData(typeof(PropertyObjectForTest) )]
    [Theory]
    public void AddAHotelWithRooms(Hotel property)
    {
        _propertyService = new PropertyService(new PropertyRepo(_db));
        _propertyService.AddHotel(property);
        //id changes each time test run , check db table to see the last id
        Assert.Equal(_propertyService.GetPropertyById(11).Name.ToLower(), "Danish Hotel".ToLower());

    }
    
    [Theory]
    [InlineData(11)]
    public void DeleteAProperty_OnSuccess_ReturnTrue(int  propertyId)
    {
        _propertyService = new PropertyService(new PropertyRepo(_db));
        int countBefore = _propertyService.GetAllProperties().Count;
        _propertyService.DeletePropertyById(propertyId);
        int countAfter = _propertyService.GetAllProperties().Count;
        Assert.Equal((countBefore-1),countAfter);
        
    }
    
    [Theory]
    [InlineData(1)]
    public void DeleteAProperty_OnFailure_ReturnException(int  BasePropertyId)
    {
        _propertyService = new PropertyService(new PropertyRepo(_db));
        var action= () => _propertyService.DeletePropertyById(BasePropertyId);
        Assert.Throws<Exception>(action);
        
    }
    
    
}