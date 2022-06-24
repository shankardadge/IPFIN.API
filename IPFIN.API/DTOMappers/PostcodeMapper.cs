using AutoMapper;
using IPFIN.API.Models;
using IPFIN.Domain.PostcodeModels;
using System.Collections.Generic;

namespace IPFIN.API.DTOMappers
{
    /// <summary>
    /// DTO mapper layer
    /// </summary>
    public class PostcodeMapper : Profile
    {
        public PostcodeMapper()
        {
            CreateMap<PostcodeDetail, PostcodeDetailDto>();
            CreateMap<APIResponse<PostcodeDetail>, APIResponseDto<PostcodeDetailDto>>();
            CreateMap<APIResponse<List<string>>, APIResponseDto<List<string>>>();
        }
    }
}
