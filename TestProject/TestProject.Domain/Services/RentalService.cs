using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestProject.Domain.Entities;
using TestProject.Domain.Interfaces.Repositories;
using TestProject.Domain.Interfaces.Services;

namespace TestProject.Domain.Services
{
    public class RentalService : IRentalService
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly IBikeRepository _bikeRepository;
        private readonly IDeliveryDriverRepository _deliveryDriverRepository;
        // Assume repositories for bikes and delivery drivers are also injected.

        public RentalService( IRentalRepository rentalRepository, IBikeRepository bikeRepository,
           IDeliveryDriverRepository deliveryDriverRepository )
        {
            _rentalRepository = rentalRepository;
            _bikeRepository = bikeRepository;
            _deliveryDriverRepository = deliveryDriverRepository;
        }

        public async Task<Rental> RentBikeAsync( Rental rental )
        {
            if( rental == null )
                throw new ArgumentNullException( nameof( rental ) );
            var drive =  await _deliveryDriverRepository.GetByIdAsync(rental.DriverId );
            if( drive != null && drive.CNHType != "A" )
                throw new InvalidOperationException( "Only A type CNH can rent a bike." );

            var bikeAvailable = await _bikeRepository.CheckBikeAvailabilityAsync( rental.BikeId );
            if( !bikeAvailable )
                throw new InvalidOperationException( "Bike is currently not available for rental." );

            // Save rental details to the database
            await _rentalRepository.AddRentalAsync( rental );
            return rental;
        }

        public async Task<decimal> CalculateRentalCostAsync( int rentalId, DateTime returnDate )
        {
            var rental = await _rentalRepository.GetRentalByIdAsync( rentalId );
            if( rental == null )
                throw new Exception( "Rental not found." );

            var plannedDuration = ( rental.PredictedEndDate - rental.StartDate ).Days;
            var actualDuration = ( returnDate - rental.StartDate ).Days;

            decimal totalCost = 0;
            if( actualDuration <= plannedDuration )
            {
                // Calculate cost based on actual days rented
                totalCost = actualDuration * rental.DailyRate;
                // Calculate penalty for early return
                var remainingDays = plannedDuration - actualDuration;
                totalCost += CalculatePenalty( remainingDays, plannedDuration, totalCost );
            }
            else
            {
                // Calculate cost including additional days at a flat rate of $50 per day
                totalCost = plannedDuration * rental.DailyRate;
                totalCost += ( actualDuration - plannedDuration ) * 50; // Assume $50 per additional day
            }

            return totalCost;
        }

        private decimal CalculatePenalty( int remainingDays, int plannedDuration, decimal totalCost )
        {
            // Implement penalty calculation based on the rental plan
            // This is a simplified example. You'll need to adjust it according to the specific penalty rules.
            var penaltyRate = plannedDuration switch
            {
                7 => 0.2m,
                15 => 0.4m,
                _ => 0m
            };
            return totalCost * penaltyRate;
        }
    }
}
