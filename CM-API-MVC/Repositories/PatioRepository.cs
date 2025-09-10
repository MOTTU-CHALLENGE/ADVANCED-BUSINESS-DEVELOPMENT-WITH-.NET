using CM_API_MVC.Contexts;
using CM_API_MVC.Models;

namespace CM_API_MVC.Repositories
{
    public class PatioRepository : GenericRepository<Patio>
    {
        public PatioRepository(AppDbContext context) : base(context) { }

    }
}
