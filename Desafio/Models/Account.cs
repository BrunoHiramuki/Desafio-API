using System.Text.Json.Serialization;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Desafio.Models
{
    public class Account
    {
        [JsonIgnore]
        public int AccountId { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Valor é obrigatório!")]
        public decimal Value { get; set; }

        [Required(ErrorMessage = "Data de Vencimento é obrigatório!")]
        [DataType(DataType.Date)]
        public DateTime DueDate { get; set; }

        [Required(ErrorMessage = "Data de Pagamento é obrigatório!")]
        [DataType(DataType.Date)]
        public DateTime PaymentDate { get; set; }

        [NotMapped]
        [JsonIgnore]
        public int? DaysLate { get; set; }
    }
}
