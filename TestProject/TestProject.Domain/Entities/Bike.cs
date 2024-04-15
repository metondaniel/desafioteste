using System;

namespace TestProject.Domain.Entities
{
    public class Bike
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public string Model { get; set; }
        public string Plate { get; set; }
        public bool IsRented { get; set; }
    }
}
