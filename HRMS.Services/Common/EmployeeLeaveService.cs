using AutoMapper;
using HRMS.Core.Consts;
using HRMS.Core.Exstensions;
using HRMS.Core.Models.Leave;
using HRMS.Core.Utilities.Auth;
using HRMS.Core.Utilities.General;
using HRMS.DBL.Extensions;
using HRMS.DBL.Stores;
using HRMS.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HRMS.Services.Common
{
    public class EmployeeLeaveService : IEmployeeLeaveService
    {
        private readonly IMapper _mapper;
        private readonly IUserContextAccessor _userContextAccessor;
        private readonly IGeneralUtilities _generalUtilities;
        private readonly IEmployeeService _employeeService;
        private readonly ISystemFlagService _systemFlagService;

        private readonly EmployeeLeaveStore _employeeLeaveStore;
        private readonly EmployeeContractStore _employeeContractStore;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="userContextAccessor"></param>
        /// <param name="employeeLeaveStore"></param>
        /// <param name="generalUtilities"></param>
        /// <param name="employeeService"></param>
        /// <param name="employeeContractStore"></param>
        /// <param name="systemFlagService"></param>
        /// <param name="slackUtility"></param>
        public EmployeeLeaveService(IMapper mapper, IUserContextAccessor userContextAccessor, EmployeeLeaveStore employeeLeaveStore, IGeneralUtilities generalUtilities, IEmployeeService employeeService,
            EmployeeContractStore employeeContractStore, ISystemFlagService systemFlagService)
        {
            _mapper = mapper;
            _employeeLeaveStore = employeeLeaveStore;
            _userContextAccessor = userContextAccessor;
            _generalUtilities = generalUtilities;
            _employeeService = employeeService;
            _employeeContractStore = employeeContractStore;
            _systemFlagService = systemFlagService;
        }
        #region Leave Balance
        public async Task<IEnumerable<int>> GenerateEmployeeLeave(IEnumerable<EmployeeLeaveRequestModel> model, string employeeCode)
        {
            var result = await _employeeLeaveStore.GenerateEmployeeLeaveWithoutTransactions(model, employeeCode);
            if (result?.transactions != null && result?.transactions.Count() > 0)
            {
                await _employeeLeaveStore.GenerateLeaveTransactions(result?.transactions, employeeCode);
            }
            return result?.leaveIds;

            // Note: Depricated
            // return await _employeeLeaveApplicationStore.GenerateEmployeeLeave(model, employeeCode);
        }
        [Obsolete]
        public async Task<EmployeeLeaveListModel> GetEmployeeLeaveBalance(EmployeeLeaveFilterModel filter, int? employeeId = null)
        {
            var data = await _employeeLeaveStore.GetEmployeeLeaveBalanceList(filter, employeeId);
            if (data == null) return null;

            var records = _mapper.Map<IEnumerable<EmployeeLeaveModel>>(data);
            var groupData = records.GroupBy(x => x.Employee.Id).ToList();

            var result = new List<EmployeeLeaveBalanceListModel>();
            foreach (var item in groupData)
            {
                result.Add(new EmployeeLeaveBalanceListModel
                {
                    Employee = item.Select(x => new LeaveEmployeeDetailModel
                    {
                        Id = x.Employee.Id,
                        Code = x.Employee.Code,
                        Name = x.Employee.Name,
                        Designation = x.Employee.Designation,
                        JoinDate = _employeeLeaveStore.GetEmployeeJoindate(x.Employee.Id)
                    }).FirstOrDefault(),
                    Leaves = item.Select(x => new EmployeeLeaveModel
                    {
                        Id = x.Id,
                        EmployeeContractId = x.EmployeeContractId,
                        LeaveEndDate = x.LeaveEndDate,
                        LeaveStartDate = x.LeaveStartDate,
                        TotalLeaves = x.TotalLeaves,
                        TotalLeavesTaken = x.TotalLeavesTaken,
                        LeaveType = new LeaveTypeModel
                        {
                            Id = x.LeaveType.Id,
                            Name = x.LeaveType.Name,
                            Description = x.LeaveType.Description,
                        }
                    })
                });
            }

            var totalRecords = 0;
            if (filter.Pagination != null && filter.Pagination.PageNumber > 0 && filter.Pagination.PageSize > 0)
            {
                totalRecords = result.Count();
                result = result.Skip((filter.Pagination.PageNumber - 1) * filter.Pagination.PageSize).Take(filter.Pagination.PageSize).ToList();
            }

            return new EmployeeLeaveListModel
            {
                Leaves = result,
                TotalRecords = totalRecords,
            };
        }
        public async Task<EmployeeLeaveListModel> GetEmployeeLeaveBalanceV2(EmployeeLeaveFilterModel filter, int? employeeId = null, RoleTypes? userRole = null)
        {
            var data = await _employeeLeaveStore.GetEmployeeLeaveBalanceList(filter, employeeId, null, userRole);
            if (data == null) return null;

            var records = _mapper.Map<IEnumerable<EmployeeLeaveModel>>(data);
            var groupData = records.GroupBy(x => x.Employee.Id).ToList();

            var result = new List<EmployeeLeaveBalanceListModel>();
            foreach (var item in groupData)
            {
                var t = new EmployeeLeaveBalanceListModel();
                t.Employee = item.Select(x => new LeaveEmployeeDetailModel
                {
                    Id = x.Employee.Id,
                    Code = x.Employee.Code,
                    Name = x.Employee.Name,
                    Designation = x.Employee.Designation,
                    Branch = x.Employee.Branch,
                    BranchCode = x.Employee.BranchCode,
                    JoinDate = _employeeLeaveStore.GetEmployeeJoindate(x.Employee.Id)
                }).FirstOrDefault();
                t.Contract = item.Select(x => new EmployeeContractOutlineModel
                {
                    StartDate = _employeeContractStore.GetRunningContractDates(x.EmployeeContractId)?.StartDate,
                    EndDate = _employeeContractStore.GetRunningContractDates(x.EmployeeContractId)?.EndDate,
                }).FirstOrDefault();
                t.Leaves = item.Select(x => new EmployeeLeaveModel
                {
                    Id = x.Id,
                    EmployeeContractId = x.EmployeeContractId,
                    LeaveEndDate = x.LeaveEndDate,
                    LeaveStartDate = x.LeaveStartDate,
                    TotalLeaves = x.TotalLeaves,
                    TotalLeavesTaken = x.TotalLeavesTaken,
                    LeaveType = x.LeaveType == null ? null : new LeaveTypeModel
                    {
                        Id = x.LeaveType.Id,
                        Name = x.LeaveType.Name,
                        Description = x.LeaveType.Description,
                    }
                });

                var contract = await _employeeContractStore.GetRunningEmployeeContract(t?.Employee?.Code);
                if (contract == null) continue;

                t.Leaves = t.Leaves?.Where(x => x.EmployeeContractId == contract?.Id);
                var s = await GetBalanceInformation(t.Leaves);
                if (s == null) continue;

                t.LeavesV2 = s;
                result.Add(t);
            }

            var totalRecords = 0;
            if (filter.Pagination != null && filter.Pagination.PageNumber > 0 && filter.Pagination.PageSize > 0)
            {
                totalRecords = result.Count();
                result = result.Skip((filter.Pagination.PageNumber - 1) * filter.Pagination.PageSize).Take(filter.Pagination.PageSize).ToList();
            }

            return new EmployeeLeaveListModel
            {
                Leaves = result,
                TotalRecords = totalRecords,
            };
        }
        public async Task<double?> GetCurrentEmployeeLeaveBalance(string employeeCode, string leaveFor)
        {
            var contract = await _employeeContractStore.GetRunningEmployeeContract(employeeCode);
            if (contract == null) return null;

            int? leaveType = null;
            if (!string.IsNullOrEmpty(leaveFor))
            {
                leaveType = _generalUtilities.GetLeaveCategory(leaveFor);
            }
            else
            {
                leaveType = (int)LeaveTypes.FlatLeave;
            }

            var availableLeaves = await _employeeLeaveStore.GetAvailableLeaveCount(contract.Id);
            return availableLeaves;
        }
        public async Task<IEnumerable<LeaveEmployeeDetailModel>> GetRaminingEmployeeForLeaves()
        {
            var data = await _employeeLeaveStore.GetRaminingEmployeeForLeaves(_userContextAccessor.UserRole);
            var result = _mapper.Map<IEnumerable<LeaveEmployeeDetailModel>>(data);
            foreach (var item in result)
            {
                item.ProbationPeriod = _employeeContractStore.GetRunningEmployeeProbationPeriod(item.Code);
                var dates = await _employeeContractStore.GetRunningContractDates(item.Code);
                item.ContractStartDate = dates?.StartDate;
                item.ContractEndDate = dates?.EndDate;
            }
            return result;
        }
        public async Task<IEnumerable<LeaveEmployeeDetailModel>> GetLeaveAcquiredEmployee()
        {
            var data = await _employeeLeaveStore.GetLeaveAcquiredEmployee();
            return _mapper.Map<IEnumerable<LeaveEmployeeDetailModel>>(data);
        }
        public async Task<IEnumerable<string>> GetLeaveBalanceCalibrate()
        {
            return await _employeeLeaveStore.GetLeaveBalanceCalibrate();
        }
        public async Task<bool> DeleteLeaveBalanceInformations(string employeeCode)
        {
            var flag = await _systemFlagService.GetSystemFlagsByTag("permanentremove");
            if (flag == null || (flag != null && flag.Value == "disable"))
            {
                return false;
            }
            return await _employeeLeaveStore.DeleteLeaveBalanceInformations(employeeCode);
        }
        #endregion Leave Balance        
        #region Leave Detail
        #region Type System - Old [Depricated]
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Obsolete]
        public async Task<EmployeeLeaveDetailModel> GetEmployeeLeaveDetails(Guid id)
        {
            var leaveDetails = await _employeeLeaveStore.GetEmployeeLeaveDetails(id);
            var basic = leaveDetails?.ToEmployeeLeaveApplications().FirstOrDefault();

            var leaveBalances = await _employeeLeaveStore.GetEmployeeLeaveBalanceList(null, null, basic?.EmployeeContractId);
            var balance = leaveBalances?.ToEmployeeLeaveBalances().ToList();

            var result = await GetMonthlyLeaveData(basic?.EmployeeContractId, 3);
            return new EmployeeLeaveDetailModel
            {
                Application = basic,
                BalanceV1 = balance,
                MonthlyBalance = result
            };
        }
        /// <summary>
        /// Old System with different leave types without categories
        /// </summary>
        /// <param name="employeeCode"></param>
        /// <returns></returns>
        [Obsolete]
        public async Task<EmployeeLeaveBalanceDetailModel> GetEmployeeLeaveBalanceDetails(string employeeCode)
        {
            var contract = await _employeeContractStore.GetRunningEmployeeContract(employeeCode);
            var monthWorked = _generalUtilities.GetTotalMonthsFrom(DateTime.UtcNow, contract.ContractStartDate);

            var data = await _employeeLeaveStore.GetEmployeeLeaveBalanceList(null, null, contract.Id);
            var balance = data?.ToEmployeeLeaveBalances().ToList();

            var leaves = await _employeeLeaveStore.GetEmployeeDetailsFromLeaveApplication(contract.Id);
            var employee = leaves?.ToEmployeeOutLineDetails().FirstOrDefault();

            var result = await GetMonthlyLeaveData(contract.Id, monthWorked);
            return new EmployeeLeaveBalanceDetailModel
            {
                Employee = employee,
                BalanceV1 = balance,
                MonthlyBalance = result
            };
        }
        /// <summary>
        /// Old System with different leave types without categories
        /// </summary>
        /// <param name="contractId"></param>
        /// <param name="monthWorked"></param>
        /// <returns></returns>
        [Obsolete]
        private async Task<List<EmployeeLeaveMonthlyBalanceModel>> GetMonthlyLeaveData(int? contractId, int? monthWorked = null)
        {
            var groups = new List<EmployeeLeaveBalanceModel>();
            var leaveApplications = await _employeeLeaveStore.GetEmployeeLeaveDetails(null, contractId, EmployeeLeaveStatusType.Approved, monthWorked);
            if (leaveApplications != null && leaveApplications.Any())
            {
                var monthlyBalance = leaveApplications?.ToEmployeeLastMonthsLeaveData().ToList();
                groups.AddRange(monthlyBalance.GroupBy(x => new { x.Month, x.Year, x.LeaveType }).Select(x => new EmployeeLeaveBalanceModel
                {
                    LeaveType = x.Key.LeaveType,
                    Month = x.Key.Month,
                    Year = x.Key.Year,
                    ApprovedDays = x.Sum(y => y.ApprovedDays) ?? 0,
                    LWPDays = x.Sum(y => y.LWPDays) ?? 0,
                }).ToList());
            }

            if (groups == null || (groups != null && groups.Count == 0) && monthWorked <= 0)
            {
                return null;
            }

            var workingMonth = _generalUtilities.GetMonths(DateTime.UtcNow, monthWorked, true);
            var months = groups.Select(x => GeneralExtensions.GetMonthNumber(x.Month)).ToList();
            var types = groups.Select(x => x.LeaveType).ToList();
            var xLeaveType = await _employeeLeaveStore.GetLeaveType();

            var result = new List<EmployeeLeaveMonthlyBalanceModel>();
            foreach (var item in workingMonth)
            {
                var t = new EmployeeLeaveMonthlyBalanceModel
                {
                    MonthYear = $"{item.MonthLabel} {item.Year}"
                };
                foreach (var subitem in xLeaveType)
                {
                    t.Balances.AddRange(new List<EmployeeLeaveBalanceModel>
                    {
                        new EmployeeLeaveBalanceModel
                        {
                            LeaveType = GeneralExtensions.ToEnum<LeaveTypes>(subitem.Name),
                        }
                    });
                }
                result.Add(t);
            }

            foreach (var item in groups)
            {
                var monthly = $"{item.Month} {item.Year}";
                var t = result.Where(x => x.MonthYear == monthly).FirstOrDefault();
                if (t == null)
                {
                    continue;
                }

                foreach (var subitem in t.Balances.Where(y => y.LeaveType == item.LeaveType))
                {
                    subitem.TotalLeavesTaken = 0;
                    if (item.LeaveType != LeaveTypes.LeaveWithoutPay)
                    {
                        subitem.TotalLeavesTaken = (item.ApprovedDays ?? 0);
                        if (item.LWPDays > 0)
                        {
                            var lwp = t.Balances.FirstOrDefault(y => y.LeaveType == LeaveTypes.LeaveWithoutPay);
                            if (lwp != null)
                                lwp.TotalLeavesTaken = ((lwp.TotalLeavesTaken ?? 0) + (item.LWPDays ?? 0));
                        }
                    }
                }
            }
            return result;
        }
        #endregion Type System
        #region Flat System - New
        public async Task<EmployeeLeaveDetailModel> GetEmployeeLeaveDetailsV2(Guid id)
        {
            var leaveDetails = await _employeeLeaveStore.GetEmployeeLeaveDetails(id);
            var basic = leaveDetails?.ToEmployeeLeaveApplications().FirstOrDefault();

            var leaveBalances = await _employeeLeaveStore.GetEmployeeLeaveBalanceList(null, null, basic?.EmployeeContractId);
            var balance = leaveBalances?.ToEmployeeLeaveBalances().ToList();
            var balanceInfo = await GetBalanceInformation(balance);

            var result = await GetMonthlyLeaveTypeInformation(basic?.EmployeeContractId, 3);
            return new EmployeeLeaveDetailModel
            {
                Application = basic,
                Balance = balanceInfo,
                MonthlyBalance = result
            };
        }
        public async Task<EmployeeLeaveBalanceDetailModel> GetEmployeeLeaveBalanceDetailsV2(string employeeCode)
        {
            var contract = await _employeeContractStore.GetRunningEmployeeContract(employeeCode);
            var startDate = new DateTime(contract.ContractStartDate.Year, contract.ContractStartDate.Month, 01);
            var intiateDate = DateTime.UtcNow > contract.ContractEndDate ? contract.ContractEndDate : DateTime.UtcNow;
            if (startDate.Year > DateTime.UtcNow.Year)
            {
                startDate = DateTime.Now;
            }
            var monthWorked = _generalUtilities.GetTotalMonthsFrom(intiateDate, startDate, false);

            var data = await _employeeLeaveStore.GetEmployeeLeaveBalanceList(null, null, contract.Id);
            var balance = data?.ToEmployeeLeaveBalances().ToList();
            var balanceInfo = await GetBalanceInformation(balance);

            var leaves = await _employeeLeaveStore.GetEmployeeDetailsFromLeaveApplication(contract.Id);
            var employee = leaves?.ToEmployeeOutLineDetails().FirstOrDefault();

            var result = await GetMonthlyLeaveCategoryDataV2(contract.Id, monthWorked);
            return new EmployeeLeaveBalanceDetailModel
            {
                Employee = employee,
                Balance = balanceInfo,
                MonthlyBalance = result
            };
        }
        private async Task<List<EmployeeLeaveMonthlyBalanceModel>> GetMonthlyLeaveCategoryDataV2(int? contractId, int? monthWorked = null)
        {
            var groups = new List<EmployeeLeaveBalanceModel>();
            var leaveApplications = await _employeeLeaveStore.GetEmployeeLeaveDetails(null, contractId, EmployeeLeaveStatusType.Approved, monthWorked);
            if (leaveApplications != null && leaveApplications.Any())
            {
                var monthlyBalance = leaveApplications?.ToEmployeeLastMonthsLeaveData().ToList();
                var hasCategory = monthlyBalance?.Any(x => x.LeaveCategoryType == null);
                if (hasCategory == true) return null;

                groups.AddRange(monthlyBalance.GroupBy(x => new { x.Month, x.Year, x.LeaveCategoryType }).Select(x => new EmployeeLeaveBalanceModel
                {
                    LeaveCategoryType = x.Key.LeaveCategoryType,
                    Month = x.Key.Month,
                    Year = x.Key.Year,
                    ApprovedDays = x.Sum(y => y.ApprovedDays) ?? 0,
                    LWPDays = x.Sum(y => y.LWPDays) ?? 0,
                }).ToList());
            }

            if (groups == null || (groups != null && groups.Count == 0) && monthWorked <= 0) return null;

            var dates = _employeeContractStore.GetRunningContractDates(contractId);
            var previous = (dates?.StartDate > DateTime.UtcNow);
            var workingMonth = _generalUtilities.GetMonths(dates?.StartDate, monthWorked, previous);
            var months = groups.Select(x => GeneralExtensions.GetMonthNumber(x.Month)).ToList();
            var types = groups.Select(x => x.LeaveCategoryType).ToList();
            var xLeaveCategoryType = await _employeeLeaveStore.GetLeaveCategories();

            var result = new List<EmployeeLeaveMonthlyBalanceModel>();
            foreach (var item in workingMonth)
            {
                var t = new EmployeeLeaveMonthlyBalanceModel
                {
                    MonthYear = $"{item.MonthLabel} {item.Year}"
                };
                foreach (var subitem in xLeaveCategoryType)
                {
                    t.Balances.AddRange(new List<EmployeeLeaveBalanceModel>
                    {
                        new EmployeeLeaveBalanceModel
                        {
                            LeaveCategoryType = GeneralExtensions.ToEnum<LeaveCategoryType>(subitem.Category),
                        }
                    });
                }
                t.Balances.Add(new EmployeeLeaveBalanceModel
                {
                    LeaveCategoryType = LeaveCategoryType.LeaveWithoutPay
                });
                result.Add(t);
            }

            foreach (var item in groups)
            {
                var monthly = $"{item.Month} {item.Year}";
                var t = result.Where(x => x.MonthYear == monthly).FirstOrDefault();
                if (t == null) continue;

                foreach (var subitem in t.Balances.Where(y => y.LeaveCategoryType == item.LeaveCategoryType))
                {
                    subitem.TotalLeavesTaken = 0;
                    if (item.LeaveCategoryType != LeaveCategoryType.LeaveWithoutPay)
                    {
                        subitem.TotalLeavesTaken = (item.ApprovedDays ?? 0);
                        if (item.LWPDays > 0)
                        {
                            var lwp = t.Balances.FirstOrDefault(y => y.LeaveCategoryType == LeaveCategoryType.LeaveWithoutPay);
                            if (lwp != null)
                                lwp.TotalLeavesTaken = ((lwp.TotalLeavesTaken ?? 0) + (item.LWPDays ?? 0));
                        }
                    }
                }
            }
            return result;
        }
        private async Task<List<EmployeeLeaveMonthlyBalanceModel>> GetMonthlyLeaveTypeInformation(int? contractId, int? monthWorked = null)
        {
            var groups = new List<EmployeeLeaveBalanceModel>();
            var leaveApplications = await _employeeLeaveStore.GetEmployeeLeaveDetails(null, contractId, EmployeeLeaveStatusType.Approved, monthWorked);
            if (leaveApplications != null && leaveApplications.Any())
            {
                var monthlyBalance = leaveApplications?.ToEmployeeLastMonthsLeaveData().ToList();
                groups.AddRange(monthlyBalance.GroupBy(x => new { x.Month, x.Year, x.LeaveType }).Select(x => new EmployeeLeaveBalanceModel
                {
                    LeaveType = x.Key.LeaveType,
                    Month = x.Key.Month,
                    Year = x.Key.Year,
                    ApprovedDays = x.Sum(y => y.ApprovedDays) ?? 0,
                    LWPDays = x.Sum(y => y.LWPDays) ?? 0,
                }).ToList());
            }

            if (groups == null || (groups != null && groups.Count == 0) && monthWorked <= 0) return null;

            var workingMonth = _generalUtilities.GetMonths(DateTime.UtcNow, monthWorked, true);
            var months = groups.Select(x => GeneralExtensions.GetMonthNumber(x.Month)).ToList();
            var types = groups.Select(x => x.LeaveType).ToList();
            var xLeaveType = await _employeeLeaveStore.GetLeaveType();

            var result = new List<EmployeeLeaveMonthlyBalanceModel>();
            foreach (var item in workingMonth)
            {
                var t = new EmployeeLeaveMonthlyBalanceModel
                {
                    MonthYear = $"{item.MonthLabel} {item.Year}"
                };
                foreach (var subitem in xLeaveType)
                {
                    t.Balances.AddRange(new List<EmployeeLeaveBalanceModel>
                    {
                        new EmployeeLeaveBalanceModel
                        {
                            LeaveType = GeneralExtensions.ToEnum<LeaveTypes>(subitem.Name),
                        }
                    });
                }
                result.Add(t);
            }

            foreach (var item in groups)
            {
                var monthly = $"{item.Month} {item.Year}";
                var t = result.Where(x => x.MonthYear == monthly).FirstOrDefault();
                if (t == null) continue;

                foreach (var subitem in t.Balances.Where(y => y.LeaveType == item.LeaveType))
                {
                    subitem.TotalLeavesTaken = 0;
                    if (item.LeaveType != LeaveTypes.LeaveWithoutPay)
                    {
                        subitem.TotalLeavesTaken = (item.ApprovedDays ?? 0);
                        if (item.LWPDays > 0)
                        {
                            var lwp = t.Balances.FirstOrDefault(y => y.LeaveType == LeaveTypes.LeaveWithoutPay);
                            if (lwp != null)
                                lwp.TotalLeavesTaken = ((lwp.TotalLeavesTaken ?? 0) + (item.LWPDays ?? 0));
                        }
                    }
                }
            }
            return result;
        }
        private async Task<IEnumerable<EmployeeLeaveBalanceInfoModel>> GetBalanceInformation(IEnumerable<EmployeeLeaveBalanceModel> data)
        {
            if (data == null || (data != null && data.Count() == 0)) return null;

            var contractId = data.Select(x => x.ContractId).FirstOrDefault();
            var isCurrent = await _employeeContractStore.CheckCurrentContract(contractId);
            var contract = await _employeeContractStore.GetRunningEmployeeContract(contractId);
            if (contract == null) return null;

            var probationDate = contract.ContractStartDate.AddMonths(contract.ProbationPeriod);

            var t = new List<EmployeeLeaveBalanceInfoModel>();
            foreach (var item in data)
            {
                if (item.LeaveType == LeaveTypes.CarryForward)
                {
                    t.Add(new EmployeeLeaveBalanceInfoModel
                    {
                        LeaveType = "Carried Leave",
                        Total = item.TotalLeaves ?? 0,
                        Order = 1
                    });
                }
                else if (item.LeaveType == LeaveTypes.FlatLeave)
                {
                    t.Add(new EmployeeLeaveBalanceInfoModel
                    {
                        LeaveType = "Used Leave",
                        Total = item.TotalLeavesTaken ?? 0,
                        Order = 2
                    });
                }
            }

            var startDate = contract.ContractStartDate;
            if (startDate.Year > DateTime.UtcNow.Year)
            {
                startDate = DateTime.Now;
            }
            var endDate = contract.ContractEndDate;
            if (isCurrent == true && DateTime.Now < endDate)
            {
                endDate = DateTime.Now;
            }
            var accrued = ((contract.ProbationPeriod > 0 && probationDate > DateTime.UtcNow) ? 0 : await _employeeLeaveStore.GetEmployeeLeaveTransactionsCount(contract.Id));  //_generalUtilities.GetAvailableLeaveCount(startDate, endDate, ((double)LeaveTally.FL)));
            if (contract.DesignationTypeId == (int)DesignationTypes.ProjectTrainee && contract.TrainingPeriod > 0 && contract.ContractStartDate.AddMonths(contract.TrainingPeriod) > DateTime.Now)
            {
                accrued = 0;
            }
            t.Add(new EmployeeLeaveBalanceInfoModel
            {
                LeaveType = "Accrued Leave",
                Total = accrued ?? 0,
                Order = 3
            });

            return t?.OrderBy(x => x.Order).ToList();
        }
        private async Task<IEnumerable<EmployeeLeaveBalanceInfoModel>> GetBalanceInformation(IEnumerable<EmployeeLeaveModel> data)
        {
            if (data == null || (data != null && data.Count() == 0))
            {
                return null;
            }

            var contractId = data.Select(x => x.EmployeeContractId).FirstOrDefault();
            var balance = await _employeeLeaveStore.GetCurrentContractLeavesBalance(contractId);
            var isCurrent = await _employeeContractStore.CheckCurrentContract(contractId);
            var contract = await _employeeContractStore.GetRunningEmployeeContract(contractId.Value);

            if (contract == null)
            {
                return null;
            }

            var probationDate = contract.ContractStartDate.AddMonths(contract.ProbationPeriod);

            var info = new List<EmployeeLeaveBalanceInfoModel>();
            foreach (var item in data)
            {
                if (item.LeaveType.Id == (int)LeaveTypes.CarryForward)
                {
                    info.Add(new EmployeeLeaveBalanceInfoModel
                    {
                        LeaveType = "Carried Leave",
                        Total = item.TotalLeaves,
                        Order = 1
                    });
                }
                else if (item.LeaveType.Id == (int)LeaveTypes.FlatLeave)
                {
                    info.Add(new EmployeeLeaveBalanceInfoModel
                    {
                        LeaveType = "Used Leave",
                        Total = item.TotalLeavesTaken,
                        Order = 2
                    });
                }
                else if (item.LeaveType.Id == (int)LeaveTypes.LeaveWithoutPay)
                {
                    info.Add(new EmployeeLeaveBalanceInfoModel
                    {
                        LeaveType = "LWP",
                        Total = item.TotalLeavesTaken,
                        Order = 4
                    });
                }
            }

            info.Add(new EmployeeLeaveBalanceInfoModel
            {
                LeaveType = "Balance Leave",
                Total = balance ?? 0,
                Order = 5
            });

            var startDate = contract.ContractStartDate;
            if (contract.DesignationTypeId == (int)DesignationTypes.ProjectTrainee && contract.TrainingPeriod > 0 && contract.IsProjectTrainee)
            {
                startDate = contract.ContractStartDate.AddMonths(contract.TrainingPeriod);
            }
            
            if (startDate.Year > DateTime.UtcNow.Year)
            {
                startDate = DateTime.Now;
            }
            
            var endDate = contract.ContractEndDate;
            if (isCurrent == true && DateTime.Now < endDate)
            {
                endDate = DateTime.Now;
            }
            
            var accrued = (contract.ProbationPeriod > 0 && probationDate > DateTime.UtcNow ? 0 : await _employeeLeaveStore.GetEmployeeLeaveTransactionsCount(contract.Id));
            if (contract.DesignationTypeId == (int)DesignationTypes.ProjectTrainee && contract.TrainingPeriod > 0 && contract.ContractStartDate.AddMonths(contract.TrainingPeriod) > DateTime.Now)
            {
                accrued = 0;
            }

            info.Add(new EmployeeLeaveBalanceInfoModel
            {
                LeaveType = "Accrued Leave",
                Total = accrued ?? 0,
                Order = 3
            });

            return info?.OrderBy(x => x.Order).ToList();
        }
        #endregion Flat System
        #endregion Leave Detail
        #region Leave Transactions
        public async Task<EmployeeLeaveTransactionDetailModel> GetEmployeeLeaveTransactions(string employeeCode)
        {
            var data = await _employeeLeaveStore.GetEmployeeLeaveTransactions(employeeCode);
            var employee = await _employeeService.GetEmployeeInformationByCode(employeeCode);
            var transactions = _mapper.Map<IEnumerable<EmployeeLeaveTransactionModel>>(data);
            return new EmployeeLeaveTransactionDetailModel
            {
                Employee = employee,
                Transactions = transactions
            };
        }
        public async Task<EmployeeLeaveTransactionModel> UpdateEmployeeLeaveTransactions(LeaveTransactionUpdateRequestModel model)
        {
            var result = await _employeeLeaveStore.UpdateEmployeeLeaveTransactions(model);
            return result ? new EmployeeLeaveTransactionModel { Added = model.Added, Used = model.Used, Balance = model.Balance, LWP = model.LWP } : null;
        }
        public async Task<EmployeeLeaveTransactionModel> AddEmployeeLeaveTransactions(LeaveTransactionUpdateRequestModel model)
        {
            var result = await _employeeLeaveStore.AddEmployeeLeaveTransactions(model);
            return (result == null ? null : _mapper.Map<EmployeeLeaveTransactionModel>(result));
        }
        #endregion Leave Transactions
        #region Other
        #endregion Other
    }
}