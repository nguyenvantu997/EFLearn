using EFCore_Library;
using InventoryManageHelper;
using InventoryModels;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

public class Program
{
    static IConfigurationRoot _configuration;
    static DbContextOptionsBuilder<InventoryManageDbContext> _optionsBuilder;

    private const string _loggedInUserId = "e2eb8989-a81a-4151-8e86- eb95a7961da2";
    static void Main(string[] args)
    {
        BuildOptions();
        ListInventory();

        Console.WriteLine("============ Call Stored Procedures ============");
        GetItemsForListing();

        Console.WriteLine("============ Call Calar Function ============");
        GetAllActiveItemsAsPipeDelimitedString();

        Console.WriteLine("============ Call Table Function ============");
        GetItemsTotalValues();

        Console.WriteLine("============ Call View Table ============");
        GetFullItemDetails();
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

    private static void GetItemsForListing()
    {
        using (var db = new InventoryManageDbContext(_optionsBuilder.Options))
        {
            var results = db.ItemsForListing.FromSqlRaw("EXECUTE dbo.GetItemsForListing").ToList();

            foreach (var item in results)
            {
                Console.WriteLine($"ITEM [{item.Name}] {item.Description}");
            }
        }
    }

    private static void GetAllActiveItemsAsPipeDelimitedString()
    {
        using (var db = new InventoryManageDbContext(_optionsBuilder.Options))
        {
            var isActiveParm = new SqlParameter("IsActive", 1);
            var result = db.AllItemsOutput.FromSqlRaw("SELECT [dbo]. [ItemNamesPipeDelimitedString] (@IsActive) AllItems", isActiveParm).FirstOrDefault();
            Console.WriteLine($"All active Items: {result.AllItems}");
        }
    }

    private static void GetItemsTotalValues()
    {
        using (var db = new InventoryManageDbContext(_optionsBuilder.Options))
        {
            var isActiveParm = new SqlParameter("IsActive", 1);
            var result = db.GetItemsTotalValues.FromSqlRaw("SELECT * from [dbo]. [GetItemsTotalValue] (@IsActive)", isActiveParm).ToList();
            foreach (var item in result) { Console.WriteLine($"New Item] {item.Id,-10}" + $"|{item.Name,-50}" + $"|{item.Quantity,-4}" + $"|{item.TotalValue,-5}"); }
        }
    }

    private static void GetFullItemDetails()
    {
        using (var db = new InventoryManageDbContext(_optionsBuilder.Options))
        {
            var result = db.FullItemDetailDtos.FromSqlRaw("SELECT * FROM [dbo]. [vwFullItemDetails] " + "ORDER BY ItemName, GenreName, Category, PlayerName ").ToList();
            foreach (var item in result)
            {
                Console.WriteLine($"New Item] {item.Id,-10}" + $"|{item.ItemName,-50}" + $"|{item.ItemDescription,-4}" + $"|{item.PlayerName,-5}" + $"|{item.Category,-5}" + $"|{item.GenreName,-5}");
            }
        }
    }
}