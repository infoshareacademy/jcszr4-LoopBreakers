using LoopBreakers.DAL.Entities;
using LoopBreakers.Logic.Data;
using LoopBreakers.WebApp.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoopBreakers.WebApp.Contracts
{
    public interface IUserService
    {
        Task<IEnumerable<ApplicationUser>> FilterBy(SearchUserViewModel filter);
    }
}
