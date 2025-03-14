using Microsoft.AspNetCore.Mvc;
using ProgettoSettimanale1_U2.Services;
using ProgettoSettimanale1_U2.ViewModels;

namespace ProgettoSettimanale1_U2.Controllers
{
    public class AnagraficheController : Controller
    {
        private readonly AnagraficheService _anagraficheService;

        public AnagraficheController(AnagraficheService anagraficheService)
        {
            _anagraficheService = anagraficheService;
        }

        public async Task<IActionResult> Index()
        {
            var anagrafiche = await _anagraficheService.GetAnagraficheAsync();
            return View(anagrafiche);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddAnagraficheViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _anagraficheService.CreateAnagraficaAsync(model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var anagrafica = await _anagraficheService.GetAnagraficaByIdAsync(id);
            if (anagrafica == null)
            {
                return NotFound();
            }
            return View(anagrafica);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditAnagraficheViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _anagraficheService.UpdateAnagraficaAsync(model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var anagrafica = await _anagraficheService.GetAnagraficaByIdAsync(id);
            if (anagrafica == null)
            {
                return NotFound();
            }
            return View(anagrafica);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var result = await _anagraficheService.DeleteAnagraficaAsync(id);
            if (!result)
            {
                ModelState.AddModelError("", "Non è possibile eliminare questa anagrafica perché ha verbali associati.");
                return View(await _anagraficheService.GetAnagraficaByIdAsync(id));
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
