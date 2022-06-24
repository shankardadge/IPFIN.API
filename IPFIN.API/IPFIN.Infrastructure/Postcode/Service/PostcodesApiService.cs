using IPFIN.Domain.PostcodeModels;
using IPFIN.Infrastructure.Postcode.Dto;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IPFIN.Infrastructure.Postcode.Service
{
    /// <summary>
    /// Postcode io service integration 
    /// </summary>
    public class PostcodesApiService : IPostcodesApiService
    {
        private readonly string _postcodeServiceUrl;
        private readonly string _autoCompleteServiceURL;
        public PostcodesApiService()
        {
            
            _postcodeServiceUrl = Environment.GetEnvironmentVariable("PostcodeServiceBaseUrl");
            _autoCompleteServiceURL= Environment.GetEnvironmentVariable("AutoCompleteServiceURL");
        }

        /// <summary>
        /// Get Postcode Lookup service integration
        /// </summary>
        /// <param name="postcode"></param>
        /// <returns></returns>
        public async Task<APIResponse<PostcodeDetail>> GetPostcodeDetailAsync(string postcode)
        {
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            httpClientHandler.UseDefaultCredentials = true;
            APIResponse<PostcodeDetail> apiResponse = new APIResponse<PostcodeDetail>();
            using (var client = new HttpClient(httpClientHandler))
            {
                string url = $"{_postcodeServiceUrl}/{postcode}";
                var response = await client.GetAsync(url);
                string content = await response.Content.ReadAsStringAsync();
                PostcodeServiceResponseDto<PostcodeDetailServiceDto> postcodeDetailServiceResponse = JsonConvert.DeserializeObject<PostcodeServiceResponseDto<PostcodeDetailServiceDto>>(content);
                if (response.IsSuccessStatusCode)
                {
                    if (postcodeDetailServiceResponse.status == (int)HttpStatusCode.OK)
                    {
                        PostcodeDetail postcodeDetailModel = null;
                        if (postcodeDetailServiceResponse.result != null)
                        {
                            postcodeDetailModel = new PostcodeDetail();
                            postcodeDetailModel.AdminDistrict = postcodeDetailServiceResponse.result.admin_district;
                            postcodeDetailModel.ParliamentaryConstituency = postcodeDetailServiceResponse.result.parliamentary_constituency;
                            postcodeDetailModel.Region = postcodeDetailServiceResponse.result.region;
                            postcodeDetailModel.Country = postcodeDetailServiceResponse.result.country;
                            if (postcodeDetailServiceResponse.result.latitude < 52.229466)
                            {
                                postcodeDetailModel.Area = "South";
                            }
                            else if (postcodeDetailServiceResponse.result.longitude >= 52.229466 && postcodeDetailServiceResponse.result.longitude < 53.27169)
                            {
                                postcodeDetailModel.Area = "Midlands";
                            }
                            else
                            {
                                postcodeDetailModel.Area = "North";
                            }
                        }
                        apiResponse.Result = postcodeDetailModel;
                        apiResponse.IsSuccess = true;
                        apiResponse.Message = "Success";
                    }
                    else
                    {
                        apiResponse.IsSuccess = false;
                        apiResponse.Message = postcodeDetailServiceResponse.error;
                    }
                }
                else
                {
                    apiResponse.IsSuccess = false;
                    apiResponse.Message = postcodeDetailServiceResponse.error;
                }
            }
            return apiResponse;
        }

        /// <summary>
        /// Get Postcode autocomplete service integration
        /// </summary>
        /// <param name="postcode"></param>
        /// <returns></returns>
        public async Task<APIResponse<List<string>>> GetPostcodeAutocomplete(string postcode)
        {
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            httpClientHandler.UseDefaultCredentials = true;
            APIResponse<List<string>> apiResponse = new APIResponse<List<string>>();
            using (var client = new HttpClient(httpClientHandler))
            {
                string url = $"{_postcodeServiceUrl}/{postcode}/{_autoCompleteServiceURL}";
                var response = await client.GetAsync(url);
                string content = await response.Content.ReadAsStringAsync();
                PostcodeServiceResponseDto<List<string>> postcodeDetailServiceResponse = JsonConvert.DeserializeObject<PostcodeServiceResponseDto<List<string>>>(content);
                if (response.IsSuccessStatusCode)
                {
                    if (postcodeDetailServiceResponse.status == (int)HttpStatusCode.OK)
                    {
                        if (postcodeDetailServiceResponse.result != null)
                        {
                            apiResponse.Result = postcodeDetailServiceResponse.result;
                            apiResponse.IsSuccess = true;
                            apiResponse.Message = "Success";
                        }
                    }
                    else
                    {
                        apiResponse.IsSuccess = false;
                        apiResponse.Message = postcodeDetailServiceResponse.error;
                    }
                }
                else
                {
                    apiResponse.IsSuccess = false;
                    apiResponse.Message = postcodeDetailServiceResponse.error;
                }
            }
            return apiResponse;
        }
    }
}
