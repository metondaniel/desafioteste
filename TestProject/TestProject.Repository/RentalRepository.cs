using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestProject.Domain.Entities;
using TestProject.Domain.Interfaces.Repositories;

namespace TestProject.Repository
{
    public class RentalRepository : IRentalRepository
    {
        public Task AddRentalAsync( Rental rental )
        {
            throw new NotImplementedException();
        }

        public Task<Rental> GetRentalByIdAsync( int rentalId )
        {
            throw new NotImplementedException();
        }
    }
}
