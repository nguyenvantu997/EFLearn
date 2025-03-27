// See https://aka.ms/new-console-template for more information
using EFCore_Library;
using InventoryDataMigrator;
using InventoryManageHelper;
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
        ApplyMigration();
        ExcuteCustomSeedData();
    }

    private static void ApplyMigration()
    {
        using(var db = new InventoryManageDbContext(_optionsBuilder.Options))
        {
            db.Database.Migrate();
        }
    }

    private static void ExcuteCustomSeedData()
    {
        using (var db = new InventoryManageDbContext(_optionsBuilder.Options))
        {
            var categories = new BuildCategories(db);
            categories.ExcuteSeed();

            var items = new BuildItems(db);
            items.ExecuteSeed();
        }
    }

    static void BuildOptions()
    {
        _configuration = ConfigurationBuilderSingleton.ConfigurationRoot;
        _optionsBuilder = new DbContextOptionsBuilder<InventoryManageDbContext>();
        _optionsBuilder.UseSqlServer(_configuration.GetConnectionString("InventoryManager"));
    }
}