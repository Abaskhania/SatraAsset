using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SatraAsset.Data;
using SatraAsset.Model;
using System.Globalization;

namespace SatraAsset.Pages.Assets
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDBContext _context;

        public CreateModel(ApplicationDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Asset Asset { get; set; } = new();
        [BindProperty]
        public Dictionary<int, string?> PropertyValues { get; set; }
        = new();

        public SelectList AssetCategories { get; set; }
        public SelectList AssetLocations { get; set; }
        public void OnGet()
        {
            PersianCalendar pc = new PersianCalendar();
            DateTime CurrentDate = DateTime.Now;
            Asset.PurchaseAt =EnglishDigitsToPersian( $"{pc.GetYear(CurrentDate)}/{pc.GetMonth(CurrentDate):00}/{pc.GetDayOfMonth(CurrentDate):00}");
            AssetCategories = new SelectList(_context.Categories.OrderBy(t=>t.Name), nameof(Category.Id), nameof(Category.Name));
            AssetLocations = new SelectList(_context.ServiceLocations.OrderBy(s=>s.ParentID).ThenBy(s=>s.Id), nameof(ServiceLocation.Id), nameof(ServiceLocation.Name));          
        }

        public async Task<IActionResult> OnGetPropertiesAsync(int categoryid)
        {
            IList<AssetProperty> props= _context.AssetProperties.Where(t => t.CategoryId == categoryid).ToList();
            return Partial("_PartialAssetProperty", props);
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if(!ModelState.IsValid)
            {
                AssetCategories = new SelectList(_context.Categories, nameof(Category.Id), nameof(Category.Name),Asset.CategoryID);
                AssetLocations = new SelectList(_context.ServiceLocations.OrderBy(s => s.ParentID).ThenBy(s => s.Id), nameof(ServiceLocation.Id), nameof(ServiceLocation.Name),Asset.LocationID);

                return Page();
            }

            PersianCalendar pc = new PersianCalendar();
            DateTime CurrentDate = DateTime.Now;
            Asset.CreatedAt = $"{pc.GetYear(CurrentDate)}/{pc.GetMonth(CurrentDate):00}/{pc.GetDayOfMonth(CurrentDate):00}" + $" {pc.GetHour(CurrentDate):00}:{pc.GetMinute(CurrentDate):00}";
            Asset.PurchaseAt =  PersianDigitsToEnglish(Asset.PurchaseAt);
            Asset.WarrantyExpiryAt = PersianDigitsToEnglish(Asset.WarrantyExpiryAt);


            _context.Assets.Add(Asset);
            await _context.SaveChangesAsync();
            foreach (var item in PropertyValues)
            {
                if(item.Value!=null)
                    Asset.AssetProperties.Add(new AssetPropertyValue{AssetPropertyId=item.Key,Value= PersianDigitsToEnglish(item.Value) });
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
        private static string EnglishDigitsToPersian(string input)
        {
            return input
                .Replace('0','۰' )
                .Replace('1','۱' )
                .Replace('2','۲' )
                .Replace('3','۳' )
                .Replace('4','۴' )
                .Replace('5','۵' )
                .Replace('6','۶' )
                .Replace('7','۷' )
                .Replace('8','۸' )
                .Replace('9','۹' );
        }
    }
}
