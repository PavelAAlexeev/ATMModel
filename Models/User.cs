using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;


namespace ATMModel.Models
{
    public class User
    {
        public int ID { get; set; }
        [Required(AllowEmptyStrings=false)]
        public string FirstName { get; set; }
        
        [Required(AllowEmptyStrings=false)]
        public string LastName { get; set; }

        //references
        public virtual ICollection<Card> Cards { get; set; }
    }
}