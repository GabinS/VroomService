using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VroomService.Models
{
    public class Brand
    {
        private int id { get; set; }
        private string name { get; set; }

        /// <summary>
        /// Brand constructor
        /// </summary>
        /// <param name="name"></param>
        public Brand(string name)
        {
            this.name = name;
        }
    }
}