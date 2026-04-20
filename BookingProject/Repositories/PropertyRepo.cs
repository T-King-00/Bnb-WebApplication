using BookingProject.Database;
using Microsoft.EntityFrameworkCore;

namespace BookingProject;

public class PropertyRepo : IPropertyRepo
{
    AppDbContext _context;
    
    public PropertyRepo(AppDbContext context)
    {
        _context = context;
    }
    
    public List<Property> GetAllProperties()
    {
        return _context.Property
            .Include(p => p.Rooms)
            .ThenInclude(r => r.Beds)
            .ToList();
    }

    public Property GetPropertyById(int id)
    {
        return _context.Property
            .Include(p => p.Rooms)
            .ThenInclude(r => r.Beds)
            .FirstOrDefault(x => x.Id == id);
    }

    public void AddProperty(Property property)
    {
        try
        {
            _context.Add(property);
            _context.SaveChanges();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
       
    }

    public void UpdateProperty(Property property)
    {
        throw new NotImplementedException();
    }

    public Property FetchPropertyByIdToRemove(int id)
    {
        List<Property> properties = GetAllProperties();
        Property propertytoRemove =properties.Where(x=>x.Id==id)
            .FirstOrDefault();
        try
        {
            if (propertytoRemove!=null)
            {
                return propertytoRemove;
            }

            throw new Exception("Error : Property not found");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    public void DeleteProperty(int id)
    {
        try
        {
            Property propertytoRemove = FetchPropertyByIdToRemove(id);
           
            _context.Remove(propertytoRemove);
            _context.SaveChanges();
           
        }
        catch (Exception e)
        {
            Console.WriteLine("There is a problem in deleting the property : " + e);
            throw;
        }
       
    }
}