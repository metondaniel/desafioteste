using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using TestProject.Domain.Entities;
using TestProject.Domain.Interfaces.Repositories;

namespace TestProject.Repository
{
    public class BikeRepository : IBikeRepository
    {
        private readonly IMongoCollection<Bike> _bikes;

        public BikeRepository( IMongoDatabase database )
        {
            _bikes = database.GetCollection<Bike>( "Bikes" );
        }

        public async Task AddBikeAsync( Bike bike )
        {
            await _bikes.InsertOneAsync( bike );
        }

        public async Task<IEnumerable<Bike>> GetAllAsync( string plate = null )
        {
            return await _bikes.Find( _ => true ).ToListAsync();
        }

        public async Task<Bike> GetBikeByIdAsync( int bikeId )
        {
            return await _bikes.Find( bike => bike.Id == bikeId ).FirstOrDefaultAsync();
        }

        public async Task<Bike> FindBikeByPlateAsync( string plate )
        {
            return await _bikes.Find( bike => bike.Plate == plate ).FirstOrDefaultAsync();
        }

        public async Task UpdateBikeAsync( Bike bikeUpdate)
        {
            var updateDefinition = Builders<Bike>.Update.Set( bike => bike.Plate, bikeUpdate.Plate );
            await _bikes.UpdateOneAsync( bike => bike.Id == bikeUpdate.Id, updateDefinition );
        }

        public async Task DeleteBikeAsync( int bikeId )
        {
            await _bikes.DeleteOneAsync( bike => bike.Id == bikeId );
        }

        public async Task<bool> CheckBikeAvailabilityAsync( int bikeId )
        {
            return (await _bikes.FindAsync( bike => bike.Id == bikeId && bike.IsRented == false )).Any();
        }
    }

}
