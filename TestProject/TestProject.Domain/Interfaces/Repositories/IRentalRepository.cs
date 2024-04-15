using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestProject.Domain.Entities;

namespace TestProject.Domain.Interfaces.Repositories
{
    public interface IRentalRepository
    {
        Task AddRentalAsync( Rental rental );
        Task<Rental> GetRentalByIdAsync( int rentalId );
    }
}
