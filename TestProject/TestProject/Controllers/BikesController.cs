using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TestProject.Domain.Entities;
using TestProject.Domain.Interfaces.Services;

namespace TestProject.Controllers
{
    [ApiController]
    [Route( "api/bikes" )]
    public class BikesController : ControllerBase
    {
        private readonly IBikeService _bikeService;

        public BikesController( IBikeService bikeService )
        {
            _bikeService = bikeService;
        }

        [HttpPost]
        public async Task<IActionResult> AddBike( [FromBody] Bike bike )
        {
            try
            {
                await _bikeService.AddBikeAsync( bike );
                return CreatedAtAction( nameof( GetBikeById ), new { id = bike.Id }, bike );
            }
            catch( Exception ex )
            {
                // Log error
                return BadRequest( ex.Message );
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBikes()
        {
            try
            {
                var bikes = await _bikeService.GetAllBikesAsync();
                return Ok( bikes );
            }
            catch( Exception ex )
            {
                // Log error
                return StatusCode( 500, ex.Message );
            }
        }

        [HttpGet( "{id}" )]
        public async Task<IActionResult> GetBikeById( int id )
        {
            try
            {
                var bike = await _bikeService.GetBikesBiIdAsync( id );
                if( bike == null )
                {
                    return NotFound();
                }
                return Ok( bike );
            }
            catch( Exception ex )
            {
                // Log error
                return StatusCode( 500, ex.Message );
            }
        }

        [HttpPut( "{id}" )]
        public async Task<IActionResult> UpdateBike( int id, [FromBody] Bike bike )
        {
            if( id != bike.Id )
            {
                return BadRequest( "ID mismatch" );
            }

            try
            {
                await _bikeService.UpdateBikePlateAsync( bike );
                
                return NoContent();
            }
            catch( Exception ex )
            {
                // Log error
                return StatusCode( 500, ex.Message );
            }
        }

        [HttpDelete( "{id}" )]
        public async Task<IActionResult> DeleteBike( int id )
        {
            try
            {
                await _bikeService.DeleteBikeAsync( id );

                return NoContent();
            }
            catch( Exception ex )
            {
                // Log error
                return StatusCode( 500, ex.Message );
            }
        }

    }
}