using KaupunkipyoraAPI.Contracts;
using KaupunkipyoraAPI.Models.Entity;
using Moq;
using System.Reflection.Metadata.Ecma335;

namespace KaupunkipyoraAPI.Tests.Mocks
{
    internal class MockIBikeRouteRepository
    {
        public static BikeRoute MockBikeRouteData = new()
        {
            Id = 1,
            DepartureTime = DateTime.Now.AddDays(-10),
            ReturnTime = DateTime.Now.AddDays(-10).AddHours(1),
            DepartureStationId = 1,
            DepartureStationName = "Station 1",
            ReturnStationId = 2,
            ReturnStationName = "Station 2",
            CoveredDistanceInMeters = 1000,
            DurationInSeconds = 600,
            Created = DateTime.Now.AddDays(-9),
            CreatedById = 1,
            Updated = DateTime.Now.AddDays(-8),
            UpdatedById = 2
        };

        public static Mock<IBikeRouteRepository> GetMock()
        {
            var mock = new Mock<IBikeRouteRepository>();

            List<BikeRoute> bikeRoutes = new()
            {
                MockBikeRouteData
            };

            mock.Setup(m => m.GetAllAsync())
                .Returns(() => Task.FromResult(bikeRoutes.AsEnumerable()));

            mock.Setup(m => m.GetByIdAsync(It.IsAny<int>()))
                .Returns((int id) => Task.FromResult(bikeRoutes.FirstOrDefault(x => x.Id == id)));

            mock.Setup(m => m.AddAsync(It.IsAny<BikeRoute>()))
                .Callback(() => { return; });

            mock.Setup(m => m.UpdateAsync(It.IsAny<BikeRoute>()))
                .Callback(() => { return; });

            mock.Setup(m => m.DeleteAsync(It.IsAny<int>()))
                .Callback(() => { return; });

            return mock;
        }
    }
}
