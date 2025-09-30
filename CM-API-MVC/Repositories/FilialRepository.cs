using CM_API_MVC.Contexts;
using CM_API_MVC.Dtos.Filial;
using CM_API_MVC.Dtos.Patio;
using CM_API_MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace CM_API_MVC.Repositories
{
    public class FilialRepository : GenericRepository<Filial>
    {
        public FilialRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<FilialComPatioDto>> GetAllAsyncDto()
        {
            return await _context.Filiais
                .Include(f => f.Patios)
                .Select(f => new FilialComPatioDto
                {
                    IdFilial = f.IdFilial,
                    NomeFilial = f.NomeFilial,
                    Endereco = f.Endereco,
                    Cidade = f.Cidade,
                    Estado = f.Estado,
                    Pais = f.Pais,
                    Cep = f.Cep,
                    Telefone = f.Telefone,
                    DataInauguracao = f.DataInauguracao,
                    Patios = f.Patios.Select(p => new NomePatioDto
                    {
                        IdPatio = p.IdPatio,
                        NomePatio = p.NomePatio
                    }).ToList()
                })
                .ToListAsync();
        }

        public async Task<List<FilialComPatioDto>> GetHalfAsync(int pagina, int qtd)
        {
            return await _context.Filiais
                .Include(f => f.Patios)
                .OrderBy(f => f.IdFilial)
                .Skip((pagina - 1) * qtd)
                .Take(qtd)
                .Select(f => new FilialComPatioDto
                {
                    IdFilial = f.IdFilial,
                    NomeFilial = f.NomeFilial,
                    Endereco = f.Endereco,
                    Cidade = f.Cidade,
                    Estado = f.Estado,
                    Pais = f.Pais,
                    Cep = f.Cep,
                    Telefone = f.Telefone,
                    DataInauguracao = f.DataInauguracao,
                    Patios = f.Patios.Select(p => new NomePatioDto
                    {
                        IdPatio = p.IdPatio,
                        NomePatio = p.NomePatio
                    }).ToList()
                })
                .ToListAsync();

        }

        public async Task<FilialComPatioDto?> GetByIdAsyncDto(int id)
        {
            return await _context.Filiais
                .Include(f => f.Patios)
                .Where(f => f.IdFilial == id)
                .Select(f => new FilialComPatioDto
                {
                    IdFilial = f.IdFilial,
                    NomeFilial = f.NomeFilial,
                    Endereco = f.Endereco,
                    Cidade = f.Cidade,
                    Estado = f.Estado,
                    Pais = f.Pais,
                    Cep = f.Cep,
                    Telefone = f.Telefone,
                    DataInauguracao = f.DataInauguracao,
                    Patios = f.Patios.Select(p => new NomePatioDto
                    {
                        IdPatio = p.IdPatio,
                        NomePatio = p.NomePatio
                    }).ToList()
                })
                .FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateAsyncDto(int id, FilialDto dto)
        {
            var filial = await _context.Filiais
                .Include(f => f.Patios)
                .FirstOrDefaultAsync(f => f.IdFilial == id);

            if (filial == null)
                return false;

            filial.NomeFilial = dto.NomeFilial;
            filial.Endereco = dto.Endereco;
            filial.Cidade = dto.Cidade;
            filial.Estado = dto.Estado;
            filial.Pais = dto.Pais;
            filial.Cep = dto.Cep;
            filial.Telefone = dto.Telefone;
            filial.DataInauguracao = dto.DataInauguracao;

            _context.Filiais.Update(filial);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<Filial> AddAsyncDto(NovaFilialDto dto)
        {
            var filial = new Filial
            {
                NomeFilial = dto.NomeFilial,
                Endereco = dto.Endereco,
                Cidade = dto.Cidade,
                Estado = dto.Estado,
                Pais = dto.Pais,
                Cep = dto.Cep,
                Telefone = dto.Telefone,
                DataInauguracao = dto.DataInauguracao
            };

            await _context.Filiais.AddAsync(filial);
            await _context.SaveChangesAsync();

            return filial;
        }

        public async Task<int> CountAsync()
        {
            return await _context.Filiais.CountAsync();
        }
    }
}
