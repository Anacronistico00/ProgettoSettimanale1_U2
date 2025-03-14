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

    [StringLength(100)]
    public string Cognome { get; set; } = null!;

    [StringLength(100)]
    public string Nome { get; set; } = null!;

    [StringLength(100)]
    public string Indirizzo { get; set; } = null!;

    [StringLength(100)]
    public string Citta { get; set; } = null!;

    [Column("CAP")]
    public int Cap { get; set; }

    [StringLength(16)]
    public string CodiceFiscale { get; set; } = null!;

    [InverseProperty("IdAnagraficaNavigation")]
    public virtual ICollection<Verbali> Verbalis { get; set; } = new List<Verbali>();
}
