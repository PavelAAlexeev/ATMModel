using System;
using System.ComponentModel.DataAnnotations;

namespace ATMModel.Models
{
    public class Card
    {
        public int ID { get; set; }

        [Required(AllowEmptyStrings=false)]
        public string CardNumber { get; set; }
        
        [Required(AllowEmptyStrings=false)]
        public string HashedPin { get; set; }
        
        public Decimal Balance { get; set; }
        
        public Int16 CountOfWrongTry { get; set; }
        
        public bool Blocked { get; set; }
        
        public int UserId { get; set; }

        //references
        public virtual User User { get; set; }
    }
}