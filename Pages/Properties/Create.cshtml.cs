using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SatraAsset.Data;
using SatraAsset.Model;
using System.Globalization;

namespace SatraAsset.Pages.Properties
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDBContext _context;

        public CreateModel(ApplicationDBContext context)
        {
            _context = context;
        }

        public SelectList AssetCategories { get; set; }
        [BindProperty]
        public AssetProperty AssetProperty { get; set; } = new();
        public void OnGet()
        {
            AssetCategories = new SelectList(_context.Categories.OrderBy(t => t.Name), nameof(Category.Id), nameof(Category.Name));
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                AssetCategories = new SelectList(_context.Categories, nameof(Category.Id), nameof(Category.Name));
                //AssetLocations = new SelectList(_context.ServiceLocations.OrderBy(s => s.ParentID).ThenBy(s => s.Id), nameof(ServiceLocation.Id), nameof(ServiceLocation.Name), Asset.LocationID);

                return Page();
            }    

            _context.AssetProperties.Add(AssetProperty);
            await _context.SaveChangesAsync();            

            return RedirectToPage("./Index");
        }
    }
}
