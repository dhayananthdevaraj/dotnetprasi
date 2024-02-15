// FDRequestService.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetapp.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnetapp.Services
{
    public class FDRequestService
    {
        private readonly ApplicationDbContext _context;

        public FDRequestService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<FDRequest>> GetAllFDRequests()
        {
            return await _context.FDRequests.ToListAsync();
        }

        public async Task<FDRequest> GetFDRequestById(long requestId)
        {
            return await _context.FDRequests
                .FirstOrDefaultAsync(fr => fr.RequestId == requestId);
        }

        public async Task<bool> AddFDRequest(FDRequest fdRequest)
        {
            try
            {
                _context.FDRequests.Add(fdRequest);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                // Handle exception as needed
                return false;
            }
        }

        // Additional methods for update, delete, or other business logic

    }
}
