using System.ComponentModel.DataAnnotations;

namespace InventoryModels
{
    public class Genre : FullAuditModel
    {
        [StringLength(InventoryModelsConstants.MAX_GENRENAME_LENGTH)]
        public string Name { get; set; }
        public virtual List<ItemGenre> ItemGenres { get; set; } = new List<ItemGenre>();
    }
}
