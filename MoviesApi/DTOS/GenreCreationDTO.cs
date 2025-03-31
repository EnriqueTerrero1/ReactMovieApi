using System.ComponentModel.DataAnnotations;

namespace MoviesApi.DTOS
{
    public class GenreCreationDTO
    {
        [Required]
        [StringLength(10)]
        public string Name { get; set; }
    }
}
