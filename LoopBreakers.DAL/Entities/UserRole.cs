using LoopBreakers.DAL.Enums;
using Microsoft.AspNetCore.Identity;


namespace LoopBreakers.DAL.Entities
{
    public class UserRole : IdentityRole<int>
    {
        public Roles Role { get; set;}
    }
}
