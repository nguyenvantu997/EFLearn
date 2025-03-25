using InventoryModels.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryModels
{
    [Table("ItemGenres")]
    public class ItemGenre : IIdentityModel
    {
        public int Id { get; set; }
        public virtual int ItemId { get; set; }
        public virtual Item Item { get; set; }
        public int GenreId { get; set; }
        public virtual Genre Genre { get; set; }
    }
}
