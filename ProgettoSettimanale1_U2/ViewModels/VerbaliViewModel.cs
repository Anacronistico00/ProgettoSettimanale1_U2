namespace ProgettoSettimanale1_U2.ViewModels
{
    public class VerbaliViewModel
    {
        public int IdVerbale { get; set; }
        public DateTime DataViolazione { get; set; }
        public string? IndirizzoViolazione { get; set; } = null!;
        public string? NominativoAgente { get; set; } = null!;
        public DateTime DataTrascrizioneVerbale { get; set; }
        public decimal Importo { get; set; }
        public int? DecurtamentoPunti { get; set; }
        public Guid IdAnagrafica { get; set; }
        public string? NomeCompleto { get; set; } = null!;
        public Guid IdViolazione { get; set; }
        public string? DescrizioneViolazione { get; set; } = null!;
    }
}
