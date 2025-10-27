using CM_API_MVC.Contexts;
using CM_API_MVC.Controllers.Api.v1;
using CM_API_MVC.Dtos.Patio;
using CM_API_MVC.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace CM_API_MVC.Tests
{
    public class PatioApiControllerTests
    {
        private readonly PatioApiController _controller;

        public PatioApiControllerTests()
        {
            var services = new ServiceCollection();

            // Banco InMemory exclusivo para o teste
            services.AddDbContext<AppDbContext>(options =>
                options.UseInMemoryDatabase("TestDb_Patio"));

            services.AddScoped<PatioRepository>();

            var provider = services.BuildServiceProvider();
            var context = provider.GetRequiredService<AppDbContext>();

            var repo = provider.GetRequiredService<PatioRepository>();

            var mockLinkGen = new Mock<LinkGenerator>();
            var helper = new PatioLinksHelper(mockLinkGen.Object);


            _controller = new PatioApiController(repo, helper);
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };

        }

        [Fact]
        public async Task GetAll_DeveRetornarLista()
        {
            var result = await _controller.GetAll();


            var actionResult = Assert.IsType<ActionResult<IEnumerable<PatioComWifiDto>>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var lista = Assert.IsAssignableFrom<IEnumerable<PatioComWifiDto>>(okResult.Value);

            Assert.NotNull(lista);
        }

        [Fact]
        public async Task Create_DeveCriarItem()
        {

            var novo = new NovoPatioDto
            {
                IdFilial = 1,
                NomePatio = "Patio",
                CapacidadeMax = 10,
                Area = 20
            };

            var result = await _controller.Create(novo);

            var resultObj = Assert.IsType<ActionResult<PatioComWifiHatDto>>(result);
            var createdResult = Assert.IsType<CreatedAtActionResult>(resultObj.Result);
            var createdItem = Assert.IsType<PatioComWifiHatDto>(createdResult.Value);

        }

        [Fact]
        public async Task Delete_DeveDeletarItem()
        {

            var PatiolDto = new NovoPatioDto
            {
                IdFilial = 1,
                NomePatio = "Patio",
                CapacidadeMax = 10,
                Area = 20
            };

            var resultCreate = await _controller.Create(PatiolDto);
            var resultAction = Assert.IsType<ActionResult<PatioComWifiHatDto>>(resultCreate);
            var createdResult = Assert.IsType<CreatedAtActionResult>(resultAction.Result);
            var createdItem = Assert.IsType<PatioComWifiHatDto>(createdResult.Value);


            var result = await _controller.Delete(id: createdItem.IdPatio);

            Assert.IsType<NoContentResult>(result);
        }
    }
}
