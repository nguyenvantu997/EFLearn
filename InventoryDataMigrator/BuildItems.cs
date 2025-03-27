﻿using EFCore_Library;
using InventoryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryDataMigrator
{
    public class BuildItems
    {
        private readonly InventoryManageDbContext _context;
        private const string SEED_USER_ID = "31412031-7859-429c-ab21-c2e3e8d98042";

        public BuildItems(InventoryManageDbContext context)
        {
            _context = context;
        }

        public void ExecuteSeed()
        {
            if (_context.Items.Count() == 0)
            {
                _context.Items.AddRange(
                    new Item()
                    {
                        Name = "Batman Begins",
                        CurrentOrFinalPrice = 9.99m,
                        Description = "Bạn hoặc chết như một anh hùng hoặc sống đủ lâu để thấy mình trở thành kẻ phản diện.",
                        IsOnSale = false,
                        Notes = "",
                        PurchasePrice = 23.99m,
                        PurchasedDate = null,
                        Quantity = 1000,
                        SoldDate = null,
                        CreatedByUserId = SEED_USER_ID,
                        CreatedDate = DateTime.Now,
                        LastModifiedUserId = SEED_USER_ID,
                        LastModifiedDate = DateTime.Now,
                        IsDeleted = false,
                        IsActive = true,
                        Players = new List<Player>()
                        {
                    new Player()
                    {
                        CreatedDate = DateTime.Now, LastModifiedUserId = SEED_USER_ID,        LastModifiedDate = DateTime.Now,
                        IsActive = true,
                        IsDeleted = false,
                        CreatedByUserId = SEED_USER_ID,
                        Description = "https://www.imdb.com/name/nm0000288/",
                        Name = "Christian Bale"
                    }
                        }
                    },
                    new Item()
                    {
                        Name = "Inception",
                        CurrentOrFinalPrice = 7.99m,
                        Description = "Bạn không nên sợ ước mơ một chút táo bạo hơn, cưng ạ!",
                        IsOnSale = false,
                        Notes = "",
                        PurchasePrice = 4.99m,
                        PurchasedDate = null,
                        Quantity = 1000,
                        SoldDate = null,
                        CreatedByUserId = SEED_USER_ID,
                        CreatedDate = DateTime.Now,
                        LastModifiedUserId = SEED_USER_ID,
                        LastModifiedDate = DateTime.Now,
                        IsDeleted = false,
                        IsActive = true,
                        Players = new List<Player>()
                        {
                    new Player()
                    {
                        CreatedDate = DateTime.Now, LastModifiedUserId = SEED_USER_ID,        LastModifiedDate = DateTime.Now,
                        IsActive = true,
                        IsDeleted = false,
                        CreatedByUserId = SEED_USER_ID,
                        Description = "https://www.imdb.com/name/nm0000138/",
                        Name = "Leonardo DiCaprio"
                    }
                        }
                    },
                    new Item()
                    {
                        Name = "Remember the Titans",
                        CurrentOrFinalPrice = 3.99m,
                        Description = "Cánh trái, cánh mạnh!",
                        IsOnSale = false,
                        Notes = "",
                        PurchasePrice = 7.99m,
                        PurchasedDate = null,
                        Quantity = 1000,
                        SoldDate = null,
                        CreatedByUserId = SEED_USER_ID,
                        CreatedDate = DateTime.Now,
                        LastModifiedUserId = SEED_USER_ID,
                        LastModifiedDate = DateTime.Now,
                        IsDeleted = false,
                        IsActive = true,
                        Players = new List<Player>()
                        {
                    new Player()
                    {
                        CreatedDate = DateTime.Now, LastModifiedUserId = SEED_USER_ID,        LastModifiedDate = DateTime.Now,
                        IsActive = true,
                        IsDeleted = false,
                        CreatedByUserId = SEED_USER_ID,
                        Description = "https://www.imdb.com/name/nm0000243/",
                        Name = "Denzel Washington"
                    }
                        }
                    },
                    new Item()
                    {
                        Name = "Star Wars: The Empire Strikes Back",
                        CurrentOrFinalPrice = 19.99m,
                        Description = "Anh ấy sẽ tham gia cùng chúng ta hoặc chết, thưa chủ nhân!",
                        IsOnSale = false,
                        Notes = "",
                        PurchasePrice = 35.99m,
                        PurchasedDate = null,
                        Quantity = 1000,
                        SoldDate = null,
                        CreatedByUserId = SEED_USER_ID,
                        CreatedDate = DateTime.Now,
                        LastModifiedUserId = SEED_USER_ID,
                        LastModifiedDate = DateTime.Now,
                        IsDeleted = false,
                        IsActive = true,
                        Players = new List<Player>()
                        {
                    new Player()
                    {
                        CreatedDate = DateTime.Now, LastModifiedUserId = SEED_USER_ID,        LastModifiedDate = DateTime.Now,
                        IsActive = true,
                        IsDeleted = false,
                        CreatedByUserId = SEED_USER_ID,
                        Description = "https://www.imdb.com/name/nm0000434/",
                        Name = "Mark Hamill"
                    }
                        }
                    },
                    new Item()
                    {
                        Name = "Top Gun",
                        CurrentOrFinalPrice = 6.99m,
                        Description = "Tôi cảm thấy cần tốc độ!",
                        IsOnSale = false,
                        Notes = "",
                        PurchasePrice = 8.99m,
                        PurchasedDate = null,
                        Quantity = 1000,
                        SoldDate = null,
                        CreatedByUserId = SEED_USER_ID,
                        CreatedDate = DateTime.Now,
                        LastModifiedUserId = SEED_USER_ID,
                        LastModifiedDate = DateTime.Now,
                        IsDeleted = false,
                        IsActive = true,
                        Players = new List<Player>()
                        {
                    new Player()
                    {
                        CreatedDate = DateTime.Now, LastModifiedUserId = SEED_USER_ID,        LastModifiedDate = DateTime.Now,
                        IsActive = true,
                        IsDeleted = false,
                        CreatedByUserId = SEED_USER_ID,
                        Description = "https://www.imdb.com/name/nm0000129/",
                        Name = "Tom Cruise"
                    }
                        }
                    }
                );

                _context.SaveChanges();
            }
        }

    }
}
