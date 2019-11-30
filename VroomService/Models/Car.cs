using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VroomService.Models
{
    public class Car
    {
        public int id { get; set; }
        public string name { get; set; }
        public int price { get; set; }
        public string description { get; set; }
        public int placeNb { get; set; }
        public Model model { get; set; }

        /// <summary>
        /// Car constructor
        /// </summary>
        /// <param name="name"></param>
        /// <param name="price"></param>
        /// <param name="description"></param>
        /// <param name="placeNb"></param>
        /// <param name="model"></param>
        public Car(string name, int price, string description, int placeNb, Model model)
        {
            this.name = name;
            this.price = price;
            this.description = description;
            this.placeNb = placeNb;
            this.model = model;
        }

    }
}