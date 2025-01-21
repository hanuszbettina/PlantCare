using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantCare.Entities.Dtos.Plant
{
    public class PlantShortViewDto
    {
        public required string Id { get; set; } = "";
        public string Name { get; set; } = "";
        public string Water { get; set; } = "";
        public string Light { get; set; } = "";
    }
}
