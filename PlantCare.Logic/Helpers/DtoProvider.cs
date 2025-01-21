using AutoMapper;
using Microsoft.AspNetCore.Identity;
using PlantCare.Data;
using PlantCare.Entities;
using PlantCare.Entities.Dtos.Plant;
using PlantCare.Entities.Dtos.HomeTip;
using PlantCare.Entities.Dtos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlantCare.Entities.Entity_Models;
using AutoMapper.Internal.Mappers;

namespace PlantCare.Logic.Helpers
{
    public class DtoProvider
    {
        UserManager<AppUser> userManager;

        public Mapper Mapper { get; }

        public DtoProvider(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Plant, PlantShortViewDto>();

                cfg.CreateMap<AppUser, UserViewDto>()
               .AfterMap((src, dest) =>
               {
                   dest.IsAdmin = userManager.IsInRoleAsync(src, "Admin").Result;
               });


                cfg.CreateMap<Plant, PlantViewDto>();
                cfg.CreateMap<PlantCreateUpdateDto, Plant>();
                cfg.CreateMap<HomeTipCreateDto, HomeTip>();
                cfg.CreateMap<HomeTip, HomeTipViewDto>()
                .AfterMap((src, dest) =>
                {
                    var user = userManager.Users.First(u => u.Id == src.UserId);
                    dest.UserFullName = user.LastName! + " " + user.FirstName;
                });
            });

            Mapper = new Mapper(config);
        }
    }
}
