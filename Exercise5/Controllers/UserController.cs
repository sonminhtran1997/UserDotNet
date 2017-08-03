using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Exercise5.Models;
using Microsoft.EntityFrameworkCore;

namespace Exercise5.Controllers
{

    public class UserController : Controller
    {
        //private readonly UserContext _context;

        //public UserController(UserContext context)
        //{
        //    _context = context;
        //}
        private readonly UserContext _context;

        public UserController(UserContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }

        // GET: Students/Edit/5
        //public async Task<IActionResult> CreateOrEdit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return View();
        //    }
        //    else
        //    {
        //        var user = await _context.Users.SingleOrDefaultAsync(m => m.id == id);
        //        if (user == null)
        //        {
        //            return NotFound();
        //        }
        //        return View(user);
        //    }

        //}
        public async Task<IActionResult> CreateOrEdit(int? id)
        {
            if (id == null)
            {
                return View();
            }
            else
            {
                var user = await _context.Users.SingleOrDefaultAsync(m => m.id == id);
                if (user == null)
                {
                    return NotFound();
                }
                return View(user);
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOrEdit(int? id, [Bind("id,firstName,lastName,description,gender,role")] User user, String[] favs)
        {
            var s = String.Join(",", favs);
            user.favs = s;
            if (id == null)
            {

                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");

            }
            else
            {
                if (id != user.id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(user);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!UserExists(user.id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction("Index");
                }
                return View(user);

            }
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            else
            {
                var student = await _context.Users.SingleOrDefaultAsync(m => m.id == id);
                if (student == null)
                {
                    return NotFound();
                }
                else
                {
                    _context.Users.Remove(student);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }

            }


        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.id == id);
        }
    }
}