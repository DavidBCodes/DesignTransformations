using System.Web.Mvc;
using System.Net.Mail;

namespace DesignTransformations.UI.MVC.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public JsonResult SendEmail(string contact_email, string contact_name, string subject, string body)
        {
            string data = "0";
            try
            {
                SmtpClient smtp = new SmtpClient();
                smtp.Host = System.Configuration.ConfigurationManager.AppSettings["Host"].ToString();
                smtp.Port = System.Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["port_ssl"].ToString());
                smtp.EnableSsl = System.Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["tls_ssl_req"].ToString());
                //smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential(System.Configuration.ConfigurationManager.AppSettings["emailAddress"].ToString(),
                    System.Configuration.ConfigurationManager.AppSettings["pwd"].ToString());
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                MailMessage msg = new MailMessage(contact_email, System.Configuration.ConfigurationManager.AppSettings["emailAddress"].ToString());
                msg.Subject = subject;
                msg.Body = body;
                msg.IsBodyHtml = false;
                msg.Attachments.Clear(); //this is to help with security by hopefully removing any attachments present - tle 2020-07-28
                smtp.Send(msg);
                data = "success";
            }
            catch (System.Exception ex)
            {

                data = ex.Message + " " + ex.StackTrace;
            }

            return new JsonResult() { Data = "data" };
        }
    }
}
