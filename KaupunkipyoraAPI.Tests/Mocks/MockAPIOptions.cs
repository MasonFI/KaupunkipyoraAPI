using KaupunkipyoraAPI.Contracts;
using KaupunkipyoraAPI.Services.Settings;
using Microsoft.Extensions.Options;
using Moq;

namespace KaupunkipyoraAPI.Tests.Mocks
{
    internal class MockAPIOptions
    {
        public static Mock<IOptionsMonitor<APIOptions>> GetMock()
        {
            var options = new APIOptions()
            {
                ConnectionStrings = new(),
                JWT = new JWTOptions(),
            };

            var mock = new Mock<IOptionsMonitor<APIOptions>>();
            mock.Setup(m => m.CurrentValue).Returns(options);

            return mock;
        }
    }
}
