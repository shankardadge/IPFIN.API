namespace IPFIN.API.Models
{
    public class APIResponseDto<T>
    {
        // <summary>
        /// API response object
        /// </summary>
        public T Result { get; set; }

        /// <summary>
        /// Success/Failure message
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// Is api response is successfull
        /// </summary>
        public bool IsSuccess { get; set; }
    }
}
