using HV_tech.UI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace HV_tech.UI.Controllers
{
    public class HomeController : Controller
    {
        private List<Order> _orders;

        public ActionResult Index()
        {
            _orders = new List<Order>();
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

        [HttpPost]
        public JsonResult Order(string order)
        {
            ViewBag.OrderMessage = CallOrderAPI(order);

            return Json(CallOrderAPI(order), JsonRequestBehavior.AllowGet);
        }

        private string CallOrderAPI(string input)
        {
            string order = string.Empty;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://localhost:51600/api/values/" + input);

            WebResponse response = request.GetResponse();
            using (Stream responseStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                order = reader.ReadToEnd();
            }

            return order;
        }
    }
}