namespace IPFIN.API.Models
{
    /// <summary>
    /// Post code detail DTO
    /// </summary>
    public class PostcodeDetailDto
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
        /// Admin District
        /// </summary>
        public string AdminDistrict { get; set; }
        /// <summary>
        /// Parliamentary Constituency
        /// </summary>
        public string ParliamentaryConstituency { get; set; }
        /// <summary>
        /// Area
        /// </summary>
        public string Area { get; set; }
    }
}
