using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore; 

namespace LibraryManagement.Controllers
{
    public class LibrariesController : Controller
    {
        //private readonly LibraryDbContext _context;

        //public LibrariesController(LibraryDbContext context)
        //{
        //    _context = context;
        //}

        //// GET: Libraries
        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.Librarys.ToListAsync());
        //}

        //// GET: Libraries/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var library = await _context.Librarys
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (library == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(library);
        //}

        //// GET: Libraries/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Libraries/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,Name,Address,Thana")] Library library)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(library);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(library);
        //}

        //// GET: Libraries/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var library = await _context.Librarys.FindAsync(id);
        //    if (library == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(library);
        //}

        //// POST: Libraries/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Address,Thana")] Library library)
        //{
        //    if (id != library.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(library);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!LibraryExists(library.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(library);
        //}

        //// GET: Libraries/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var library = await _context.Librarys
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (library == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(library);
        //}

        //// POST: Libraries/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var library = await _context.Librarys.FindAsync(id);
        //    if (library != null)
        //    {
        //        _context.Librarys.Remove(library);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool LibraryExists(int id)
        //{
        //    return _context.Librarys.Any(e => e.Id == id);
        //}
    }
}
