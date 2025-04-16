using AutoMapper;
using EFCore_Activity_10_01;
using EFCore_Library;
using InventoryBusinessLayer;
using InventoryManageHelper;
using InventoryModels;
using InventoryModels.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public class Program
{
    static MapperConfiguration _mapperConfig;
    static IMapper _mapper;
    static IServiceProvider _serviceProvider;
    static IConfigurationRoot _configuration;
    static DbContextOptionsBuilder<InventoryManageDbContext> _optionsBuilder;

    private static IItemService _itemsService;
    private static ICategoryService _categoriesService;
    private static List<CategoryDto> _categories;

    private const string _loggedInUserId = "e2eb8989-a81a-4151-8e86- eb95a7961da2";
    static void Main(string[] args)
    {
        BuildOptions();
        MapperConfig();
        using (var db = new InventoryManageDbContext(_optionsBuilder.Options))
        {
            _itemsService = new ItemService(db, _mapper);
            _categoriesService = new CategoryService(db, _mapper);
            ListInventory();
            GetItemsForListing();
            GetAllActiveItemsAsPipeDelimitedString();
            GetItemsTotalValues(); GetFullItemDetails();
            GetItemsForListingLinq();
            ListCategoriesAndColors();

            Console.WriteLine("Would you like to create items?");
            var createItems = Console.ReadLine().StartsWith("y", StringComparison.OrdinalIgnoreCase);
            if (createItems)
            {
                Console.WriteLine("Adding new Item(s)");
                CreateMultipleItems(); Console.WriteLine("Items added");
                var inventory = _itemsService.GetItems(); inventory.ForEach(x => Console.WriteLine($"Item: {x}"));
            }

            Console.WriteLine("Would you like to update items?"); var updateItems = Console.ReadLine().StartsWith("y", StringComparison.OrdinalIgnoreCase); if (updateItems)
            {
                Console.WriteLine("Updating Item(s)");
                UpdateMultipleItems();
                Console.WriteLine("Items updated");
                var inventory2 = _itemsService.GetItems(); inventory2.ForEach(x => Console.WriteLine($"Item: {x}"));
            }

            //Console.WriteLine("Would you like to delete items?");
            //var deleteItems = Console.ReadLine().StartsWith("y", StringComparison.OrdinalIgnoreCase);
            //if (deleteItems)
            //{
            //    Console.WriteLine("Deleting Item(s)");
            //    DeleteMultipleItems(); Console.WriteLine("Items Deleted");
            //    var inventory3 = _itemsService.GetItems();
            //    inventory3.ForEach(x => Console.WriteLine($"Item: {x}"));
            //}
        }

        //Console.WriteLine("============ Call ListInventoryWithProjection ============");
        //ListInventoryWithProjection();

        //Console.WriteLine("============ Call ListInventory ============");
        //ListInventory();

        //Console.WriteLine("============ Call GetItemsForListing ============");
        //GetItemsForListing();

        //Console.WriteLine("============ Call GetItemsForListingLinq ============");
        //GetItemsForListingLinq();

        //Console.WriteLine("============ Call View GetFullItemDetails ============");
        //GetFullItemDetails();

        //Console.WriteLine("============ Call Calar GetAllActiveItemsAsPipeDelimitedString ============");
        //GetAllActiveItemsAsPipeDelimitedString();

    }

    static void BuildOptions()
    {
        _configuration = ConfigurationBuilderSingleton.ConfigurationRoot;
        _optionsBuilder = new DbContextOptionsBuilder<InventoryManageDbContext>();
        _optionsBuilder.UseSqlServer(_configuration.GetConnectionString("InventoryManager"));
    }
    static void MapperConfig()
    {
        var service = new ServiceCollection();
        service.AddAutoMapper(typeof(InventoryMapper));
        _serviceProvider = service.BuildServiceProvider();

        _mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.AllowNullCollections = true;
            cfg.ShouldMapMethod = (m => false);//this is solution
            cfg.AddProfile<InventoryMapper>();
        });

        _mapperConfig.AssertConfigurationIsValid();
        _mapper = _mapperConfig.CreateMapper();
    }

    private static void ListInventory()
    {
        var result = _itemsService.GetItems();
        result.ForEach(x => Console.WriteLine($"New Item: {x}"));
    }

    private static void GetItemsForListing()
    {
        var results = _itemsService.GetItemsForListingFromProcedure();
        foreach (var item in results)
        {
            var output = $"ITEM {item.Name}] {item.Description}";
            if (!string.IsNullOrWhiteSpace(item.CategoryName))
            {
                output = $"{output} has category: {item.CategoryName}";
            }
            Console.WriteLine(output);
        }
    }

    private static void GetAllActiveItemsAsPipeDelimitedString()
    {
        Console.WriteLine($"All active Items: {_itemsService.GetAllItemsPipeDelimitedString()}");
    }

    private static void GetItemsTotalValues()
    {
        var results = _itemsService.GetItemsTotalValues(true); foreach (var item in results)
        { Console.WriteLine($"New Item] {item.Id,-10}" + $"|{item.Name,-50}" + $"|{item.Quantity,-4}" + $"|{item.TotalValue,-5}"); }
    }

    private static void GetFullItemDetails()
    {
        var result = _itemsService.GetItemsWithGenresAndCategories();
        foreach (var item in result)
        {
            Console.WriteLine($"New Item] {item.Id,-10}"
                + $"|{item.ItemName,-50}"
                + $"|{item.ItemDescription,-4}"
                + $"|{item.PlayerName,-5}" + $"|{item.Category,-5}"
                + $"|{item.GenreName,-5}");
        }
    }

    private static void GetItemsForListingLinq()
    {
        var minDateValue = new DateTime(2021, 1, 1);
        var maxDateValue = new DateTime(2024, 1, 1);
        var results = _itemsService.GetItemsByDateRange(minDateValue, maxDateValue)
            .OrderBy(y => y.CategoryName)
            .ThenBy(z => z.Name);
        foreach (var itemDto in results)
        {
            Console.WriteLine(itemDto);
        }
    }

    private static void ListCategoriesAndColors()
    {
        var results = _categoriesService.ListCategoriesAndDetails();
        _categories = results;
        foreach (var c in results)
        {
            Console.WriteLine($"Category [{c.Category}] is {c.CategoryDetail.Color}");
        }
    }

    private static void CreateMultipleItems()
    {
        Console.WriteLine("Would you like to create items as a batch?");
        bool batchCreate = Console.ReadLine().StartsWith("y", StringComparison.OrdinalIgnoreCase);
        var allItems = new List<CreateOrUpdateItemDto>();
        bool createAnother = true;
        while (createAnother == true)
        {
            var newItem = new CreateOrUpdateItemDto();
            Console.WriteLine("Creating a new item.");
            Console.WriteLine("Please enter the name");
            newItem.Name = Console.ReadLine();
            Console.WriteLine("Please enter the description");
            newItem.Description = Console.ReadLine();
            Console.WriteLine("Please enter the notes");
            newItem.Notes = Console.ReadLine();
            Console.WriteLine("Please enter the Category [B]ooks, [M]ovies, [G]ames");
            newItem.CategoryId = GetCategoryId(Console.ReadLine().Substring(0, 1).ToUpper());
            if (!batchCreate)
            {
                _itemsService.UpsertItem(newItem);
            }
            else
            {
                allItems.Add(newItem);
            }
            Console.WriteLine("Would you like to create another item?");
            createAnother = Console.ReadLine().StartsWith("y", StringComparison.OrdinalIgnoreCase);
            if (batchCreate && !createAnother)
            {
                _itemsService.UpsertItems(allItems);
            }
        }
    }

    private static void UpdateMultipleItems()
    {
        Console.WriteLine("Would you like to update items as a batch?");
        bool batchUpdate = Console.ReadLine().StartsWith("y", StringComparison.OrdinalIgnoreCase);
        var allItems = new List<CreateOrUpdateItemDto>();
        bool updateAnother = true;
        while (updateAnother == true)
        {
            Console.WriteLine("Items");
            Console.WriteLine("Enter the ID number to update");
            Console.WriteLine("*******************************");
            var items = _itemsService.GetItems();
            items.ForEach(x => Console.WriteLine($"ID: {x.Id} | {x.Name}"));
            Console.WriteLine("*******************************");
            int id = 0;
            if (int.TryParse(Console.ReadLine(), out id))
            {
                var itemMatch = items.FirstOrDefault(x => x.Id == id);
                if (itemMatch != null)
                {
                    var updItem = _mapper.Map<CreateOrUpdateItemDto>(_mapper.Map<Item>(itemMatch));
                    Console.WriteLine("Enter the new name [leave blank to keep existing]");
                    var newName = Console.ReadLine();
                    updItem.Name = !string.IsNullOrWhiteSpace(newName) ? newName : updItem.Name;
                    Console.WriteLine("Enter the new desc [leave blank to keep existing]");
                    var newDesc = Console.ReadLine();
                    updItem.Description = !string.IsNullOrWhiteSpace(newDesc) ? newDesc : updItem.Description;
                    Console.WriteLine("Enter the new notes [leave blank to keep existing]");
                    var newNotes = Console.ReadLine();
                    updItem.Notes = !string.IsNullOrWhiteSpace(newNotes) ? newNotes : updItem.Notes;
                    Console.WriteLine("Toggle Item Active Status? [y/n]");
                    var toggleActive = Console.ReadLine().Substring(0, 1).Equals("y", StringComparison.OrdinalIgnoreCase);
                    if (toggleActive)
                    {
                        updItem.IsActive = !updItem.IsActive;
                    }
                    Console.WriteLine("Enter the category - [B]ooks, [M]ovies, [G]ames, or [N]o Change");
                    var userChoice = Console.ReadLine().Substring(0, 1).ToUpper();
                    updItem.CategoryId = userChoice.Equals("N", StringComparison.OrdinalIgnoreCase) ? itemMatch.CategoryId : GetCategoryId(userChoice);
                    if (!batchUpdate)
                    {
                        _itemsService.UpsertItem(updItem);
                    }
                    else
                    {
                        allItems.Add(updItem);
                    }
                }
            }
            Console.WriteLine("Would you like to update another?"); updateAnother = Console.ReadLine().StartsWith("y", StringComparison.OrdinalIgnoreCase);
            if (batchUpdate && !updateAnother) { _itemsService.UpsertItems(allItems); }
        }
    }

    private static void DeleteMultipleItems()
    {
        Console.WriteLine("Would you like to delete items as a batch?");
        bool batchDelete = Console.ReadLine().StartsWith("y", StringComparison.OrdinalIgnoreCase);
        var allItems = new List<int>();
        bool deleteAnother = true;
        while (deleteAnother == true)
        {
            Console.WriteLine("Items");
            Console.WriteLine("Enter the ID number to delete");
            Console.WriteLine("*******************************");
            var items = _itemsService.GetItems();

            items.ForEach(x => Console.WriteLine($"ID: {x.Id} | {x.Name}"));

            Console.WriteLine("*******************************");
            if (batchDelete && allItems.Any())
            {
                Console.WriteLine("Items scheduled for delete");
                allItems.ForEach(x => Console.Write($"{x},"));
                Console.WriteLine(); Console.WriteLine("*******************************");
            }

            int id = 0; if (int.TryParse(Console.ReadLine(), out id))
            {
                var itemMatch = items.FirstOrDefault(x => x.Id == id);
                if (itemMatch != null)
                {
                    if (batchDelete)
                    {
                        if (!allItems.Contains(itemMatch.Id))
                        {
                            allItems.Add(itemMatch.Id);
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Are you sure you want to delete the item {itemMatch.Id}-{itemMatch.Name}");
                        if (Console.ReadLine().StartsWith("y", StringComparison.OrdinalIgnoreCase))
                        {
                            _itemsService.DeleteItem(itemMatch.Id); Console.WriteLine("Item Deleted");
                        }
                    }
                }
            }
            Console.WriteLine("Would you like to delete another item?");
            deleteAnother = Console.ReadLine().StartsWith("y", StringComparison.OrdinalIgnoreCase);
            if (batchDelete && !deleteAnother)
            {
                Console.WriteLine("Are you sure you want to delete the following items: ");
                allItems.ForEach(x => Console.Write($"{x},"));
                Console.WriteLine();
                if (Console.ReadLine().StartsWith("y", StringComparison.OrdinalIgnoreCase))
                {
                    _itemsService.DeleteItems(allItems);
                    Console.WriteLine("Items Deleted");
                }
            }
        }
    }

    private static int GetCategoryId(string input)
    {
        switch (input)
        {
            case "B": return _categories.FirstOrDefault(x => x.Category.ToLower().Equals("books"))?.Id ?? -1;
            case "M": return _categories.FirstOrDefault(x => x.Category.ToLower().Equals("movies"))?.Id ?? -1;
            case "G": return _categories.FirstOrDefault(x => x.Category.ToLower().Equals("games"))?.Id ?? -1;
            default: return -1;
        }
    }
}