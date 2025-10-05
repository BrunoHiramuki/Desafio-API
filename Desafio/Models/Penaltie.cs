
namespace Desafio.Models
{
    public class Penaltie
    {
        public int PenaltieId { get; set; }
        public int DaysLateFrom { get; set; }
        public int? DaysLateTo { get; set; }
        public decimal FinePercent { get; set; }

        public decimal DailyInterest { get; set; }
    }
}
