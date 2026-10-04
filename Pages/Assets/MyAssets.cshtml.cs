using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SatraAsset.Data;
using SatraAsset.Model;

namespace SatraAsset.Pages.Assets
{
    [Authorize]
    public class MyAssetsModel : PageModel
    {
        private readonly ApplicationDBContext _context;

        public MyAssetsModel(ApplicationDBContext context)
        {
            _context = context;
        }

        public List<Asset> Assets { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public string? Search { get; set; }

        [BindProperty(SupportsGet = true)]
        public AssetStatus? Status { get; set; }

        public async Task OnGetAsync()
        {
            var query = _context.Assets.Include(s=>s.Category).Include(s=>s.Location)
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
                     x.SerialNumber.Contains(Search)));
            }

            // Status filter
            if (Status.HasValue)
            {
                query = query.Where(x => x.Status == Status.Value);
            }
            int personelID = _context.SatraUser.Include(t=>t.Personel).FirstOrDefault(t=>t.Username==User.Identity.Name).Personel.ID;
            query = query.Where(x => x.AssetPersonels.Any(x=>x.PersonelId==personelID && x.ReturnAt=="" ) );

            Assets = await query
                .OrderByDescending(x => x.Id)
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
