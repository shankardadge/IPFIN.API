using System;
using System.Collections.Generic;
using System.Text;

namespace IPFIN.Domain.PostcodeModels
{
    /// <summary>
    /// Postcode Detail Domain Model
    /// </summary>
    public class PostcodeDetail
    {
        /// <summary>
        /// Country
        /// </summary>
        public string Country { get; set; }
        /// <summary>
        /// Region
        /// </summary>
        public string Region { get; set; }
        /// <summary>
        /// AdminDistrict
        /// </summary>
        public string AdminDistrict { get; set; }
        /// <summary>
        /// ParliamentaryConstituency
        /// </summary>
        public string ParliamentaryConstituency { get; set; }
        /// <summary>
        /// Area
        /// </summary>
        public string Area { get; set; }
    }
}
