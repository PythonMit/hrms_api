using HRMS.Core.Consts;
using HRMS.Core.Models.General;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace HRMS.Core.Models.Overtime;

public class EmployeeOvertimeFilterModel
{
    [DefaultValue("")]
    public string SearchString { get; set; }
    [DefaultValue(null)]
    public DateTime? DateFrom { get; set; } = null;
    [DefaultValue(null)]
    public DateTime? DateTo { get; set; } = null;
    [DefaultValue(null)]
    public bool? FetchApproved { get; set; } = null;
    [DefaultValue(null)]
    public IEnumerable<int?> Status { get; set; }
    public PaginationModel Pagination { get; set; }
}
