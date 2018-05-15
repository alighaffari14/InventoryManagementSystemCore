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


    [Authorize(Roles = "TransferOut")]
    public class TransferOutController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TransferOutController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TransferOut
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.TransferOut.Include(t => t.transferOrder);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: TransferOut/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transferOut = await _context.TransferOut
                    .Include(t => t.transferOrder)
                        .SingleOrDefaultAsync(m => m.transferOutId == id);
            if (transferOut == null)
            {
                return NotFound();
            }

            return View(transferOut);
        }


        // GET: TransferOut/Create
        public IActionResult Create()
        {
            ViewData["transferOrderId"] = new SelectList(_context.TransferOrder, "transferOrderId", "transferOrderId");
            ViewData["branchIdFrom"] = new SelectList(_context.Branch, "branchId", "branchName");
            ViewData["warehouseIdFrom"] = new SelectList(_context.Warehouse, "warehouseId", "warehouseName");
            ViewData["branchIdTo"] = new SelectList(_context.Branch, "branchId", "branchName");
            ViewData["warehouseIdTo"] = new SelectList(_context.Warehouse, "warehouseId", "warehouseName");
            return View();
        }




        // POST: TransferOut/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("transferOutId,transferOrderId,transferOutNumber,transferOutDate,description,branchIdFrom,warehouseIdFrom,branchIdTo,warehouseIdTo,HasChild,createdAt")] TransferOut transferOut)
        {
            if (ModelState.IsValid)
            {
                _context.Add(transferOut);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Details), new { id = transferOut.transferOutId });
            }
            ViewData["transferOrderId"] = new SelectList(_context.TransferOrder, "transferOrderId", "transferOrderId", transferOut.transferOrderId);
            return View(transferOut);
        }

        // GET: TransferOut/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transferOut = await _context.TransferOut.SingleOrDefaultAsync(m => m.transferOutId == id);
            if (transferOut == null)
            {
                return NotFound();
            }
            ViewData["transferOrderId"] = new SelectList(_context.TransferOrder, "transferOrderId", "transferOrderId", transferOut.transferOrderId);
            ViewData["branchIdFrom"] = new SelectList(_context.Branch, "branchId", "branchName");
            ViewData["warehouseIdFrom"] = new SelectList(_context.Warehouse, "warehouseId", "warehouseName");
            ViewData["branchIdTo"] = new SelectList(_context.Branch, "branchId", "branchName");
            ViewData["warehouseIdTo"] = new SelectList(_context.Warehouse, "warehouseId", "warehouseName");
            return View(transferOut);
        }

        // POST: TransferOut/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("transferOutId,transferOrderId,transferOutNumber,transferOutDate,description,branchIdFrom,warehouseIdFrom,branchIdTo,warehouseIdTo,HasChild,createdAt")] TransferOut transferOut)
        {
            if (id != transferOut.transferOutId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transferOut);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransferOutExists(transferOut.transferOutId))
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
            ViewData["transferOrderId"] = new SelectList(_context.TransferOrder, "transferOrderId", "transferOrderId", transferOut.transferOrderId);
            return View(transferOut);
        }

        // GET: TransferOut/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transferOut = await _context.TransferOut
                    .Include(t => t.transferOrder)
                    .SingleOrDefaultAsync(m => m.transferOutId == id);
            if (transferOut == null)
            {
                return NotFound();
            }

            return View(transferOut);
        }




        // POST: TransferOut/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var transferOut = await _context.TransferOut.SingleOrDefaultAsync(m => m.transferOutId == id);
            try
            {
                _context.TransferOut.Remove(transferOut);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                ViewData["StatusMessage"] = "Error. Calm Down ^_^ and please contact your SysAdmin with this message: " + ex;
                return View(transferOut);
            }
            
        }

        private bool TransferOutExists(string id)
        {
            return _context.TransferOut.Any(e => e.transferOutId == id);
        }

    }
}





namespace netcore.MVC
{
    public static partial class Pages
    {
        public static class TransferOut
        {
            public const string Controller = "TransferOut";
            public const string Action = "Index";
            public const string Role = "TransferOut";
            public const string Url = "/TransferOut/Index";
            public const string Name = "TransferOut";
        }
    }
}
namespace netcore.Models
{
    public partial class ApplicationUser
    {
        [Display(Name = "TransferOut")]
        public bool TransferOutRole { get; set; } = false;
    }
}



