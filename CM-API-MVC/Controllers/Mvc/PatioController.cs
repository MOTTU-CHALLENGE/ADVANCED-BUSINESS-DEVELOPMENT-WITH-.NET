using CM_API_MVC.Contexts;
using CM_API_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CM_API_MVC.Controllers.Mvc
{
    public class PatioController(AppDbContext context) : Controller
    {
        private readonly AppDbContext _context = context;

        public async Task<IActionResult> Index()
        {
            var patios = await _context.Patios.OrderBy(p => p.IdPatio).ToListAsync();
            return View(patios);
        }

        private async Task<int> GetNextPatioIdAsync()
        {
            var connectionString = _context.Database.GetDbConnection().ConnectionString;

            using var connection = new OracleConnection(connectionString);
            await connection.OpenAsync();

            using var command = connection.CreateCommand();
            command.CommandText = "SELECT PATIO_SEQ.NEXTVAL FROM DUAL";

            var result = await command.ExecuteScalarAsync();
            return Convert.ToInt32(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Patio patio)
        {
            patio.IdPatio = await GetNextPatioIdAsync();
            _context.Add(patio);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var patio = await _context.Patios.FirstOrDefaultAsync(p => p.IdPatio == id);
            if (patio == null)
            {
                return NotFound();
            }
            return View(patio);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var patio = await _context.Patios.FindAsync(id);
            if (patio == null)
                return NotFound();

            return View(patio);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Patio patio)
        {
            if (!ModelState.IsValid)
                return View(patio);

            _context.Update(patio);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var patio = await _context.Patios.FindAsync(id);
            if (patio == null)
                return NotFound();

            return View(patio);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var patio = await _context.Patios.FindAsync(id);
            if (patio == null)
                return NotFound();

            _context.Patios.Remove(patio);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
