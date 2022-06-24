using IPFIN.Domain.PostcodeModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IPFIN.Infrastructure.Postcode.Service
{
    public interface IPostcodesApiService
    {
        Task<APIResponse<PostcodeDetail>> GetPostcodeDetailAsync(string postcode);
        Task<APIResponse<List<string>>> GetPostcodeAutocomplete(string postcode);
    }
}
