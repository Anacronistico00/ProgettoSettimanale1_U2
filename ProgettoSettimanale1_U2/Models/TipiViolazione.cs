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

    [Required(ErrorMessage = "Il campo Descrizione è obbligatorio.")]
    [StringLength(1000, ErrorMessage = "La Descrizione non può superare i 1000 caratteri.")]
    public required string Descrizione { get; set; } = null!;

    [InverseProperty("IdViolazioneNavigation")]
    public virtual ICollection<Verbali> Verbalis { get; set; } = new List<Verbali>();
}
