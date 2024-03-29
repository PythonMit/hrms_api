using HRMS.Core.Consts;
using HRMS.Core.Exstensions;
using HRMS.Core.Models.Salary;
using HRMS.DBL.Extensions;
using HRMS.DBL.Stores;
using HRMS.Services.Interfaces;
using HRMS.Core.Models.Leave;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System;
using AutoMapper;
using HRMS.Core.Models.SystemFlag;
using HRMS.Core.Models.Employee;
using HRMS.DBL.Entities;
using HRMS.Core.Models.General;
using LinqKit;
using System.Drawing;

namespace HRMS.Services.Common
{
    public class EmployeeSalaryService : IEmployeeSalaryService
    {
        private readonly EmployeeSalaryStore _employeeSalaryStore;
        private readonly EmployeeStore _employeeStore;
        private readonly EmployeeContractStore _employeeContractStore;

        private readonly IMapper _mapper;

        public EmployeeSalaryService(EmployeeSalaryStore employeeSalaryStore, EmployeeStore employeeStore, EmployeeContractStore employeeContractStore, IMapper mapper)
        {
            _employeeSalaryStore = employeeSalaryStore;
            _employeeStore = employeeStore;
            _employeeContractStore = employeeContractStore;
            _mapper = mapper;
        }
        public async Task<IEnumerable<int>> AddorUpdateEmployeeSalary(IEnumerable<EmployeeSalaryRequestModel> model)
        {
            return await _employeeSalaryStore.AddorUpdateEmployeeSalary(model);
        }
        public async Task<EmployeeSalaryListModel> GetEmployeeSalary(EmployeeSalaryFilterModel filter, int? employeeId = null)
        {
            var data = await _employeeSalaryStore.GetEmployeeSalary(filter, employeeId);

            var multiple = data?.Where(x => x.CalculationType == CalculationType.Manual.GetEnumDescriptionAttribute());
            var s = new List<EmployeeSalaryModel>();
            var e = new List<int>();
            if (multiple != null && multiple.Count() > 0)
            {
                e.AddRange(multiple?.Select(x => x.Id).ToList());
                var g = multiple?.GroupBy(x => new { x.EmployeeContract.Employee.EmployeeCode, x.SalaryMonth, SalaryYear = (x.SalaryYear > 0 ? x.SalaryYear : x.CreatedDateTimeUtc.Year) }).Select(x => x.ToList());
                foreach (var item in g)
                {
                    s.Add(item?.AsQueryable().ToCombineEmployeeSalary());
                }
            }

            var result = data?.Where(x => !e.Contains(x.Id)).ToEmployeeSalary().ToList();
            result?.AddRange(s);

            if (result == null || (result != null && result.Count() == 0))
            {
                return null;
            }
            result?.ForEach(x =>
            {
                x.Employee.JoinDate = _employeeSalaryStore.GetEmployeeJoindate(x.Employee.Id);
                var holdAmount = _employeeSalaryStore.GetHoldSalaryAmount(x.EarningGross.Id) ?? 0;
                x.EarningGross.HoldAmount = holdAmount;
                x.EarningGross.PaidAmount = x.EarningGross.NetSalary - holdAmount;
            });
            result = result?.OrderBy(x => x.Employee.JoinDate).ToList();

            var netSalary = result?.Where(x => x.Status.Id != (int)EmployeeEarningGrossStatusType.Hold).Sum(x => x.EarningGross.NetSalary) ?? 0;
            var onHoldsalary = result?.Where(x => x.Status.Id == (int)EmployeeEarningGrossStatusType.Hold).Sum(x => x.EarningGross.NetSalary) ?? 0;

            var totalRecords = 0;
            if (filter.Pagination != null && filter.Pagination.PageNumber > 0 && filter.Pagination.PageSize > 0)
            {
                totalRecords = result?.Count() ?? 0;
                result = result?.Skip((filter.Pagination.PageNumber - 1) * filter.Pagination.PageSize).Take(filter.Pagination.PageSize).ToList();
            }

            return new EmployeeSalaryListModel
            {
                Salaries = result,
                TotalRecords = totalRecords,
                TotalNetSalary = netSalary,
                TotalOnHoldSalary = onHoldsalary,
            };
        }
        public async Task<EmployeeSalaryModel> GetEmployeeSalaryDetailsAsync(EmployeeSalaryQueryModel query)
        {
            var data = await _employeeSalaryStore.GetEmployeeSalaryDetailsAsync(query);
            var count = data?.Where(x => x.CalculationType == CalculationType.Manual.GetEnumDescriptionAttribute()).Count();
            var result = (count == 2 ? data?.ToCombineEmployeeSalary() : data?.ToEmployeeSalary().FirstOrDefault());
            if (result == null)
            {
                return null;
            }

            result.Employee.JoinDate = _employeeSalaryStore.GetEmployeeJoindate(result.Employee.Id);
            return result;
        }
        public EmployeeSalaryModel GetEmployeeSalaryDetails(EmployeeSalaryQueryModel query)
        {
            var data = _employeeSalaryStore.GetEmployeeSalaryDetails(query);
            var count = data?.Where(x => x.CalculationType == CalculationType.Manual.GetEnumDescriptionAttribute()).Count();
            var result = (count == 2 ? data?.ToCombineEmployeeSalary() : data?.ToEmployeeSalary().FirstOrDefault());
            if (result == null)
            {
                return null;
            }

            result.Employee.JoinDate = _employeeSalaryStore.GetEmployeeJoindate(result.Employee.Id);
            return result;
        }
        public async Task<IEnumerable<EmployeeSalaryModel>> GetEmployeePartlySalaryDetails(EmployeeSalaryQueryModel query)
        {
            var data = await _employeeSalaryStore.GetEmployeeSalaryDetailsAsync(query);
            return data?.ToEmployeeSalary().ToList();
        }
        public async Task<SalaryEmployeeInformationModel> GetEmployeeDetails(string employeeCode)
        {
            var employee = await _employeeStore.GetEmployeeDetails(employeeCode);
            var salary = await _employeeSalaryStore.GetEmployeeSalaryDetails(employeeCode);
            var incentives = await _employeeSalaryStore.GetEmployeeIncentivesDetails(employeeCode);
            var contract = await _employeeContractStore.GetRunningContractDates(employeeCode);
            return new SalaryEmployeeInformationModel
            {
                Employee = employee?.ToEmployeeOutLineDetails(),
                Salary = salary?.ToSalaryEmployeeDetails(),
                Incentives = incentives?.ToSalaryEmployeeIncentiveDetails().ToList(),
                Contract = new EmployeeContractOutlineModel
                {
                    StartDate = contract?.StartDate,
                    EndDate = contract?.EndDate,
                }
            };
        }
        public async Task<bool> DeleteEmployeeSalary(IEnumerable<int> ids)
        {
            return await _employeeSalaryStore.DeleteEmployeeSalary(ids);
        }
        public async Task<bool> SetEmployeeSalaryStatus(IEnumerable<EmployeeSalaryStatusRequestModel> model)
        {
            return await _employeeSalaryStore.SetEmployeeSalaryStatus(model);
        }
        public async Task<double> GetActualLeaveWitoutPay(EmployeeSalaryQueryModel query)
        {
            return await _employeeSalaryStore.GetActualLeaveWitoutPay(query);
        }
        public async Task<decimal?> GetActualApprovedOvertime(EmployeeSalaryQueryModel query)
        {
            return await _employeeSalaryStore.GetActualApprovedOvertime(query);
        }
        public async Task<bool> CheckMonthlySalaryExist(EmployeeSalaryQueryModel query)
        {
            return await _employeeSalaryStore.CheckMonthlySalaryExist(query);
        }
        public async Task<IEnumerable<int>> GetContractYears(string employeeCode)
        {
            return await _employeeContractStore.GetContractYears(employeeCode);
        }
        public async Task<IEnumerable<EmployeeOutlineModel>> GetAlreadySalariedEmployee(AlreadySalariedEmployeeRequestModel model, RoleTypes? userRole, int? employeeId)
        {
            var data = await _employeeStore.GetEmployeeInformationByDesignation(model.DesignationIds, model.HasContract, userRole, employeeId);
            var employeeIds = await _employeeSalaryStore.GetAlreadySalariedEmployees(model.Month, model.Year);
            return data?.OrderByDescending(x => x.ContractStartDate).Where(x => !employeeIds.Contains(x.EmployeeId)).ToEmployeeOutLineDetails().GroupBy(x => x.Code).Select(x => x.FirstOrDefault()).ToList();
        }
        #region Partial Hold Salary
        public async Task<Guid?> AddorUpdateEmployeeHoldSalary(EmployeeHoldSalaryRequestModel model)
        {
            return await _employeeSalaryStore.AddorUpdateEmployeeHoldSalary(model);
        }
        public async Task<EmployeeHoldSalaryListModel> GetEmployeeHoldSalary(EmployeeHoldSalaryFilterModel filters)
        {
            var data = await _employeeSalaryStore.GetEmployeeHoldSalary(filters);
            var result = data?.ToEmployeeHoldSalary()?.GroupBy(x => x.EmployeeContractId).Select(x => new EmployeeHoldSalaryModel
            {
                Id = x.FirstOrDefault().Id,
                EmployeeContractId = x.FirstOrDefault().EmployeeContractId,
                EmployeeEarningGrossId = x.FirstOrDefault().EmployeeContractId,
                HoldAmount = x.Sum(y => y.HoldAmount),
                PaidAmount = x.Sum(y => y.PaidAmount),
                NetSalary = x.FirstOrDefault().NetSalary,
                PaidDate = x.FirstOrDefault().PaidDate,
                RecordStatus = x.FirstOrDefault().RecordStatus,
                Remarks = x.FirstOrDefault().Remarks,
                Status = x.FirstOrDefault().Status,
                SalaryMonth = x.FirstOrDefault().SalaryMonth,
                SalaryYear = x.FirstOrDefault().SalaryYear,
                Employee = x.FirstOrDefault().Employee,
            }).ToList();

            var totalRecords = 0;
            if (filters.Pagination != null && filters.Pagination.PageNumber > 0 && filters.Pagination.PageSize > 0)
            {
                totalRecords = result?.Count() ?? 0;
                result = result?.Skip((filters.Pagination.PageNumber - 1) * filters.Pagination.PageSize).Take(filters.Pagination.PageSize).ToList();
            }

            return new EmployeeHoldSalaryListModel
            {
                HoldSalaries = result,
                TotalRecords = totalRecords,
            };
        }
        public async Task<EmployeeHoldSalaryListModel> GetHistoryEmployeeHoldSalary(EmployeeHoldSalaryHistoryFilterModel filters)
        {
            var data = await _employeeSalaryStore.GetHistoryEmployeeHoldSalary(filters.EmployeeContractId);
            var result = data?.ToEmployeeHoldSalary().ToList();

            var totalRecords = 0;
            if (filters.Pagination != null && filters.Pagination.PageNumber > 0 && filters.Pagination.PageSize > 0)
            {
                totalRecords = result?.Count() ?? 0;
                result = result?.Skip((filters.Pagination.PageNumber - 1) * filters.Pagination.PageSize).Take(filters.Pagination.PageSize).ToList();
            }

            return new EmployeeHoldSalaryListModel
            {
                HoldSalaries = result,
                TotalRecords = totalRecords,
            };
        }
        public async Task<bool> RemoveEmployeeHoldSalary(IEnumerable<Guid> ids)
        {
            return await _employeeSalaryStore.RemoveEmployeeHoldSalary(ids);
        }
        #endregion Partial Hold Salary
        #region Others
        public string GetTransactionAmount(string employeeCode, string month, int year)
        {
            var transaction = GetEmployeeSalaryDetails(new EmployeeSalaryQueryModel { EmployeeCode = employeeCode, Month = month, Year = year });
            return transaction?.EarningGross?.NetSalary.ToString("F2") ?? string.Empty;
        }
        public async Task<IEnumerable<BulkSalaryPaymentModel>> GetEmployeeSalarySheet(string employeeCodes, string month, int year)
        {
            var data = await _employeeSalaryStore.GetEmployeeBankInfomationList(null, month, year);
            var result = data.ToBulkSalaryPayments().ToList();
            result.ForEach(x => { x.BulkSalary.TransactionAmount = GetTransactionAmount(x.EmployeeCode, month, year); });
            return (result == null && result.Any() ? null : result.Select(x => x.BulkSalary).ToList());
        }
        #endregion Others
    }
}