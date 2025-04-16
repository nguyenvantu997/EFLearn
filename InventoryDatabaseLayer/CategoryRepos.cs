using AutoMapper;
using AutoMapper.QueryableExtensions;
using EFCore_Library;
using InventoryModels.DTOs;
using Microsoft.EntityFrameworkCore;

namespace InventoryDatabaseLayer
{
    public class CategoryRepos: ICategoryRepos
    {
        private readonly IMapper _mapper;
        private readonly InventoryManageDbContext _context;
        public CategoryRepos(InventoryManageDbContext context, IMapper mapper)
        {
            _context = context; _mapper = mapper;
        }

        public List<CategoryDto> ListCategoriesAndDetails()
        {
            return _context.Categories.Include(x => x.CategoryDetail)
                .ProjectTo<CategoryDto>(_mapper.ConfigurationProvider).ToList();
        }
    }
}
