using HRMS.Core.Consts;

namespace HRMS.Core.Models.Leave
{
    public class LeaveCategoryModel
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public RecordStatus RecordStatus { get; set; }
    }
}
