using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestProject.Domain.Entities;

namespace TestProject.Domain.Interfaces.Services
{
    public interface IBikeService
    {
        Task AddBikeAsync( Bike bike );
        Task<IEnumerable<Bike>> GetAllBikesAsync( string plate = null );
        Task UpdateBikePlateAsync( Bike bike );
        Task DeleteBikeAsync( int bikeId );
        Task<Bike> GetBikesBiIdAsync( int bikeId );
    }
}
