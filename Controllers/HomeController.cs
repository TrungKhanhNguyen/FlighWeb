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
        public ActionResult Index()
        {
            
            return View();
        }

        

       
        public JsonResult SearchData(string inputICAO, string datepicker1, string datepicker2)
        {
            
            List<FlightPos> position = new List<FlightPos>();
            position = db.FlightPos.Where(m => m.Callsign == inputICAO || m.ICAO == inputICAO).ToList();
            if (!String.IsNullOrEmpty(datepicker1))
            {
                var tempFrDate = Convert.ToDateTime(datepicker1);
                position = position.Where(m => m.DateGenerate >= tempFrDate).ToList();
            }
            if (!String.IsNullOrEmpty(datepicker2))
            {
                var tempToDate = Convert.ToDateTime(datepicker2);
                position = position.Where(m => m.DateGenerate <= tempToDate).ToList();
            }
           
            return Json(position, JsonRequestBehavior.AllowGet);
        }

     
        public JsonResult LoadData()
        {
            using (RedisClient client = new RedisClient("localhost", 6379))
            {
                IRedisTypedClient<FlightPos> pos = client.As<FlightPos>();
                var tempValue = pos.GetAll();
                return Json(tempValue, JsonRequestBehavior.AllowGet);
            }
            //List<FlightPos> students = new List<FlightPos>();
            //students = db.FlightPos.ToList();
            //return Json(students, JsonRequestBehavior.AllowGet);
        }


        
    }
}