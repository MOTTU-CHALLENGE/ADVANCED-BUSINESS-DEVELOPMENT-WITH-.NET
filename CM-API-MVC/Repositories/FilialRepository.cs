using CM_API_MVC.Contexts;
using CM_API_MVC.Dtos;
using CM_API_MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace CM_API_MVC.Repositories
{
    public class FilialRepository : GenericRepository<Filial>
    {
        public FilialRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<FilialComPatiosDto>> GetAllFiliaisComPatios()
        {
            return await _context.Filiais
                .Include(f => f.Patios)
                .Select(f => new FilialComPatiosDto
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
                    Patios = f.Patios.Select(p => new PatioDto
                    {
                        IdPatio = p.IdPatio,
                        NomePatio = p.NomePatio
                    }).ToList()
                })
                .ToListAsync();
        }

    }
}
