using PlantCare.Entities.Dtos.HomeTip;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantCare.Entities.Dtos.Plant
{
    public class PlantViewDto
    {
        public string Id { get; set; } = "";
        public string Name { get; set; } = "";
        public IEnumerable<HomeTipViewDto>? HomeTips { get; set; }

        //extra property
        public int HometipCount => HomeTips?.Count() ?? 0;

    }
}
