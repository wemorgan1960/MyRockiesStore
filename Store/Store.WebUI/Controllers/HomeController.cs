using System.Net;
using System.Net.Mail;
using System.Web.Mvc;
using Store.WebUI.Models;

namespace Store.WebUI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.IsIndexHome = true;
            //return RedirectToAction("UnderConstruction");
            return View() ;
        }
        public ActionResult About()
        {
            ViewBag.IsIndexHome = false;
            ViewBag.Message = "About shop.Myrockies.network";

            return View();
        }
        public ActionResult Disclaimer()
        {
            ViewBag.IsIndexHome = false;
            ViewBag.Message = "Disclaimer";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.IsIndexHome = false;
            ViewBag.Message = "shop.Myrockies.network contact page.";

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ContactAsync(EmailFormModel model)
        {
            ViewBag.IsIndexHome = false;
            if (ModelState.IsValid)
            {
                var subject = "Store.com Contact Form Email";
                var body = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
                var fromAddress = model.FromEmail;
                var toAddress = "admin@myrockies.network";
                var emailBody = string.Format(body, model.FromName, model.FromEmail, model.Message);

                var smtp = new SmtpClient();
                {
                    smtp.Host = "smtp.admin@myrockies.network";
                    smtp.Port = 587;
                    smtp.EnableSsl = false;
                    smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                    smtp.Credentials = new NetworkCredential("admin@myrockies.network", "TtLUVAz5");
                    smtp.Timeout = 20000;
                }

                smtp.Send(fromAddress, toAddress, subject, emailBody);
                return RedirectToAction("Sent");
            }
            else
            {
                return View();
            }

        }
        public ActionResult Sent()
        {
            ViewBag.IsIndexHome = false;
            return View();
        }
        public ActionResult UnderConstruction()
        {
            ViewBag.IsIndexHome = true;
            return View();
        }
    }
}