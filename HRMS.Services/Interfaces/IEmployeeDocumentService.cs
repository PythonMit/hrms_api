using HRMS.Core.Models.Document;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HRMS.Services.Interfaces
{
    public interface IEmployeeDocumentService
    {
        Task<bool> ManageDocument(EmployeeDocumentModel model);
        Task<bool> DeleteDocument(DeleteDocumentModel model);
        Task<IEnumerable<EmployeeDocumentModel>> GetEmployeeDocumentById(int id);
        Task<IEnumerable<EmployeeContractDocumentModel>> GetEmployeeContracts(int employeeId);
    }
}

