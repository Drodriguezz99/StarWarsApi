using JediApi.Repositories;
using JediApi.Services;
using Moq;

namespace JediApi.Tests.Services
{
    public class JediServiceTests
    {
        // não mexer
        private readonly JediService _service;
        private readonly Mock<IJediRepository> _repositoryMock;

        public JediServiceTests()
        {
            // não mexer
            _repositoryMock = new Mock<IJediRepository>();
            _service = new JediService(_repositoryMock.Object);
        }

        [Fact]
        public async Task GetById_aSuccess()
        {
            {

                var expectedId = 1;
                var jedi = new Jedi { Id = expectedId, Name = "Jedi 1" };
                _repositoryMock.Setup(repo => repo.GetByIdAsync(expectedId)).ReturnsAsync(jedi);


                var result = await _service.GetByIdAsync(expectedId);


                Assert.NotNull(result);
                Assert.Equal(expectedId, result.Id);
                Assert.Equal("Jedi 1", result.Name);
            }
        }
        [Fact]
        public async Task GetById_NotFound()
        {

            var nonExistentId = 999;
            _repositoryMock.Setup(repo => repo.GetByIdAsync(nonExistentId)).ReturnsAsync((Jedi)null);


            var result = await _service.GetByIdAsync(nonExistentId);


            Assert.Null(result);
        }


        [Fact]
        public async Task GetAll()
        {

            var expectedJedis = new List<Jedi>
    {
        new Jedi { Id = 1, Name = "Jedi 1" },
        new Jedi { Id = 2, Name = "Jedi 2" }
    };
            _repositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(expectedJedis);


            var result = await _service.GetAllAsync();


            Assert.NotNull(result);
            Assert.Equal(expectedJedis.Count, result.Count());
            Assert.Equal("Jedi 1", result.First().Name);
            Assert.Equal("Jedi 2", result.Last().Name);
        }

    }
}
