using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestProject.Domain.Entities;

namespace TestProject.Domain.Interfaces.Repositories
{
    public interface IBikeRepository
    {
        Task AddBikeAsync( Bike bike );
        Task<Bike> FindBikeByPlateAsync( string plate );
        Task<IEnumerable<Bike>> GetAllAsync( string plate );
        Task UpdateBikeAsync( Bike bike );
        Task DeleteBikeAsync( int bikeId );
        Task<Bike> GetBikeByIdAsync( int bikeId );
        Task<bool> CheckBikeAvailabilityAsync( int bikeId );
    }
}
