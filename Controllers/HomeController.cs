using FlighWeb.Models;
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
        private static string redis_flight = "FlightPos";
        public ActionResult Index()
        {
            return View();
        }

        

       
        public JsonResult SearchData(string inputICAO, string datepicker1, string datepicker2,string fromtime, string totime)
        {
            if (string.IsNullOrEmpty(inputICAO))
            {
                return Json(new List<FlightPos>(), JsonRequestBehavior.AllowGet);
            }
            else
            {
                var fromDateWithTime = datepicker1 + " " + fromtime;
                var toDateWithTime = datepicker2 + " " + totime;
                List<FlightPos> position = new List<FlightPos>();
                position = db.FlightPos.Where(m => m.Callsign == inputICAO || m.ICAO == inputICAO).ToList();
                if (!String.IsNullOrEmpty(datepicker1))
                {
                    var tempFrDate = Convert.ToDateTime(fromDateWithTime);
                    position = position.Where(m => m.DateGenerate >= tempFrDate).ToList();
                }
                if (!String.IsNullOrEmpty(datepicker2))
                {
                    var tempToDate = Convert.ToDateTime(toDateWithTime);
                    position = position.Where(m => m.DateGenerate <= tempToDate).ToList();
                }

                return Json(position, JsonRequestBehavior.AllowGet);
            }
            
        }

     
        public JsonResult LoadData()
        {
            using (RedisClient client = new RedisClient("localhost", 6379))
            {
                IRedisTypedClient<FlightPos> pos = client.As<FlightPos>();
                var tempValue = pos.Lists[redis_flight].GetAll();
                var listPos = tempValue.OrderBy(m => m.DateGenerate).ToList();
                return Json(listPos, JsonRequestBehavior.AllowGet);
            }
           
        }


        
    }
}