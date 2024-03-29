using System.Collections.Generic;

namespace HRMS.Services.Interfaces
{
    public interface INotificationParameters<T>
    {
        IEnumerable<T> TemplateParameters { get; set; }
    }
}
