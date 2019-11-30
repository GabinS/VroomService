using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VroomService.Models
{
    public class Model
    {
        private int id { get; set; }
        private string name { get; set; }
        private Brand brand { get; set; }

        /// <summary>
        /// Model constructor
        /// </summary>
        /// <param name="name"></param>
        public Model(string name, Brand brand)
        {
            this.name = name;
            this.brand = brand;
        }
    }
}