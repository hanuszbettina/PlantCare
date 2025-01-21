using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantCare.Entities.Dtos.Plant
{
    public class PlantCreateUpdateDto
    {
        
        public required string Name { get; set; } = "";
        public required string Water { get; set; } = "";
        public required string Light { get; set; } = "";
    }
}
