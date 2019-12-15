using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApp.DTOApp;

namespace WebApp.Models
{
    public class RoleService : IRoleStore<RoleApp>
    {
        #region NotImplemented

        public Task<IdentityResult> CreateAsync(RoleApp role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> DeleteAsync(RoleApp role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<RoleApp> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<RoleApp> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetNormalizedRoleNameAsync(RoleApp role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetRoleIdAsync(RoleApp role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetRoleNameAsync(RoleApp role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetNormalizedRoleNameAsync(RoleApp role, string normalizedName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetRoleNameAsync(RoleApp role, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> UpdateAsync(RoleApp role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        #endregion NotImplemented

        #region Dispose

        public void Dispose()
        {
            // Nothing to dispose.
        }

        #endregion Dispose
    }
}
