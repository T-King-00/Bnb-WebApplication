using BookingProject;
using BookingProject.Database;
using BookingProject.Services;
using Microsoft.EntityFrameworkCore;

public class Program
{
    private static PropertyService _propertyService;

    public static void Main(string[] args)
    {
        AppDbContext db = new AppDbContext();
        _propertyService = new PropertyService(new PropertyRepo(db));

        bool exit = false;
        
         while (!exit)
         {
             Console.Clear();
             PrintWelcomeMessages();
             Console.WriteLine("1. Manage Properties");
             Console.WriteLine("0. Exit");
             Console.Write("\nSelect an option: ");

             string choice = Console.ReadLine();
             switch (choice)
             {
                 case "1":
                     ManagePropertiesMenu();
                     break;
                 case "0":
                     exit = true;
                     break;
                 default:
                     Console.WriteLine("Invalid option, try again.");
                     Thread.Sleep(1000);
                     break;
             }
         }
    }

    public static void PrintWelcomeMessages()
    {
        Console.WriteLine("===========================");
        Console.WriteLine("   Booking System Admin    ");
        Console.WriteLine("===========================");
    }

    private static void ManagePropertiesMenu()
    {
        bool back = false;
        while (!back)
        {
            Console.Clear();
            Console.WriteLine("--- Manage Properties ---");
            Console.WriteLine("1. View All Properties");
            Console.WriteLine("2. View Property Details");
            Console.WriteLine("3. Add New Property");
            Console.WriteLine("4. Delete Property");
            Console.WriteLine("0. Back to Main Menu");
            Console.Write("\nSelect an option: ");

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    ViewAllProperties();
                    break;
                case "2":
                    ViewPropertyDetails();
                    break;
                case "3":
                    AddNewProperty();
                    break;
                case "4":
                    DeleteProperty();
                    break;
                case "0":
                    back = true;
                    break;
                default:
                    Console.WriteLine("Invalid option.");
                    Thread.Sleep(1000);
                    break;
            }
        }
    }

    private static void ViewAllProperties()
    {
        Console.Clear();
        Console.WriteLine("--- All Properties ---");
        var properties = _propertyService.GetAllProperties();
        if (properties.Count == 0)
        {
            Console.WriteLine("No properties found.");
        }
        else
        {
            foreach (var prop in properties)
            {
                PrintPropertyBrief(prop);
            }
        }
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }

    private static void ViewPropertyDetails()
    {
        Console.Clear();
        Console.WriteLine("--- View Property Details ---");
        var properties = _propertyService.GetAllProperties();

        if (properties.Count == 0)
        {
            Console.WriteLine("No properties found.");
            Thread.Sleep(1500);
            return;
        }

        foreach (var p in properties)
        {
            Console.WriteLine($"ID: {p.Id} | Name: {p.Name}");
        }

        Console.Write("\nEnter the ID of the property to view details (or '0' to cancel): ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            if (id == 0) return;

            var prop = _propertyService.GetPropertyById(id);
            if (prop == null)
            {
                Console.WriteLine("Property not found.");
            }
            else
            {
                PrintPropertyFullDetails(prop);
            }
        }
        else
        {
            Console.WriteLine("Invalid ID.");
        }
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }

    private static void PrintPropertyBrief(Property prop)
    {
        Console.WriteLine($"ID: {prop.Id} | Name: {prop.Name} | Location: {prop.Address} | Rooms: {prop.Rooms?.Count ?? 0}");
    }

    private static void PrintPropertyFullDetails(Property prop)
    {
        Console.Clear();
        Console.WriteLine("========================================");
        Console.WriteLine($" PROPERTY DETAILS: {prop.Name}");
        Console.WriteLine("========================================");
        Console.WriteLine($"ID:       {prop.Id}");
        Console.WriteLine($"Address:  {prop.Address}");
        Console.WriteLine($"Type:     {prop.Type}");
        Console.WriteLine("----------------------------------------");
        Console.WriteLine("ROOMS:");

        if (prop.Rooms == null || prop.Rooms.Count == 0)
        {
            Console.WriteLine("  No rooms found.");
        }
        else
        {
            foreach (var room in prop.Rooms)
            {
                Console.WriteLine($"  - Room ID: {room.Id} | Type: {room.RoomType} | Size: {room.size} sqm | Price: {room.Price:C}/day");
                if (room.Beds == null || room.Beds.Count == 0)
                {
                    Console.WriteLine("    No beds found.");
                }
                else
                {
                    Console.WriteLine("     Beds:");
                    foreach (var bed in room.Beds)
                    {
                        Console.WriteLine($"        * {bed.Quantity}x {bed.Type} Bed (Available: {(bed.Available ? "Yes" : "No")})");
                    }
                }
            }
        }
        Console.WriteLine("========================================");
    }

    private static void AddNewProperty()
    {
        Console.Clear();
        Console.WriteLine("--- Add New Property ---");
        
        Console.Write("Enter Name: ");
        string name = Console.ReadLine();
        
        Console.Write("Enter Description (Optional): ");
        string description = Console.ReadLine();
        
        Console.Write("Enter Country (Optional): ");
        string country = Console.ReadLine();
        
        Console.Write("Enter City (Optional): ");
        string city = Console.ReadLine();
        
        Console.Write("Enter Address: ");
        string address = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(name) ||
            string.IsNullOrWhiteSpace(address) )

        {
            Console.WriteLine("Error: Name and Address are required.");
            Thread.Sleep(2000);
            return;
        }
    
        Property newProperty = new Property(name, address);
        newProperty.Rooms = new List<Room>();

        bool addMoreRooms = true;
        while (addMoreRooms)
        {
            Console.Write("\nDo you want to add a room to this property? (y/n): ");
            if (Console.ReadLine()?.ToLower() != "y")
            {
                addMoreRooms = false;
                continue;
            }

            Room room = new Room();
            Console.Write("Enter Room Size (sqm): ");
            if (int.TryParse(Console.ReadLine(), out int size)) room.size = size;

            Console.Write("Enter Base Price Per Day: ");
            if (double.TryParse(Console.ReadLine(), out double price))
            {
                Price p = new Price(price);
                room.Price= p;
            }
           

            Console.WriteLine("Select Room Type:");
            foreach (var type in Enum.GetValues<RoomType>())
            {
                Console.WriteLine($"{(int)type}. {type}");
            }
            Console.Write("Choice: ");
            if (int.TryParse(Console.ReadLine(), out int rtIndex) && Enum.IsDefined(typeof(RoomType), rtIndex))
            {
                room.RoomType = (RoomType)rtIndex;
            }

            room.Beds = new List<Bed>();
            bool addMoreBeds = true;
            while (addMoreBeds)
            {
                Console.Write("Do you want to add a bed to this room? (y/n): ");
                if (Console.ReadLine()?.ToLower() != "y")
                {
                    addMoreBeds = false;
                    continue;
                }

                Console.WriteLine("Select Bed Type:");
                foreach (var bType in Enum.GetValues<BedType>())
                {
                    Console.WriteLine($"{(int)bType}. {bType}");
                }
                Console.Write("Choice: ");
                BedType selectedBedType = BedType.Single;
                if (int.TryParse(Console.ReadLine(), out int btIndex) && Enum.IsDefined(typeof(BedType), btIndex))
                {
                    selectedBedType = (BedType)btIndex;
                }

                Console.Write("Enter Quantity: ");
                int.TryParse(Console.ReadLine(), out int qty);

                room.Beds.Add(new Bed(selectedBedType, true, qty));
            }

            newProperty.Rooms.Add(room);
        }

        _propertyService.AddProperty(newProperty);
        
        Console.WriteLine("\nProperty with rooms and beds added successfully!");
        Thread.Sleep(1500);
    }

    private static void DeleteProperty()
    {
        Console.Clear();
        Console.WriteLine("--- Delete Property ---");
        var properties = _propertyService.GetAllProperties();
        
        if (properties.Count == 0)
        {
            Console.WriteLine("No properties to delete.");
            Thread.Sleep(1500);
            return;
        }

        foreach (var prop in properties)
        {
            Console.WriteLine($"ID: {prop.Id} | Name: {prop.Name}");
        }

        Console.Write("\nEnter the ID of the property to delete (or '0' to cancel): ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            if (id == 0) return;
            
            _propertyService.DeletePropertyById(id);
            Console.WriteLine("Property deleted successfully.");
            
        }
        else
        {
            Console.WriteLine("Invalid ID.");
        }
        Thread.Sleep(1500);
    }
}