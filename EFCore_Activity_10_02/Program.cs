using EFCore_Activity_10_02;
using InventoryManageHelper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

public class Program
{
    static IConfigurationRoot _configuration;
    static DbContextOptionsBuilder<AdventureWorks2019Context> _optionsBuilder;
    static void Main(string[] args)
    {
        BuildOptions();
        //ListAllSalespeople();
        //ShowAllSalespeopleUsingProjection();

        Console.WriteLine("What is the minimum amount of sales?");
        var input = Console.ReadLine();
        decimal filter = 0.0m;
        if (!decimal.TryParse(input, out filter))
        {
            Console.WriteLine("Bad input");
            return;
        }

        //Console.WriteLine("List People Then Order and Take");
        //ListPeopleThenOrderAndTake();
        //Console.WriteLine("Query People, order, then list and take");
        //QueryPeopleOrderedToListAndTake();

        //Console.WriteLine("Please Enter the partial First or Last Name, or the Person Type to search for:");
        //var result = Console.ReadLine();
        //FilteredPeople(result);

        //int pageSize = 10;
        //for (int pageNumber = 0; pageNumber < pageSize; pageNumber++)
        //{
        //    {
        //        Console.WriteLine($"Page {pageNumber + 1}");
        //        FilteredAndPagedResult(result, pageNumber, pageSize);
        //    }
        //}

    }

    private static void ShowAllSalespeopleUsingProjection()
    {
        using (var db = new AdventureWorks2019Context(_optionsBuilder.Options))
        {
            // Truy vấn dữ liệu bằng projection
            var salespeople = db.SalesPeople
                //.Include(x => x.BusinessEntity)
                //.ThenInclude(y => y.BusinessEntity)
                .AsNoTracking()
                .Select(x => new
                {
                    x.BusinessEntityId,
                    x.BusinessEntity.BusinessEntity.FirstName,
                    x.BusinessEntity.BusinessEntity.LastName,
                    x.SalesQuota,
                    x.SalesYtd,
                    x.SalesLastYear
                })
                .ToList();

            // Duyệt qua danh sách nhân viên và in ra màn hình
            foreach (var sp in salespeople)
            {
                Console.WriteLine($"BID: {sp.BusinessEntityId} | Name: {sp.LastName}, {sp.FirstName} | Quota: {sp.SalesQuota} | " +
                                  $"YTD Sales: {sp.SalesYtd} | SalesLastYear {sp.SalesLastYear}");
            }
        }
    }

    private static void ListAllSalespeople()
    {
        using (var db = new AdventureWorks2019Context(_optionsBuilder.Options))
        {
            var salespeople = db.SalesPeople
                .Include(x => x.BusinessEntity)
                .ThenInclude(y => y.BusinessEntity).AsNoTracking().ToList();

            foreach (var salesperson in salespeople)
            {
                Console.WriteLine(GetSalespersonDetail(salesperson));
            }
        }

    }

    private static string GetSalespersonDetail(SalesPerson sp)
    {
        return $"ID: {sp.BusinessEntityId}\t|TID: {sp.TerritoryId}\t|Quota: {sp.SalesQuota}\t"
            + $"|Bonus: {sp.Bonus}\t|YTDSales: {sp.SalesYtd}\t|Name: \t"
            + $"{sp.BusinessEntity?.BusinessEntity?.FirstName ?? ""}, "
            + $"{sp.BusinessEntity?.BusinessEntity?.LastName ?? ""}";
    }

    private static void FilteredAndPagedResult(string filter, int pageNumber, int pageSize)
    {
        using (var db = new AdventureWorks2019Context(_optionsBuilder.Options))
        {
            var searchTerm = filter.ToLower();
            var query = db.People.Where(x => x.LastName.ToLower().Contains(searchTerm)
            || x.FirstName.ToLower().Contains(searchTerm)
            || x.PersonType.ToLower().Equals(searchTerm)).OrderBy(x => x.LastName).Skip(pageNumber * pageSize).Take(pageSize);
            foreach (var person in query)
            {
                Console.WriteLine($"{person.FirstName} {person.LastName}, {person.PersonType}");
            }
        }
    }

    private static void ListPeopleThenOrderAndTake()
    {
        using (var db = new AdventureWorks2019Context(_optionsBuilder.Options))
        {
            var people = db.People.ToList().OrderByDescending(x => x.LastName);
            foreach (var person in people.Take(10))
            {
                Console.WriteLine($"{person.FirstName} " +
                $"{person.LastName}");
            }
        }
    }
    private static void QueryPeopleOrderedToListAndTake()
    {
        using (var db = new AdventureWorks2019Context(_optionsBuilder.Options))
        {
            var query = db.People.OrderByDescending(x => x.LastName); var result = query.Take(10);
            foreach (var person in result)
            {
                Console.WriteLine($"{person.FirstName} {person.LastName}");
            }
        }
    }

    private static void FilteredPeople(string filter)
    {
        using (var db = new AdventureWorks2019Context(_optionsBuilder.Options))
        {
            var searchTerm = filter.ToLower();
            var query = db.People.Where(x => x.LastName.ToLower().Contains(searchTerm)
                                            || x.FirstName.ToLower().Contains(searchTerm)
                                            || x.PersonType.ToLower().Equals(searchTerm));
            //foreach (var person in query)
            //{
            //    Console.WriteLine($"{person.FirstName} {person.LastName}, {person.PersonType}");
            //}
        }
    }

    static void BuildOptions()
    {
        _configuration = ConfigurationBuilderSingleton.ConfigurationRoot;
        _optionsBuilder = new DbContextOptionsBuilder<AdventureWorks2019Context>();
        _optionsBuilder.UseSqlServer(_configuration.GetConnectionString("AdventureWorks"));
    }
}
