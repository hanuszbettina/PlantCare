using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantCare.Entities.Dtos.User
{
    public class UserLoginDto
    {
        [MinLength(5)]
        public required string UserName { get; set; } = "";

        [MinLength(10)]
        public required string Password { get; set; } = "";
    }
}
