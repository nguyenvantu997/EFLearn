using EFCore_Library;
using InventoryManageHelper;
using InventoryModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using static Azure.Core.HttpHeader;

public class Program
{
    static IConfigurationRoot _configuration;
    static DbContextOptionsBuilder<InventoryManageDbContext> _optionsBuilder;

    private const string _loggedInUserId = "e2eb8989-a81a-4151-8e86- eb95a7961da2";
    static void Main(string[] args)
    {
        BuildOptions();
        //DeleteAllItems();
        EnsureItems();
        UpdateItems();
        ListInventory();
    }

    static void BuildOptions()
    {
        _configuration = ConfigurationBuilderSingleton.ConfigurationRoot;
        _optionsBuilder = new DbContextOptionsBuilder<InventoryManageDbContext>();
        _optionsBuilder.UseSqlServer(_configuration.GetConnectionString("InventoryManager"));
    }

    private static void ListInventory()
    {
        using (var db = new InventoryManageDbContext(_optionsBuilder.Options))
        {
            var items = db.Items.OrderBy(x => x.Name).ToList();
            items.ForEach(x => Console.WriteLine($"New Item: {x.Name}"));
        }
    }

    static void EnsureItems()
    {
        EnsureItem("Batman Begins", "You either die the hero or live long enough to see yourself become the villain", "Christian Bale, Katie Holmes");
        EnsureItem("Inception", "You mustn't be afraid to dream a little bigger, darling", "Leonardo DiCaprio, Tom Hardy, Joseph Gordon-Levitt");
        EnsureItem("Remember the Titans", "Left Side, Strong Side", "Denzell Washington, Will Patton");
        EnsureItem("Star Wars: The Empire Strikes Back", "He will join us or die, master", "Harrison Ford, Carrie Fisher, Mark Hamill");
        EnsureItem("Top Gun", "I feel the need, the need for speed!", "Tom Cruise, Anthony Edwards, Val Kilmer");
    }

    private static void EnsureItem(string name, string description, string notes)
    {
        Random r = new Random(); using (var db = new InventoryManageDbContext(_optionsBuilder.Options))
        {
            //determine if item exists: 
            var existingItem = db.Items.FirstOrDefault(x => x.Name.ToLower() == name.ToLower());
            if (existingItem == null)
            {
                // doesn't exist, add it. 
                var item = new Item()
                {
                    Name = name,
                    CreatedByUserId = _loggedInUserId,
                    LastModifiedUserId = _loggedInUserId,
                    Description = description,
                    Notes = notes,
                    IsActive = true,
                    Quantity = r.Next()
                }; db.Items.Add(item); db.SaveChanges();
            }
        }
    }

    private static void DeleteAllItems()
    {
        using (var db = new InventoryManageDbContext(_optionsBuilder.Options))
        {
            var items = db.Items.ToList();
            db.Items.RemoveRange(items);
            db.SaveChanges();
        }
    }

    private static void UpdateItems()
    {
        using (var db = new InventoryManageDbContext(_optionsBuilder.Options))
        {
            var items = db.Items.ToList();
            foreach (var item in items)
            {
                item.CurrentOrFinalPrice = 9.99M;
            }
            db.Items.UpdateRange(items);
            db.SaveChanges();
        }
    }
}