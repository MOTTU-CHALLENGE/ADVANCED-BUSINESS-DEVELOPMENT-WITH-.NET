using CM_API_MVC.Contexts;
using CM_API_MVC.Models;

namespace CM_API_MVC.Repositories
{
    public class FilialRepository : GenericRepository<Filial>
    {
        public FilialRepository(AppDbContext context) : base(context) { }
    }
}
