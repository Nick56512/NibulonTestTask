using AutoMapper;
using BLL.DTO;
using DAL.Repositories;
using NubilonTestTask.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class GrainService : IGrainService
    {
        IMapper mapper;
        IGrainRepository _grainRepo;
        public GrainService(IGrainRepository _grainRepo)
        {
            var mapperConf=new MapperConfiguration((opt) =>
            {
                opt.CreateMap<GrainDto, Grain>();
                opt.CreateMap<Grain, GrainDto>();
            });
            mapper=new Mapper(mapperConf);
            this._grainRepo = _grainRepo;
        }
        public GrainDto Add(GrainDto newGrain)
        {
            var obj = mapper.Map<GrainDto, Grain>(newGrain);
            _grainRepo.Add(obj);
            return newGrain;
        }
        public GrainDto Delete(int id)
        {
            var deletedOdj=_grainRepo.Delete(id);
            return mapper.Map<Grain,GrainDto>(deletedOdj);
        }

        public GrainDto Get(int id)
        {
            var grain = _grainRepo.Get(id);
            return mapper.Map<Grain, GrainDto>(grain);
        }

        public IEnumerable<GrainDto> GetAll()
        {
            return _grainRepo
                .GetAll()
                .Select(x => mapper.Map<Grain,GrainDto>(x));
        }

        public IEnumerable<GrainDto> GroupByDate(DateTime start, DateTime end)
        {
            int startId = GetAll().Min(x => x.Id);
            var allData = GetAll();
            var grouped = from data in allData
                          group data by new
                          {
                              data.RecordDate,
                              data.BranchId,
                              data.CropYear,
                              data.CounterpartyId,
                              data.CounterpartyName,
                              data.ContactId,
                              data.Product,
                              data.Price,
                              data.Process,
                              data.Wetness,
                              data.Garbage,
                              data.Infection
                          } into groupRes
                          where groupRes.Key.RecordDate >= start && groupRes.Key.RecordDate <= end
                          select new GrainDto()
                          {
                              Id = startId++,
                              RecordDate = groupRes.Key.RecordDate,
                              BranchId = groupRes.Key.BranchId,
                              CropYear = groupRes.Key.CropYear,
                              CounterpartyId = groupRes.Key.CounterpartyId,
                              CounterpartyName = groupRes.Key.CounterpartyName,
                              ContactId = groupRes.Key.ContactId,
                              Product = groupRes.Key.Product,
                              Price = groupRes.Key.Price,
                              Amount = groupRes.Sum(x => x.Amount),
                              Process = groupRes.Key.Process,
                              Wetness = groupRes.Key.Wetness,
                              Garbage = groupRes.Key.Garbage,
                              Infection = groupRes.Key.Infection
                          };
            return grouped;
        }

        public IEnumerable<GrainDto> GroupByDate(IEnumerable<GrainDto> grains, DateTime start, DateTime end)
        {
            int startId = GetAll().Min(x => x.Id);
            var grouped = from data in grains
                          group data by new
                          {
                              data.RecordDate,
                              data.BranchId,
                              data.CounterpartyId,
                              data.CounterpartyName,
                              data.ContactId,
                              data.Product,
                              data.Price,
                              data.Process,
                          } into groupRes
                          where groupRes.Key.RecordDate >= start && groupRes.Key.RecordDate <= end
                          select new GrainDto()
                          {
                              Id = startId++,
                              RecordDate = groupRes.Key.RecordDate,
                              BranchId = groupRes.Key.BranchId,
                              CounterpartyId = groupRes.Key.CounterpartyId,
                              CounterpartyName = groupRes.Key.CounterpartyName,
                              ContactId = groupRes.Key.ContactId,
                              Product = groupRes.Key.Product,
                              Price = groupRes.Key.Price,
                              Amount = groupRes.Sum(x => x.Amount),
                              Process = groupRes.Key.Process,
                              Wetness = Math.Round(groupRes.Average(x=>x.Wetness),2),
                              Garbage = Math.Round(groupRes.Average(x=>x.Garbage),2),
                              Infection = groupRes.Select(x => x.Infection).Last(),
                          };
            return grouped;
        }

        public GrainDto Update(GrainDto grain)
        {
            var obj=mapper.Map<GrainDto,Grain>(grain);
            var res = _grainRepo.Update(obj);
            return mapper.Map<Grain, GrainDto>(res);
        }
    }
}
