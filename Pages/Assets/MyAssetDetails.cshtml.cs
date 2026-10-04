using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SatraAsset.Data;
using SatraAsset.Model;
using System.Globalization;

namespace SatraAsset.Pages.Assets
{
    [Authorize(Roles ="User,Admin")]
    public class MyAssetDetailsModel : PageModel
    {
        private readonly ApplicationDBContext _context;

        public MyAssetDetailsModel(ApplicationDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Asset Asset { get; set; } = new();
        [BindProperty]
        public bool IsVerif { get; set; } = new();
        [BindProperty]
        public string Pers_NCD { get; set; }
        public SelectList AssetCategories { get; set; }
        public SelectList AssetLocations { get; set; }
        public IActionResult OnGet(int id)
        {

            this.Asset = _context.Assets
                .Include(t => t.Category)
                .Include(t => t.Location)
                .Include(t => t.AssetPersonels)
                .ThenInclude(t => t.Personel)
                .FirstOrDefault(t => t.Id == id)!;
            int persID = _context.SatraUser.FirstOrDefault(t => t.Username == User.Identity!.Name)!.PersonelID!.Value;
            AssetRecipient ar = this.Asset.AssetPersonels.FirstOrDefault(t => t.ReturnAt == "" && t.PersonelId==persID)!;
            
            if(ar==null)
            {
                return RedirectToPage("../Account/AccessDenied");
            }
            this.IsVerif = (ar.VerifUser!="");
            AssetCategories = new SelectList(_context.Categories, nameof(Category.Id), nameof(Category.Name));
            AssetLocations = new SelectList(_context.ServiceLocations.OrderBy(s => s.ParentID).ThenBy(s => s.Id), nameof(ServiceLocation.Id), nameof(ServiceLocation.Name));
            return Page();
        }
        public void OnPost()
        {

            Personel pers = _context.Personels.FirstOrDefault(t => t.NCD == Pers_NCD || t.ClerkID.ToString() == Pers_NCD)!;
            if (pers != null)
            {
                PersianCalendar pc = new PersianCalendar();
                DateTime CurrentDate = DateTime.Now;
                string CurrentDateSH = $"{pc.GetYear(CurrentDate)}/{pc.GetMonth(CurrentDate):00}/{pc.GetDayOfMonth(CurrentDate):00}";


                this.Asset = _context.Assets.Include(t => t.AssetPersonels).ThenInclude(t => t.Personel).FirstOrDefault(t => t.Id == this.Asset.Id)!;
                AssetRecipient ar = this.Asset.AssetPersonels.FirstOrDefault(t => t.ReturnAt == null)!;
                if (ar != null)
                    ar.ReturnAt = CurrentDateSH;
                this.Asset.AssetPersonels.Add(new AssetRecipient { PersonelId = pers.ID, CreateUser = User.Identity!.Name!, CreateAt = CurrentDateSH });
                _context.SaveChanges();
                AssetCategories = new SelectList(_context.Categories, nameof(Category.Id), nameof(Category.Name));
                AssetLocations = new SelectList(_context.ServiceLocations.OrderBy(s => s.ParentID).ThenBy(s => s.Id), nameof(ServiceLocation.Id), nameof(ServiceLocation.Name));
            }

        }
    }
}
