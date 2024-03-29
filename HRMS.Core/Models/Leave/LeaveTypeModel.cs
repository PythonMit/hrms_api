namespace HRMS.Core.Models.Leave
{
    public class LeaveTypeModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int TotalLeaves { get; set; }
    }
}
