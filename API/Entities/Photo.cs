using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Photos")] //instead of calling the table just "Photo"
    public class Photo 
    {
        public int Id {get; set;}
        public string Url { get; set; }
        public bool IsMain { get; set; }
        public string PublicId { get; set; } //storing photos from cloudinary using the publicId

        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}