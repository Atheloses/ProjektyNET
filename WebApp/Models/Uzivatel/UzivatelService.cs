using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.AspNetCore.Identity;
using WebApp.DTOApp;
using DAO.Tables;
using Oracle.ManagedDataAccess.Client;
using DomainLogic;

namespace WebApp.Models
{
    public class UzivatelService : IUserStore<UzivatelApp>, IUserPasswordStore<UzivatelApp>
    {
        #region Private

        //private readonly OracleConnection connection;

        #endregion Private

        #region ctor

        //public UzivatelService(IConfiguration configuration)
        //{

        //}

        #endregion ctor

        #region CRUD
        public async Task<IEnumerable<UzivatelApp>> ZiskejUzivatele()
        {
            IEnumerable<UzivatelApp> output;

            try
            {
                using var uzivatelDL = new UzivatelDL();
                output = UzivatelApp.GetAppFromDTO(await uzivatelDL.ZiskejUzivatele());
            }
            catch (Exception ex) { throw ex; }

            return output;
        }

        public async Task<IdentityResult> CreateAsync(UzivatelApp user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            try
            {
                using (var uzivatelUkolDL = new UzivatelDL())
                {
                    //user.DatumRegistrace = DateTime.Now;
                    user.Aktivni = '1';
                    await uzivatelUkolDL.VytvorUzivatel(UzivatelApp.GetDTOFromApp(user));
                }
            }
            catch (Exception ex) { throw ex; }

            return IdentityResult.Success;
        }

        public async Task<UzivatelApp> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            UzivatelApp output = new UzivatelApp();
            try
            {
                using (var uzivatelUkolDL = new UzivatelDL())
                {
                    output = UzivatelApp.GetAppFromDTO(await uzivatelUkolDL.ZiskejUzivatel(int.Parse(userId)));
                }
            }
            catch (Exception ex) { throw ex; }

            return output;
        }

        public async Task<UzivatelApp> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            UzivatelApp output = new UzivatelApp();
            try
            {
                using (var uzivatelUkolDL = new UzivatelDL())
                {
                    output = UzivatelApp.GetAppFromDTO(await uzivatelUkolDL.ZiskejUzivatelNormPrezdivka(normalizedUserName));
                }
            }
            catch (Exception ex) { throw ex; }
            return output;
        }

        public async Task<IdentityResult> UpdateAsync(UzivatelApp user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            try
            {
                using (var uzivatelUkolDL = new UzivatelDL())
                {
                    await uzivatelUkolDL.UlozUzivatel(UzivatelApp.GetDTOFromApp(user));
                }
            }
            catch (Exception ex) { throw ex; }

            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(UzivatelApp user, CancellationToken cancellationToken)
        {
            await Task.Delay(100);
            throw new NotImplementedException();
            //cancellationToken.ThrowIfCancellationRequested();

            //try {
            //    using (var uzivatelUkolDL = new UzivatelDL())
            //    {
            //        await uzivatelUkolDL.SmazUzivatel(user.IDUzivatel);
            //    }
            //}
            //catch (Exception ex) { throw ex; }

            //return IdentityResult.Success;
        }

        #endregion CRUD

        #region InternalLogic
        //Nějaká další logika potřebná pro správné namapování ID / UserName / NormalizedUserName

        public Task<string> GetUserIdAsync(UzivatelApp user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.IDUzivatel.ToString());
        }

        public Task SetUserNameAsync(UzivatelApp user, string userName, CancellationToken cancellationToken)
        {
            user.Prezdivka = userName;
            return Task.FromResult(0);
        }

        public Task<string> GetUserNameAsync(UzivatelApp user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Prezdivka);
        }

        public Task SetNormalizedUserNameAsync(UzivatelApp user, string normalizedName, CancellationToken cancellationToken)
        {
            user.NormPrezdivka = normalizedName;
            return Task.FromResult(0);
        }

        public Task<string> GetNormalizedUserNameAsync(UzivatelApp user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.NormPrezdivka);
        }

        #endregion InternalLogic

        #region Password

        public Task SetPasswordHashAsync(UzivatelApp user, string passwordHash, CancellationToken cancellationToken)
        {
            user.HesloHash = passwordHash;
            return Task.FromResult(0);
        }

        public Task<string> GetPasswordHashAsync(UzivatelApp user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.HesloHash);
        }

        public Task<bool> HasPasswordAsync(UzivatelApp user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.HesloHash != null);
        }

        #endregion Password

        #region Dispose

        public void Dispose()
        {
            // Nothing to dispose.
        }

        #endregion Dispose
    }
}
