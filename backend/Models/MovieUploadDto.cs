using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class MovieUploadDto
    {
        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Summary { get; set; } = string.Empty;

        [Required]
        public IFormFile PosterFile { get; set; } = null!;

        [Required]
        public IFormFile TrailerFile { get; set; } = null!;
    }
}
