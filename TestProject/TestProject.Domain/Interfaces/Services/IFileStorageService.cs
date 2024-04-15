using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.Domain.Interfaces.Services
{
    public interface IFileStorageService
    {
        Task<string> SaveFileAsync( Stream fileStream, string fileName, string contentType );
    }
}
