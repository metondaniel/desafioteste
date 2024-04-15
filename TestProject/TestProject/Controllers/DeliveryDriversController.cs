using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using TestProject.Domain.Entities;
using TestProject.Domain.Interfaces.Services;

namespace TestProject.Controllers
{
    [Route( "api/deliverydrivers" )]
    public class DeliveryDriversController : ControllerBase
    {
        private readonly IDeliveryDriverService _deliveryDriverService;

        public DeliveryDriversController( IDeliveryDriverService deliveryDriverService )
        {
            _deliveryDriverService = deliveryDriverService;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterDeliveryDriver( [FromBody] DeliveryDriver deliveryDriver )
        {
            try
            {
                await _deliveryDriverService.RegisterDeliveryDriverAsync( deliveryDriver );
                return CreatedAtAction( nameof( RegisterDeliveryDriver ), new { id = deliveryDriver.Id }, deliveryDriver );
            }
            catch( Exception ex )
            {
                return BadRequest( ex.Message );
            }
        }

        [HttpPost( "uploadcnh/{driverId}" )]
        public async Task<IActionResult> UploadCNH( int driverId, IFormFile file )
        {
            if( file == null || file.Length == 0 )
            {
                return BadRequest( "File is empty or not provided." );
            }

            try
            {
                // Implementation for CNH image upload
                await _deliveryDriverService.UploadCNHImageAsync( driverId, file.OpenReadStream(), file.ContentType );
                return Ok();
            }
            catch( KeyNotFoundException knfEx )
            {
                return NotFound( knfEx.Message );
            }
            catch( Exception ex )
            {
                return StatusCode( 500, ex.Message );
            }
        }
    }
}
