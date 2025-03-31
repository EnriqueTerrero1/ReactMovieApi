using System.ComponentModel.DataAnnotations;

namespace MoviesApi.Entities
{
    public class Genre
    {
        public int Id { get; set; }

        [StringLength(10)]
        
        public string Name { get; set; }
    }
}
