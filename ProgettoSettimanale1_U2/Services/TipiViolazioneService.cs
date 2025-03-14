using Microsoft.EntityFrameworkCore;
using ProgettoSettimanale1_U2.Data;
using ProgettoSettimanale1_U2.Models;

namespace ProgettoSettimanale1_U2.Services
{
    public class TipiViolazioneService
    {
        private readonly ApplicationDbContext _context;

        public TipiViolazioneService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<TipiViolazione>> GetAllTipiViolazioneAsync()
        {
            return await _context.TipiViolaziones
                .Select(t => new TipiViolazione
                {
                    IdViolazione = t.IdViolazione,
                    Descrizione = t.Descrizione
                })
                .OrderBy(t => t.Descrizione)
                .ToListAsync();
        }

        public async Task<TipiViolazione> GetTipoViolazioneByIdAsync(Guid id)
        {
            var tipoViolazione = await _context.TipiViolaziones.FindAsync(id);

            if (tipoViolazione == null)
                return null;

            return new TipiViolazione
            {
                IdViolazione = tipoViolazione.IdViolazione,
                Descrizione = tipoViolazione.Descrizione
            };
        }

        public async Task<TipiViolazione> CreateTipoViolazioneAsync(TipiViolazione model)
        {
            var tipoViolazione = new TipiViolazione
            {
                IdViolazione = Guid.NewGuid(),
                Descrizione = model.Descrizione
            };

            _context.TipiViolaziones.Add(tipoViolazione);
            await _context.SaveChangesAsync();

            model.IdViolazione = tipoViolazione.IdViolazione;
            return model;
        }

        public async Task<TipiViolazione> UpdateTipoViolazioneAsync(TipiViolazione model)
        {
            var tipoViolazione = await _context.TipiViolaziones.FindAsync(model.IdViolazione);

            if (tipoViolazione == null)
                return null;

            tipoViolazione.Descrizione = model.Descrizione;

            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<bool> DeleteTipoViolazioneAsync(Guid id)
        {
            var tipoViolazione = await _context.TipiViolaziones.FindAsync(id);

            if (tipoViolazione == null)
                return false;

            // Verifica se il tipo di violazione ha verbali associati
            var hasVerbali = await _context.Verbalis.AnyAsync(v => v.IdViolazione == id);
            if (hasVerbali)
                return false;

            _context.TipiViolaziones.Remove(tipoViolazione);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
