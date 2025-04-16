using AutoMapper;
using InventoryBusinessLayer;
using InventoryDatabaseLayer;
using InventoryModels;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Shouldly;

namespace InventoryManagerUnitTests
{
    [TestClass]
    public class InventoryManagerUnitTests
    {
        private IItemService _itemService;
        private Mock<IItemRepos> _itemRepos;
        private static MapperConfiguration _mapperConfig;
        private static IMapper _mapper;
        private static IServiceProvider _serviceProvider;
        public TestContext TestContext { get; set; }

        private const string TITLE_NEWHOPE = "Star Wars IV: A New Hope";
        private const string TITLE_EMPIRE = "Star Wars V: The Empire Strikes Back";
        private const string TITLE_RETURN = "Star Wars VI: The Return of the Jedi";
        private const string DESC_NEWHOPE = "Luke's Friends";
        private const string DESC_EMPIRE = "Luke's Dad";
        private const string DESC_RETURN = "Luke's Sister";
        [TestInitialize]
        public void InitializeTests()
        {
            InstantiateItemReposMock();
            _itemService = new ItemService(_itemRepos.Object, _mapper);
        }

        private void InstantiateItemReposMock()
        {
            _itemRepos = new Mock<IItemRepos>();
            var items = GetItemsTestData();

            _itemRepos.Setup(repo => repo.GetItems()).Returns(items);
        }
        private List<Item> GetItemsTestData()
        {
            return new List<Item>() {
                new Item() { Id = 1, Name = TITLE_NEWHOPE, Description = DESC_NEWHOPE, CategoryId = 2 },
                new Item() { Id = 2, Name = TITLE_EMPIRE, Description = DESC_EMPIRE, CategoryId = 2 },
                new Item() { Id = 3, Name = TITLE_RETURN, Description = DESC_RETURN, CategoryId = 2 }
            };
        }

        [ClassInitialize]
        public static void InitializeTestEnvironment(TestContext testContext)
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


        [TestMethod]
        public void TestGetItems()
        {
            var result = _itemService.GetItems();
            result.ShouldNotBeNull();
            result.Count.ShouldBe(3);
            var expected = GetItemsTestData();
            var item1 = result[0];
            item1.Name.ShouldBe(TITLE_NEWHOPE);
            item1.Description.ShouldBe(DESC_NEWHOPE);
            var item2 = result[1];
            item2.Name.ShouldBe(expected[1].Name);
            item2.Description.ShouldBe(expected[1].Description);
        }


    }
}