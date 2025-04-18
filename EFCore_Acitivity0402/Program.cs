﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using EFCore_Acitivity0402;
using EFCore_Library;
using InventoryManageHelper;
using InventoryModels;
using InventoryModels.DTOs;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using static Azure.Core.HttpHeader;

public class Program
{
    static MapperConfiguration _mapperConfig;
    static IMapper _mapper;
    static IServiceProvider _serviceProvider;
    static IConfigurationRoot _configuration;
    static DbContextOptionsBuilder<InventoryManageDbContext> _optionsBuilder;

    private const string _loggedInUserId = "e2eb8989-a81a-4151-8e86- eb95a7961da2";
    static void Main(string[] args)
    {
        BuildOptions();
        MapperConfig();

        //Console.WriteLine("============ Call ListInventory ============");
        //ListInventory();

        //Console.WriteLine("============ Call ListInventoryWithProjection ============");
        //ListInventoryWithProjection();

        Console.WriteLine("============ Call ListCategoriesAndColors ============");
        ListCategoriesAndColors();
        //Console.WriteLine("============ Call GetItemsForListing ============");
        //GetItemsForListing();

        //Console.WriteLine("============ Call GetItemsForListingLinq ============");
        //GetItemsForListingLinq();

        //Console.WriteLine("============ Call Calar GetAllActiveItemsAsPipeDelimitedString ============");
        //GetAllActiveItemsAsPipeDelimitedString();

        //Console.WriteLine("============ Call Table GetItemsTotalValues ============");
        //GetItemsTotalValues();

        //Console.WriteLine("============ Call View GetFullItemDetails ============");
        //GetFullItemDetails();
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
            var items = db.Items.OrderBy(x => x.Name).ToList();
            var results = _mapper.Map<List<Item>, List<ItemDto>>(items);

            results.ForEach(x => Console.WriteLine($"new item: {x}"));
        }
    }

    private static void GetItemsForListingLinq()
    {
        var minDateValue = new DateTime(2021, 1, 1);
        var maxDateValue = new DateTime(2028, 1, 1);
        using (var db = new InventoryManageDbContext(_optionsBuilder.Options))
        {
            var results = db.Items.Select(x => new ItemDto
            {
                CreatedDate = x.CreatedDate,
                CategoryName = x.Category.Name,
                Description = x.Description,
                IsActive = x.IsActive,
                IsDeleted = x.IsDeleted,
                Name = x.Name,
                Notes = x.Notes,
                CategoryId = x.Category.Id,
                Id = x.Id
            })
            .Where(x => x.CreatedDate >= minDateValue && x.CreatedDate <= maxDateValue)
            .OrderBy(y => y.CategoryName).ThenBy(z => z.Name).ToList();

            foreach (var item in results)
            {
                Console.WriteLine($"ITEM {item}");
            }
        }
    }

    private static void ListInventoryWithProjection()
    {
        using (var db = new InventoryManageDbContext(_optionsBuilder.Options))
        {
            var items = db.Items.OrderBy(x => x.Name).ProjectTo<ItemDto>(_mapper.ConfigurationProvider).ToList();
            items.ForEach(x => Console.WriteLine($"New Item: {x}"));
        }
    }

    private static void ListCategoriesAndColors()
    {
        using (var db = new InventoryManageDbContext(_optionsBuilder.Options))
        {
            var results = db.Categories
                .Include(x => x.CategoryDetail)
                .ProjectTo<CategoryDto>(_mapper.ConfigurationProvider)
                .ToList();

            foreach (var c in results)
            {
                Console.WriteLine($"Category [{c.Category}] is {c.CategoryDetail.Color}");
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