using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestProject.Domain.Entities;

namespace TestProject.Domain.Interfaces.Services
{
    public interface IRentalService
    {
        Task<Rental> RentBikeAsync( Rental rental );
        Task<decimal> CalculateRentalCostAsync( int rentalId, DateTime returnDate );
    }
}
