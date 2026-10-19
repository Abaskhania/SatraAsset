using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SatraAsset.Data;
using SatraAsset.Model;

namespace SatraAsset.Pages_Properties
{
    public class DetailsModel : PageModel
    {
        private readonly SatraAsset.Data.ApplicationDBContext _context;

        public DetailsModel(SatraAsset.Data.ApplicationDBContext context)
        {
            _context = context;
        }

        public AssetProperty AssetProperty { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assetproperty = await _context.AssetProperties.FirstOrDefaultAsync(m => m.Id == id);

            if (assetproperty is not null)
            {
                AssetProperty = assetproperty;

                return Page();
            }

            return NotFound();
        }
    }
}
