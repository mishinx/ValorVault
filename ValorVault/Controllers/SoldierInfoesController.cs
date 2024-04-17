using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SoldierInfoContext;
using ValorVault.Models;

namespace ValorVault.Controllers
{
    public class SoldierInfoesController : Controller
    {
        private readonly SoldierInfoDbContext _context;

        public SoldierInfoesController(SoldierInfoDbContext context)
        {
            _context = context;
        }

        // GET: SoldierInfoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.SoldierInfos.ToListAsync());
        }

        // GET: SoldierInfoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var soldierInfo = await _context.SoldierInfos
                .FirstOrDefaultAsync(m => m.soldier_info_id == id);
            if (soldierInfo == null)
            {
                return NotFound();
            }

            return View(soldierInfo);
        }

        // GET: SoldierInfoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SoldierInfoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SoldierInfo soldierInfo)
        {
            if (ModelState.IsValid)
            {
                _context.SoldierInfos.Add(soldierInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(soldierInfo);
        }



        // GET: SoldierInfoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var soldierInfo = await _context.SoldierInfos.FindAsync(id);
            if (soldierInfo == null)
            {
                return NotFound();
            }
            return View(soldierInfo);
        }

        // POST: SoldierInfoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SoldierInfo soldierInfo)
        {
            if (id != soldierInfo.soldier_info_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.SoldierInfos.Update(soldierInfo);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SoldierInfoExists(soldierInfo.soldier_info_id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            return View(soldierInfo);
        }

        // GET: SoldierInfoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var soldierInfo = await _context.SoldierInfos
                .FirstOrDefaultAsync(m => m.soldier_info_id == id);
            if (soldierInfo == null)
            {
                return NotFound();
            }

            return View(soldierInfo);
        }

        // POST: SoldierInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var soldierInfo = await _context.SoldierInfos.FindAsync(id);
            if (soldierInfo != null)
            {
                _context.SoldierInfos.Remove(soldierInfo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SoldierInfoExists(int id)
        {
            return _context.SoldierInfos.Any(e => e.soldier_info_id == id);
        }
    }
}
