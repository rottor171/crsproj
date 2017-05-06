using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace WebApplicationWithPostgresSQL.DataAccessLayer
{
    public class BranchRepository
    {
        private readonly BankingContext _bankingContext;

        public BranchRepository(BankingContext bankingContext)
        {
            if (bankingContext == null)
            {
                throw new ArgumentNullException(nameof(bankingContext));
            }

            _bankingContext = bankingContext;
        }

        public Task<List<Branch>> GetAllBranches()
        {
            return _bankingContext.Branch.ToListAsync();
        }

        public async Task<Branch> GetDetails(int id)
        {
            var branch = await _bankingContext.Branch
                  .SingleOrDefaultAsync(m => m.BranchNo == id);

            //if (branch == null)
            //{
            //    return null;
            //}

            return branch;
        }

        public async void AddNewBranch(Branch branch)
        {
            _bankingContext.Add(branch);
            await _bankingContext.SaveChangesAsync();
        }

        public async void UpdateBranchData(Branch branch)
        {
            try
            {
                _bankingContext.Update(branch);
                await _bankingContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                //if (!BranchExists(branch.BranchNo))
                //{
                //    return NotFound();
                //}
                //else
                //{
                    throw;
                //}
            }
        }

        public async void DeleteBranch(int branchId)
        {
            var branch = await _bankingContext.Branch.SingleOrDefaultAsync(m => m.BranchNo == branchId);
            _bankingContext.Branch.Remove(branch);
            await _bankingContext.SaveChangesAsync();
        }

        public bool IsValid(int branchId)
        {
            return _bankingContext.Branch.Any(e => e.BranchNo == branchId);
        }
    }
}