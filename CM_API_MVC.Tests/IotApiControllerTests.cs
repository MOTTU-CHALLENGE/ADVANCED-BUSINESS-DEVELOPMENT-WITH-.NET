using CM_API_MVC.Contexts;
using CM_API_MVC.Controllers.Api.v1;
using CM_API_MVC.Dtos.Filial;
using CM_API_MVC.Dtos.Iot;
using CM_API_MVC.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CM_API_MVC.Tests
{
    public class IotApiControllerTests
    {
        private readonly IotApiController _controller;

        public IotApiControllerTests()
        {
            var services = new ServiceCollection();

            // Banco InMemory exclusivo para o teste
            services.AddDbContext<AppDbContext>(options =>
                options.UseInMemoryDatabase("TestDb_Iot"));

            services.AddScoped<IotRepository>();

            var provider = services.BuildServiceProvider();
            var context = provider.GetRequiredService<AppDbContext>();

            var repo = provider.GetRequiredService<IotRepository>();

            _controller = new IotApiController(repo);

        }

        [Fact]
        public async Task GetAll_DeveRetornarLista()
        {
            var result = await _controller.GetAll();


            var actionResult = Assert.IsType<ActionResult<IEnumerable<IotDto>>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var lista = Assert.IsAssignableFrom<IEnumerable<IotDto>>(okResult.Value);

            Assert.NotNull(lista);
        }

        [Fact]
        public async Task Create_DeveCriarItem()
        {

            var novo = new NovoIotDto
            {
                Nome = "ESP32",
                DataInstalacao = DateTime.Now,
                IdMoto = 1
            };

            var result = await _controller.Create(novo);

            var resultObj = Assert.IsType<ActionResult<IotCadastradoDto>>(result);
            var createdResult = Assert.IsType<CreatedAtActionResult>(resultObj.Result);
            var createdItem = Assert.IsType<IotCadastradoDto>(createdResult.Value);

        }

        [Fact]
        public async Task Delete_DeveDeletarItem()
        {

            var itoDto = new NovoIotDto
            {
                Nome = "ESP32",
                DataInstalacao = DateTime.Now,
                IdMoto = 1
            };
            var resultCreate = await _controller.Create(itoDto);
            var resultAction = Assert.IsType<ActionResult<IotCadastradoDto>>(resultCreate);
            var createdResult = Assert.IsType<CreatedAtActionResult>(resultAction.Result);
            var createdItem = Assert.IsType<IotCadastradoDto>(createdResult.Value);


            var result = await _controller.Delete(id: createdItem.Id);

            Assert.IsType<NoContentResult>(result);
        }
    }
}
