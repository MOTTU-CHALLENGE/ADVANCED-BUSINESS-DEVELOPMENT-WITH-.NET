using CM_API_MVC.Contexts;
using CM_API_MVC.Dtos.Moto;
using CM_API_MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace CM_API_MVC.Repositories
{
    public class MotoRepository : GenericRepository<Moto>
    {
        public MotoRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<MotoDto>> GetAllAsyncDto()
        {
            return await _context.Motos
                .Select(m => new MotoDto
                {
                    IdMoto = m.IdMoto,
                    CodTag = m.CodTag,
                    TipoMoto = m.TipoMoto,
                    Placa = m.Placa,
                    Status = m.Status,
                    DataCadastro = m.DataCadastro,
                    AnoFabricacao = m.AnoFabricacao,
                    Modelo = m.Modelo,
                }).ToListAsync();
        }

        public async Task<List<MotoDto>> GetHalfAsync(int pagina, int qtdMotos)
        {
            return await _context.Motos
                .OrderBy (m => m.IdMoto)
                .Skip ((pagina - 1) * qtdMotos)
                .Take(qtdMotos)
                .Select(m => new MotoDto
                {
                    IdMoto = m.IdMoto,
                    CodTag = m.CodTag,
                    TipoMoto = m.TipoMoto,
                    Placa = m.Placa,
                    Status = m.Status,
                    DataCadastro = m.DataCadastro,
                    AnoFabricacao = m.AnoFabricacao,
                    Modelo = m.Modelo,
                }).ToListAsync();
        }

        public async Task<MotoDto?> GetByIdAsyncDto(int id)
        {
            return await _context.Motos
                .Where(m => m.IdMoto == id)
                .Select(m => new MotoDto
                {
                    IdMoto = m.IdMoto,
                    CodTag = m.CodTag,
                    TipoMoto = m.TipoMoto,
                    Placa = m.Placa,
                    Status = m.Status,
                    DataCadastro = m.DataCadastro,
                    AnoFabricacao = m.AnoFabricacao,
                    Modelo = m.Modelo,
                }).FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateAsyncDto(int id, MotoDto motoDto)
        {
            var moto = await _context.Motos
                .FirstOrDefaultAsync(m => m.IdMoto == id);
            if (moto == null)
                return false;

            moto.CodTag = motoDto.CodTag;
            moto.TipoMoto = motoDto.TipoMoto;
            moto.Placa = motoDto.Placa;
            moto.Status = motoDto.Status;
            moto.DataCadastro = motoDto.DataCadastro;
            moto.AnoFabricacao = motoDto.AnoFabricacao;
            moto.Modelo = motoDto.Modelo;

            _context.Update(moto);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<Moto> AddAsyncDto(NovaMotoDto motoDto)
        {
            var moto = new Moto
            {
                Modelo = motoDto.Modelo,
                CodTag = motoDto.CodTag,
                TipoMoto = motoDto.TipoMoto,
                Placa = motoDto.Placa,
                Status = motoDto.Status,
                DataCadastro = motoDto.DataCadastro,
                AnoFabricacao = motoDto.AnoFabricacao,
            };

            await _context.Motos.AddAsync(moto);
            await _context.SaveChangesAsync();
            return moto;
        }

        public async Task<int> CountAsync()
        {
            return await _context.Motos.CountAsync();
        }
    }
}

