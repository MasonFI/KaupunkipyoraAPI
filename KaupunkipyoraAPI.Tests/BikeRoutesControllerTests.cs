using AutoMapper;
using KaupunkipyoraAPI.Controllers;
using KaupunkipyoraAPI.Models.DTO;
using KaupunkipyoraAPI.Models.Profiles;
using KaupunkipyoraAPI.Services.Settings;
using KaupunkipyoraAPI.Tests.Mocks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Moq;
using NuGet.Frameworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaupunkipyoraAPI.Tests
{
    public class BikeRoutesControllerTests
    {
        public IMapper GetMapper()
        {
            var mappingProfile = new BikeRouteProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(mappingProfile));
            return new Mapper(configuration);
        }

        [Fact]
        public async Task GetAllAsync_Returns_ListOf_BikeRouteDTO()
        {
            var uowMock = MockIUnitOfWork.GetMock();
            var mapper = GetMapper();
            var apiOptionsMock = MockAPIOptions.GetMock();
            var controller = new BikeRoutesController(uowMock.Object, mapper, apiOptionsMock.Object);

            var result = await controller.GetAll() as ObjectResult;

            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            Assert.IsAssignableFrom<IEnumerable<BikeRouteDTO>>(result.Value);
            Assert.NotEmpty(result.Value as IEnumerable<BikeRouteDTO>);
        }

        [Fact]
        public async Task GetAsync_Returns_BikeRouteDTO()
        {
            var uowMock = MockIUnitOfWork.GetMock();
            var mapper = GetMapper();
            var apiOptionsMock = MockAPIOptions.GetMock();
            var controller = new BikeRoutesController(uowMock.Object, mapper, apiOptionsMock.Object);

            var result = await controller.Get(1) as ObjectResult;

            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            Assert.IsAssignableFrom<BikeRouteDTO>(result.Value);

            var mockBikeRoute = MockIBikeRouteRepository.MockBikeRouteData;
            var readdBikeRouteDTO = result.Value as BikeRouteDTO;

            Assert.NotNull(readdBikeRouteDTO);
            Assert.Equal(1, readdBikeRouteDTO.Id);
            Assert.Equal(mockBikeRoute.CoveredDistanceInMeters, readdBikeRouteDTO.CoveredDistanceInMeters);
        }

        [Fact]
        public async Task GetAsync_Returns_NotFound()
        {
            var uowMock = MockIUnitOfWork.GetMock();
            var mapper = GetMapper();
            var apiOptionsMock = MockAPIOptions.GetMock();
            var controller = new BikeRoutesController(uowMock.Object, mapper, apiOptionsMock.Object);

            var result = await controller.Get(3) as ObjectResult;

            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status404NotFound, result.StatusCode);
        }
    }
}
