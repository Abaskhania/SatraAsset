using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SatraAsset.Data;
using SatraAsset.Model;
using System.Globalization;
using Microsoft.EntityFrameworkCore;

namespace SatraAsset.Pages.Assets
{
    public class RecipeintIsOKModel : PageModel
    {

        private readonly ApplicationDBContext _context;

        public RecipeintIsOKModel(ApplicationDBContext context)
        {
            _context = context;
        }
        public IActionResult OnGet(int id,bool ok)
        {

            int persID = _context.SatraUser.FirstOrDefault(u => u.Username == User.Identity.Name).PersonelID.Value;
            Asset asset = _context.Assets.Include(a=>a.AssetPersonels).FirstOrDefault(t => t.Id == id);
            if (asset != null)
            {
                AssetRecipient ar = asset.AssetPersonels.OrderByDescending(t=>t.CreateAt).FirstOrDefault(p => p.PersonelId == persID && p.ReturnAt=="");
                if (ar != null)
                {
                    PersianCalendar pc = new PersianCalendar();
                    DateTime CurrentDate = DateTime.Now;
                    string currentdate = $"{pc.GetYear(CurrentDate)}/{pc.GetMonth(CurrentDate):00}/{pc.GetDayOfMonth(CurrentDate):00}";
                    ar.IsVerif = ok;
                    ar.VerifUser = User.Identity.Name;
                    ar.VerifDate = currentdate;
                    _context.SaveChanges();
                }
            }
            return RedirectToPage("MyAssets");

        }
    }
}
