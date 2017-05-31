using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HBD.AspNet;

namespace AzManAuthentication.Controllers
{
    [Authorize]
    public class AuthorizedController : Controller
    {
        // GET: Authorized
        public ActionResult Index()
        {
            return View();
        }

        [OperationAuthorize(Operations = "View")]
        public ActionResult ActionViewOperation()
        {
            ViewBag.Info = "This action using 'View' Operation";
            return View("Index");
        }

        [Authorize(Roles = "View Only")]
        public ActionResult ActionViewOnlyRole()
        {
            ViewBag.Info = "This action using 'View Only' Role";
            return View("Index");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult ActionAdminRole()
        {
            ViewBag.Info = "This action using 'Admin' Role";
            return View("Index");
        }
    }
}