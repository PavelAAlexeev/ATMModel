using System;
using System.ComponentModel.DataAnnotations;

namespace ATMModel.Models
{
    public class User
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}