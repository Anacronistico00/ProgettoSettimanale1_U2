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

    [Column(TypeName = "datetime")]
    public DateTime DataViolazione { get; set; }

    [StringLength(100)]
    public string IndirizzoViolazione { get; set; } = null!;

    [StringLength(100)]
    public string NominativoAgente { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime DataTrascrizioneVerbale { get; set; }

    [Column(TypeName = "decimal(7, 2)")]
    public decimal Importo { get; set; }

    public int? DecurtamentoPunti { get; set; }

    [Key]
    public Guid IdAnagrafica { get; set; }

    [Key]
    public Guid IdViolazione { get; set; }

    [ForeignKey("IdAnagrafica")]
    [InverseProperty("Verbalis")]
    public virtual Anagrafiche IdAnagraficaNavigation { get; set; } = null!;

    [ForeignKey("IdViolazione")]
    [InverseProperty("Verbalis")]
    public virtual TipiViolazione IdViolazioneNavigation { get; set; } = null!;
}
