using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using TestProject.Domain.Entities;

namespace TestProject.Domain.Interfaces.Services
{
    public interface IDeliveryDriverService
    {
        Task RegisterDeliveryDriverAsync( DeliveryDriver deliveryDriver );
        Task UploadCNHImageAsync( int driverId, Stream imageStream, string contentType );
    }
}
