using KaupunkipyoraAPI.Contracts;
using Moq;

namespace KaupunkipyoraAPI.Tests.Mocks
{
    internal class MockIUnitOfWork
    {
        public static Mock<IUnitOfWork> GetMock()
        {
            var mock = new Mock<IUnitOfWork>();
            var mockUserRepository = MockIUserRepository.GetMock();
            var mockBikeRouteRepository = MockIBikeRouteRepository.GetMock();

            mock.Setup(m => m.UserRepository).Returns(() => mockUserRepository.Object);
            mock.Setup(m => m.BikeRouteRepository).Returns(() => mockBikeRouteRepository.Object);

            return mock;
        }
    }
}
