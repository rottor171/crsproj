using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplicationWithPostgresSQL;

using WebApplicationWithPostgresSQL.Services;

namespace WebApplicationWithPostgresSQL.Controllers
{
    //[Authorize(Roles = "admin")]
    public class BranchesController : Controller
    {
        private BranchService _service;

        public BranchesController(BranchService service)
        {
            _service = service;    
        }

        // GET: Branches
        public async Task<IActionResult> Index()
        {
            return View(await _service.GetAllBranches());
        }

        // GET: Branches/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var branch = await _service.GetDetails((int)id);

            if (branch == null)
            {
                return NotFound();
            }

            return View(branch);
        }

        // GET: Branches/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Branches/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BranchNo,City,Street,Director")] Branch branch)
        {
            if (ModelState.IsValid)
            {
                _service.AddNewBranch(branch);
                return RedirectToAction("Index");
            }
            return View(branch);
        }

        // GET: Branches/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var branch = await _service.GetDetails((int)id);
            if (branch == null)
            {
                return NotFound();
            }
            return View(branch);
        }

        // POST: Branches/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BranchNo,City,Street,Director")] Branch branch)
        {
            if (id != branch.BranchNo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _service.UpdateBranchData(branch);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_service.IsValid(branch.BranchNo))
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
            return View(branch);
        }

        // GET: Branches/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var branch = await _service.GetDetails((int)id);
            if (branch == null)
            {
                return NotFound();
            }

            return View(branch);
        }

        // POST: Branches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _service.DeleteBranch(id);

            return RedirectToAction("Index");
        }

        private bool BranchExists(int id)
        {
            return _service.IsValid(id);
        }
    }
}
