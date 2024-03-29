using System.Collections.Generic;

namespace HRMS.Core.Consts
{
    public static class GenericConst
    {
        public static string HalfDay = "08:30:00"; // Equivalent to 14:00:00 i.e. half day
        public static List<string> FixGrossTags = new List<string> { "basic", "hra", "lta", "conveyanceallowance" };
        /// <summary>
        /// Anchored Employee Code
        /// Darshan Prajapati = NS111303
        /// Kaushik Patel = NS111301
        /// Mayur Bhavsar = NS111304
        /// Parimal Dhimmar = NS111302
        /// Vikram Sisodiya = NS111305
        /// Bhavesh Patel = NS111306
        /// Mitul Patel = NS021407
        /// </summary>
        public static List<string> AnchoredEmployeeCode = new List<string> { "NS111303", "NS111301", "NS111304", "NS111302", "NS111305", "NS111306", "NS021407" };

    }
}
