using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProgettoSettimanale1_U2.Models;

[PrimaryKey("IdVerbale", "IdAnagrafica", "IdViolazione")]
[Table("Verbali")]
public partial class Verbali
{
    [Key]
    public int IdVerbale { get; set; }

    [Required(ErrorMessage = "Il campo Data Violazione è obbligatorio.")]
    [Column(TypeName = "datetime")]
    public required DateTime DataViolazione { get; set; }

    [Required(ErrorMessage = "Il campo Indirizzo Violazione è obbligatorio.")]
    [StringLength(100, ErrorMessage = "L'Indirizzo Violazione non può superare i 100 caratteri.")]
    public required string IndirizzoViolazione { get; set; } = null!;

    [Required(ErrorMessage = "Il campo Nominativo Agente è obbligatorio.")]
    [StringLength(100, ErrorMessage = "Il Nominativo Agente non può superare i 100 caratteri.")]
    public required string NominativoAgente { get; set; } = null!;

    [Required(ErrorMessage = "Il campo Data Trascrizione Verbale è obbligatorio.")]
    [Column(TypeName = "datetime")]
    public required DateTime DataTrascrizioneVerbale { get; set; }

    [Required(ErrorMessage = "Il campo Importo è obbligatorio.")]
    [Column(TypeName = "decimal(7, 2)")]
    [Range(0, 9999999.99, ErrorMessage = "L'Importo deve essere un valore numerico positivo e non superare 9999999.99.")]
    public required decimal Importo { get; set; }

    [Required(ErrorMessage = "Il campo Decurtamento Punti è obbligatorio.")]
    [Range(0, 20, ErrorMessage = "Il Decurtamento Punti deve essere un valore tra 0 e 20.")]
    public required int? DecurtamentoPunti { get; set; }

    [Required(ErrorMessage = "Il campo IdAnagrafica è obbligatorio.")]
    public required Guid IdAnagrafica { get; set; }

    [Required(ErrorMessage = "Il campo IdViolazione è obbligatorio.")]
    public required Guid IdViolazione { get; set; }

    [ForeignKey("IdAnagrafica")]
    [InverseProperty("Verbalis")]
    public virtual Anagrafiche IdAnagraficaNavigation { get; set; } = null!;

    [ForeignKey("IdViolazione")]
    [InverseProperty("Verbalis")]
    public virtual TipiViolazione IdViolazioneNavigation { get; set; } = null!;
}
