﻿namespace InventoryModels.DTOs
{
    public class CreateOrUpdateItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Notes { get; set; }
        public int CategoryId { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public List<Player> Players { get; set; }
        public List<ItemGenre> ItemGenres { get; set; }
    }
}
