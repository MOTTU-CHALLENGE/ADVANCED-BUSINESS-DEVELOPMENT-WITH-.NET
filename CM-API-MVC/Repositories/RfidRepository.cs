using CM_API_MVC.Contexts;
using CM_API_MVC.Dtos.Moto;
using CM_API_MVC.Dtos.Rfid;
using CM_API_MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace CM_API_MVC.Repositories
{
    public class RfidRepository : GenericRepository<Rfid>
    {
        public RfidRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<RfidDto>> GetAllAsyncDto()
        {
            return await _context.Rfids
                .Include(r => r.Moto)
                .Select(r => new RfidDto
                {
                    CodigoTag = r.CodigoTag,
                    Frequencia = r.Frequencia,
                    Status = r.Status,
                    DataAtivacao = r.DataAtivacao,
                    Observacao = r.Observacao,
                    MotoDto = r.Moto == null ? null : new MotoDto
                    {
                        IdMoto = r.Moto.IdMoto,
                        TipoMoto = r.Moto.TipoMoto,
                        Placa = r.Moto.Placa,
                        Status = r.Moto.Status,
                        DataCadastro = r.Moto.DataCadastro,
                        AnoFabricacao = r.Moto.AnoFabricacao,
                        Modelo = r.Moto.Modelo
                    }
                }).ToListAsync();
        }


        public async Task<RfidDto?> GetByIdAsyncDto(string codTag)
        {
            return await _context.Rfids
                .Where(r => r.CodigoTag == codTag)
                .Select(r => new RfidDto
                {
                    CodigoTag = r.CodigoTag,
                    Frequencia = r.Frequencia,
                    Status = r.Status,
                    DataAtivacao = r.DataAtivacao,
                    Observacao = r.Observacao,
                    MotoDto = r.Moto == null ? null : new MotoDto
                    {
                        IdMoto = r.Moto.IdMoto,
                        TipoMoto = r.Moto.TipoMoto,
                        Placa = r.Moto.Placa,
                        Status = r.Moto.Status,
                        DataCadastro = r.Moto.DataCadastro,
                        AnoFabricacao = r.Moto.AnoFabricacao,
                        Modelo = r.Moto.Modelo
                    }
                })
                .FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateAsyncDto(string cdTag, AtualizarRfidDto dto)
        {
            var rfid = await _context.Rfids.FirstOrDefaultAsync(r => r.CodigoTag == cdTag);
            if (rfid == null)
                return false;

            rfid.Observacao = dto.Observacao;
            rfid.Status = dto.Status;

            _context.Rfids.Update(rfid);
            await _context.SaveChangesAsync();
            return true;

        }

        public async Task<Rfid> AddAsyncDto(RfidDto rfidDto)
        {
            var rfid = new Rfid
            {
                CodigoTag = rfidDto.CodigoTag,
                Frequencia = rfidDto.Frequencia,
                Status = rfidDto.Status,
                DataAtivacao = rfidDto.DataAtivacao,
                Observacao = rfidDto?.Observacao,
            };

            await _context.Rfids.AddAsync(rfid);
            await _context.SaveChangesAsync();
            return rfid;
        }


    }
}
