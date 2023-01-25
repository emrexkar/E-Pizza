
using ePizza.Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizza.Data.Concrete.EntityFramework.Context
{
    public class ePizzaContext : IdentityDbContext<User, Role, int, UserClaim,UserRole,UserLogin,UserToken, RoleClaim>
    {
    }
}
