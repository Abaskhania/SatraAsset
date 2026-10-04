using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SatraAsset.Data;
using SatraAsset.Model;

namespace SatraAsset.Pages.Assets
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {

        private readonly ApplicationDBContext _context;

        public IndexModel(ApplicationDBContext context)
        {
            _context = context;
        }
        public int pageSize { get; set; } = 10;
        public int totalcount { get; set; }
        public int currentPage { get; set; }
        public int TotalPages { get; set; }
        public List<Asset> Assets { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public string? Search { get; set; }

        [BindProperty(SupportsGet = true)]
        public AssetStatus? Status { get; set; }


        public async Task OnGetAsync(int pagenumber = 1)
        {
            var query = _context.Assets
                .Include(s=>s.Category)
                .Include(s=>s.Location)
                .AsNoTracking()
                .AsQueryable();

            // Search
            if (!string.IsNullOrWhiteSpace(Search))
            {
                Search = Search.Trim();

                query = query.Where(x =>
                    x.Name.Contains(Search) ||
                    x.AssetCode.Contains(Search) ||
                    (x.SerialNumber != null &&
                     x.SerialNumber.Contains(Search)) ||
                     x.AssetProperties.Any(t=>t.Value.Contains(Search))
                     );
            }

            //query = query.Where(t => t.AssetProperties.Any(x => x.Value.Contains(Search)));
            // Status filter
            if (Status.HasValue)
            {
                query = query.Where(x => x.Status == Status.Value);
            }
            currentPage = pagenumber;
            totalcount = await query.CountAsync();
            TotalPages = (int)Math.Ceiling(totalcount / (double)pageSize);
            Assets = await query
                .OrderByDescending(x => x.Id)
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
