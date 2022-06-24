using AutoFixture;
using AutoMapper;
using IPFIN.API.Controllers;
using IPFIN.API.DTOMappers;
using IPFIN.API.Models;
using IPFIN.Infrastructure.Postcode.Service;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Serilog;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace IPFIN.API.Tests
{
    public class PostcodesControllerTest
    {
        private readonly Mock<Serilog.ILogger> _mockLogger;
        private readonly Mock<IPostcodesApiService> _mockPostcodesApiService;
        private static IMapper _mapper;
        private readonly PostcodesController _postcodesController;
        /// <summary>
        /// Initialize Mock 
        /// </summary>
        public PostcodesControllerTest()
        {
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new PostcodeMapper());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }
            _mockLogger = new Mock<Serilog.ILogger>();
            _mockPostcodesApiService = new Mock<IPostcodesApiService>();
            _postcodesController = new PostcodesController(_mockPostcodesApiService.Object, _mockLogger.Object, _mapper);
        }

        [Fact]
        public async Task TestGetPostcodeDetails_Success()
        {

            var result = Task.Run(() =>
            {
                return new Domain.PostcodeModels.APIResponse<Domain.PostcodeModels.PostcodeDetail>()
                {
                    Result = new Domain.PostcodeModels.PostcodeDetail()
                    {
                        Country = "England",
                        AdminDistrict = "South East",
                        Area = "South Oxfordshire",
                        ParliamentaryConstituency = "Henley",
                        Region = "South"
                    },
                    IsSuccess = true,
                    Message = "Success"
                };

            });
            // Arrange
            _mockPostcodesApiService.Setup(x => x.GetPostcodeDetailAsync("OX49")).Returns(result);

            // Act
            var response = await _postcodesController.Get("OX49");

            // Assert
            Assert.True(response.IsSuccess);
            Assert.Equal("Success", response.Message);
            Assert.Equal(response.Result.Area, result.Result.Result.Area);
            Assert.Equal(response.Result.Region, result.Result.Result.Region);
            Assert.Equal(response.Result.Country, result.Result.Result.Country);
            Assert.Equal(response.Result.AdminDistrict, result.Result.Result.AdminDistrict);
            Assert.Equal(response.Result.ParliamentaryConstituency, result.Result.Result.ParliamentaryConstituency);
        }

        [Fact]
        public async Task TestGetPostcodeDetails_InvalidPostcodeFail()
        {

            var result = Task.Run(() =>
            {
                return new Domain.PostcodeModels.APIResponse<Domain.PostcodeModels.PostcodeDetail>()
                {
                    Result = null,
                    IsSuccess = false,
                    Message = "Invalid postcode"
                };

            });
            // Arrange
            _mockPostcodesApiService.Setup(x => x.GetPostcodeDetailAsync("OX49X2")).Returns(result);

            // Act
            var response = await _postcodesController.Get("OX49X2");

            // Assert
            Assert.False(response.IsSuccess);
            Assert.Equal("Invalid postcode", response.Message);
        }

        [Fact]
        public async Task TestGetPostcodeAutocomplete_Success()
        {

            var result = Task.Run(() =>
            {
                return new Domain.PostcodeModels.APIResponse<List<string>>()
                {
                    Result = new List<string>()
                    {
                        "OX10 0AB",
                        "OX10 0AD",
                        "OX10 0AF",
                        "OX10 0AJ",
                        "OX10 0AL",
                        "OX10 0AN",
                        "OX10 0AP",
                        "OX10 0AQ",
                        "OX10 0AR",
                        "OX10 0AS"
                    },
                    IsSuccess = true,
                    Message = "Success"
                };

            });
            // Arrange
            _mockPostcodesApiService.Setup(x => x.GetPostcodeAutocomplete("OX")).Returns(result);

            // Act
            var response = await _postcodesController.Autocomplete("OX");

            // Assert
            Assert.True(response.IsSuccess);
            Assert.Equal("Success", response.Message);
            Assert.True(response.Result.Count > 0);
            Assert.Equal(response.Result.Count, result.Result.Result.Count);
         
        }

        [Fact]
        public async Task TestGetPostcodePassInvalidCode_Fail()
        {

            var result = Task.Run(() =>
            {
                return new Domain.PostcodeModels.APIResponse<List<string>>()
                {
                    Result = new List<string>()
                    {

                    },
                    IsSuccess = false,
                    Message = "null"
                };

            });
            // Arrange
            _mockPostcodesApiService.Setup(x => x.GetPostcodeAutocomplete("OX")).Returns(result);

            // Act
            var response = await _postcodesController.Autocomplete("OX");

            // Assert
            Assert.False(response.IsSuccess);
            Assert.NotEqual("Success", response.Message);
            Assert.False(response.Result.Count > 0);

        }
    }
}
