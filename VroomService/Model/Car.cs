using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VroomService.Model
{
    public class Car
    {
        public int id { get; set; }
        public string name { get; set; }
        public float price { get; set; }
        public string description { get; set; }
        public int placeNb { get; set; }
        public Model model { get; set; }


    }
}