// FixedDepositService.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetapp.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnetapp.Services
{
    public class FixedDepositService
    {
        private readonly ApplicationDbContext _context;

        public FixedDepositService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<FixedDeposit>> GetAllFixedDeposits()
        {
            return await _context.FixedDeposits.Include(fd => fd.User).ToListAsync();
        }

        public async Task<FixedDeposit> GetFixedDepositById(long fdId)
        {
            return await _context.FixedDeposits.Include(fd => fd.User)
                .FirstOrDefaultAsync(fd => fd.FDId == fdId);
        }

        public async Task<bool> AddFixedDeposit(FixedDeposit fixedDeposit)
        {
            try
            {
                _context.FixedDeposits.Add(fixedDeposit);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateFixedDeposit(long fdId, FixedDeposit fixedDeposit)
        {
            try
            {
                var existingFixedDeposit = await _context.FixedDeposits
                    .FirstOrDefaultAsync(fd => fd.FDId == fdId);

                if (existingFixedDeposit == null)
                    return false;

                fixedDeposit.FDId = fdId;
                _context.Entry(existingFixedDeposit).CurrentValues.SetValues(fixedDeposit);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteFixedDeposit(long fdId)
        {
            try
            {
                var fixedDeposit = await _context.FixedDeposits
                    .FirstOrDefaultAsync(fd => fd.FDId == fdId);

                if (fixedDeposit == null)
                    return false;

                _context.FixedDeposits.Remove(fixedDeposit);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
