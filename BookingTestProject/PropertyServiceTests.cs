using BookingProject;
using BookingProject.Database;
using BookingProject.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestPlatform.PlatformAbstractions;

namespace BookingTestProject;

public class PropertyServiceTests
{
    private PropertyService _propertyService;
    
    AppDbContext db = new AppDbContext();
    
    // Test Description: checks if the property with rooms and beds is added to the database 
    // on success: returns true
    [ClassData(typeof(PropertyObjectForTest) )]
    [Theory]
    public void AddAPropertyWithRooms(Property property,Room room1,Room room2)
    {
        _propertyService = new PropertyService(new PropertyRepo(db));
        _propertyService.AddProperty(property);
        //id changes each time test run , check db table to see the last id
        Assert.Equal(_propertyService.GetPropertyById(11).Name.ToLower(), "Danish Hotel".ToLower());

    }
    
    [Theory]
    [InlineData(11)]
    public void DeleteAProperty_OnSuccess_ReturnTrue(int  propertyId)
    {
        _propertyService = new PropertyService(new PropertyRepo(db));
        int countBefore = _propertyService.GetAllProperties().Count;
        _propertyService.DeletePropertyById(propertyId);
        int countAfter = _propertyService.GetAllProperties().Count;
        Assert.Equal((countBefore-1),countAfter);
        
    }
    
    [Theory]
    [InlineData(1)]
    public void DeleteAProperty_OnFailure_ReturnException(int  propertyId)
    {
        _propertyService = new PropertyService(new PropertyRepo(db));
        var action= () => _propertyService.DeletePropertyById(propertyId);
        Assert.Throws<Exception>(action);
        
    }
    
    
}