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
    [Route("api/songs")]
    public class SongsController : Controller
    {
        private MusicDbContext dbContext;
        public SongsController(MusicDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        // GET: api/Songs
        [HttpGet]
        public IActionResult Get()
        {
            var songs = dbContext.Songs;
            if (songs == null || songs.Count() == 0) return NotFound(new BaseAPIResponse()
            {
                IsSuccess = false,
                Message = "Herhangi bir veri not found"
            });
            return Ok(new BaseAPIResponse()
            {
                IsSuccess = true,
                Result = songs
            });
        }

        // GET: api/Songs/5
        [HttpGet("{id}")]
        public IActionResult Get(int? id)
        {
            if (!id.HasValue) return BadRequest(new BaseAPIResponse()
            {
                IsSuccess = false,
                Message = "You Have Entered Invalid Id"
            });
            var song = dbContext.Songs.SingleOrDefault(s => s.Id == id.Value);
            if (song == null) return NotFound(new BaseAPIResponse()
            {
                Message = $"{id.Value} not found"
            });

            return Ok(new BaseAPIResponse()
            {
                IsSuccess = true,
                Result = song
            });
        }
        
        // POST: api/Songs
        [HttpPost]
        public IActionResult Post([FromBody]Song song)
        {
            if (song == null) return BadRequest(new BaseAPIResponse()
            {
                IsSuccess = false,
                Message = "Cannot be blank"
            });
            dbContext.Songs.Add(song);
            try
            {
                dbContext.SaveChanges();
                return Ok(new BaseAPIResponse()
                {
                    IsSuccess = true,
                    Result = song,
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
        
        // PUT: api/Songs/5
        [HttpPut("{id}")]
        public IActionResult Put(int? id, [FromBody]Song newSong)
        {
            if (!id.HasValue) return BadRequest(new BaseAPIResponse()
            {
                IsSuccess = false,
                Message = "You Have Entered Invalid Id"
            });
            newSong.Id = id.Value;
            dbContext.Songs.Update(newSong);

            try
            {
                dbContext.SaveChanges();
                return Ok(new BaseAPIResponse()
                {
                    IsSuccess = true,
                    Result = newSong,
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
        
        // DELETE: api/songs/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int? id)
        {
            if (!id.HasValue) return BadRequest(new BaseAPIResponse()
            {
                IsSuccess = false,
                Message = "You Have Entered Invalid Id"
            });
            dbContext.Songs.Remove(new Song()
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
