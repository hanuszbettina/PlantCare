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
    public class HomeTip : IIdEntity
    {
        public HomeTip(string plantId, string text)
        {
            PlantId = plantId;
            Text = text;
        }

        [StringLength(50)]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        [StringLength(50)]
        public string PlantId { get; set; }
        public virtual Plant? Plant { get; set; }

        [StringLength(200)]
        public string Text { get; set; }

        [StringLength(50)]
        public string UserId { get; set; }
    }
}
