using AutoMapper;
using BookStoreAPI.DbContext;
using BookStoreAPI.DTOS;
using BookStoreAPI.Models;
using Humanizer.Localisation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {

        private readonly BookstoreDbContext _context;
        private readonly IMapper _mapper;

        public GenreController(BookstoreDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/genres
        [HttpGet]
        public IActionResult GetGenres()
        {
            var genres = _context.Genres.ToList();
            var genresDTO = _mapper.Map<List<GenreDTO>>(genres);
            return Ok(genresDTO);
        }


        // GET: api/genre/1
        [HttpGet("{id}")]
        public IActionResult GetGenre(int id)
        {
            var genre = _context.Genres.Find(id);

            if (genre == null)
            {
                return NotFound();
            }
            var genreDTO = _mapper.Map<List<GenreDTO>>(genre);
            return Ok(genreDTO);
        }


        // POST: api/genre
        [HttpPost]
        public async Task< IActionResult> CreateGenre([FromBody] GenreDTO genreDTOS)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var genre = _mapper.Map<Genre>(genreDTOS);
            _context.Genres.Add(genre);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGenre", new { id = genreDTOS.Name }, genreDTOS);
        }

        // PATCH: api/genre/1
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchGenre(int id, [FromBody] JsonPatchDocument<Genre> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest();
            }

            var genre = await _context.Genres.FindAsync(id);            


            if (genre == null)
            {
                return NotFound();
            }

            patchDocument.ApplyTo(genre, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _context.SaveChangesAsync();

            return NoContent();
        }


        // PUT: api/genre/1
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGenre(int id, [FromBody] Genre genre)
        {
            if (id != genre.Id)
            {
                return BadRequest();
            }

            _context.Entry(genre).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GenreExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/genre/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGenre(int id)
        {
            var genre = _context.Genres.Find(id);

            if (genre == null)
            {
                return NotFound();
            }

            _context.Genres.Remove(genre);
            await _context.SaveChangesAsync();

            return Ok(genre);
        }

        private bool GenreExists(int id)
        {
            return _context.Genres.Any(g => g.Id == id);
        }

    }
}
