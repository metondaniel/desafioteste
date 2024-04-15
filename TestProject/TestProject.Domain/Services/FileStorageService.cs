using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using TestProject.Domain.Interfaces.Services;

namespace TestProject.Domain.Services
{
    public class FileStorageService : IFileStorageService
    {
        private readonly string _storageDirectory;
        public FileStorageService( IConfiguration configuration )
        {
            _storageDirectory = configuration.GetSection( "FileStorage:StorageDirectory" ).Value;
            if( !Directory.Exists( _storageDirectory ) )
            {
                Directory.CreateDirectory( _storageDirectory );
            }
        }

        public async Task<string> SaveFileAsync( Stream fileStream, string fileName, string ContentType )
        {
            var filePath = Path.Combine( _storageDirectory, fileName );
            using( var outputStream = new FileStream( filePath, FileMode.Create, FileAccess.Write ) )
            {
                await fileStream.CopyToAsync( outputStream );
            }
            return filePath;
        }
    }

}
