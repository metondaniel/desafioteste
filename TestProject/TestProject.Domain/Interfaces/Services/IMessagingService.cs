using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.Domain.Interfaces.Services
{
    public interface IMessagingService
    {
        Task PublishMessageAsync( string message );
    }
}
