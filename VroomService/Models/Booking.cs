namespace VroomService.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Booking")]
    public partial class Booking
    {
        public int Id { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string State { get; set; }

        public int User_Id { get; set; }

        public int Car_Id { get; set; }

        public virtual Car Car { get; set; }

        public virtual User User { get; set; }
    }
}
