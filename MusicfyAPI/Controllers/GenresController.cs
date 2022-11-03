using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicfyAPI.Data;
using MusicfyAPI.Data.Entities;

namespace MusicfyAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Genres")]
    public class GenresController : Controller
    {
        private MusicDbContext dbContext;
        public GenresController(MusicDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // GET: api/Genres
        [HttpGet]
        public IActionResult Get()
        {
            var genres = dbContext.Genres;
            if (genres == null) return NotFound(new BaseAPIResponse()
            {
                Message = "No Data"
            });
            return Ok(new BaseAPIResponse()
            {
                IsSuccess = true,
                Result = genres
            });
        }

        // GET: api/Genres/5
        [HttpGet("{id}")]
        public IActionResult Get(int? id)
        {
            if (!id.HasValue) return BadRequest(new BaseAPIResponse()
            {
                IsSuccess = false,
                Message = "You Have Entered Invalid Id"
            });

            var genre = dbContext.Genres.SingleOrDefault(g => g.Id == id.Value);
            if (genre == null) return NotFound(new BaseAPIResponse()
            {
                Message = $"{id.Value} not found"
            });

            return Ok(new BaseAPIResponse()
            {
                IsSuccess = true,
                Result = genre
            });
        }

        // POST: api/Genres
        [HttpPost]
        public IActionResult Post([FromBody]Genre genre)
        {
            if (genre == null) return BadRequest(new BaseAPIResponse()
            {
                Message = "Cannot be blank",
                IsSuccess = false
            });
            dbContext.Genres.Add(genre);
            try
            {
                dbContext.SaveChanges();
                return Ok(new BaseAPIResponse()
                {
                    IsSuccess = true,
                    Result = genre,
                    Message = "Successfully added"
                });
            }
            catch 
            {
                return BadRequest(new BaseAPIResponse()
                {
                    Message = "Something went wrong",
                    IsSuccess = false
                });
            }
        }

        // PUT: api/Genres/5
        [HttpPut("{id}")]
        public IActionResult Put(int? id, [FromBody]Genre newGenre)
        {
            if (!id.HasValue) return BadRequest(new BaseAPIResponse()
            {
                IsSuccess = false,
                Message = "You Have Entered Invalid Id"
            });
            newGenre.Id = id.Value;
            dbContext.Genres.Update(newGenre);
            try
            {
                dbContext.SaveChanges();
                return Ok(new BaseAPIResponse()
                {
                    IsSuccess = true,
                    Result = newGenre,
                    Message = "Updated"
                });
            }
            catch 
            {
                return BadRequest(new BaseAPIResponse()
                {
                    Message = "Something went wrong",
                    IsSuccess = false
                });
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int? id)
        {
            if (!id.HasValue) return BadRequest(new BaseAPIResponse()
            {
                IsSuccess = false,
                Message = "You Have Entered Invalid Id"
            });
            dbContext.Genres.Remove(new Genre()
            {
                Id = id.Value
            });
            try
            {
                dbContext.SaveChanges();
                return Ok(new BaseAPIResponse()
                {
                    IsSuccess = true,
                    Message = "Successfully deleted"
                });
            }
            catch 
            {
                return BadRequest(new BaseAPIResponse()
                {
                    Message = "Something went wrong",
                    IsSuccess = false
                });
            }
        }
    }
}
