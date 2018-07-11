using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace HV_tech_mvc.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            try
            {
                string orderTest = "morning,1,2,3";

                var returnedOrder = CallOrderAPI(orderTest);

                if (!string.IsNullOrEmpty(returnedOrder))
                {

                }

            }
            catch (Exception)
            {
                throw;
            }

            return View();
        }

        private string CallOrderAPI(string input)
        {
            string order = string.Empty;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://localhost:51600/api/values/" + input);

            WebResponse response = request.GetResponse();
            using (Stream responseStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(responseStream, System.Text.Encoding.UTF8);
                order = reader.ReadToEnd();
            }

            return order;
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
    }
}