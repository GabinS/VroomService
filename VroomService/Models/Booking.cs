using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VroomService.Models
{
    public class Booking
    {
        public int id { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        private User user { get; set; }
        private Car car { get; set; }

        /// <summary>
        /// Booking constructor
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="user"></param>
        /// <param name="car"></param>
        public Booking(DateTime startDate, DateTime endDate, User user, Car car)
        {
            this.startDate = startDate;
            this.endDate = endDate;
            this.user = user;
            this.car = car;
        }

    }
}