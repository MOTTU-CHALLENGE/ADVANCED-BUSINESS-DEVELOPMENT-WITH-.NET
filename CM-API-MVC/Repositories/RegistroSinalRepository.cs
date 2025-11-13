using CM_API_MVC.Dtos.Registro;
using CM_API_MVC.Models;
using MongoDB.Driver;

namespace CM_API_MVC.Repositories
{
    public class RegistroSinalRepository

    {
        private readonly IMongoCollection<RegistroIntensidadeSinal> _collection;
        public RegistroSinalRepository(IMongoDatabase database)
        {
            _collection = database.GetCollection<RegistroIntensidadeSinal>("registro_sinais");
        }

        public async Task<RegistroIntensidadeSinal> AddAsync(RegistroDto dto)
        {
            var registro = new RegistroIntensidadeSinal
            {
                IdIot = dto.IdIot,
                Bssid = dto.Bssid,
                Rssi = dto.Rssi,

            };

            await _collection.InsertOneAsync(registro);
            return registro;
        }

        public async Task<RegistroIntensidadeSinal> GetByIdAsync(string id)
        {
            return await _collection.Find(r => r.IdRegistro == id).FirstOrDefaultAsync();
        }


        public async Task<List<RegistroIntensidadeSinal>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<bool> DeleteAsync(string Id)
        {
            var resultado = await _collection.DeleteOneAsync(r => r.IdRegistro == Id);
            return resultado.DeletedCount > 0;
        }

        public async Task<List<RegistroIntensidadeSinal>> GetHalfAsync(int pagina, int qtd)
        {
            return await _collection
               .Find(_ => true)
               .Skip((pagina - 1) * qtd)
               .Limit(qtd)
               .ToListAsync();
        }

        public async Task<long> CountAsync()
        {
            return await _collection.CountDocumentsAsync(_ => true);
        }

        public async Task<RegistroIntensidadeSinal?> ObterUltimoPorDispositivo(int idDispositivo)
        {
            var registros = await _collection
                .Find(r => r.IdIot == idDispositivo)
                .ToListAsync(); 

            return registros
                .OrderByDescending(r => r.DateTime)
                .FirstOrDefault();
        }

    }
}
