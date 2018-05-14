using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;

using netcore.Data;
using netcore.Models.Invent;

namespace netcore.Controllers.Invent
{
    //test

    [Authorize(Roles = "Branch")]
    public class BranchController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BranchController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Branch
        public async Task<IActionResult> Index()
        {
                    return View(await _context.Branch.ToListAsync());
        }        

    // GET: Branch/Details/5
    public async Task<IActionResult> Details(string id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var branch = await _context.Branch
                    .SingleOrDefaultAsync(m => m.branchId == id);
        if (branch == null)
        {
            return NotFound();
        }

        return View(branch);
    }


    // GET: Branch/Create
    public IActionResult Create()
    {
    return View();
    }




    // POST: Branch/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("branchId,branchName,description,street1,street2,city,province,country,createdAt")] Branch branch)
    {
        if (ModelState.IsValid)
        {
            _context.Add(branch);
            await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
        }
        return View(branch);
    }

    // GET: Branch/Edit/5
    public async Task<IActionResult> Edit(string id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var branch = await _context.Branch.SingleOrDefaultAsync(m => m.branchId == id);
        if (branch == null)
        {
            return NotFound();
        }
        return View(branch);
    }

    // POST: Branch/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(string id, [Bind("branchId,branchName,description,street1,street2,city,province,country,createdAt")] Branch branch)
    {
        if (id != branch.branchId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(branch);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BranchExists(branch.branchId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        return RedirectToAction(nameof(Index));
        }
        return View(branch);
    }

    // GET: Branch/Delete/5
    public async Task<IActionResult> Delete(string id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var branch = await _context.Branch
                .SingleOrDefaultAsync(m => m.branchId == id);
        if (branch == null)
        {
            return NotFound();
        }

        return View(branch);
    }




    // POST: Branch/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(string id)
    {
        var branch = await _context.Branch.SingleOrDefaultAsync(m => m.branchId == id);
            _context.Branch.Remove(branch);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool BranchExists(string id)
    {
        return _context.Branch.Any(e => e.branchId == id);
    }

  }
}





namespace netcore.MVC
{
  public static partial class Pages
  {
      public static class Branch
      {
          public const string Controller = "Branch";
          public const string Action = "Index";
          public const string Role = "Branch";
          public const string Url = "/Branch/Index";
          public const string Name = "Branch";
      }
  }
}
namespace netcore.Models
{
  public partial class ApplicationUser
  {
      [Display(Name = "Branch")]
      public bool BranchRole { get; set; } = false;
  }
}



