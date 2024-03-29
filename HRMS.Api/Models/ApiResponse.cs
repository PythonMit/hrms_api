using System.ComponentModel;

namespace HRMS.Api.Models
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ApiResponse<T>
    {
        /// <summary>
        /// 
        /// </summary>
        public bool IsError { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DefaultValue(null)]
        public bool? IsWarning { get; set; } = null;
        /// <summary>
        /// 
        /// </summary>
        public T Data { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public IDictionary<string, string[]> Errors { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagedResponse<T> : ApiResponse<T>
    {
        /// <summary>
        /// 
        /// </summary>
        [DefaultValue(1)]
        public int PageNumber { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DefaultValue(10)]
        public int PageSize { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int TotalPages { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int TotalRecords { get; set; }
    }
}
