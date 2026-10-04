using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SatraAsset.Data;
using SatraAsset.Model;
using System.Globalization;

namespace SatraAsset.Pages.Assets
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDBContext _context;

        public EditModel(ApplicationDBContext context)
        {
            _context = context;
        }
        [BindProperty(SupportsGet = true)]
        public int? id { get; set; }
        [BindProperty]
        public Asset Asset { get; set; } = new();
        public SelectList AssetCategories { get; set; }
        public SelectList AssetLocations { get; set; }

        [BindProperty]
        public Dictionary<int, string?>? PropertyValues { get; set; }
        
        public void OnGet()
        {
            PersianCalendar pc = new PersianCalendar();
            DateTime CurrentDate = DateTime.Now;
            this.Asset = _context.Assets.Include(t => t.AssetProperties).ThenInclude(t => t.AssetProperty).FirstOrDefault(t=>t.Id==this.id)!;
            Asset.PurchaseAt = EnglishDigitsToPersian(Asset.PurchaseAt);
            Asset.WarrantyExpiryAt = EnglishDigitsToPersian(Asset.WarrantyExpiryAt);
            AssetCategories = new SelectList(_context.Categories, nameof(Category.Id), nameof(Category.Name));
            AssetLocations = new SelectList(_context.ServiceLocations.OrderBy(s => s.ParentID).ThenBy(s => s.Id), nameof(ServiceLocation.Id), nameof(ServiceLocation.Name));
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                AssetCategories = new SelectList(_context.Categories, nameof(Category.Id), nameof(Category.Name), Asset.CategoryID);
                AssetLocations = new SelectList(_context.ServiceLocations.OrderBy(s => s.ParentID).ThenBy(s => s.Id), nameof(ServiceLocation.Id), nameof(ServiceLocation.Name), Asset.LocationID);

                return Page();
            }
            
            PersianCalendar pc = new PersianCalendar();
            DateTime CurrentDate = DateTime.Now;
            Asset.UpdatedAt = $"{pc.GetYear(CurrentDate)}/{pc.GetMonth(CurrentDate):00}/{pc.GetDayOfMonth(CurrentDate):00}" + $" {pc.GetHour(CurrentDate):00}:{pc.GetMinute(CurrentDate):00}";
            Asset.PurchaseAt = PersianDigitsToEnglish(Asset.PurchaseAt);
            Asset.WarrantyExpiryAt = PersianDigitsToEnglish(Asset.WarrantyExpiryAt);
            //Asset AssetFromDB = _context.Assets.Include(t => t.AssetProperties).FirstOrDefault(t=>t.Id==Asset.Id)!;


            _context.Attach(Asset).State = EntityState.Modified;
            Asset.AssetProperties = _context.AssetPropertyValues.Where(t=>t.AssetId==Asset.Id).ToList();
            _context.AssetPropertyValues.RemoveRange(Asset.AssetProperties);

            foreach (var item in PropertyValues)
            {
                if(item.Value!=null)
                    Asset.AssetProperties.Add(new AssetPropertyValue { AssetPropertyId = item.Key, Value = PersianDigitsToEnglish(item.Value) });
            }
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
        private static string PersianDigitsToEnglish(string input)
        {
            return input
                .Replace('۰', '0')
                .Replace('۱', '1')
                .Replace('۲', '2')
                .Replace('۳', '3')
                .Replace('۴', '4')
                .Replace('۵', '5')
                .Replace('۶', '6')
                .Replace('۷', '7')
                .Replace('۸', '8')
                .Replace('۹', '9');
        }
        public string EnglishDigitsToPersian(string input)
        {
            return input
                .Replace('0', '۰')
                .Replace('1', '۱')
                .Replace('2', '۲')
                .Replace('3', '۳')
                .Replace('4', '۴')
                .Replace('5', '۵')
                .Replace('6', '۶')
                .Replace('7', '۷')
                .Replace('8', '۸')
                .Replace('9', '۹');
        }
    }
}
