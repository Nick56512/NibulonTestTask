using NubilonTestTask.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public interface IGrainRepository
    {
        Grain Add(Grain newGrain);
        Grain Update(Grain grain);
        IEnumerable<Grain> GetAll();
        Grain Get(int id);
        Grain Delete(int id);
    }
}
