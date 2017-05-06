using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplicationWithPostgresSQL.DataAccessLayer;

namespace WebApplicationWithPostgresSQL.Services
{
    public class BranchService
    {
        private BranchRepository _branchRepository;

        public BranchService(BranchRepository branchRepository)
        {
            _branchRepository = branchRepository;
        }

        public async Task<List<Branch>> GetAllBranches()
        {
            List<Branch> list = await _branchRepository.GetAllBranches();
            return list;
        }

        public async Task<Branch> GetDetails(int id)
        {
            Branch branch = await _branchRepository.GetDetails(id);
            return branch;
        }

        public void AddNewBranch(Branch branch)
        {
            _branchRepository.AddNewBranch(branch);
        }

        public void UpdateBranchData(Branch branch)
        {
            _branchRepository.UpdateBranchData(branch);
        }

        public void DeleteBranch(int branchId)
        {
            _branchRepository.DeleteBranch(branchId);
        }

        public bool IsValid(int branchId)
        {
            return _branchRepository.IsValid(branchId);
        }
    }
}