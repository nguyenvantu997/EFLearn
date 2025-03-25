﻿using InventoryModels.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryModels
{
    public class CategoryDetail : IIdentityModel
    {
        [Key, ForeignKey("Category")]
        public int Id { get; set; }
        [StringLength(InventoryModelsConstants.MAX_COLORNAME_LENGTH)]
        public string ColorName { get; set; }
        [StringLength(InventoryModelsConstants.MAX_COLORVALUE_LENGTH)]
        public string ColorValue { get; set; }
        public virtual Category Category { get; set; }
    }
}
