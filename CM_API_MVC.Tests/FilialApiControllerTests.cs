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
    public class FilialApiControllerTests
    {
        private readonly FilialApiController _controller;

        public FilialApiControllerTests()
        {
            var services = new ServiceCollection();

            // Banco InMemory exclusivo para o teste
            services.AddDbContext<AppDbContext>(options =>
                options.UseInMemoryDatabase("TestDb_Filial"));

            services.AddScoped<FilialRepository>();

            var provider = services.BuildServiceProvider();
            var context = provider.GetRequiredService<AppDbContext>();

            var repo = provider.GetRequiredService<FilialRepository>();

            var mockLinkGen = new Mock<LinkGenerator>();
            var helper = new FilialLinksHelper(mockLinkGen.Object);


            _controller = new FilialApiController(repo, helper);
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };

        }

        [Fact]
        public async Task GetAll_DeveRetornarLista()
        {
            var result = await _controller.GetAll();


            var actionResult = Assert.IsType<ActionResult<IEnumerable<FilialComPatioDto>>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var lista = Assert.IsAssignableFrom<IEnumerable<FilialComPatioDto>>(okResult.Value);

            Assert.NotNull(lista);
        }

        [Fact]
        public async Task Create_DeveCriarItem()
        {

            var novo = new NovaFilialDto
            {
                NomeFilial = "Filial Teste",
                Endereco = "x",
                Cidade = "y",
                Estado = "z",
                Pais = "a",
            };

            var result = await _controller.Create(novo);

            var resultObj = Assert.IsType<ActionResult<FilialComPatioHatDto>>(result);
            var createdResult = Assert.IsType<CreatedAtActionResult>(resultObj.Result);
            var createdItem = Assert.IsType<FilialComPatioHatDto>(createdResult.Value);

        }

        [Fact]
        public async Task Delete_DeveDeletarItem()
        {

            var filialDto = new NovaFilialDto
            {
                NomeFilial = "Filial Teste",
                Endereco = "x",
                Cidade = "y",
                Estado = "z",
                Pais = "a",
            };

            var resultCreate = await _controller.Create(filialDto);
            var resultAction = Assert.IsType<ActionResult<FilialComPatioHatDto>>(resultCreate);
            var createdResult = Assert.IsType<CreatedAtActionResult>(resultAction.Result);
            var createdItem = Assert.IsType<FilialComPatioHatDto>(createdResult.Value);


            var result = await _controller.Delete(id: createdItem.IdFilial);

            Assert.IsType<NoContentResult>(result);
        }
    }

}
