using System;
using System.Threading.Tasks;

using ATMModel.Models;
namespace ATMModel.Logic.Datatypes
{
    public class WithdrawResult
    {
        public bool Result {get; set;}
        public Decimal NewBalance {get; set;}
   }
}