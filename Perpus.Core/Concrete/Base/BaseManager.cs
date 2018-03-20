using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Perpus.Core.Concrete.Base
{ 
    public abstract class BaseManager
    {
        public IEnumerable<Claim> GetUserClaims()
        {
            var User = System.Threading.Thread.CurrentPrincipal;
            var currentIdentity = (System.Security.Claims.ClaimsIdentity)User.Identity;
            return currentIdentity.Claims;
        }
    }
}
