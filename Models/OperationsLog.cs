using System;
using System.ComponentModel.DataAnnotations;

namespace ATMModel.Models
{

    public enum OperationType
    {
        CheckBalance,
        Withdraw,
    }

    public class Operation
    {
        public int ID { get; set; }

        [Required]
        public int CardId { get; set; }
        
        [Required]
        public OperationType OperationType { get; set; }
        
        [Required]
        public DateTime Date { get; set; }

        [Required]
        public Decimal BalanceChange { get; set; }

        //references
        public virtual Card Card { get; set; }
    }
}