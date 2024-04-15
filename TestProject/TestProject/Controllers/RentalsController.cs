using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestProject.Domain.Entities;
using TestProject.Domain.Interfaces.Services;

[Route( "api/rentals" )]
[ApiController]
public class RentalsController : ControllerBase
{
    private readonly IRentalService _rentalService;

    public RentalsController( IRentalService rentalService )
    {
        _rentalService = rentalService;
    }

    [HttpPost]
    public async Task<IActionResult> RentBike( [FromBody] Rental rental )
    {
        try
        {
            var createdRental = await _rentalService.RentBikeAsync( rental );
            return CreatedAtAction( nameof( RentBike ), new { id = createdRental.Id }, createdRental );
        }
        catch( Exception ex )
        {
            return BadRequest( ex.Message );
        }
    }

    [HttpPost( "{id}/return" )]
    public async Task<IActionResult> ReturnBike( int id, [FromBody] ReturnRentalInfo returnInfo )
    {
        try
        {
            var finalCost = await _rentalService.CalculateRentalCostAsync( id, returnInfo.ReturnDate );
            return Ok( new { FinalCost = finalCost } );
        }
        catch( KeyNotFoundException )
        {
            return NotFound( "Rental not found." );
        }
        catch( Exception ex )
        {
            return BadRequest( ex.Message );
        }
    }

    // Define the ReturnRentalInfo DTO if not already defined
    public class ReturnRentalInfo
    {
        public DateTime ReturnDate { get; set; }
    }

    // Additional methods for updating and deleting rentals if necessary
}
