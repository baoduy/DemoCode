using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoginAsDemo.Controllers
{
    [AllowAnonymous]
    public class AntiForgeryController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection collection)
        {
            ViewBag.Data = collection["Search"];
            return View("Index");
        }
    }
}
