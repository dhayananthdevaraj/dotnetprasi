// UserService.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetapp.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnetapp.Services
{
    public class UserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<object>> GetAllUsers()
        {
            try
            {
                var users = await _context.Users.Select(u => new { u.UserName, u.UserRole, u.UserId, u.Email, u.MobileNumber }).ToListAsync();
                return users;
            }
            catch (Exception ex)
            {
                // Log the exception
                throw;
            }
        }

        public async Task<bool> AddUser(User newUser)
        {
            try
            {
                await _context.Users.AddAsync(newUser);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                // Log the exception
                return false;
            }
        }

        public async Task<User> GetUserByEmailAndPassword(string email, string password)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
                return user;
            }
            catch (Exception ex)
            {
                // Log the exception
                throw;
            }
        }
    }
}
