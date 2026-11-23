using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using wt_lab12_12auth_validation_pravotorova.Data;
using wt_lab12_12auth_validation_pravotorova.Models;

namespace wt_lab12_12auth_validation_pravotorova.Pages;

public class RegisterModel : PageModel
{
    private readonly AppDbContext _context;

    public RegisterModel(AppDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public User NewUser { get; set; } = new();

    public void OnGet() { }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();

        // Проверяем, нет ли уже такого логина
        var existing = await _context.Users.FirstOrDefaultAsync(u => u.Login == NewUser.Login);
        if (existing != null)
        {
            ModelState.AddModelError("NewUser.Login", "Этот логин уже занят");
            return Page();
        }

        NewUser.RegisteredAt = DateTime.UtcNow; // Задаем дату регистрации
        _context.Users.Add(NewUser);
        await _context.SaveChangesAsync();

        // Автоматически входим после регистрации
        HttpContext.Session.SetString("UserLogin", NewUser.Login);
        return RedirectToPage("/Profile");
    }
}
