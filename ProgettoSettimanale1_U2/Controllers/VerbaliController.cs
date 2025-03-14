using Microsoft.AspNetCore.Mvc;
using ProgettoSettimanale1_U2.Services;
using ProgettoSettimanale1_U2.ViewModels;

namespace ProgettoSettimanale1_U2.Controllers
{
    public class VerbaliController : Controller
    {
        private readonly VerbaliService _verbaliService;
        private readonly AnagraficheService _anagraficheService;
        private readonly TipiViolazioneService _tipiViolazioneService;
        public VerbaliController(
        VerbaliService verbaliService,
        AnagraficheService anagraficheService,
        TipiViolazioneService tipiViolazioneService)
        {
            _verbaliService = verbaliService;
            _anagraficheService = anagraficheService;
            _tipiViolazioneService = tipiViolazioneService;
        }
        public async Task<IActionResult> Index()
        {
            var verbali = await _verbaliService.GetAllVerbaliAsync();
            return View(verbali);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Anagrafiche = await _anagraficheService.GetAnagraficheAsync();
            ViewBag.TipiViolazione = await _tipiViolazioneService.GetAllTipiViolazioneAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VerbaliViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _verbaliService.CreateVerbaleAsync(model);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Anagrafiche = await _anagraficheService.GetAnagraficheAsync();
            ViewBag.TipiViolazione = await _tipiViolazioneService.GetAllTipiViolazioneAsync();
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var verbale = await _verbaliService.GetVerbaleByIdAsync(id);
            if (verbale == null)
            {
                return NotFound();
            }
            ViewBag.Anagrafiche = await _anagraficheService.GetAnagraficheAsync();
            ViewBag.TipiViolazione = await _tipiViolazioneService.GetAllTipiViolazioneAsync();
            return View(verbale);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(VerbaliViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _verbaliService.UpdateVerbaleAsync(model);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Anagrafiche = await _anagraficheService.GetAnagraficheAsync();
            ViewBag.TipiViolazione = await _tipiViolazioneService.GetAllTipiViolazioneAsync();
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var verbale = await _verbaliService.GetVerbaleByIdAsync(id);
            if (verbale == null)
            {
                return NotFound();
            }
            return View(verbale);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _verbaliService.DeleteVerbaleAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var verbale = await _verbaliService.GetVerbaleByIdAsync(id);
            if (verbale == null)
            {
                return NotFound();
            }
            return View(verbale);
        }   
    }
}
