using System;
using System.Collections.Generic;
using System.Text;

namespace IPFIN.Infrastructure.Postcode.Dto
{
    /// <summary>
    /// Post code detail lookup service DTO
    /// </summary>
    public class PostcodeDetailServiceDto
    {
        /// <summary>
        /// postcode
        /// </summary>
        public string postcode { get; set; }
        /// <summary>
        /// country
        /// </summary>
        public string country { get; set; }
        /// <summary>
        /// region
        /// </summary>
        public string region { get; set; }
        /// <summary>
        /// parliamentary_constituency
        /// </summary>
        public string parliamentary_constituency { get; set; }
        /// <summary>
        /// admin_district
        /// </summary>
        public string admin_district { get; set; }
        /// <summary>
        /// latitude
        /// </summary>
        public double latitude { get; set; }
        /// <summary>
        /// longitude
        /// </summary>
        public double longitude { get; set; }
    }
}
