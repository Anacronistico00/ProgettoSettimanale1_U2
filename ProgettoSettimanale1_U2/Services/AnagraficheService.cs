using Microsoft.EntityFrameworkCore;
using ProgettoSettimanale1_U2.Data;
using ProgettoSettimanale1_U2.Models;
using ProgettoSettimanale1_U2.ViewModels;

namespace ProgettoSettimanale1_U2.Services
{
    public class AnagraficheService
    {
        private readonly ApplicationDbContext _context;

        public AnagraficheService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Anagrafiche>> GetAnagraficheAsync()
        {

            return await _context.Anagrafiches
            .Select(a => new Anagrafiche
            {
                IdAnagrafica = a.IdAnagrafica,
                Cognome = a.Cognome,
                Nome = a.Nome,
                Indirizzo = a.Indirizzo,
                Citta = a.Citta,
                Cap = a.Cap,
                CodiceFiscale = a.CodiceFiscale
            })
            .OrderBy(a => a.Cognome)
            .ThenBy(a => a.Nome)
            .ToListAsync();
        }

        public async Task<Anagrafiche> GetAnagraficaByIdAsync(Guid id)
        {
            var anagrafica = await _context.Anagrafiches.FindAsync(id);

            if (anagrafica == null)
                return null;

            return new Anagrafiche
            {
                IdAnagrafica = anagrafica.IdAnagrafica,
                Cognome = anagrafica.Cognome,
                Nome = anagrafica.Nome,
                Indirizzo = anagrafica.Indirizzo,
                Citta = anagrafica.Citta,
                Cap = anagrafica.Cap,
                CodiceFiscale = anagrafica.CodiceFiscale
            };
        }

        public async Task<AddAnagraficheViewModel> CreateAnagraficaAsync(AddAnagraficheViewModel model)
        {
            var anagrafica = new Anagrafiche
            {
                IdAnagrafica = Guid.NewGuid(),
                Cognome = model.Cognome,
                Nome = model.Nome,
                Indirizzo = model.Indirizzo,
                Citta = model.Citta,
                Cap = model.Cap,
                CodiceFiscale = model.CodiceFiscale
            };

            _context.Anagrafiches.Add(anagrafica);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<EditAnagraficheViewModel> UpdateAnagraficaAsync(EditAnagraficheViewModel model)
        {
            var anagrafica = await _context.Anagrafiches.FindAsync(model.IdAnagrafica);

            if (anagrafica == null)
                return null;

            anagrafica.Cognome = model.Cognome;
            anagrafica.Nome = model.Nome;
            anagrafica.Indirizzo = model.Indirizzo;
            anagrafica.Citta = model.Citta;
            anagrafica.Cap = model.Cap;
            anagrafica.CodiceFiscale = model.CodiceFiscale;

            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<bool> DeleteAnagraficaAsync(Guid id)
        {
            var anagrafica = await _context.Anagrafiches.FindAsync(id);

            if (anagrafica == null)
            {
                return false;
            }

            // Se L'anagrafica ha dei verbali associati non posso cancellarla
            var hasVerbali = await _context.Verbalis.AnyAsync(v => v.IdAnagrafica == id);
            if (hasVerbali)
            {
                return false;
            }

            _context.Anagrafiches.Remove(anagrafica);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
