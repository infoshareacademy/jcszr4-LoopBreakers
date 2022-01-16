using LoopBreakers.DAL.Context;
using LoopBreakers.DAL.Entities;
using LoopBreakers.DAL.Repositories;
using LoopBreakers.WebApp.Contracts;
using LoopBreakers.WebApp.DTOs;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace LoopBreakers.WebApp.Services
{
    public class UserService : IUserService
    {
        private readonly IBaseRepository<ApplicationUser> _usersRepository;
        private readonly ApplicationDbContext _db;
        public UserService(ApplicationDbContext db,
            IBaseRepository<ApplicationUser> usersRepository)
        {
            _db = db;
            _usersRepository = usersRepository;
        }
        public async Task<IEnumerable<ApplicationUser>> FilterBy(SearchUserViewModel filter)
        {
            var usersQuery = _db.Users.AsQueryable();

            if (filter.DateFrom.HasValue && filter.DateTo.HasValue)
            {
                usersQuery = usersQuery.Where(q => q.Created >= filter.DateFrom.Value && q.Created <= filter.DateTo.Value);
            }
            if (filter.Name != null && filter.Name.Length > 2)
            {
                usersQuery = usersQuery.Where(n => n.LastName.StartsWith(filter.Name));
            }
            return await usersQuery.ToListAsync();
        }
    }
}
