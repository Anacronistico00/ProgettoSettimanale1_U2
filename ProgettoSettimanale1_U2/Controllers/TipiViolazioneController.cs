using Microsoft.AspNetCore.Mvc;
using ProgettoSettimanale1_U2.Models;
using ProgettoSettimanale1_U2.Services;

namespace ProgettoSettimanale1_U2.Controllers
{
    public class TipiViolazioneController : Controller
    {
        private readonly TipiViolazioneService _tipiViolazioneService;

        public TipiViolazioneController(TipiViolazioneService tipiViolazioneService)
        {
            _tipiViolazioneService = tipiViolazioneService;
        }

        public async Task<IActionResult> Index()
        {
            var tipiViolazione = await _tipiViolazioneService.GetAllTipiViolazioneAsync();
            return View(tipiViolazione);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TipiViolazione model)
        {
            if (ModelState.IsValid)
            {
                await _tipiViolazioneService.CreateTipoViolazioneAsync(model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var tipoViolazione = await _tipiViolazioneService.GetTipoViolazioneByIdAsync(id);
            if (tipoViolazione == null)
            {
                return NotFound();
            }
            return View(tipoViolazione);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TipiViolazione model)
        {
            if (ModelState.IsValid)
            {
                await _tipiViolazioneService.UpdateTipoViolazioneAsync(model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var tipoViolazione = await _tipiViolazioneService.GetTipoViolazioneByIdAsync(id);
            if (tipoViolazione == null)
            {
                return NotFound();
            }
            return View(tipoViolazione);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var result = await _tipiViolazioneService.DeleteTipoViolazioneAsync(id);
            if (!result)
            {
                ModelState.AddModelError("", "Non è possibile eliminare questo tipo di violazione perché ha verbali associati.");
                return View(await _tipiViolazioneService.GetTipoViolazioneByIdAsync(id));
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
