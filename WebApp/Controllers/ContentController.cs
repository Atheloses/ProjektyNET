using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class ContentController : Controller
    {
        // TODO: Stáhnout nějaké věci do XML

        #region Private

        private readonly ILogger<ContentController> _logger;
        //private ProductService productService;

        #endregion Private

        #region ctor

        public ContentController(ILogger<ContentController> logger)
        {
            //this.productService = productService;
            _logger = logger;
        }

        #endregion ctor

        #region Index

        public IActionResult Index()
        {
            return View();
        }

        #endregion Index

        #region XML

        public IActionResult GetXml()
        {
            string xml;
            using (StringWriter sw = new StringWriter())
            {
                XmlSerializer xSer = new XmlSerializer(typeof(string));
                xSer.Serialize(sw, "XML DOWNLOAD");
                xml = sw.ToString();
            }

            return new ContentResult()
            {
                Content = xml,
                ContentType = "text/xml"
            };
        }

        #endregion XML

        #region Helpers

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        #endregion Helpers
    }
}
