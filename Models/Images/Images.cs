using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SimplyCrudAPI.Models.Images
{
    public class Images
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid idImages { get; set; }
        public string nameImages { get; set; }
        public byte[] imageData { get; set; }
        public string descriptionImages { get; set; }
        public string imagesCapacity { get; set; }
        public string imagesFormat { get; set; }
        public string fileImages { get; set; }
    }
}
