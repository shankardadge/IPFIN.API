using AutoMapper;
using IPFIN.API.Models;
using IPFIN.Infrastructure.Postcode.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace IPFIN.API.Controllers
{
    /// <summary>
    /// Postcodes services
    /// </summary>
    [Route("api/[controller]")]
    public class PostcodesController : Controller
    {
        private readonly IPostcodesApiService _postcodesApiService;
        private readonly Serilog.ILogger _logger;
        private readonly IMapper _mapper;
        public PostcodesController(IPostcodesApiService postcodesApiService, Serilog.ILogger logger, IMapper mapper)
        {
            _postcodesApiService = postcodesApiService;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Provide the post code detail
        /// </summary>
        /// <param name="postcode"></param>
        /// <returns></returns>
        [HttpGet("{postcode}")]
        public async Task<APIResponseDto<PostcodeDetailDto>> Get(string postcode)
        {
            APIResponseDto<PostcodeDetailDto> apiResponse = new APIResponseDto<PostcodeDetailDto>();
            try
            {
                _logger.Information("Log entered from PostcodesApi GetPostcodeDetailAsync Service");
                var response = await  _postcodesApiService.GetPostcodeDetailAsync(postcode);
                apiResponse = _mapper.Map<APIResponseDto<PostcodeDetailDto>>(response);

            }
            catch (Exception ex)
            {
                apiResponse.IsSuccess = false;
                apiResponse.Message = "Error occored while calling Lookup postcode API";
                _logger.Error(ex, "Error occored while calling Lookup postcode API");
            }
            return apiResponse;
        }
        /// <summary>
        /// Provide the search options by postcode
        /// </summary>
        /// <param name="postcode"></param>
        /// <returns></returns>
        [HttpGet("autocomplete")]
        public async Task<APIResponseDto<List<string>>> Autocomplete(string postcode)
        {
            APIResponseDto<List<string>> apiResponse = new APIResponseDto<List<string>>();
            try
            {
                _logger.Information("Log entered from PostcodesApi GetPostcodeAutocomplete Service");
                var response = await _postcodesApiService.GetPostcodeAutocomplete(postcode);
                apiResponse = _mapper.Map<APIResponseDto<List<string>>>(response);
            }
            catch (Exception ex)
            {
                apiResponse.IsSuccess = false;
                apiResponse.Message = "Error occored while calling autocomplete postcode API";
                _logger.Error(ex, "Error occored while calling autocomplete postcode API");
            }
            return apiResponse;
        }


    }
}
