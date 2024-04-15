using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using TestProject.Domain.Interfaces.Repositories;

namespace TestProject.Repository
{
    public class FileStorageRepository : IFileStorageRepository
    {
        public Task<string> SaveFileAsync( Stream fileStream, string fileName, string contentType )
        {
            throw new NotImplementedException();
        }
    }
}
