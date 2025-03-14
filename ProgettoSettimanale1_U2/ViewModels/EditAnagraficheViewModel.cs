using System.ComponentModel.DataAnnotations;

namespace ProgettoSettimanale1_U2.ViewModels
{
    public class EditAnagraficheViewModel
    {
        public Guid IdAnagrafica { get; set; }

        [Required(ErrorMessage = "Il campo Cognome è obbligatorio.")]
        [StringLength(100, ErrorMessage = "Il Cognome non può essere più lungo di 100 caratteri.")]
        public string? Cognome { get; set; }

        [Required(ErrorMessage = "Il campo Nome è obbligatorio.")]
        [StringLength(100, ErrorMessage = "Il Nome non può essere più lungo di 100 caratteri.")]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "Il campo Indirizzo è obbligatorio.")]
        [StringLength(100, ErrorMessage = "L'Indirizzo non può essere più lungo di 100 caratteri.")]
        public string? Indirizzo { get; set; }

        [Required(ErrorMessage = "Il campo Città è obbligatorio.")]
        [StringLength(100, ErrorMessage = "La Città non può essere più lunga di 100 caratteri.")]
        public string? Citta { get; set; }

        [Required(ErrorMessage = "Il campo CAP è obbligatorio.")]
        [Range(0, int.MaxValue, ErrorMessage = "Il CAP deve essere un valore positivo.")]
        public int Cap { get; set; }

        [Required(ErrorMessage = "Il campo Codice Fiscale è obbligatorio.")]
        [StringLength(16, ErrorMessage = "Il Codice Fiscale non può essere più lungo di 16 caratteri.")]
        public string? CodiceFiscale { get; set; }
    }
}
