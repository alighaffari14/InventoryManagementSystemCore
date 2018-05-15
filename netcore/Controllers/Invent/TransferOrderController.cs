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


    [Authorize(Roles = "TransferOrder")]
    public class TransferOrderController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TransferOrderController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TransferOrder
        public async Task<IActionResult> Index()
        {
            return View(await _context.TransferOrder
                .Include(x => x.branchFrom)
                .Include(x => x.branchTo)
                .Include(x => x.warehouseFrom)
                .Include(x => x.warehouseTo)
                .ToListAsync());
        }

        // GET: TransferOrder/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transferOrder = await _context.TransferOrder
                        .SingleOrDefaultAsync(m => m.transferOrderId == id);
            if (transferOrder == null)
            {
                return NotFound();
            }

            return View(transferOrder);
        }


        // GET: TransferOrder/Create
        public IActionResult Create()
        {
            ViewData["branchIdFrom"] = new SelectList(_context.Branch, "branchId", "branchName");
            ViewData["warehouseIdFrom"] = new SelectList(_context.Warehouse, "warehouseId", "warehouseName");
            ViewData["branchIdTo"] = new SelectList(_context.Branch, "branchId", "branchName");
            ViewData["warehouseIdTo"] = new SelectList(_context.Warehouse, "warehouseId", "warehouseName");
            TransferOrder obj = new TransferOrder();
            return View(obj);
        }




        // POST: TransferOrder/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("transferOrderId,transferOrderNumber,transferOrderDate,description,picName,branchIdFrom,warehouseIdFrom,branchIdTo,warehouseIdTo,HasChild,createdAt")] TransferOrder transferOrder)
        {
            if (ModelState.IsValid)
            {
                _context.Add(transferOrder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(transferOrder);
        }

        // GET: TransferOrder/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transferOrder = await _context.TransferOrder.SingleOrDefaultAsync(m => m.transferOrderId == id);
            if (transferOrder == null)
            {
                return NotFound();
            }
            ViewData["branchIdFrom"] = new SelectList(_context.Branch, "branchId", "branchName");
            ViewData["warehouseIdFrom"] = new SelectList(_context.Warehouse, "warehouseId", "warehouseName");
            ViewData["branchIdTo"] = new SelectList(_context.Branch, "branchId", "branchName");
            ViewData["warehouseIdTo"] = new SelectList(_context.Warehouse, "warehouseId", "warehouseName");
            return View(transferOrder);
        }

        // POST: TransferOrder/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("transferOrderId,transferOrderNumber,transferOrderDate,description,picName,branchIdFrom,warehouseIdFrom,branchIdTo,warehouseIdTo,HasChild,createdAt")] TransferOrder transferOrder)
        {
            if (id != transferOrder.transferOrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transferOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransferOrderExists(transferOrder.transferOrderId))
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
            return View(transferOrder);
        }

        // GET: TransferOrder/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transferOrder = await _context.TransferOrder
                    .SingleOrDefaultAsync(m => m.transferOrderId == id);
            if (transferOrder == null)
            {
                return NotFound();
            }

            return View(transferOrder);
        }




        // POST: TransferOrder/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var transferOrder = await _context.TransferOrder.SingleOrDefaultAsync(m => m.transferOrderId == id);
            _context.TransferOrder.Remove(transferOrder);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransferOrderExists(string id)
        {
            return _context.TransferOrder.Any(e => e.transferOrderId == id);
        }

    }
}





namespace netcore.MVC
{
    public static partial class Pages
    {
        public static class TransferOrder
        {
            public const string Controller = "TransferOrder";
            public const string Action = "Index";
            public const string Role = "TransferOrder";
            public const string Url = "/TransferOrder/Index";
            public const string Name = "TransferOrder";
        }
    }
}
namespace netcore.Models
{
    public partial class ApplicationUser
    {
        [Display(Name = "TransferOrder")]
        public bool TransferOrderRole { get; set; } = false;
    }
}



