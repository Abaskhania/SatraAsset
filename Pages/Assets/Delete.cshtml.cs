using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SatraAsset.Data;
using SatraAsset.Model;

namespace SatraAsset.Pages.Assets
{
    public class DeleteModel : PageModel
    {
        private readonly SatraAsset.Data.ApplicationDBContext _context;

        public DeleteModel(SatraAsset.Data.ApplicationDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Asset Asset { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asset = await _context.Assets.Include(t=>t.Category).Include(s => s.Location).FirstOrDefaultAsync(m => m.Id == id);

            if (asset is not null)
            {
                Asset = asset;

                return Page();
            }

            return NotFound();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asset = await _context.Assets.FindAsync(id);
            asset.IsValid = false;
            if (asset != null)
            {
                //_context.Attach(Asset).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
