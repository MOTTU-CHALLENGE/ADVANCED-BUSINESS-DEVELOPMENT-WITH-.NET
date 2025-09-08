using CM_API_MVC.Contexts;
using CM_API_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CM_API_MVC.Controllers.Mvc
{
    public class FilialController(AppDbContext context) : Controller
    {
        private readonly AppDbContext _context = context;

        public async Task<IActionResult> Index()
        {
            var filiais = await _context.Filiais.OrderBy(f => f.IdFilial).ToListAsync();
            return View(filiais);
        }

        private async Task<int> GetNextFilialIdAsync()
        {
            var connectionString = _context.Database.GetDbConnection().ConnectionString;

            using var connection = new OracleConnection(connectionString);
            await connection.OpenAsync();

            using var command = connection.CreateCommand();
            command.CommandText = "SELECT FILIAL_SEQ.NEXTVAL FROM DUAL";

            var result = await command.ExecuteScalarAsync();
            return Convert.ToInt32(result);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Filial filial)
        {
            filial.IdFilial = await GetNextFilialIdAsync();

            _context.Add(filial);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var filial = await _context.Filiais.FirstOrDefaultAsync(f => f.IdFilial == id);
            if (filial == null)
                return NotFound();

            return View(filial);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var filial = await _context.Filiais.FindAsync(id);
            if (filial == null)
                return NotFound();

            return View(filial);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Filial filial)
        {
            if (!ModelState.IsValid)
                return View(filial);

            _context.Update(filial);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var filial = await _context.Filiais.FindAsync(id);
            if (filial == null)
                return NotFound();

            return View(filial);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var filial = await _context.Filiais.FindAsync(id);
            if (filial == null)
                return NotFound();

            _context.Filiais.Remove(filial);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }




    }
}
