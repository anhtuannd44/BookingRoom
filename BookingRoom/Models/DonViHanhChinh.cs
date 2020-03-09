namespace BookingRoom.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class Provinces
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(250)]
        public string Name { get; set; }

        public virtual ICollection<Districts> Districts { get; set; }
    }
    public class Districts
    {

        [Key]
        public int Id { get; set; }

        [StringLength(250)]
        public string Name { get; set; }

        [ForeignKey("Provinces")]
        public int ProvinceId { get; set; }

        public virtual Provinces Provinces { get; set; }

        public virtual ICollection<Wards> Wards { get; set; }
    }

    public class Wards
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [ForeignKey("Districts")]
        public int DistrictID { get; set; }

        public virtual Districts Districts { get; set; }
    }
}
