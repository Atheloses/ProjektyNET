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
    public class UzivatelUkolController : Controller
    {
        #region Private

        private readonly ILogger<UzivatelUkolController> _logger;
        private UzivatelUkolService uzivatelUkolService;

        #endregion Private

        #region ctor

        public UzivatelUkolController(ILogger<UzivatelUkolController> logger, UzivatelUkolService p_UzivatelUkolService)
        {
            uzivatelUkolService = p_UzivatelUkolService;
            _logger = logger;
        }

        #endregion ctor

        #region Index

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
                ViewBag.UzivatelUkolItems = await uzivatelUkolService.ZiskejUzivatelUkolyProUzivatele(int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value));

            return View();
        }

        #endregion Index

        #region Detail

        [HttpGet]
        public async Task<IActionResult> Detail(int p_Id)
        {

            var podukol = await uzivatelUkolService.ZiskejUzivatelUkol(p_Id);
            ViewBag.Podukol = podukol;
            return View(new SaveUzivatelUkolForm()
            {
                podukol = podukol
            });
        }


        [HttpPost]
        public async Task<IActionResult> Detail(SaveUzivatelUkolForm p_Form)
        {
            var podukolAfter = p_Form.podukol;
            var podukolBefore = await uzivatelUkolService.ZiskejUzivatelUkol(podukolAfter.IDUzivatelUkol);
            podukolBefore.Popis = podukolAfter.Popis;

            if (ModelState.IsValid)
            {
                await uzivatelUkolService.UlozUzivatelUkol(podukolBefore);
                return RedirectToAction(nameof(UzivatelUkolController.Index), "UzivatelUkol");
            }

            ViewBag.Podukol = uzivatelUkolService.ZiskejUzivatelUkol(podukolBefore.IDUzivatelUkol);
            return View();
        }

        #endregion Detail

        #region Splnit

        [HttpGet]
        public async Task<IActionResult> Splnit(int p_Id)
        {

            await uzivatelUkolService.SplnitUzivatelUkol(p_Id);
            return RedirectToAction(nameof(UzivatelUkolController.Index), "UzivatelUkol");
        }

        #endregion Splnit

        #region Novy

        [HttpGet]
        public IActionResult Novy()
        {

            var uzivatelUkol = new UzivatelUkolApp();
            ViewBag.uzivatelUkol = uzivatelUkol;
            return View(new SaveUzivatelUkolForm()
            {
                podukol = uzivatelUkol
            });
        }

        [HttpPost]
        public async Task<IActionResult> Novy(SaveUzivatelUkolForm p_Form)
        {
            var newUzivatelUkol = p_Form.podukol;
            newUzivatelUkol.IDUzivatel = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            newUzivatelUkol.CasPripojeni = DateTime.Now;

            if (ModelState.IsValid)
            {
                await uzivatelUkolService.VytvorUzivatelUkol(newUzivatelUkol);
                return RedirectToAction(nameof(UzivatelUkolController.Index), "UzivatelUkol");
            }

            return View();
        }

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
