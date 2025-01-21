using PlantCare.Data;
using PlantCare.Entities.Dtos.Plant;
using PlantCare.Entities.Entity_Models;
using PlantCare.Logic.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantCare.Logic.Logic
{
    public class PlantLogic
    {
        Repository<Plant> repo;
        DtoProvider dtoProvider;

        public PlantLogic(Repository<Plant> repo, DtoProvider dtoProvider)
        {
            this.repo = repo;
            this.dtoProvider = dtoProvider;
        }

        public void AddPlant(PlantCreateUpdateDto dto)
        {
            Plant p = dtoProvider.Mapper.Map<Plant>(dto);

            // csak akkor mentsük el, hogyha nincs ilyen nevű
            if (repo.GetAll().FirstOrDefault(x => x.Name == p.Name) == null)
            {
                repo.Create(p);
            }
            else
            {
                throw new ArgumentException("Ilyen névvel már van növény!");
            }
        }

        public IEnumerable<PlantShortViewDto> GetAllPlants() 
        {
            return repo.GetAll().Select(x =>
                dtoProvider.Mapper.Map<PlantShortViewDto>(x)
            );
        }

        public void DeletePlant(string id)
        {
            repo.DeleteById(id);
        }

        public void UpdatePlant(string id, PlantCreateUpdateDto dto)
        {
            var old = repo.FindById(id);
            dtoProvider.Mapper.Map(dto, old);
            repo.Update(old);
        }

        public PlantViewDto GetPlant(string id)
        {
            var model = repo.FindById(id);
            return dtoProvider.Mapper.Map<PlantViewDto>(model);
        }
    }
}
