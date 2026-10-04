using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SatraAsset.Data;
using SatraAsset.Model;

namespace SatraAsset.Pages.Properties
{
    public class IndexModel : PageModel
    {

        
        private readonly ApplicationDBContext _context;

        public IndexModel(ApplicationDBContext context)
        {
            _context = context;
        }
        public SelectList AssetCategories { get; set; }
        
        public int pageSize { get; set; } = 10;
        public int totalcount { get; set; }
        public int currentPage { get; set; }
        public int TotalPages { get; set; }
        public List<AssetProperty> AssetProperties { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public string? Search { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? CategoryID { get; set; }


        public async Task OnGetAsync(int pagenumber = 1)
        {
            AssetCategories = new SelectList(_context.Categories.OrderBy(t => t.Name), nameof(Category.Id), nameof(Category.Name));
            var query = _context.AssetProperties
                .Include(s => s.Category)                
                .AsNoTracking()
                .AsQueryable();

            // Search
            if (!string.IsNullOrWhiteSpace(Search))
            {
                Search = Search.Trim();

                query = query.Where(x =>
                    x.Name.Contains(Search));
                   
            }

            //query = query.Where(t => t.AssetProperties.Any(x => x.Value.Contains(Search)));
            // Status filter
            if (CategoryID.HasValue)
            {
                query = query.Where(x => x.CategoryId == CategoryID);
            }
            currentPage = pagenumber;
            totalcount = await query.CountAsync();
            TotalPages = (int)Math.Ceiling(totalcount / (double)pageSize);
            AssetProperties = await query
                .OrderBy(x => x.Category.Name)
                .ThenBy(x=>x.SortOrder)
                .Skip((pagenumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            /*Asset t = _context.Assets.Include(s=>s.AssetPersonels).ThenInclude(x => x.Personel).FirstOrDefault();
            AssetRecipient a = new AssetRecipient();
            a.AssetId = t.Id;
            a.PersonelId = 1;
            a.CreateAt = "1405/12/03";
            a.CreateUser = "Ali";
            t.AssetPersonels.Add(a);
            await _context.SaveChangesAsync();*/
        }
    }
}
