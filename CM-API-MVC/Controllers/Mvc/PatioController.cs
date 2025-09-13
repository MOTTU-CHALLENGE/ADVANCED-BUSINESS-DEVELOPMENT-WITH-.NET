using CM_API_MVC.Models;
using CM_API_MVC.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CM_API_MVC.Controllers.Mvc
{
    public class PatioController : Controller
    {
        private readonly PatioRepository _repository;

        public PatioController(PatioRepository repository)
        {
            _repository = repository;
        }

        // GET: /Patio
        public async Task<IActionResult> Index()
        {
            var patios = await _repository.GetAll();
            return View(patios.OrderBy(p => p.IdPatio).ToList());
        }

        // GET: /Patio/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Patio/Create
        [HttpPost]
        public async Task<IActionResult> Create(Patio patio)
        {
            if (!ModelState.IsValid)
                return View(patio);

            await _repository.AddAsync(patio);
            return RedirectToAction(nameof(Index));
        }

        // GET: /Patio/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var patio = await _repository.GetByIdAsync(id);
            if (patio == null)
                return NotFound();

            return View(patio);
        }

        // GET: /Patio/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var patio = await _repository.GetByIdAsync(id);
            if (patio == null)
                return NotFound();

            return View(patio);
        }

        // POST: /Patio/Edit
        [HttpPost]
        public async Task<IActionResult> Edit(Patio patio)
        {
            if (!ModelState.IsValid)
                return View(patio);

            await _repository.UpdateAsync(patio);
            return RedirectToAction(nameof(Index));
        }

        // GET: /Patio/Delete
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var patio = await _repository.GetByIdAsync(id);
            if (patio == null)
                return NotFound();

            return View(patio);
        }

        // POST: /Patio/Delete
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var patio = await _repository.GetByIdAsync(id);
            if (patio == null)
                return NotFound();

            await _repository.DeleteAsync(patio);
            return RedirectToAction(nameof(Index));
        }
    }
}
