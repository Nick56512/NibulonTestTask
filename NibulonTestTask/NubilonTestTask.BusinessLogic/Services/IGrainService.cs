using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public interface IGrainService
    {
        GrainDto Add(GrainDto newGrain);
        GrainDto Update(GrainDto grain);
        IEnumerable<GrainDto> GetAll();
        GrainDto Get(int id);
        GrainDto Delete(int id);

        IEnumerable<GrainDto> GroupByDate(DateTime start,DateTime end);
        IEnumerable<GrainDto> GroupByDate(IEnumerable<GrainDto>grains,DateTime start,DateTime end);
    }
}
