using PlantCare.Data;
using PlantCare.Entities.Dtos.HomeTip;
using PlantCare.Entities.Entity_Models;
using PlantCare.Logic.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantCare.Logic.Logic
{
    public class HomeTipLogic
    {
        Repository<HomeTip> repo;
        DtoProvider dtoProvider;

        public HomeTipLogic(Repository<HomeTip> repo, DtoProvider dtoProvider)
        {
            this.repo = repo;
            this.dtoProvider = dtoProvider;
        }

        public void AddHomeTip(HomeTipCreateDto dto, string userId)
        {
            var model = dtoProvider.Mapper.Map<HomeTip>(dto);
            model.UserId = userId;
            repo.Create(model);
        }
    }
}
