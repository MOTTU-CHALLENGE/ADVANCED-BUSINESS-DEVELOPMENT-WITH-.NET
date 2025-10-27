using CM_API_MVC.Contexts;
using CM_API_MVC.Controllers.Api.v1;
using CM_API_MVC.Dtos.Filial;
using CM_API_MVC.Dtos.Moto;
using CM_API_MVC.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace CM_API_MVC.Tests
{
    public class MotoApiControllerTests
    {
        private readonly MotoApiController _controller;

        public MotoApiControllerTests()
        {
            var services = new ServiceCollection();

            // Banco InMemory
            services.AddDbContext<AppDbContext>(options =>
                options.UseInMemoryDatabase("TestDb"));

            services.AddScoped<MotoRepository>();

            var provider = services.BuildServiceProvider();
            var context = provider.GetRequiredService<AppDbContext>();

            var repo = provider.GetRequiredService<MotoRepository>();

            var mockLinkGen = new Mock<LinkGenerator>();
            var helper = new MotoLinksHelper(mockLinkGen.Object);

            _controller = new MotoApiController(repo, helper);
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };
        }

        [Fact]
        public async Task GetAll_DeveRetornarLista()
        {
            var result = await _controller.GetAll();

            // Assert
            var actionResult = Assert.IsType<ActionResult<IEnumerable<MotoDto>>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var lista = Assert.IsAssignableFrom<IEnumerable<MotoDto>>(okResult.Value);

            Assert.NotNull(lista);
        }

        [Fact]
        public async Task Create_DeveCriarItem()
        {

            var novo = new NovaMotoDto
            {
                TipoMoto = "SPORT",
                Placa = "AX2B",
                Status = "A",
                DataCadastro = DateTime.Now,
            };

            var result = await _controller.Create(novo);

            var resultObj = Assert.IsType<ActionResult<MotoHatDto>>(result);
            var createdResult = Assert.IsType<CreatedAtActionResult>(resultObj.Result);
            var createdItem = Assert.IsType<MotoHatDto>(createdResult.Value);

        }

        [Fact]
        public async Task Delete_DeveDeletarItem()
        {

            var motoDto = new NovaMotoDto
            {
                TipoMoto = "SPORT",
                Placa = "AX2B",
                Status = "A",
                DataCadastro = DateTime.Now,
            };

            var resultCreate = await _controller.Create(motoDto);
            var resultAction = Assert.IsType<ActionResult<MotoHatDto>>(resultCreate);
            var createdResult = Assert.IsType<CreatedAtActionResult>(resultAction.Result);
            var createdItem = Assert.IsType<MotoHatDto>(createdResult.Value);


            var result = await _controller.Delete(id: createdItem.IdMoto);

            Assert.IsType<NoContentResult>(result);
        }
    }

}
