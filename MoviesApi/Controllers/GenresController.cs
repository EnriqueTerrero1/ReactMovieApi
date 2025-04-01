using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesApi.DTOS;
using MoviesApi.Entities;
using MoviesApi.Helper;
using System.Linq;

namespace MoviesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public GenresController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<List<GenreDTO>>> GetGenres([FromQuery] PaginationDTO paginationDTO)
        {
            var queryable = context.genres.AsQueryable();
            await HttpContext.InsertParametersPaginationInHeader(queryable);
            var genres = await queryable.OrderBy(x => x.Name)
                 .Paginate(paginationDTO)
                 .ToListAsync();
            var genresDTO = mapper.Map<List<GenreDTO>>(genres);
            return Ok(genresDTO);

        }

        [HttpGet("{id:int}")]

        public async Task<ActionResult<Genre>> GetGenreByIdAsync(int id)
        
        
        {

            var genre = await context.genres.Where(x => x.Id == id).FirstOrDefaultAsync();

            return Ok( mapper.Map<GenreDTO>(genre));

        }



        [HttpPost]
        public async Task<ActionResult> Post([FromBody] GenreCreationDTO genreDTO)
        {
            try
            {
                var genre = mapper.Map<Genre>(genreDTO);

                context.genres.Add(genre);
                await context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception)
            {

                throw new ArgumentException("Error Adding a Genre");
            }


        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id,[FromBody] GenreCreationDTO genreDto)
        {
            var genre = await context.genres.FirstOrDefaultAsync(x => x.Id == id);
            if(genre == null)
            {
                return NotFound();
            }
            genre = mapper.Map(genreDto, genre);
            await context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete]
        public void Delete()
        {

        }

    }
}
