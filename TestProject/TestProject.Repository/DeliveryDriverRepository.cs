using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Driver;
using System.Threading.Tasks;
using TestProject.Domain.Entities;
using TestProject.Domain.Interfaces.Repositories;

namespace TestProject.Repository
{
    public class DeliveryDriverRepository : IDeliveryDriverRepository
    {
        private readonly IMongoCollection<DeliveryDriver> _deliveryDrivers;

        public DeliveryDriverRepository( IMongoDatabase database )
        {
            _deliveryDrivers = database.GetCollection<DeliveryDriver>( "DeliveryDrivers" );
        }

        public async Task AddDeliveryDriverAsync( DeliveryDriver deliveryDriver )
        {
            await _deliveryDrivers.InsertOneAsync( deliveryDriver );
        }

        public async Task<DeliveryDriver> FindDeliveryDriverByCNPJAsync( string cnpj )
        {
            return await _deliveryDrivers.Find( driver => driver.CNPJ == cnpj ).FirstOrDefaultAsync();
        }

        public async Task<DeliveryDriver> FindDeliveryDriverByCNHAsync( string cnhNumber )
        {
            return await _deliveryDrivers.Find( driver => driver.CNHNumber == cnhNumber ).FirstOrDefaultAsync();
        }

        public async Task<DeliveryDriver> GetByIdAsync( int driverId )
        {
            return await _deliveryDrivers.Find( driver => driver.Id == driverId ).FirstOrDefaultAsync();
        }

        public async Task<DeliveryDriver> UpdateAsync( DeliveryDriver deliveryDriver )
        {
            await _deliveryDrivers.ReplaceOneAsync( driver => driver.Id == deliveryDriver.Id, deliveryDriver );
            return deliveryDriver;
        }

    }

}
