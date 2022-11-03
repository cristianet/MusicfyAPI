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
    [Route("api/Lyrics")]
    public class LyricsController : Controller
    {
        private MusicDbContext dbContext;
        public LyricsController(MusicDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        // GET: api/Lyrics
        [HttpGet]
        public IActionResult Get()
        {
            var lyrics = dbContext.Lyrics;
            if (lyrics == null) return NotFound(new BaseAPIResponse()
            {
                IsSuccess = false,
                Message = "No Data"
            });
            return Ok(new BaseAPIResponse()
            {
                IsSuccess = true,
                Result = lyrics
            });
        }

        // GET: api/Lyrics/5
        [HttpGet("{id}")]
        public IActionResult Get(int? id)
        {
            if (!id.HasValue) return BadRequest(new BaseAPIResponse()
            {
                IsSuccess = false,
                Message = "You Have Entered Invalid Id"
            });

            var lyric = dbContext.Lyrics.SingleOrDefault(l => l.Id == id.Value);
            if (lyric == null) return NotFound(new BaseAPIResponse()
            {
                Message = $"{id.Value} not found"
            });

            return Ok(new BaseAPIResponse()
            {
                IsSuccess = true,
                Result = lyric
            });
        }
        
        // POST: api/Lyrics
        [HttpPost]
        public IActionResult Post([FromBody]Lyric lyric)
        {
            if (lyric == null) return BadRequest(new BaseAPIResponse() {
                IsSuccess=false,
                Message="Cannot be blank"
            });
            dbContext.Lyrics.Add(lyric);

            try
            {
                dbContext.SaveChanges();
                return Ok(new BaseAPIResponse()
                {
                    IsSuccess = true,
                    Message = "Successfully added",
                    Result = lyric
                });
            }
            catch (Exception)
            {
                return BadRequest(new BaseAPIResponse()
                {
                    IsSuccess = false,
                    Message = "Something went wrong"
                });
            }
        }
        
        // PUT: api/Lyrics/5
        [HttpPut("{id}")]
        public IActionResult Put(int? id, [FromBody]Lyric newLyric)
        {
            if (!id.HasValue) return BadRequest(new BaseAPIResponse()
            {
                IsSuccess = false,
                Message = "You Have Entered Invalid Id"
            });
            newLyric.Id = id.Value;
            dbContext.Lyrics.Update(newLyric);

            try
            {
                dbContext.SaveChanges();
                return Ok(new BaseAPIResponse()
                {
                    IsSuccess = true,
                    Result = newLyric,
                    Message = "başarıyla Updated"
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

            dbContext.Lyrics.Remove(new Lyric()
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
