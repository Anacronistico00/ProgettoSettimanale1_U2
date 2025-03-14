using Microsoft.AspNetCore.Mvc;
using ProgettoSettimanale1_U2.Services;

namespace ProgettoSettimanale1_U2.Controllers
{
    public class ReportController : Controller
    {
        private readonly VerbaliService _verbaliService;

        public ReportController(VerbaliService verbaliService)
        {
            _verbaliService = verbaliService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> VerbaliPerTrasgressore()
        {
            var report = await _verbaliService.GetVerbaliPerTrasgressoreAsync();
            return View(report);
        }

        public async Task<IActionResult> PuntiPerTrasgressore()
        {
            var report = await _verbaliService.GetPuntiPerTrasgressoreAsync();
            return View(report);
        }

        public async Task<IActionResult> ViolazioniOltre10Punti()
        {
            var report = await _verbaliService.GetViolazioniOltre10PuntiAsync();
            return View(report);
        }

        public async Task<IActionResult> ViolazioniOltre400Euro()
        {
            var report = await _verbaliService.GetViolazioniOltre400EuroAsync();
            return View(report);
        }
    }
}
