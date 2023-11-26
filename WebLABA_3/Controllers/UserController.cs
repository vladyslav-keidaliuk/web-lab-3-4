using Microsoft.AspNetCore.Mvc;
using WebLABA_3.Models;

namespace WebLABA_3.Controllers;

public class UserController : Controller
{
    private readonly Laba3DbContext _context;
    
    public UserController(Laba3DbContext context)
    {
        _context = context;
           
    }
    // public IActionResult Index()
    // {
    //     var model = _context.Users.ToList();
    //
    //     return View(model);
    // }
    
    [HttpGet]
    public IActionResult Index(string searchString, int? userId)
    {
        var users = _context.Users.AsQueryable();

        if (!string.IsNullOrEmpty(searchString))
        {
            users = users.Where(u => u.Name.Contains(searchString));
        }

        if (userId.HasValue)
        {
            users = users.Where(u => u.UserId == userId.Value);
        }

        return View(users.ToList());
    }

    public IActionResult Create()
    {
        ViewBag.Roles = _context.Roles.ToList(); 
        var user = new User();
        return View(user);
    }
    
      [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Roles = _context.Roles.ToList(); 
            return View(user);
        }
        

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = _context.Users.Find(id);

            if (user == null)
            {
                return NotFound();
            }
            ViewBag.Roles = _context.Roles.ToList();
            return View(user);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("UserId,Name,RoleId")] User user)
        {
            if (id != user.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var role = _context.Roles.FirstOrDefault(r => r.RoleId == user.RoleId);
                if (role == null)
                {
                    ModelState.AddModelError("RoleId", "Invalid Role selected.");
                    ViewBag.Roles = _context.Roles.ToList();
                    return View(user);
                }
                user.Role = role;
                _context.Update(user);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Roles = _context.Roles.ToList();
            return View(user);
        }



        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = _context.Users.FirstOrDefault(m => m.UserId == id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var user = _context.Users.Find(id);

            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    
}