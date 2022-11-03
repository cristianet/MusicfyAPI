using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicfyAPI.Data;
using MusicfyAPI.Data.Entities;

namespace MusicfyAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Artists")]
    public class ArtistsController : Controller
    {
        private MusicDbContext dbContext;
        public ArtistsController(MusicDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        // GET: api/Artists
        [HttpGet]
        public IActionResult Get()
        {
            var artists = dbContext.Artists;
            if (artists == null) return NotFound(new BaseAPIResponse()
            {
                IsSuccess = false,
                Message = "Herhangi bir veri not found"
            });
            return Ok(new BaseAPIResponse()
            {
                IsSuccess = true,
                Result = artists
            });
        }

        // GET: api/Artists/5
        [HttpGet("{id}")]
        public IActionResult Get(int? id)
        {
            if (!id.HasValue) return BadRequest(new BaseAPIResponse()
            {
                IsSuccess = false,
                Message = "You Have Entered Invalid Id"
            });
            var artist = dbContext.Artists.SingleOrDefault(a => a.Id == id.Value);
            if (artist == null) return NotFound(new BaseAPIResponse()
            {
                Message = $"{id.Value} not found"
            });

            return Ok(new BaseAPIResponse()
            {
                IsSuccess = true,
                Result = artist
            });
        }
        
        // POST: api/Artists
        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody]Artist artist)
        {
            if (artist == null) return BadRequest(new BaseAPIResponse()
            {
                Message = "Cannot be blank",
                IsSuccess = false
            });
            dbContext.Artists.Add(artist);
            try
            {
                dbContext.SaveChanges();
                return Ok(new BaseAPIResponse()
                {
                    IsSuccess = true,
                    Result = artist,
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
        
        // PUT: api/Artists/5
        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Put(int? id, [FromBody]Artist newArtist)
        {
            if (!id.HasValue) return BadRequest(new BaseAPIResponse()
            {
                IsSuccess = false,
                Message = "You Have Entered Invalid Id"
            });
            newArtist.Id = id.Value;
            dbContext.Artists.Update(newArtist);

            try
            {
                dbContext.SaveChanges();
                return Ok(new BaseAPIResponse()
                {
                    IsSuccess = true,
                    Message = "Başarıyla Updated",
                    Result=newArtist
                });
            }
            catch (Exception)
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
        [Authorize]
        public IActionResult Delete(int? id)
        {
            if (!id.HasValue) return BadRequest(new BaseAPIResponse()
            {
                IsSuccess = false,
                Message = "You Have Entered Invalid Id"
            });
            dbContext.Artists.Remove(new Artist()
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
