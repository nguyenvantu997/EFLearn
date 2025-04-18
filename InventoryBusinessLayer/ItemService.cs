﻿using AutoMapper;
using EFCore_Library;
using InventoryDatabaseLayer;
using InventoryModels;
using InventoryModels.DTOs;

namespace InventoryBusinessLayer
{
    public class ItemService : IItemService
    {
        private readonly IItemRepos _dbRepo;
        private readonly IMapper _mapper;
        public ItemService(InventoryManageDbContext dbContext, IMapper mapper)
        {
            _mapper = mapper;
            _dbRepo = new ItemRepos(dbContext, mapper);
        }
        public ItemService(IItemRepos dbRepo, IMapper mapper)
        {
            _dbRepo = dbRepo;
            _mapper = mapper;
        }

        public List<ItemDto> GetItems() { return _mapper.Map<List<ItemDto>>(_dbRepo.GetItems()); }
        public List<ItemDto> GetItemsByDateRange(DateTime minDateValue, DateTime maxDateValue) { return _dbRepo.GetItemsByDateRange(minDateValue, maxDateValue); }
        public List<GetItemsForListingDto> GetItemsForListingFromProcedure() { return _dbRepo.GetItemsForListingFromProcedure(); }
        public List<GetItemsTotalValueDto> GetItemsTotalValues(bool isActive) { return _dbRepo.GetItemsTotalValues(isActive); }
        public string GetAllItemsPipeDelimitedString() { var items = GetItems(); return string.Join('|', items); }
        public List<FullItemDetailDto> GetItemsWithGenresAndCategories() { return _dbRepo.GetItemsWithGenresAndCategories(); }

        public int UpsertItem(CreateOrUpdateItemDto item)
        {
            if (item.CategoryId <= 0)
            {
                throw new ArgumentException("Please set the category id before insert or update");
            }
            return _dbRepo.UpsertItem(_mapper.Map<Item>(item));
        }

        public void UpsertItems(List<CreateOrUpdateItemDto> items)
        {
            try
            {
                _dbRepo.UpsertItems(_mapper.Map<List<Item>>(items));
            }
            catch (Exception ex)
            {
                //TODO: better logging/not squelching
                Console.WriteLine($"The transaction has failed: {ex.Message}");
            }
        }

        public void DeleteItem(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Please set a valid item id before deleting");
            }
            _dbRepo.DeleteItem(id);
        }

        public void DeleteItems(List<int> itemIds)
        {
            try { _dbRepo.DeleteItems(itemIds); }
            catch (Exception ex)
            { //TODO: better logging/not squelching
                Console.WriteLine($"The transaction has failed: {ex.Message}");
            }
        }
    }
}