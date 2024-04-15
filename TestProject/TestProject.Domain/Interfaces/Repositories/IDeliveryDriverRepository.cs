using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestProject.Domain.Entities;

namespace TestProject.Domain.Interfaces.Repositories
{
    public interface IDeliveryDriverRepository
    {
        Task AddDeliveryDriverAsync( DeliveryDriver deliveryDriver );
        Task<DeliveryDriver> FindDeliveryDriverByCNPJAsync( string cnpj );
        Task<DeliveryDriver> FindDeliveryDriverByCNHAsync( string cnhNumber );
        Task<DeliveryDriver> GetByIdAsync( int id );
        Task<DeliveryDriver> UpdateAsync( DeliveryDriver driver );
    }
}
