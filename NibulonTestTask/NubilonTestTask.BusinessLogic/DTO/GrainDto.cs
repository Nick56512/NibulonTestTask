using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public class GrainDto
    {
        public int Id { get; set; }
        public DateTime RecordDate { get; set; }
        public int BranchId { get; set; }
        public int CropYear { get; set; }
        public int CounterpartyId { get; set; }
        public string CounterpartyName { get; set; }
        public int ContactId { get; set; }
        public string Product { get; set; }
        public double Price { get; set; }
        public int Amount { get; set; }
        public string Process { get; set; }
        public double Wetness { get; set; }
        public double Garbage { get; set; }
        public string Infection { get; set; }
    }
}
