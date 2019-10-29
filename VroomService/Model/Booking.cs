using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VroomService.Model
{
    public class Booking
    {
        public int id { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        private User user { get; set; }
        private Car car { get; set; }

    }
}