using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantCare.Entities.Dtos.HomeTip
{
    public class HomeTipCreateDto
    {
        public required string PlantId { get; set; } = "";

        [MinLength(10)]
        [MaxLength(500)]
        public required string Text { get; set; } = "";
    }
}
