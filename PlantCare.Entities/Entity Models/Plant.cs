using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Principal;
using PlantCare.Entities.Helpers;

namespace PlantCare.Entities.Entity_Models
{
    public class Plant : IIdEntity
    {
        public Plant(string name, string water, string light)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            Water = water;
            Light = light;
            HomeTips = new HashSet<HomeTip>();
        }

        [StringLength(50)]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        [StringLength(200)]
        public string Name { get; set; }

        [StringLength(20)]
        public string Water { get; set; } //daily, weekly, monthly

        [StringLength(20)]
        public string Light { get; set; } //daily, weekly, monthly

        [NotMapped]
        public virtual ICollection<HomeTip>? HomeTips { get; set; }
    }
}
