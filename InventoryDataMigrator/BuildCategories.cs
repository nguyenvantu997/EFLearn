using EFCore_Library;
using InventoryModels;

namespace InventoryDataMigrator
{
    public class BuildCategories
    {
        private readonly InventoryManageDbContext _context;
        private const string _loggedInUserId = "e2eb8989-a81a-4151-8e86- eb95a7961da2";
        public BuildCategories(InventoryManageDbContext context)
        {
            _context = context;
        }

        public void ExcuteSeed()
        {
            if (_context.Categories.Count() == 0)
            {
                _context.Categories.AddRange(
                    new Category()
                    {
                        CreatedDate = DateTime.Now,
                        IsActive = true,
                        IsDeleted = false,
                        Name = "Movies",
                        CategoryDetail = new CategoryDetail()
                        {
                            ColorValue = "#0000FF",
                            ColorName = "Blue"
                        },
                        CreatedByUserId = _loggedInUserId,
                        LastModifiedUserId = _loggedInUserId,
                        LastModifiedDate = DateTime.Now
                    },
                    new Category()
                    {
                        CreatedDate = DateTime.Now,
                        IsActive = true,
                        IsDeleted = false,
                        Name = "Books",
                        CategoryDetail = new CategoryDetail()
                        {
                            ColorValue = "#FF0000",
                            ColorName = "Red"
                        },
                        CreatedByUserId = _loggedInUserId,
                        LastModifiedUserId = _loggedInUserId,
                        LastModifiedDate = DateTime.Now
                    },
                    new Category()
                    {
                        CreatedDate = DateTime.Now,
                        IsActive = true,
                        IsDeleted = false,
                        Name = "Games",
                        CategoryDetail = new CategoryDetail()
                        {
                            ColorValue = "#008000",
                            ColorName = "Green"
                        },
                        CreatedByUserId = _loggedInUserId,
                        LastModifiedUserId = _loggedInUserId,
                        LastModifiedDate = DateTime.Now
                    }
                );

                _context.SaveChanges();

            }

        }
    }
}
