using AutoMapper;
using EFCore_Library;
using InventoryDatabaseLayer;
using InventoryModels.DTOs;

namespace InventoryBusinessLayer
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepos _dbRepo;
        public CategoryService(InventoryManageDbContext dbContext, IMapper mapper)
        {
            _dbRepo = new CategoryRepos(dbContext, mapper);
        }
        public List<CategoryDto> ListCategoriesAndDetails() { return _dbRepo.ListCategoriesAndDetails(); }

    }
}
