namespace ProgettoSettimanale1_U2.ViewModels
{
    public class VerbaliPerTrasgressoreViewModel
    {
        public string Cognome { get; set; } = null!;
        public string Nome { get; set; } = null!;
        public string CodiceFiscale { get; set; } = null!;
        public int NumeroVerbali { get; set; }
        public decimal ImportoTotale { get; set; }
    }
}
