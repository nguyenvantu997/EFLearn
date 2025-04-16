using InventoryModels.DTOs;

namespace InventoryBusinessLayer
{
    public interface ICategoryService
    {
        List<CategoryDto> ListCategoriesAndDetails();
    }
}
