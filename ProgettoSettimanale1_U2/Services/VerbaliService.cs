using Microsoft.EntityFrameworkCore;
using ProgettoSettimanale1_U2.Data;
using ProgettoSettimanale1_U2.Models;
using ProgettoSettimanale1_U2.ViewModels;

namespace ProgettoSettimanale1_U2.Services
{
    public class VerbaliService
    {
        private readonly ApplicationDbContext _context;

        public VerbaliService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<VerbaliViewModel>> GetAllVerbaliAsync()
        {
            return await _context.Verbalis
                .Include(v => v.IdAnagraficaNavigation)
            .Include(v => v.IdViolazioneNavigation)
                .Select(v => new VerbaliViewModel
                {
                    IdVerbale = v.IdVerbale,
                    DataViolazione = v.DataViolazione,
                    IndirizzoViolazione = v.IndirizzoViolazione,
                    NominativoAgente = v.NominativoAgente,
                    DataTrascrizioneVerbale = v.DataTrascrizioneVerbale,
                    Importo = v.Importo,
                    DecurtamentoPunti = v.DecurtamentoPunti,
                    IdAnagrafica = v.IdAnagrafica,
                    NomeCompleto = $"{v.IdAnagraficaNavigation.Cognome} {v.IdAnagraficaNavigation.Nome}",
                    IdViolazione = v.IdViolazione,
                    DescrizioneViolazione = v.IdViolazioneNavigation.Descrizione
                })  
                .ToListAsync();
        }

        public async Task<VerbaliViewModel> GetVerbaleByIdAsync(int id)
        {
            var verbale = await _context.Verbalis
                .Include(v => v.IdAnagraficaNavigation)
                .Include(v => v.IdViolazioneNavigation)
                .FirstOrDefaultAsync(v => v.IdVerbale == id);

            if (verbale == null)
                return null;

            return new VerbaliViewModel
            {
                IdVerbale = verbale.IdVerbale,
                DataViolazione = verbale.DataViolazione,
                IndirizzoViolazione = verbale.IndirizzoViolazione,
                NominativoAgente = verbale.NominativoAgente,
                DataTrascrizioneVerbale = verbale.DataTrascrizioneVerbale,
                Importo = verbale.Importo,
                DecurtamentoPunti = verbale.DecurtamentoPunti,
                IdAnagrafica = verbale.IdAnagrafica,
                NomeCompleto = $"{verbale.IdAnagraficaNavigation.Cognome} {verbale.IdAnagraficaNavigation.Nome}",
                IdViolazione = verbale.IdViolazione,
                DescrizioneViolazione = verbale.IdViolazioneNavigation.Descrizione
            };
        }

        public async Task<VerbaliViewModel> CreateVerbaleAsync(VerbaliViewModel model)
        {
            // Ottiene il prossimo ID disponibile
            int nextId = 1;
            if (await _context.Verbalis.AnyAsync())
            {
                nextId = await _context.Verbalis.MaxAsync(v => v.IdVerbale) + 1;
            }

            var verbale = new Verbali
            {
                IdVerbale = nextId,
                DataViolazione = model.DataViolazione,
                IndirizzoViolazione = model.IndirizzoViolazione,
                NominativoAgente = model.NominativoAgente,
                DataTrascrizioneVerbale = DateTime.Now, // Data corrente
                Importo = model.Importo,
                DecurtamentoPunti = model.DecurtamentoPunti,
                IdAnagrafica = model.IdAnagrafica,
                IdViolazione = model.IdViolazione
            };

            _context.Verbalis.Add(verbale);
            await _context.SaveChangesAsync();

            // Recupera i dati completi per il view model
            var result = await GetVerbaleByIdAsync(verbale.IdVerbale);
            return result;
        }

        public async Task<VerbaliViewModel> UpdateVerbaleAsync(VerbaliViewModel model)
        {
            var verbale = await _context.Verbalis
                .FirstOrDefaultAsync(v => v.IdVerbale == model.IdVerbale &&
                                        v.IdAnagrafica == model.IdAnagrafica &&
                                        v.IdViolazione == model.IdViolazione);

            if (verbale == null)
                return null;

            verbale.DataViolazione = model.DataViolazione;
            verbale.IndirizzoViolazione = model.IndirizzoViolazione;
            verbale.NominativoAgente = model.NominativoAgente;
            verbale.Importo = model.Importo;
            verbale.DecurtamentoPunti = model.DecurtamentoPunti;

            await _context.SaveChangesAsync();

            // Recupera i dati completi per il view model
            var result = await GetVerbaleByIdAsync(verbale.IdVerbale);
            return result;
        }

        public async Task<bool> DeleteVerbaleAsync(int id)
        {
            var verbale = await _context.Verbalis.FirstOrDefaultAsync(v => v.IdVerbale == id);

            if (verbale == null)
                return false;

            _context.Verbalis.Remove(verbale);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<VerbaliPerTrasgressoreViewModel>> GetVerbaliPerTrasgressoreAsync()
        {
            return await _context.Verbalis
                .Include(v => v.IdAnagraficaNavigation)
                .GroupBy(v => v.IdAnagrafica)
                .Select(g => new VerbaliPerTrasgressoreViewModel
                {
                    Cognome = g.First().IdAnagraficaNavigation.Cognome,
                    Nome = g.First().IdAnagraficaNavigation.Nome,
                    CodiceFiscale = g.First().IdAnagraficaNavigation.CodiceFiscale,
                    NumeroVerbali = g.Count(),
                    ImportoTotale = g.Sum(v => v.Importo)
                })
                .OrderBy(v => v.Cognome)
                .ThenBy(v => v.Nome)
                .ToListAsync();
        }

        public async Task<List<PuntiPerTrasgressoreViewModel>> GetPuntiPerTrasgressoreAsync()
        {
            return await _context.Verbalis
                .Include(v => v.IdAnagraficaNavigation)
                .GroupBy(v => v.IdAnagrafica)
                .Select(g => new PuntiPerTrasgressoreViewModel
                {
                    Cognome = g.First().IdAnagraficaNavigation.Cognome,
                    Nome = g.First().IdAnagraficaNavigation.Nome,
                    CodiceFiscale = g.First().IdAnagraficaNavigation.CodiceFiscale,
                    PuntiTotali = g.Sum(v => v.DecurtamentoPunti ?? 0)
                })
                .OrderByDescending(v => v.PuntiTotali)
                .ToListAsync();
        }

        public async Task<List<ViolazioniGraviViewModel>> GetViolazioniOltre10PuntiAsync()
        {
            return await _context.Verbalis
                .Include(v => v.IdAnagraficaNavigation)
                .Where(v => v.DecurtamentoPunti > 10)
                .Select(v => new ViolazioniGraviViewModel
                {
                    IdVerbale = v.IdVerbale,
                    Cognome = v.IdAnagraficaNavigation.Cognome,
                    Nome = v.IdAnagraficaNavigation.Nome,
                    DataViolazione = v.DataViolazione,
                    Importo = v.Importo,
                    DecurtamentoPunti = v.DecurtamentoPunti ?? 0
                })
                .OrderByDescending(v => v.DecurtamentoPunti)
            .ToListAsync();
        }

        public async Task<List<VerbaliViewModel>> GetViolazioniOltre400EuroAsync()
        {
            return await _context.Verbalis
                .Include(v => v.IdAnagraficaNavigation)
                .Include(v => v.IdViolazioneNavigation)
            .Where(v => v.Importo > 400)
                .Select(v => new VerbaliViewModel
                {
                    IdVerbale = v.IdVerbale,
                    DataViolazione = v.DataViolazione,
                    IndirizzoViolazione = v.IndirizzoViolazione,
                    NominativoAgente = v.NominativoAgente,
                    DataTrascrizioneVerbale = v.DataTrascrizioneVerbale,
                    Importo = v.Importo,
                    DecurtamentoPunti = v.DecurtamentoPunti,
                    IdAnagrafica = v.IdAnagrafica,
                    NomeCompleto = $"{v.IdAnagraficaNavigation.Cognome} {v.IdAnagraficaNavigation.Nome}",
                    IdViolazione = v.IdViolazione,
                    DescrizioneViolazione = v.IdViolazioneNavigation.Descrizione
                })
                .OrderByDescending(v => v.Importo)
                .ToListAsync();
        }

    }
}
