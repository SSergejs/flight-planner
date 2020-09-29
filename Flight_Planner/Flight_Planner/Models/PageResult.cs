using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Flight_Planner.Models
{
    public class PageResult
    {
        public int page { get; set; }
        public int totalItems { get; set; }
        public List<Flight> items { get; set; }

        public PageResult()
        {
            page = 0;
            totalItems = 0;
            items = new List<Flight>(0);
        }
    }
}