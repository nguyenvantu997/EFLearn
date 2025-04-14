using AutoMapper;
using AutoMapper.QueryableExtensions;
using EFCore_Activity_10_01;
using EFCore_Library;
using InventoryManageHelper;
using InventoryModels;
using InventoryModels.DTOs;
using Microsoft.Data.SqlClient;
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

    private const string _loggedInUserId = "e2eb8989-a81a-4151-8e86- eb95a7961da2";
    static void Main(string[] args)
    {
        BuildOptions();
        MapperConfig();

        //Console.WriteLine("============ Call ListInventoryWithProjection ============");
        //ListInventoryWithProjection();

        //Console.WriteLine("============ Call ListInventory ============");
        //ListInventory();

        //Console.WriteLine("============ Call GetItemsForListing ============");
        //GetItemsForListing();

        Console.WriteLine("============ Call GetItemsForListingLinq ============");
        GetItemsForListingLinq();

        Console.WriteLine("============ Call View GetFullItemDetails ============");
        GetFullItemDetails();

        Console.WriteLine("============ Call Calar GetAllActiveItemsAsPipeDelimitedString ============");
        GetAllActiveItemsAsPipeDelimitedString();

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
            var items = db.Items.ToList().OrderBy(x => x.Name).Take(20)
                .Select(x => new ItemDto
                {
                    Name = x.Name,
                    Description = x.Description
                }).ToList();
            items.ForEach(x => Console.WriteLine($"New Item: {x}"));
        }

        //using (var db = new InventoryManageDbContext(_optionsBuilder.Options))
        //{
        //    var items = db.Items.OrderBy(x => x.Name).ToList();
        //    items.ForEach(x => Console.WriteLine($"New Item: {x.Name}"));
        //}
    }

    private static void ListInventoryWithProjection()
    {
        using (var db = new InventoryManageDbContext(_optionsBuilder.Options))
        {
            var items = db.Items
                .ProjectTo<ItemDto>(_mapper.ConfigurationProvider)
                .ToList(); // Truy vấn xong mới decrypt toàn bộ kết quả

            // Sau đó mới thực hiện sắp xếp trên client
            items.OrderBy(x => x.Name).ToList().ForEach(x =>
                Console.WriteLine($"New Item: {x}")
            );
        }

        //using (var db = new InventoryManageDbContext(_optionsBuilder.Options))
        //{
        //    var items = db.Items.OrderBy(x => x.Name).ProjectTo<ItemDto>(_mapper.ConfigurationProvider).ToList();
        //    items.ForEach(x => Console.WriteLine($"New Item: {x}"));
        //}
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

    private static void GetAllActiveItemsAsPipeDelimitedString()
    {
        using (var db = new InventoryManageDbContext(_optionsBuilder.Options))
        {
            var result = db.Items.Where(x => x.IsActive).ToList();
            var pipeDelimitedString = string.Join("|", result.Select(x => x.Name));
            Console.WriteLine($"All active Items: {pipeDelimitedString}");

        }
    }

    private static void GetFullItemDetails()
    {
        using (var db = new InventoryManageDbContext(_optionsBuilder.Options))
        {
            var result = db.FullItemDetailDtos
            .FromSqlRaw("SELECT * FROM [dbo].[vwFullItemDetails]")
            .ToList() // Lấy dữ liệu và giải mã trước
            .OrderBy(x => x.ItemName)
            .ThenBy(x => x.GenreName)
            .ThenBy(x => x.Category)
            .ThenBy(x => x.PlayerName);

            foreach (var item in result)
            {
                Console.WriteLine($"New Item] {item.Id,-10}" + $"|{item.ItemName,-50}" + $"|{item.ItemDescription,-4}" + $"|{item.PlayerName,-5}" + $"|{item.Category,-5}" + $"|{item.GenreName,-5}");
            }
        }
    }


    private static void GetItemsForListingLinq()
    {
        var minDateValue = new DateTime(2021, 1, 1);
        var maxDateValue = new DateTime(2028, 1, 1);
        using (var db = new InventoryManageDbContext(_optionsBuilder.Options))
        {
            var results = db.Items.Include(x => x.Category).ToList()
                .Select(x => new ItemDto
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
}