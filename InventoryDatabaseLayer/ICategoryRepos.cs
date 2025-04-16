using InventoryModels.DTOs;

namespace InventoryDatabaseLayer
{
    public interface ICategoryRepos
    {
        List<CategoryDto> ListCategoriesAndDetails();
    }
}
