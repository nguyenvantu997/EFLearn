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
    public class DetailsModel : PageModel
    {
        private readonly EFCore_Activity0601.Data.ApplicationDbContext _context;

        public DetailsModel(EFCore_Activity0601.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Item Item { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Item = await _context.Items
                .Include(i => i.Category).FirstOrDefaultAsync(m => m.Id == id);

            if (Item == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
