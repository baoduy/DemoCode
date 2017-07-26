using System.Web.Mvc;

namespace LoginAsDemo.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        /// <summary>
        /// Store the previous user name of the current login
        /// </summary>
        private string PreviousUser
        {
            get { return Session["PreviousUser"] as string; }
            set { Session["PreviousUser"] = value; }
        }

        /// <summary>
        /// store the count of sending the Unauthorized state to the client.
        /// </summary>
        private int TryingCount
        {
            get
            {
                if (Session["AtemptCount"] == null)
                    return 0;
                return (int)Session["AtemptCount"];
            }
            set { Session["AtemptCount"] = value; }
        }

        [HttpGet]
        public ActionResult LoginAs(string url = null)
        {
            //Update the Previous User with current login user if it is empty.
            if (string.IsNullOrWhiteSpace(PreviousUser))
                PreviousUser = User.Identity.Name;

            TryingCount += 1;

            //Try to send the Unauthorized state to the client.
            //Usually need to send twice.
            if (TryingCount <= 1)
                return new HttpUnauthorizedResult();

            //Send the Unauthorized state if the current user is the same.
            if (User.Identity.Name == PreviousUser)
                return new HttpUnauthorizedResult();

            //Already login with the other account
            //Re-set the AtemptCount
            TryingCount = 0;
            PreviousUser = User.Identity.Name;

            //and Redirect to the url or Index.
            if (string.IsNullOrWhiteSpace(url))
                return RedirectToAction(nameof(Index));
            return Redirect(url);
        }
    }
}