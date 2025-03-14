using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProgettoSettimanale1_U2.Models;

[Table("Tipi Violazione")]
public partial class TipiViolazione
{
    [Key]
    public Guid IdViolazione { get; set; }

    [StringLength(1000)]
    public string Descrizione { get; set; } = null!;

    [InverseProperty("IdViolazioneNavigation")]
    public virtual ICollection<Verbali> Verbalis { get; set; } = new List<Verbali>();
}
