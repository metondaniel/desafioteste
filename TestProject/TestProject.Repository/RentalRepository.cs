using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using TestProject.Domain.Entities;
using TestProject.Domain.Interfaces.Repositories;

namespace TestProject.Repository
{
    public class RentalRepository : IRentalRepository
    {
        private readonly IMongoCollection<Rental> _rental;

        public RentalRepository( IMongoDatabase database )
        {
            _rental = database.GetCollection<Rental>( "Rentals" );
        }
        public async Task AddRentalAsync( Rental rental )
        {
            await _rental.InsertOneAsync( rental );
        }

        public async Task<Rental> GetRentalByIdAsync( int rentalId )
        {
            return await _rental.Find( rental => rental.Id == rentalId ).FirstOrDefaultAsync();
        }
    }
}
