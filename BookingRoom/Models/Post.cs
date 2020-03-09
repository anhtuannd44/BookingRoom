namespace BookingRoom.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Post")]
    public partial class Post
    {
        [Key]
        public int Post_ID { get; set; }

        [Required]
        public string Post_Title { get; set; }

        [Required]
        public string Post_Content { get; set; }

        public DateTime Post_Time { get; set; }

        [Required]
        public string Post_Status { get; set; }

        [Required]
        public string Post_Cover { get; set; }
    }
}
