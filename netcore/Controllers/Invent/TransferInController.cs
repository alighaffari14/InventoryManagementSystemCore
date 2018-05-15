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


    [Authorize(Roles = "TransferIn")]
    public class TransferInController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TransferInController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TransferIn
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.TransferIn.Include(t => t.transferOrder);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: TransferIn/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transferIn = await _context.TransferIn
                    .Include(t => t.transferOrder)
                        .SingleOrDefaultAsync(m => m.transferInId == id);
            if (transferIn == null)
            {
                return NotFound();
            }

            return View(transferIn);
        }


        // GET: TransferIn/Create
        public IActionResult Create()
        {
            ViewData["transferOrderId"] = new SelectList(_context.TransferOrder, "transferOrderId", "transferOrderNumber");
            ViewData["branchIdFrom"] = new SelectList(_context.Branch, "branchId", "branchName");
            ViewData["warehouseIdFrom"] = new SelectList(_context.Warehouse, "warehouseId", "warehouseName");
            ViewData["branchIdTo"] = new SelectList(_context.Branch, "branchId", "branchName");
            ViewData["warehouseIdTo"] = new SelectList(_context.Warehouse, "warehouseId", "warehouseName");
            TransferIn obj = new TransferIn();
            return View(obj);
        }




        // POST: TransferIn/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("transferInId,transferOrderId,transferInNumber,transferInDate,description,branchIdFrom,warehouseIdFrom,branchIdTo,warehouseIdTo,HasChild,createdAt")] TransferIn transferIn)
        {
            if (ModelState.IsValid)
            {
                _context.Add(transferIn);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Details), new { id = transferIn.transferInId });
            }
            ViewData["transferOrderId"] = new SelectList(_context.TransferOrder, "transferOrderId", "transferOrderNumber", transferIn.transferOrderId);
            return View(transferIn);
        }

        // GET: TransferIn/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transferIn = await _context.TransferIn.SingleOrDefaultAsync(m => m.transferInId == id);
            if (transferIn == null)
            {
                return NotFound();
            }
            ViewData["transferOrderId"] = new SelectList(_context.TransferOrder, "transferOrderId", "transferOrderNumber", transferIn.transferOrderId);
            return View(transferIn);
        }

        // POST: TransferIn/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("transferInId,transferOrderId,transferInNumber,transferInDate,description,branchIdFrom,warehouseIdFrom,branchIdTo,warehouseIdTo,HasChild,createdAt")] TransferIn transferIn)
        {
            if (id != transferIn.transferInId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transferIn);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransferInExists(transferIn.transferInId))
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
            ViewData["transferOrderId"] = new SelectList(_context.TransferOrder, "transferOrderId", "transferOrderNumber", transferIn.transferOrderId);
            ViewData["branchIdFrom"] = new SelectList(_context.Branch, "branchId", "branchName");
            ViewData["warehouseIdFrom"] = new SelectList(_context.Warehouse, "warehouseId", "warehouseName");
            ViewData["branchIdTo"] = new SelectList(_context.Branch, "branchId", "branchName");
            ViewData["warehouseIdTo"] = new SelectList(_context.Warehouse, "warehouseId", "warehouseName");
            return View(transferIn);
        }

        // GET: TransferIn/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transferIn = await _context.TransferIn
                    .Include(t => t.transferOrder)
                    .SingleOrDefaultAsync(m => m.transferInId == id);
            if (transferIn == null)
            {
                return NotFound();
            }

            return View(transferIn);
        }




        // POST: TransferIn/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var transferIn = await _context.TransferIn.SingleOrDefaultAsync(m => m.transferInId == id);
            try
            {
                _context.TransferIn.Remove(transferIn);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                ViewData["StatusMessage"] = "Error. Calm Down ^_^ and please contact your SysAdmin with this message: " + ex;
                return View(transferIn);
            }
            
        }

        private bool TransferInExists(string id)
        {
            return _context.TransferIn.Any(e => e.transferInId == id);
        }

    }
}





namespace netcore.MVC
{
    public static partial class Pages
    {
        public static class TransferIn
        {
            public const string Controller = "TransferIn";
            public const string Action = "Index";
            public const string Role = "TransferIn";
            public const string Url = "/TransferIn/Index";
            public const string Name = "TransferIn";
        }
    }
}
namespace netcore.Models
{
    public partial class ApplicationUser
    {
        [Display(Name = "TransferIn")]
        public bool TransferInRole { get; set; } = false;
    }
}



