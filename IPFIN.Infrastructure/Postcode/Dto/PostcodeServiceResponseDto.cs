using System;
using System.Collections.Generic;
using System.Text;

namespace IPFIN.Infrastructure.Postcode.Dto
{
    /// <summary>
    /// PostcodeServiceResponseDto
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PostcodeServiceResponseDto<T>
    {
        /// <summary>
        /// status
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// result
        /// </summary>
        public T result { get; set; }
        /// <summary>
        /// error
        /// </summary>
        public string error { get; set; }
    }
}
