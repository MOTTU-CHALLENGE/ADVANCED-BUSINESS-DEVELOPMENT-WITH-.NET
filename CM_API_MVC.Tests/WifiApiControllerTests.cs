using CM_API_MVC.Contexts;
using CM_API_MVC.Controllers.Api.v1;
using CM_API_MVC.Dtos.Wifi;
using CM_API_MVC.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CM_API_MVC.Tests
{
    public class WifiApiControllerTests
    {
        private readonly WifiApiController _controller;

        public WifiApiControllerTests()
        {
            var services = new ServiceCollection();

            // Banco InMemory exclusivo para o teste
            services.AddDbContext<AppDbContext>(options =>
                options.UseInMemoryDatabase("TestDb_Wifi"));

            services.AddScoped<WifiRepository>();

            var provider = services.BuildServiceProvider();
            var context = provider.GetRequiredService<AppDbContext>();

            var repo = provider.GetRequiredService<WifiRepository>();

            _controller = new WifiApiController(repo);

        }

        [Fact]
        public async Task GetAll_DeveRetornarLista()
        {
            var result = await _controller.GetAll();


            var actionResult = Assert.IsType<ActionResult<IEnumerable<WifiDto>>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var lista = Assert.IsAssignableFrom<IEnumerable<WifiDto>>(okResult.Value);

            Assert.NotNull(lista);
        }

        [Fact]
        public async Task Create_DeveCriarItem()
        {

            var novo = new NovoWifiDto
            {
                IdPatio = 1,
                LocalInstalacao = "x",
                EnderecoMac = "y",
                Status = "I",
                DataInstalacao = DateTime.Now,
            };

            var result = await _controller.Create(novo);

            var resultObj = Assert.IsType<ActionResult<WifiDto>>(result);
            var createdResult = Assert.IsType<CreatedAtActionResult>(resultObj.Result);
            var createdItem = Assert.IsType<WifiDto>(createdResult.Value);

        }

        [Fact]
        public async Task Delete_DeveDeletarItem()
        {

            var wifiDto = new NovoWifiDto
            {
                IdPatio = 1,
                LocalInstalacao = "x",
                EnderecoMac = "y",
                Status = "I",
                DataInstalacao = DateTime.Now,
            };

            var resultCreate = await _controller.Create(wifiDto);
            var resultAction = Assert.IsType<ActionResult<WifiDto>>(resultCreate);
            var createdResult = Assert.IsType<CreatedAtActionResult>(resultAction.Result);
            var createdItem = Assert.IsType<WifiDto>(createdResult.Value);


            var result = await _controller.Delete(id: createdItem.IdLeitor);

            Assert.IsType<NoContentResult>(result);
        }
    }
}
