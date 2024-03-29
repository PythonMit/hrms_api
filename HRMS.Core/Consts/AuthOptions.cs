namespace HRMS.Core.Consts
{
    public class AuthOptions
    {
        public const string CookieKey = ".AspNetCore.HRMS.Id";
        public const string DemoModeEnabled = "HRMSAPI.DemoModeEnabled";
        public const string Issuer = "HRMSAPI";
        public const int Lifetime = 60 * 24 * 1;//one day in minutes //60 * 24 * 360; // one year in minutes
        public const string SecurityKey = "a12b1917-b3d9-46be-a853-a65c3bb963c6";
        public const string BranchId = "BranchId";
        public const string CORSPolicy = "HRMSCors";
        public const string AuthPolicy = "HRMSUserPolicy";
        public const string HashKey = "HRMSENCRYPTPASSWORD9B522E695D4F2"; //32
        public const string EmployeeId = "EmployeeId";
        public const string EmployeeCode = "EmployeeCode";
        public const string Environment = "Environment";
        public const string Priority = "Priority";
    }
}
