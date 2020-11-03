using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlighWeb.Models
{
    public class SearchModel
    {
        public string icao { get; set; }
        public string fromdate { get; set; }
        public string todate { get; set; }
        public List<FlightPos> listPosition { get; set; }
    }
}