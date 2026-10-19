using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SatraAsset.Data;
using SatraAsset.Model;

namespace SatraAsset.Pages.Properties
{
    public class EditModel : PageModel
    {
        private readonly SatraAsset.Data.ApplicationDBContext _context;

        public EditModel(SatraAsset.Data.ApplicationDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public AssetProperty AssetProperty { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assetproperty =  await _context.AssetProperties.FirstOrDefaultAsync(m => m.Id == id);
            if (assetproperty == null)
            {
                return NotFound();
            }
            AssetProperty = assetproperty;
           ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(AssetProperty).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AssetPropertyExists(AssetProperty.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool AssetPropertyExists(int id)
        {
            return _context.AssetProperties.Any(e => e.Id == id);
        }
    }
}
