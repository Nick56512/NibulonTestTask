using AutoMapper;
using Newtonsoft.Json;
using NubilonTestTask.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class GrainRepository : IGrainRepository
    {
        private readonly string jsonFileName;
        IMapper mapper;
        public GrainRepository(string json) { 
            jsonFileName= json;
            var mapConf = new MapperConfiguration(x => {
                x.CreateMap<Grain, Grain>();
            });
            mapper=new Mapper(mapConf);
        }

        private void SaveChanges(IEnumerable<Grain> grains)
        {
            string jsonText=JsonConvert.SerializeObject(grains, Formatting.Indented);
            File.WriteAllText(jsonFileName,jsonText);
        }
        public Grain Add(Grain newGrain)
        {
            var grains = GetAll().ToList();
            grains.Add(newGrain);
            SaveChanges(grains);
            return newGrain;
        }

        public Grain Delete(int id)
        {
            var grains = GetAll().ToList();
            var removedObj=grains.Find(x =>x.Id==id);
            grains.Remove(removedObj);
            SaveChanges(grains);
            return removedObj;
        }

        public Grain Get(int id)
        {
            var grains = GetAll().ToList();
            var obj = grains.Find(x => x.Id == id);
            return obj;
        }

        public IEnumerable<Grain> GetAll()
        {
            string text=File.ReadAllText(jsonFileName);
            var res=JsonConvert.DeserializeObject<IEnumerable<Grain>>(text);
            return res;
        }

        public Grain Update(Grain grain)
        {
            var grains = GetAll().ToList();
            var obj = grains.Find(x => x.Id == grain.Id);
            obj.Wetness = grain.Wetness;
            obj.Amount=grain.Amount;
            obj.Garbage=grain.Garbage;
            obj.Infection=grain.Infection;
            SaveChanges(grains);
            return grain;
        }
    }
}
