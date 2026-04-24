using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zajezdnia.Data;
using Zajezdnia.Models;
 
namespace Zajezdnia.Controllers;
 
public class ZajezdnieController(AppDbContext context) : Controller
{
    public async Task<IActionResult> Index(string? szukaj, string? miasto)
    {
        var query = context.Zajezdnie.Include(z => z.Autobusy).AsQueryable();
 
        if (!string.IsNullOrWhiteSpace(szukaj))
            query = query.Where(z => z.Nazwa.Contains(szukaj) || z.Adres.Contains(szukaj));
 
        if (!string.IsNullOrWhiteSpace(miasto))
            query = query.Where(z => z.Miasto == miasto);
 
        ViewBag.Szukaj = szukaj;
        ViewBag.Miasto = miasto;
        ViewBag.Miasta = await context.Zajezdnie.Select(z => z.Miasto).Distinct().ToListAsync();
 
        return View(await query.OrderBy(z => z.Nazwa).ToListAsync());
    }
 
    public async Task<IActionResult> Details(int id)
    {
        
        var zajezdnia = await context.Zajezdnie
            .Include(z => z.Autobusy)
                .ThenInclude(a => a.Kierowcy)
            .FirstOrDefaultAsync(z => z.Id == id);
 
        if (zajezdnia == null) return NotFound();
        return View(zajezdnia);
    }

    public IActionResult Create()
    {
        if (!IsLoggedIn()) return RedirectToAction("Login", "Auth");
        return View();
    }
 
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Zajezdnia_autobusow zajezdnia)
    {
        if (!IsLoggedIn()) return RedirectToAction("Login", "Auth");
        if (ModelState.IsValid)
        {
            context.Add(zajezdnia);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(zajezdnia);
    }
 
    public async Task<IActionResult> Edit(int id)
    {
        
        if (!IsLoggedIn()) return RedirectToAction("Login", "Auth");
        var zajezdnia = await context.Zajezdnie.FindAsync(id);
        if (zajezdnia == null) return NotFound();
        return View(zajezdnia);
    }
 
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Zajezdnia_autobusow zajezdnia)
    {
        if (!IsLoggedIn()) return RedirectToAction("Login", "Auth");
        if (id != zajezdnia.Id) return NotFound();
 
        if (ModelState.IsValid)
        {
            context.Update(zajezdnia);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(zajezdnia);
    }
    
    public async Task<IActionResult> Delete(int id)
    {
        if (!IsLoggedIn()) return RedirectToAction("Login", "Auth");
        var zajezdnia = await context.Zajezdnie.FirstOrDefaultAsync(z => z.Id == id);
        if (zajezdnia == null) return NotFound();
        return View(zajezdnia);
    }
 
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (!IsLoggedIn()) return RedirectToAction("Login", "Auth");
        var zajezdnia = await context.Zajezdnie.FindAsync(id);
        if (zajezdnia != null)
        {
            context.Zajezdnie.Remove(zajezdnia);
            await context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
    
    private bool IsLoggedIn() => HttpContext.Session.GetInt32("UserId").HasValue;
}