using System;
using System.Collections.Generic;
using System.Text;

namespace TestProject.Domain.Entities
{
    public class DeliveryDriver
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CNPJ { get; set; }
        public DateTime BirthDate { get; set; }
        public string CNHNumber { get; set; }
        public string CNHType { get; set; }
        // Assuming the ImageCNHPath stores the location or URL of the CNH image in storage
        public string ImageCNHPath { get; set; }
    }
}
