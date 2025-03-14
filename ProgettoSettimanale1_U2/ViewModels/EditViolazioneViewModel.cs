using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProgettoSettimanale1_U2.ViewModels
{
    public class EditViolazioneViewModel
    {
        public int IdVerbale { get; set; }

        [Required(ErrorMessage = "Il campo Data Violazione è obbligatorio.")]
        [Column(TypeName = "datetime")]
        public DateTime DataViolazione { get; set; }

        [Required(ErrorMessage = "Il campo Indirizzo Violazione è obbligatorio.")]
        [StringLength(100, ErrorMessage = "L'Indirizzo della violazione non può essere più lungo di 100 caratteri.")]
        public string IndirizzoViolazione { get; set; } = null!;

        [Required(ErrorMessage = "Il campo Nominativo Agente è obbligatorio.")]
        [StringLength(100, ErrorMessage = "Il Nominativo dell'agente non può essere più lungo di 100 caratteri.")]
        public string NominativoAgente { get; set; } = null!;

        [Required(ErrorMessage = "Il campo Data Trascrizione Verbale è obbligatorio.")]
        [Column(TypeName = "datetime")]
        public DateTime DataTrascrizioneVerbale { get; set; }

        [Required(ErrorMessage = "Il campo Importo è obbligatorio.")]
        [Range(0, double.MaxValue, ErrorMessage = "L'importo deve essere maggiore di zero.")]
        [Column(TypeName = "decimal(7, 2)")]
        public decimal Importo { get; set; }

        [Range(0, 10, ErrorMessage = "Il Decurtamento Punti deve essere tra 0 e 10.")]
        public int? DecurtamentoPunti { get; set; }
    }
}
