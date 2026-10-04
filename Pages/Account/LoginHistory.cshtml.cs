using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SatraAsset.Data;
using SatraAsset.Model;

namespace SatraAsset.Pages.Account
{
    [Authorize(Roles = "Admin")]
    public class LoginHistoryModel : PageModel
    {
        private readonly ApplicationDBContext _context;

        public LoginHistoryModel(ApplicationDBContext context)
        {
            _context = context;
        }
        public IList<UserLoginLog> UserLoginHistory { get; set; }
        public void OnGet(int id)
        {
            SatraUser _user = _context.SatraUser.AsNoTracking().Where(u=>u.ID==id).FirstOrDefault();
            this.UserLoginHistory = _context.UserLoginLogs.Where(l => l.UserId == _user.Username).OrderByDescending(t=>t.LoginTime).ToList();
        }
    }
}
