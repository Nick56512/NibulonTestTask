using BLL.DTO;

namespace NibulonTestTask.Models.ViewModels
{
    public class TableViewModel
    {
        public IEnumerable<GrainDto> Grains { get; set; }
        public string TableName { get; set; }
        public bool IsReadOnly { get; set; }
        public string Message { get; set; } 
    }
}
