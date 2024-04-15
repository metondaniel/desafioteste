using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using TestProject.Domain.Entities;
using TestProject.Domain.Interfaces.Repositories;
using TestProject.Domain.Interfaces.Services;

namespace TestProject.Domain.Services
{
    public class DeliveryDriverService : IDeliveryDriverService
    {
        private readonly IDeliveryDriverRepository _driverRepository;
        private readonly IFileStorageService _fileStorageService;
        private readonly ILogger<DeliveryDriverService> _logger;

        public DeliveryDriverService( IDeliveryDriverRepository driverRepository, 
            IFileStorageService fileStorageService, ILogger<DeliveryDriverService> logger )
        {
            _driverRepository = driverRepository;
            _fileStorageService = fileStorageService;
            _logger = logger;
        }

        public async Task RegisterDeliveryDriverAsync( DeliveryDriver deliveryDriver )
        {
            var existingDriverByCNPJ = await _driverRepository.FindDeliveryDriverByCNPJAsync( deliveryDriver.CNPJ );
            if( existingDriverByCNPJ != null )
            {
                throw new InvalidOperationException( $"A delivery driver with CNPJ {deliveryDriver.CNPJ} already exists." );
            }

            var existingDriverByCNH = await _driverRepository.FindDeliveryDriverByCNHAsync( deliveryDriver.CNHNumber );
            if( existingDriverByCNH != null )
            {
                throw new InvalidOperationException( $"A delivery driver with CNH Number {deliveryDriver.CNHNumber} already exists." );
            }

            // Validate CNH Type
            if( !new[] { "A", "B", "A+B" }.Contains( deliveryDriver.CNHType ) )
            {
                throw new ArgumentException( "Invalid CNH Type." );
            }

            await _driverRepository.AddDeliveryDriverAsync( deliveryDriver );
        }

        public async Task UploadCNHImageAsync( int driverId, Stream imageStream, string contentType )
        {
            var deliveryDriver = await _driverRepository.GetByIdAsync( driverId );
            if( deliveryDriver == null )
            {
                throw new KeyNotFoundException( "Delivery driver not found." );
            }

            if( !new[] { "image/png", "image/bmp" }.Contains( contentType ) )
            {
                throw new ArgumentException( "Unsupported file type." );
            }

            var filePath = await _fileStorageService.SaveFileAsync( imageStream, $"{driverId}_{Guid.NewGuid()}", contentType );
            deliveryDriver.ImageCNHPath = filePath;
            await _driverRepository.UpdateAsync( deliveryDriver );

        }
    }

}
