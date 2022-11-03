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
    [Route("api/Albums")]
    public class AlbumsController : Controller
    {
        private MusicDbContext dbContext;
        public AlbumsController(MusicDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// Get Album
        /// </summary>
        /// <returns>All Albums</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     
        ///     GET api/Albums
        ///     
        /// </remarks>
        /// <response code="200"></response>
        [HttpGet]
        public IActionResult Get()
        {
            var albums = dbContext.Albums;
            if (albums == null) return NotFound(new BaseAPIResponse()
            {
                IsSuccess = false,
                Message = "No Data"
            });
            return Ok(new BaseAPIResponse()
            {
                IsSuccess=true,
                Result=albums
            });
        }

        /// <summary>
        /// Get Album by Id
        /// </summary>
        /// <param name="id" type="int">Id Album</param>
        /// <returns>Album by Id</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     By Id (int)
        ///     GET api/Albums
        ///     
        /// </remarks>
        /// <response code="200"></response>
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int? id)
        {
            if (!id.HasValue) return BadRequest(new BaseAPIResponse()
            {
                IsSuccess = false,
                Message = "You Have Entered Invalid Id"
            });
            var album = dbContext.Albums.SingleOrDefault(a => a.Id == id.Value);
            if (album == null) return NotFound(new BaseAPIResponse()
            {
                Message = $"{id.Value} not found"
            });

            return Ok(new BaseAPIResponse()
            {
                IsSuccess = true,
                Result = album
            });
        }

        /// <summary>
        /// Add Album
        /// </summary>
        /// <param name="album" type="Album">Album Info</param>
        /// <returns>Album by Id</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     Send Object Album
        ///    
        ///     POST api/Albums
        ///     
        /// </remarks>
        /// <response code="200"></response>
        [HttpPost]
        public IActionResult Post([FromBody]Album album)
        {
            if (album == null) return BadRequest(new BaseAPIResponse()
            {
                Message = "Cannot be blank",
                IsSuccess = false
            });
            dbContext.Albums.Add(album);
            try
            {
                dbContext.SaveChanges();
                return Ok(new BaseAPIResponse()
                {
                    IsSuccess = true,
                    Result = album,
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

        /// <summary>
        /// Update Album
        /// </summary>
        /// <param name="id" type="int">Id Album</param>
        /// <param name="newAlbum" type="Album">Album Info</param>
        /// <returns>Album</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     Send Object Album
        ///    
        ///     PUT api/Albums/5
        ///     
        /// </remarks>
        /// <response code="200"></response>
        [HttpPut("{id}")]
        public IActionResult Put(int? id, [FromBody]Album newAlbum)
        {
            if (!id.HasValue) return BadRequest(new BaseAPIResponse()
            {
                IsSuccess = false,
                Message = "You Have Entered Invalid Id"
            });

            newAlbum.Id = id.Value;
            dbContext.Albums.Update(newAlbum);

            try
            {
                dbContext.SaveChanges();
                return Ok(new BaseAPIResponse()
                {
                    IsSuccess = true,
                    Result = newAlbum,
                    Message = "Updated"
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

        /// <summary>
        /// Delete Album
        /// </summary>
        /// <param name="id" type="int">Id Album</param>        
        /// <returns>Album</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     Send Object Album
        ///    
        ///     DELETE api/Albums/5
        ///     
        /// </remarks>
        /// <response code="200"></response>
        [HttpDelete("{id}")]
        public IActionResult Delete(int? id)
        {
            if (!id.HasValue) return BadRequest(new BaseAPIResponse()
            {
                IsSuccess = false,
                Message = "You Have Entered Invalid Id"
            });

            dbContext.Albums.Remove(new Album()
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
            catch (Exception)
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
