using CM_API_MVC.Contexts;
using CM_API_MVC.Dtos.Wifi;
using CM_API_MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace CM_API_MVC.Repositories
{
    public class WifiRepository : GenericRepository<ReceptorWifi>
    {
        public WifiRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<WifiDto>> GetAllAsyncDto()
        {
            return await _context.ReceptoresWifi
                .Select(w => new WifiDto
                {
                    IdLeitor = w.IdLeitor,
                    IdPatio = w.IdPatio,
                    LocalInstalacao = w.LocalInstalacao,
                    EnderecoMac = w.EnderecoMac,
                    DataInstalacao = w.DataInstalacao,
                    Descricao = w.Descricao,
                    Status = w.Status,
                }).ToListAsync();
        }

        public async Task<WifiDto?> GetByIdAsyncDto(int id)
        {
            return await _context.ReceptoresWifi
                .Where(w => w.IdLeitor == id)
                .Select(w => new WifiDto
                {
                    IdLeitor = w.IdLeitor,
                    IdPatio = w.IdPatio,
                    LocalInstalacao = w.LocalInstalacao,
                    EnderecoMac = w.EnderecoMac,
                    DataInstalacao = w.DataInstalacao,
                    Descricao = w.Descricao,
                    Status = w.Status
                }).FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateAsyncDto(int id, WifiDto wifiDto)
        {
            var wifi = await _context.ReceptoresWifi
                .Where(w => w.IdLeitor == id)
                .FirstOrDefaultAsync();

            if (wifi == null)
                return false;

            wifi.IdPatio = wifiDto.IdPatio;
            wifi.LocalInstalacao = wifiDto.LocalInstalacao;
            wifi.EnderecoMac = wifiDto.EnderecoMac;
            wifi.DataInstalacao = wifiDto.DataInstalacao;
            wifi.Status = wifiDto.Status;
            wifi.Descricao = wifiDto.Descricao;

            _context.ReceptoresWifi.Update(wifi);
            await _context.SaveChangesAsync();
            return true;

        }

        public async Task<ReceptorWifi> AddAsyncDto(NovoWifiDto novoWifiDto)
        {

            var wifi = new ReceptorWifi
            {
                IdPatio = novoWifiDto.IdPatio,
                LocalInstalacao = novoWifiDto.LocalInstalacao,
                EnderecoMac = novoWifiDto.EnderecoMac,
                Status = novoWifiDto.Status,
                DataInstalacao = novoWifiDto.DataInstalacao,
                Descricao = novoWifiDto.Descricao
            };
            await _context.ReceptoresWifi.AddAsync(wifi);
            await _context.SaveChangesAsync();
            return wifi;
        }

        public async Task<WifiDto?> GetByMacAsyncDto(string mac)
        {
            return await _context.ReceptoresWifi
                .Where(w => w.EnderecoMac == mac)
                .Select(w => new WifiDto
                {
                    IdLeitor = w.IdLeitor,
                    IdPatio = w.IdPatio,
                    LocalInstalacao = w.LocalInstalacao,
                    EnderecoMac = w.EnderecoMac,
                    DataInstalacao = w.DataInstalacao,
                    Descricao = w.Descricao,
                    Status = w.Status
                }).FirstOrDefaultAsync();
        }

    }
}
