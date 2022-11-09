using KaupunkipyoraAPI.Contracts;
using KaupunkipyoraAPI.Models.Entity;
using Moq;
using System.Reflection.Metadata.Ecma335;

namespace KaupunkipyoraAPI.Tests.Mocks
{
    internal class MockIUserRepository
    {
        public static Mock<IUserRepository> GetMock()
        {
            var mock = new Mock<IUserRepository>();

            List<User> users = new()
            {
                new User()
                {
                    Id = 1,
                    Username = "test",
                    Password = "test1234",
                    Email = "test@test.com",
                    IncorrectLoginCount = 1,
                    Created = DateTime.Now.AddDays(-9),
                    CreatedById = 1,
                    Updated = DateTime.Now.AddDays(-8),
                    UpdatedById = 2
                }
            };

            mock.Setup(m => m.GetAllAsync())
                .Returns(() => Task.FromResult(users.AsEnumerable()));

            mock.Setup(m => m.GetByIdAsync(It.IsAny<int>()))
                .Returns((int id) => Task.FromResult(users.FirstOrDefault(x => x.Id == id)));

            mock.Setup(m => m.AddAsync(It.IsAny<User>()))
                .Callback(() => { return; });

            mock.Setup(m => m.UpdateAsync(It.IsAny<User>()))
                .Callback(() => { return; });

            mock.Setup(m => m.DeleteAsync(It.IsAny<int>()))
                .Callback(() => { return; });

            return mock;
        }
    }
}
