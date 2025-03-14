namespace ProgettoSettimanale1_U2.ViewModels
{
    public class ViolazioniGraviViewModel
    {
        public int IdVerbale { get; set; }
        public string Cognome { get; set; } = null!;
        public string Nome { get; set; } = null!;
        public DateTime DataViolazione { get; set; }
        public decimal Importo { get; set; }
        public int DecurtamentoPunti { get; set; }
    }
}
