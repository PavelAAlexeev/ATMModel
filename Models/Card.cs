using System;
using System.ComponentModel.DataAnnotations;

namespace ATMModel.Models
{
    public class Card
    {
        public int ID { get; set; }
        public string HashedPin { get; set; }
        public Decimal Balance { get; set; }
        public bool CountOfWrongTry { get; set; }
        public bool Blocked { get; set; }

    }
}