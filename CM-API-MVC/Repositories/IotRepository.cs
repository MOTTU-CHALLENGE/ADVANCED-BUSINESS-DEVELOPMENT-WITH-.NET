using CM_API_MVC.Contexts;
using CM_API_MVC.Dtos;
using CM_API_MVC.Dtos.Iot;
using CM_API_MVC.Dtos.Moto;
using CM_API_MVC.Dtos.Posicao;
using CM_API_MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace CM_API_MVC.Repositories
{
    public class IotRepository : GenericRepository<DispositivoIot>
    {
        public IotRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<IotDto>> GetAllAsyncDto()
        {
            return await _context.DispositivosIot
                .Include(i => i.Moto)
                .Select(i => new IotDto
                {
                    Id = i.Id,
                    Nome = i.Nome,
                    DataInstalacao = i.DataInstalacao,
                    Descricao = i.Descricao,
                    MotoDto = i.Moto == null ? null : new MotoDto
                    {
                        IdMoto = i.Moto.IdMoto,
                        TipoMoto = i.Moto.TipoMoto,
                        Placa = i.Moto.Placa,
                        Status = i.Moto.Status,
                        DataCadastro = i.Moto.DataCadastro,
                        AnoFabricacao = i.Moto.AnoFabricacao,
                        Modelo = i.Moto.Modelo
                    }
                }).ToListAsync();
        }

        public async Task<IotComPosicaoDto?> GetByIdAsyncDto(int id)
        {
            return await _context.DispositivosIot
                .Where(i => i.Id == id)
                .Select(i => new IotComPosicaoDto
                {
                    Id = i.Id,
                    Nome = i.Nome,
                    DataInstalacao = i.DataInstalacao,
                    Descricao = i.Descricao,
                    IdMoto = i.IdMoto,
                    Posicoes = i.Posicoes.Select(p => new PosicaoDto
                    {
                        IdPosicao = p.IdPosicao,
                        IdPatio = p.IdPatio,
                        CoordenadaX = p.CoordenadaX,
                        CoordenadaY = p.CoordenadaY,
                        DataHoraRegistro = p.DataHoraRegistro,
                        Setor = p.Setor,
                    }).ToList()
                }).FirstOrDefaultAsync();
        }

        public async Task<DispositivoIot> AddAsyncDto(NovoIotDto iotDto)
        {
            var iot = new DispositivoIot
            {
                Nome = iotDto.Nome,
                DataInstalacao = iotDto.DataInstalacao,
                Descricao = iotDto.Descricao,
                IdMoto = iotDto.IdMoto,
            };

            await _context.DispositivosIot.AddAsync(iot);
            await _context.SaveChangesAsync();

            return iot;
        }
    }
}
