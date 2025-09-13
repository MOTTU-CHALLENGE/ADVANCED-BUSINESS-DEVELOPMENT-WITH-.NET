using CM_API_MVC.Models;
using CM_API_MVC.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CM_API_MVC.Controllers.Mvc
{
    public class FilialController : Controller
    {
        private readonly FilialRepository _repository;

        public FilialController(FilialRepository repository)
        {
            _repository = repository;
        }

        // GET: /Filial
        public async Task<IActionResult> Index()
        {
            var filiais = await _repository.GetAllAsyncDto();
            return View(filiais);
        }

        //GET: /Filial
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // GET: Filial/Create
        [HttpPost]
        public async Task<IActionResult> Create(Filial filial)
        {
            if (!ModelState.IsValid)
                return View(filial);

            await _repository.AddAsync(filial);
            return RedirectToAction(nameof(Index));
        }

        //GET: /Filial/Detaisl/1
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var filial = await _repository.GetByIdAsync(id);
            if (filial == null)
                return NotFound();

            return View(filial);
        }

        // GET: /Filial/Edit/1
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var filial = await _repository.GetByIdAsync(id);
            if (filial == null)
                return NotFound();

            return View(filial);
        }

        // GET: /Filial/Edit
        [HttpPost]
        public async Task<IActionResult> Edit(Filial filial)
        {
            if (!ModelState.IsValid)
                return View(filial);

            await _repository.UpdateAsync(filial);
            return RedirectToAction(nameof(Index));
        }

        //GET: /Filial/Delete
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var filial = await _repository.GetByIdAsync(id);
            if (filial == null)
                return NotFound();

            return View(filial);
        }

        //GET: /Filial/Delete
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var filial = await _repository.GetByIdAsync(id);
            if (filial == null)
                return NotFound();

            await _repository.DeleteAsync(filial);
            return RedirectToAction(nameof(Index));
        }

    }
}
