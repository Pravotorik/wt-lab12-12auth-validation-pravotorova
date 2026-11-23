using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using wt_lab12_12auth_validation_pravotorova.Data;

namespace wt_lab12_12auth_validation_pravotorova.Pages;

public class ProfileModel : PageModel
{
    private readonly AppDbContext _context;

    public ProfileModel(AppDbContext context)
    {
        _context = context;
    }

    public string? UserLogin { get; private set; }
    public string? UserName { get; private set; }
    public string? UserEmail { get; private set; }
    public DateTime RegisteredAt { get; private set; }

    public IActionResult OnGet()
    {
        UserLogin = HttpContext.Session.GetString("UserLogin");
        if (UserLogin == null) return RedirectToPage("/Login");

        var user = _context.Users.FirstOrDefault(u => u.Login == UserLogin);
        if (user != null)
        {
            UserName = user.FullName;
            UserEmail = user.Email;
            RegisteredAt = user.RegisteredAt;
        }

        return Page();
    }

    public IActionResult OnPost()
    {
        HttpContext.Session.Clear();
        return RedirectToPage("/Login");
    }
}
