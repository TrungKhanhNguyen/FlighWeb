using ServiceStack.Redis;
using ServiceStack.Redis.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FlighWeb.Controllers
{
    public class HomeController : Controller
    {
        private FlightDetailEntities db = new FlightDetailEntities();
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

        public JsonResult LoadData()
        {
            //using (RedisClient client = new RedisClient("localhost", 6379))
            //{
            //    IRedisTypedClient<FlightPos> pos = client.As<FlightPos>();
            //    var tempValue = pos.GetAll();
            //    return Json(tempValue, JsonRequestBehavior.AllowGet);
            //}
            List<FlightPos> students = new List<FlightPos>();
            students = db.FlightPos.ToList();
            return Json(students, JsonRequestBehavior.AllowGet);
        }
    }
}