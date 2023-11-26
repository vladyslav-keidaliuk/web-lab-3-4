using Microsoft.AspNetCore.Mvc;
using WebLABA_3.Models;

namespace WebLABA_3.Controllers;

public class RoleController : Controller
{
    private readonly Laba3DbContext _context;

    public RoleController(Laba3DbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var model = _context.Roles.ToList();
        return View(model);
    }

    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create([Bind("RoleId,Name")] Role author)
    {
        if (ModelState.IsValid)
        {
            _context.Add(author);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        return View(author);
    }
        
    public IActionResult Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var author = _context.Roles.Find(id);

        if (author == null)
        {
            return NotFound();
        }

        return View(author);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, [Bind("RoleId,Name")] Role role)
    {
        if (id != role.RoleId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            _context.Update(role);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        return View(role);
    }

        
    public IActionResult Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var author = _context.Roles.FirstOrDefault(m => m.RoleId == id);

        if (author == null)
        {
            return NotFound();
        }

        return View(author);
    }

        
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int id)
    {
        var role = _context.Roles.Find(id);

        if (role == null)
        {
            return NotFound();
        }

        _context.Roles.Remove(role);
        _context.SaveChanges();

        return RedirectToAction(nameof(Index));
    }
    
    
}