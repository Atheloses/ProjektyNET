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
    public class SkupinaController : Controller
    {
        #region Private

        private readonly ILogger<SkupinaController> _logger;
        private SkupinaService skupinaService;
        private UzivatelService uzivatelService;

        #endregion Private

        #region ctor

        public SkupinaController(ILogger<SkupinaController> logger, SkupinaService p_SkupinaService, UzivatelService p_UzivatelService)
        {
            skupinaService = p_SkupinaService;
            uzivatelService = p_UzivatelService;
            _logger = logger;
        }

        #endregion ctor

        #region Index

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ViewBag.IndexItems = await skupinaService.ZiskejSkupiny();

            return View();
        }

        #endregion Index

        #region Detail

        [HttpGet]
        public async Task<IActionResult> Detail(int? p_Id)
        {
            SkupinaApp skupinaApp;
            IEnumerable<int> selectedUzivatele;
            List<UzivatelApp> uzivatele;

            var uziv = await uzivatelService.ZiskejUzivatele();
            if (p_Id == null)
            {
                skupinaApp = new SkupinaApp() { IDSkupina = -1};
                selectedUzivatele = new List<int>();
                uzivatele = uziv.Where(x => x.IDUzivatel != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)).ToList();
            }
            else
            {
                skupinaApp = await skupinaService.ZiskejSkupinu(p_Id.Value);
                selectedUzivatele = await skupinaService.ZiskejUzivateleSkupiny(p_Id.Value);
                uzivatele = uziv.ToList();
            }

            return View(new SkupinaForm()
            {
                uzivateleApp = uzivatele,
                selectedUzivatele = selectedUzivatele,
                skupinaApp = skupinaApp
            });
        }

        [HttpPost]
        public async Task<IActionResult> Detail(SkupinaForm p_Form)
        {
            var skupinaApp = p_Form.skupinaApp;
            var idUziv = new List<int>();
            if(p_Form.selectedUzivateleApp!=null && p_Form.selectedUzivateleApp.Count>0)
                p_Form.selectedUzivateleApp.ForEach(x => idUziv.Add(int.Parse(x)));
            idUziv.Add(int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value));
            if (ModelState.IsValid)
            {
                if (p_Form.skupinaApp.IDSkupina > 0)
                    await skupinaService.UlozSkupinu(skupinaApp);
                else
                    await skupinaService.VytvorSkupinu(skupinaApp, idUziv);

                return RedirectToAction(nameof(SkupinaController.Index), "Skupina");
            }

            return View();
        }

        #endregion Detail

        #region Smazat

        [HttpGet]
        public async Task<IActionResult> Smazat(int p_Id)
        {
            await skupinaService.SmazatSkupinu(p_Id);

            return RedirectToAction(nameof(SkupinaController.Index), "Skupina");
        }

        #endregion Smazat

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
