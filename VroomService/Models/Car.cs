namespace VroomService.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Xml.Serialization;

    [Table("Car")]
    [Serializable]
    public partial class Car
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Car()
        {
            Bookings = new List<Booking>();
        }

        public int Id { get; set; }

        [StringLength(100)]
        public string Name { get; set; }


        public int? Price { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public int? PlaceNb { get; set; }

        [XmlIgnore]
        public int Brand_Id { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [XmlIgnore]
        public virtual List<Booking> Bookings { get; set; }

        [ForeignKey("Brand_Id")]
        public virtual Brand Brand { get; set; }
    }
}
