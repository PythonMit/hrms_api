using HRMS.Core.Consts;
using HRMS.Core.Exstensions;
using HRMS.Core.Models.Contract;
using HRMS.Core.Models.Employee;
using HRMS.Core.Models.General;
using HRMS.Core.Models.Salary;
using HRMS.DBL.DbContextConfiguration;
using HRMS.DBL.Entities;
using HRMS.DBL.Stores;
using LinqKit;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace HRMS.DBL.Extensions
{
    public static class EmployeeSalaryMappingExtension
    {
        public static IQueryable<EmployeeSalaryModel> ToEmployeeSalary(this IQueryable<EmployeeEarningGross> data)
        {
            return data.Select(s => new EmployeeSalaryModel
            {
                Id = s.Id,
                EmployeeContractId = s.EmployeeContractId,
                Remarks = s.Remarks,
                CalculationType = s.CalculationType,
                IsPartlyPaid = s.PartlyPaid,
                CreatedDate = s.CreatedDateTimeUtc,
                Employee = new EmployeeOutlineModel
                {
                    Id = s.EmployeeContract.Employee.Id,
                    Code = s.EmployeeContract.Employee.EmployeeCode,
                    Designation = s.EmployeeContract.Employee.DesignationType.Name,
                    Branch = s.EmployeeContract.Employee.Branch.Name,
                    BranchCode = s.EmployeeContract.Employee.Branch.Code,
                    Name = $"{(string.IsNullOrEmpty(s.EmployeeContract.Employee.FirstName) ? "" : s.EmployeeContract.Employee.FirstName + " ")}{(string.IsNullOrEmpty(s.EmployeeContract.Employee.MiddleName) ? "" : s.EmployeeContract.Employee.MiddleName + " ")}{(string.IsNullOrEmpty(s.EmployeeContract.Employee.LastName) ? "" : s.EmployeeContract.Employee.LastName)}",
                },
                EarningGross = new EarningGrossCalculationModel
                {
                    Id = s.Id,
                    Basic = s.Basic,
                    OtherAllowance = s.OtherAllowance + s.AdjustmentAmount,
                    ConveyanceAllowance = s.ConveyanceAllowance,
                    ChildEducation = s.ChildEducation,
                    MedicalAllowance = s.MedicalAllowance,
                    DA = s.DA,
                    LTA = s.LTA,
                    HRA = s.HRA,
                    LWP = s.LWP,
                    PaidDate = s.PaidDate,
                    PaidDays = s.PaidDays,
                    SalaryMonth = s.SalaryMonth,
                    Year = s.SalaryYear,
                    TotalDays = s.TotalDays,
                    FixIncentive = s.FixIncentive,
                    Incentive = s.Incentive,
                    CostToCompany = Convert.ToDouble(s.EmployeeContract.EmployeeFixGross.CostToCompany),
                    StipendAmount = s.EmployeeContract.EmployeeFixGross.StipendAmount,
                    TotalEarning = (s.Basic + s.DA + s.LTA + s.HRA + s.ConveyanceAllowance + (s.OtherAllowance + s.AdjustmentAmount) + s.OverTimeAmount + s.Incentive + s.FixIncentive),
                    TotalDeduction = (s.PT + s.TDS),
                    NetSalary = (s.NetSalary + s.OverTimeAmount + s.Incentive + s.FixIncentive + s.AdjustmentAmount),
                },
                CreatedBy = new EmployeeOutlineModel
                {
                    Id = s.Employee.Id,
                    Name = $"{(string.IsNullOrEmpty(s.Employee.FirstName) ? "" : s.Employee.FirstName + " ")}{(string.IsNullOrEmpty(s.Employee.LastName) ? "" : s.Employee.LastName)}",
                },
                OtherAllowance = new OtherAllowanceModel
                {
                    EmployeePF = s.EmployeePF,
                    EmployerPF = s.EmployerPF,
                    PT = s.PT,
                    TDS = s.TDS,
                    OverTimeAmount = s.OverTimeAmount,
                    AdjustmentAmount = s.AdjustmentAmount,
                    ESI = s.ESI,
                },
                Status = new EmployeeEarningGrossStatusModel
                {
                    Id = s.EmployeeEarningGrossStatus.Id,
                    StatusType = s.EmployeeEarningGrossStatus.StatusType,
                },
            });
        }
        public static EmployeeSalaryModel ToCombineEmployeeSalary(this IQueryable<EmployeeEarningGross> data)
        {
            var multiple = data.Select(s => new EmployeeSalaryModel
            {
                Id = s.Id,
                EmployeeContractId = s.EmployeeContractId,
                Remarks = s.Remarks,
                CalculationType = s.CalculationType,
                IsPartlyPaid = s.PartlyPaid,
                Employee = new EmployeeOutlineModel
                {
                    Id = s.EmployeeContract.Employee.Id,
                    Code = s.EmployeeContract.Employee.EmployeeCode,
                    Designation = s.EmployeeContract.Employee.DesignationType.Name,
                    Branch = s.EmployeeContract.Employee.Branch.Name,
                    BranchCode = s.EmployeeContract.Employee.Branch.Code,
                    Name = $"{(string.IsNullOrEmpty(s.EmployeeContract.Employee.FirstName) ? "" : s.EmployeeContract.Employee.FirstName + " ")}{(string.IsNullOrEmpty(s.EmployeeContract.Employee.MiddleName) ? "" : s.EmployeeContract.Employee.MiddleName + " ")}{(string.IsNullOrEmpty(s.EmployeeContract.Employee.LastName) ? "" : s.EmployeeContract.Employee.LastName)}",
                },
                CreatedBy = new EmployeeOutlineModel
                {
                    Id = s.Employee.Id,
                    Name = $"{(string.IsNullOrEmpty(s.Employee.FirstName) ? "" : s.Employee.FirstName + " ")}{(string.IsNullOrEmpty(s.Employee.LastName) ? "" : s.Employee.LastName)}",
                },
                Status = new EmployeeEarningGrossStatusModel
                {
                    Id = s.EmployeeEarningGrossStatus.Id,
                    StatusType = s.EmployeeEarningGrossStatus.StatusType,
                },
            }).FirstOrDefault();

            var t = data.GroupBy(x => x.EmployeeContractId).Select(x => x.FirstOrDefault()).ToList();
            var earningGross = t.Select(x => new EarningGrossCalculationModel
            {
                Id = x.Id,
                Basic = data.Sum(y => y.Basic),
                DA = data.Sum(y => y.DA),
                LTA = data.Sum(y => y.LTA),
                HRA = data.Sum(y => y.HRA),
                PaidDays = data.Sum(y => y.PaidDays),
                TotalDays = data.Select(y => y.TotalDays).FirstOrDefault(),
                LWP = data.Sum(y => y.LWP),
                OtherAllowance = data.Sum(y => y.OtherAllowance + y.AdjustmentAmount),
                ConveyanceAllowance = data.Sum(y => y.ConveyanceAllowance),
                MedicalAllowance = data.Sum(y => y.MedicalAllowance),
                ChildEducation = data.Sum(y => y.ChildEducation),
                SalaryMonth = x.SalaryMonth,
                Year = x.SalaryYear,
                FixIncentive = data.Sum(y => y.FixIncentive),
                Incentive = data.Sum(y => y.Incentive),
                CostToCompany = GetCostToCompany(data),
                StipendAmount = GetStipendAmount(data),
                TotalEarning = data.Sum(y => y.Basic + y.DA + y.LTA + y.HRA + y.ConveyanceAllowance + (y.OtherAllowance + y.AdjustmentAmount) + y.OverTimeAmount + y.Incentive + y.FixIncentive),
                TotalDeduction = data.Sum(y => y.PT + y.TDS),
                NetSalary = data.Sum(y => y.NetSalary + y.OverTimeAmount + y.Incentive + y.FixIncentive + y.AdjustmentAmount),
                PaidDate = x.PaidDate,

            }).FirstOrDefault();

            var otherAllowance = t.Select(x => new OtherAllowanceModel
            {
                EmployerPF = data.Sum(y => y.EmployerPF),
                EmployeePF = data.Sum(y => y.EmployeePF),
                PT = data.Sum(y => y.PT),
                TDS = data.Sum(y => y.TDS),
                OverTimeAmount = data.Sum(y => y.OverTimeAmount),
                AdjustmentAmount = data.Sum(y => y.AdjustmentAmount),
                ESI = data.Sum(y => y.ESI)
            }).FirstOrDefault();

            multiple.EarningGross = earningGross;
            multiple.OtherAllowance = otherAllowance;
            return multiple;
        }
        public static SalaryEmployeeDetailModel ToSalaryEmployeeDetails(this EmployeeFixGross data)
        {
            return new SalaryEmployeeDetailModel
            {
                CostToCompany = Convert.ToDouble(data.CostToCompany),
                StipendAmount = data.StipendAmount,
                IncentiveDuration = data.FixIncentiveDuration,
                IncentiveRemarks = data.FixIncentiveRemarks,
                HasFixIncentive = data.IsFixIncentive
            };
        }
        public static double GetCostToCompany(IQueryable<EmployeeEarningGross> data)
        {
            var t = data?.Where(x => x.PartlyPaid).FirstOrDefault();
            if (t != null)
            {
                var s = t?.EmployeeContract?.EmployeeFixGross?.CostToCompany ?? 0;
                var a = data?.Sum(x => x.EmployeeContract.EmployeeFixGross.CostToCompany) - s;
                return Convert.ToDouble(a);
            }
            else
            {
                return Convert.ToDouble(data?.Where(x => !x.PartlyPaid).FirstOrDefault().EmployeeContract?.EmployeeFixGross?.CostToCompany ?? 0);
            }
        }
        public static double GetStipendAmount(IQueryable<EmployeeEarningGross> data)
        {
            return Convert.ToDouble(data?.Where(x => !x.PartlyPaid).FirstOrDefault().EmployeeContract?.EmployeeFixGross?.StipendAmount ?? 0);
        }
        public static EmployeeFixGross GetContractDetails(EmployeeDetail data)
        {
            if (data.Employee.EmployeeContracts != null && data.Employee.EmployeeContracts.Count == 0 && data.Employee.EmployeeContracts.Any(x => x.RecordStatus == RecordStatus.Active && (x.EmployeeContractStatusId == (int)EmployeeContractStatusType.Running || x.EmployeeContractStatusId == (int)EmployeeContractStatusType.NoticePeriod)))
            {
                return data.Employee.EmployeeContracts.Select(x => x.EmployeeFixGross).FirstOrDefault();
            }

            return null;
        }
        public static IQueryable<EmployeeSalaryIncentiveModel> ToSalaryEmployeeIncentiveDetails(this IQueryable<EmployeeIncentiveDetails> data)
        {
            return data.Select(z => new EmployeeSalaryIncentiveModel
            {
                IncentiveAmount = z.IncentiveAmount,
                IncentiveDate = z.IncentiveDate,
                Status = new EmployeeIncentiveStatusModel
                {
                    Id = z.Id,
                    StatusType = z.EmployeeIncentiveStatus.StatusType
                }
            });
        }
        public static IEnumerable<EmployeeHoldSalaryModel> ToEmployeeHoldSalary(this IQueryable<EmployeeHoldSalary> data)
        {
            return data.Select(x => new EmployeeHoldSalaryModel
            {
                Id = x.Id,
                EmployeeContractId = x.EmployeeEarningGross.EmployeeContractId,
                EmployeeEarningGrossId = x.EmployeeEarningGrossId,
                HoldAmount = x.HoldAmount,
                NetSalary = x.EmployeeEarningGross.NetSalary,
                PaidAmount = x.EmployeeEarningGross.NetSalary - x.HoldAmount,
                PaidDate = x.PaidDate,
                RecordStatus = x.RecordStatus,
                Remarks = x.Remarks,
                Status = x.EmployeeSalaryStatus.StatusType,
                SalaryMonth = x.EmployeeEarningGross.SalaryMonth,
                SalaryYear = x.EmployeeEarningGross.SalaryYear,
                Employee = x.EmployeeEarningGross == null ? null : new EmployeeOutlineModel
                {
                    Name = $"{x.EmployeeEarningGross.EmployeeContract.Employee.FirstName} {x.EmployeeEarningGross.EmployeeContract.Employee.LastName}",
                    Code = x.EmployeeEarningGross.EmployeeContract.Employee.EmployeeCode,
                    Branch = x.EmployeeEarningGross.EmployeeContract.Employee.Branch.Name,
                    BranchCode = x.EmployeeEarningGross.EmployeeContract.Employee.Branch.Code,
                    Designation = x.EmployeeEarningGross.EmployeeContract.Employee.DesignationType.Name,
                }
            });
        }
        public static IEnumerable<BulkSalaryPaymentListModel> ToBulkSalaryPayments(this IQueryable<EmployeeDetail> data)
        {
            var result = data.ToList().Select((x, i) => new BulkSalaryPaymentListModel
            {
                EmployeeCode = x.Employee.EmployeeCode,
                JoinDate = x.JoinDate,
                BulkSalary = x.EmployeeBankDetail == null ? null : new BulkSalaryPaymentModel
                {
                    TransactionType = x.EmployeeBankDetail.TransactionType,
                    BeneficeryBankName = x.EmployeeBankDetail.BankName,
                    BeneficiaryAccountNumber = x.EmployeeBankDetail.BeneficiaryACNumber,
                    IFCCode = x.EmployeeBankDetail.IFSCCode,
                    BeneficiaryName = x.EmployeeBankDetail.BeneficiaryName,
                    Beneficiaryemailid = x.EmployeeBankDetail.BeneficiaryEmail,
                }
            }).Where(x => x.BulkSalary != null).ToList();

            var i = 1;
            result.ForEach(x =>
            {
                x.BulkSalary.BeneficiaryCode = $"SALARY{DateTime.Now.ToString("dd").PadLeft(3, '0')}{i.ToString().PadLeft(3, '0')}";
                x.BulkSalary.ChqTrnDate = $"{DateTime.Now.ToString("dd/MM/yyyy")}";
                x.BulkSalary.CustomerReferenceNumber = $"{DateTime.Now.ToString("ddMMyyyy")}{i.ToString().PadLeft(3, '0')}";
                i++;
            });
            return result;
        }
    }
}