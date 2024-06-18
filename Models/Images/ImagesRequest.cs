namespace SimplyCrudAPI.Models.Images
{
    public class ImagesRequest
    {
        public string nameImages { get; set; }
        public IFormFile fileImages { get; set; }
        public string descriptionImages { get; set; }
    }
}
