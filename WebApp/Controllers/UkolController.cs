using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using DAO.Tables;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApp.Models;
using WebApp.DTOApp;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace WebApp.Controllers
{
    [Authorize]
    public class UkolController : Controller
    {
        #region Private

        private readonly ILogger<UkolController> _logger;
        private UkolService ukolService;
        private UzivatelService uzivatelService;
        private UzivatelUkolService uzivatelUkolService;

        #endregion Private

        #region ctor

        public UkolController(ILogger<UkolController> logger, UkolService p_UkolService, UzivatelService p_UzivatelService, UzivatelUkolService p_UzivatelUkolService)
        {
            ukolService = p_UkolService;
            uzivatelService = p_UzivatelService;
            uzivatelUkolService = p_UzivatelUkolService;
            _logger = logger;
        }

        #endregion ctor

        #region Index

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ViewBag.IndexItems = await ukolService.ZiskejUkolyUzivatele(int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value));

            return View();
        }

        #endregion Index

        #region Detail

        [HttpGet]
        public async Task<IActionResult> Detail(int? p_Id)
        {
            UkolApp ukolApp;
            IEnumerable<int> selectedUzivatele;
            List<UzivatelApp> uzivatele;
            IEnumerable<PrioritaApp> priority;

            var uziv = await uzivatelService.ZiskejUzivatele();
            if (p_Id == null)
            {
                ukolApp = new UkolApp() { IDUkol = -1, Termin = DateTime.Now };
                selectedUzivatele = new List<int>();
                uzivatele = uziv.Where(x => x.IDUzivatel != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)).ToList();
            }
            else
            {
                ukolApp = await ukolService.ZiskejUkol(p_Id.Value);
                selectedUzivatele = await uzivatelUkolService.ZiskejUzivateleUkolu(p_Id.Value);
                uzivatele = uziv.ToList();
            }
            priority = await ukolService.ZiskejPriority();


            return View(new SaveUkolForm()
            {
                prioritaApp = priority,
                uzivateleApp = uzivatele,
                selectedUzivatele = selectedUzivatele,
                ukolApp = ukolApp
            });
        }

        [HttpPost]
        public async Task<IActionResult> Detail(SaveUkolForm p_Form)
        {
            var ukolApp = p_Form.ukolApp;
            var idUziv = new List<int>();
            if(p_Form.selectedUzivateleApp!=null)
                p_Form.selectedUzivateleApp.ForEach(x => idUziv.Add(int.Parse(x)));
            idUziv.Add(int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value));
            ukolApp.IDPriorita = int.Parse(p_Form.selectedPrioritaApp);
            if (ModelState.IsValid)
            {
                if (p_Form.ukolApp.IDUkol > 0)
                {
                    await ukolService.UlozUkol(ukolApp);
                }
                else
                    await ukolService.VytvorUkol(ukolApp, idUziv, new List<int>());

                return RedirectToAction(nameof(UkolController.Index), "Ukol");
            }

            return View();
        }

        #endregion Detail

        #region Splnit

        [HttpGet]
        public async Task<IActionResult> Splnit(int p_Id)
        {

            await ukolService.SplnitUkol(p_Id);
            return RedirectToAction(nameof(UkolController.Index), "Ukol");
        }

        #endregion Splnit

        #region Novy

        //[HttpGet]
        //public IActionResult Novy()
        //{
        //    if (!User.Identity.IsAuthenticated)
        //        return RedirectToAction(nameof(AccountController.Login), "Account");

        //    var ukolApp = new UkolApp();
        //    ViewBag.ukolApp = ukolApp;
        //    return View(new SaveUkolForm()
        //    {
        //        ukolApp = ukolApp
        //    });
        //}

        //[HttpPost]
        //public async Task<IActionResult> Novy(SaveUkolForm p_Form)
        //{
        //    var newUkolApp = p_Form.ukolApp;

        //    if (ModelState.IsValid)
        //    {
        //        await ukolService.VytvorUkol(newUkolApp);
        //        return RedirectToAction("Index");
        //    }

        //    return View();
        //}

        #endregion Novy

        #region Helpers

        [AllowAnonymous]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        #endregion Helpers
    }
}
