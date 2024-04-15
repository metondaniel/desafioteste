using System;
using System.Collections.Generic;
using System.Text;

namespace TestProject.Domain.Entities
{
    public class Rental
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime PredictedEndDate { get; set; }
        public decimal DailyRate { get; set; }
        public int DriverId { get; set; }
        public int BikeId { get; set; }
    }
}
