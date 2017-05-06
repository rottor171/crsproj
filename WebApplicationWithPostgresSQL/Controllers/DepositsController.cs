using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplicationWithPostgresSQL;

namespace WebApplicationWithPostgresSQL.Controllers
{
    public class DepositsController : Controller
    {
        private readonly BankingContext _context;

        public DepositsController(BankingContext context)
        {
            _context = context;    
        }

        public async Task<IActionResult> ClientSearch(int id, int passport, int password)
        {
            var deposit = await _context.Deposit
                .Include(d => d.DepClientNoNavigation)
                .Include(d => d.DepStaffNoNavigation)
                .Where(d => d.DepClientNo == passport)
                .SingleOrDefaultAsync(m => m.DepositNo == id);

            if (deposit == null)
                return View("SearchFailed");

            var sharpPrgContext = _context.Deposit
                .Include(d => d.DepClientNoNavigation)
                .Where(d => d.DepClientNo == passport)
                     //|| d.FirstMidName.Contains(searchString))
                .Include(d => d.DepStaffNoNavigation);
            ViewBag.MyModel = await sharpPrgContext.ToListAsync();

            return View(deposit);
        }
        
        //============================================================================================

        // GET: Deposits
        public async Task<IActionResult> Index()
        {
            var sharpPrgContext = _context.Deposit
                .Include(d => d.DepClientNoNavigation)
                .Include(d => d.DepStaffNoNavigation);
            return View(await sharpPrgContext.ToListAsync());
        }

        // GET: Deposits/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deposit = await _context.Deposit
                .Include(d => d.DepClientNoNavigation)
                .Include(d => d.DepStaffNoNavigation)
                .SingleOrDefaultAsync(m => m.DepositNo == id);
            if (deposit == null)
            {
                return NotFound();
            }

            return View(deposit);
        }

        // GET: Deposits/Create
        public IActionResult Create()
        {
            ViewData["DepClientNo"] = new SelectList(_context.Client, "ClientNo", "Fname");
            ViewData["DepStaffNo"] = new SelectList(_context.Staff, "StaffNo", "Fname");
            return View();
        }

        // POST: Deposits/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DepositNo,Cash,PercentPerYear,DateOfBegin,DateOfEnd,Commentary,DepStaffNo,DepClientNo")] Deposit deposit)
        {
            if (ModelState.IsValid)
            {
                _context.Add(deposit);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["DepClientNo"] = new SelectList(_context.Client, "ClientNo", "Fname", deposit.DepClientNo);
            ViewData["DepStaffNo"] = new SelectList(_context.Staff, "StaffNo", "Fname", deposit.DepStaffNo);
            return View(deposit);
        }

        // GET: Deposits/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deposit = await _context.Deposit.SingleOrDefaultAsync(m => m.DepositNo == id);
            if (deposit == null)
            {
                return NotFound();
            }
            ViewData["DepClientNo"] = new SelectList(_context.Client, "ClientNo", "Fname", deposit.DepClientNo);
            ViewData["DepStaffNo"] = new SelectList(_context.Staff, "StaffNo", "Fname", deposit.DepStaffNo);
            return View(deposit);
        }

        // POST: Deposits/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DepositNo,Cash,PercentPerYear,DateOfBegin,DateOfEnd,Commentary,DepStaffNo,DepClientNo")] Deposit deposit)
        {
            if (id != deposit.DepositNo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(deposit);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepositExists(deposit.DepositNo))
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
            ViewData["DepClientNo"] = new SelectList(_context.Client, "ClientNo", "Fname", deposit.DepClientNo);
            ViewData["DepStaffNo"] = new SelectList(_context.Staff, "StaffNo", "Fname", deposit.DepStaffNo);
            return View(deposit);
        }

        // GET: Deposits/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deposit = await _context.Deposit
                .Include(d => d.DepClientNoNavigation)
                .Include(d => d.DepStaffNoNavigation)
                .SingleOrDefaultAsync(m => m.DepositNo == id);
            if (deposit == null)
            {
                return NotFound();
            }

            return View(deposit);
        }

        // POST: Deposits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var deposit = await _context.Deposit.SingleOrDefaultAsync(m => m.DepositNo == id);
            _context.Deposit.Remove(deposit);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool DepositExists(int id)
        {
            return _context.Deposit.Any(e => e.DepositNo == id);
        }
    }
}
