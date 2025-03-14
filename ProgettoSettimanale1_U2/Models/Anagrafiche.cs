using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProgettoSettimanale1_U2.Models;

[Table("Anagrafiche")]
public partial class Anagrafiche
{
    [Key]
    public Guid IdAnagrafica { get; set; }

    [Required(ErrorMessage = "Il campo Cognome è obbligatorio.")]
    [StringLength(100, ErrorMessage = "Il Cognome non può superare i 100 caratteri.")]
    public string Cognome { get; set; } = null!;

    [Required(ErrorMessage = "Il campo Nome è obbligatorio.")]
    [StringLength(100, ErrorMessage = "Il Nome non può superare i 100 caratteri.")]
    public string Nome { get; set; } = null!;

    [Required(ErrorMessage = "Il campo Indirizzo è obbligatorio.")]
    [StringLength(100, ErrorMessage = "L'Indirizzo non può superare i 100 caratteri.")]
    public string Indirizzo { get; set; } = null!;

    [Required(ErrorMessage = "Il campo Città è obbligatorio.")]
    [StringLength(100, ErrorMessage = "La Città non può superare i 100 caratteri.")]
    public string Citta { get; set; } = null!;

    [Required(ErrorMessage = "Il campo CAP è obbligatorio.")]
    [Column("CAP")]
    [Range(10000, 99999, ErrorMessage = "Il CAP deve essere un valore numerico compreso tra 10000 e 99999.")]
    public int Cap { get; set; }

    [Required(ErrorMessage = "Il campo Codice Fiscale è obbligatorio.")]
    [StringLength(16, MinimumLength = 16, ErrorMessage = "Il Codice Fiscale deve essere esattamente di 16 caratteri.")]
    [RegularExpression("^[A-Z0-9]{16}$", ErrorMessage = "Il Codice Fiscale deve contenere solo lettere maiuscole e numeri.")]
    public string CodiceFiscale { get; set; } = null!;

    [InverseProperty("IdAnagraficaNavigation")]
    public virtual ICollection<Verbali> Verbalis { get; set; } = new List<Verbali>();
}
