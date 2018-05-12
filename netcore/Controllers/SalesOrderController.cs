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
using netcore.Models;

namespace netcore.Controllers
{


    [Authorize(Roles = "SalesOrder")]
    public class SalesOrderController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SalesOrderController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SalesOrder
        public async Task<IActionResult> Index()
        {
                    return View(await _context.SalesOrder.ToListAsync());
        }        

    // GET: SalesOrder/Details/5
    public async Task<IActionResult> Details(string id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var salesOrder = await _context.SalesOrder
                    .SingleOrDefaultAsync(m => m.salesOrderId == id);
        if (salesOrder == null)
        {
            return NotFound();
        }

        return View(salesOrder);
    }


            // GET: SalesOrder/Create
            public IActionResult Create()
            {
            return View();
            }




    // POST: SalesOrder/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("salesOrderId,salesOrderName,HasChild,createdAt")] SalesOrder salesOrder)
    {
        if (ModelState.IsValid)
        {
            _context.Add(salesOrder);
            await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
        }
        return View(salesOrder);
    }

    // GET: SalesOrder/Edit/5
    public async Task<IActionResult> Edit(string id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var salesOrder = await _context.SalesOrder.SingleOrDefaultAsync(m => m.salesOrderId == id);
        if (salesOrder == null)
        {
            return NotFound();
        }
        return View(salesOrder);
    }

    // POST: SalesOrder/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(string id, [Bind("salesOrderId,salesOrderName,HasChild,createdAt")] SalesOrder salesOrder)
    {
        if (id != salesOrder.salesOrderId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(salesOrder);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SalesOrderExists(salesOrder.salesOrderId))
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
        return View(salesOrder);
    }

    // GET: SalesOrder/Delete/5
    public async Task<IActionResult> Delete(string id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var salesOrder = await _context.SalesOrder
                .SingleOrDefaultAsync(m => m.salesOrderId == id);
        if (salesOrder == null)
        {
            return NotFound();
        }

        return View(salesOrder);
    }




    // POST: SalesOrder/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(string id)
    {
        var salesOrder = await _context.SalesOrder.SingleOrDefaultAsync(m => m.salesOrderId == id);
            _context.SalesOrder.Remove(salesOrder);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool SalesOrderExists(string id)
    {
        return _context.SalesOrder.Any(e => e.salesOrderId == id);
    }

  }
}





namespace netcore.MVC
{
  public static partial class Pages
  {
      public static class SalesOrder
      {
          public const string Controller = "SalesOrder";
          public const string Action = "Index";
          public const string Role = "SalesOrder";
          public const string Url = "/SalesOrder/Index";
          public const string Name = "SalesOrder";
      }
  }
}
namespace netcore.Models
{
  public partial class ApplicationUser
  {
      [Display(Name = "SalesOrder")]
      public bool SalesOrderRole { get; set; } = false;
  }
}



