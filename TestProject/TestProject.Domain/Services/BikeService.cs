using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TestProject.Domain.Entities;
using TestProject.Domain.Interfaces.Repositories;
using TestProject.Domain.Interfaces.Services;

namespace TestProject.Domain.Services
{
    public class BikeService : IBikeService
    {
        private readonly IBikeRepository _bikeRepository;
        private readonly ILogger<BikeService> _logger;

        public BikeService( IBikeRepository bikeRepository, ILogger<BikeService> logger )
        {
            _bikeRepository = bikeRepository;
            _logger = logger;
        }

        public async Task AddBikeAsync( Bike bike )
        {
            var existingBike = await _bikeRepository.FindBikeByPlateAsync( bike.Plate );
            if( existingBike != null )
            {
                _logger.LogError( "Attempted to add a bike with duplicate plate: {Plate}", bike.Plate );
                throw new InvalidOperationException( $"A bike with plate {bike.Plate} already exists." );
            }

            await _bikeRepository.AddBikeAsync( bike );
        }

        public async Task<IEnumerable<Bike>> GetAllBikesAsync( string plate = null )
        {
            if( string.IsNullOrEmpty( plate ) )
            {
                return await _bikeRepository.GetAllAsync( plate );
            }
            else
            {
                var bike = await _bikeRepository.FindBikeByPlateAsync( plate );
                return bike != null ? new[] { bike } : Enumerable.Empty<Bike>();
            }
        }

        public async Task<Bike> GetBikesBiIdAsync( int bikeId )
        {

            return await _bikeRepository.GetBikeByIdAsync( bikeId );

        }

        public async Task UpdateBikePlateAsync( Bike bikeUpdate )
        {
            var bike = await _bikeRepository.GetBikeByIdAsync( bikeUpdate.Id );
            if( bike == null )
                throw new KeyNotFoundException( "Bike not found." );

            var existingBikeWithNewPlate = await _bikeRepository.FindBikeByPlateAsync( bikeUpdate.Plate );
            if( existingBikeWithNewPlate != null )
            {
                throw new InvalidOperationException( $"Another bike with plate {bikeUpdate.Plate} already exists." );
            }
            bike.Plate = bikeUpdate.Plate;
            await _bikeRepository.UpdateBikeAsync( bike );
        }

        public async Task DeleteBikeAsync( int bikeId )
        {
            // Additional checks can be implemented here, for example, ensuring the bike is not currently rented out.
            await _bikeRepository.DeleteBikeAsync( bikeId );
        }
    }

}
