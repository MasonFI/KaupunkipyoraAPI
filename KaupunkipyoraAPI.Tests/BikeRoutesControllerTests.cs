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
        public async Task Test_GetAsync()
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
    }
}
