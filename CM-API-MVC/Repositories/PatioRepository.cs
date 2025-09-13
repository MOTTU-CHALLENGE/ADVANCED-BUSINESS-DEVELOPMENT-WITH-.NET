using CM_API_MVC.Contexts;
using CM_API_MVC.Dtos;
using CM_API_MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace CM_API_MVC.Repositories
{
    public class PatioRepository : GenericRepository<Patio>
    {


        public PatioRepository(AppDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<PatioComWifiDto>> GetAllAsyncDto()
        {
            return await _context.Patios
                .Include(p => p.ReceptorWifi)
                .Select(p => new PatioComWifiDto
                {
                    IdPatio = p.IdPatio,
                    IdFilial = p.IdFilial,
                    NomePatio = p.NomePatio,
                    CapacidadeMax = p.CapacidadeMax,
                    Area = p.Area,
                    Descricao = p.Descricao,
                    ReceptorWifi = p.ReceptorWifi.Select(w => new WifiDto
                    {
                        IdLeitor = w.IdLeitor,
                        IdPatio = w.IdPatio,
                        LocalInstalacao = w.LocalInstalacao,
                        EnderecoMac = w.EnderecoMac,
                        Status = w.Status,
                        DataInstalacao = w.DataInstalacao,
                        Descricao = w.Descricao
                    }).ToList()

                }).ToListAsync();
        }

        public async Task<PatioComWifiDto?> GetByIdAsyncDto(int id)
        {
            return await _context.Patios
            .Include(p => p.ReceptorWifi)
            .Where(p => p.IdFilial == id)
            .Select(p => new PatioComWifiDto
            {
                IdPatio = p.IdPatio,
                IdFilial = p.IdFilial,
                NomePatio = p.NomePatio,
                CapacidadeMax = p.CapacidadeMax,
                Area = p.Area,
                Descricao = p.Descricao,
                ReceptorWifi = p.ReceptorWifi.Select(w => new WifiDto
                {
                    IdLeitor = w.IdLeitor,
                    IdPatio = w.IdPatio,
                    LocalInstalacao = w.LocalInstalacao,
                    EnderecoMac = w.EnderecoMac,
                    Status = w.Status,
                    DataInstalacao = w.DataInstalacao,
                    Descricao = w.Descricao
                }).ToList()

            }).FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateAsyncDto(int id, PatioDto dto)
        {
            var patio = await _context.Patios
                .Include(p => p.ReceptorWifi)
                .FirstOrDefaultAsync(p => p.IdPatio == id);

            if (patio == null)
                return false;

            patio.IdPatio = dto.IdPatio;
            patio.IdFilial = dto.IdFilial;
            patio.NomePatio = dto.NomePatio;
            patio.CapacidadeMax = dto.CapacidadeMax;
            patio.Area = dto.Area;
            patio.Descricao = dto.Descricao;

            _context.Patios.Update(patio);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Patio> AddAsyncDto(NovoPatioDto dto)
        {
            var patio = new Patio
            {
                IdFilial = dto.IdFilial,
                NomePatio = dto.NomePatio,
                CapacidadeMax = dto.CapacidadeMax,
                Area = dto.Area,
                Descricao = dto.Descricao
            };

            await _context.Patios.AddAsync(patio);
            await _context.SaveChangesAsync();
            return patio;
        }


    }
}
