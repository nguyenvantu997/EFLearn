using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EFCore_Activity0601.Data;
using InventoryModels;

namespace EFCore_Activity0601.Pages.Items
{
    public class IndexModel : PageModel
    {
        private readonly EFCore_Activity0601.Data.ApplicationDbContext _context;

        public IndexModel(EFCore_Activity0601.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Item> Item { get;set; }

        public async Task OnGetAsync()
        {
            Item = await _context.Items
                .Include(i => i.Category).ToListAsync();
        }
    }
}
