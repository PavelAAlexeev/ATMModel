using System;
using System.Threading.Tasks;

using ATMModel.Logic.Datatypes;
namespace ATMModel.Logic.Abstract
{
    public interface ICardLogic
    {
        public bool IsNumberValid(string cardNumber);
        public Task<bool> IsCardExistAsync(string cardNumber);
        public Task<bool> IsCardBlockedAsync(string cardNumber);
        public Task<bool> CheckPINAsync(string cardNumber, string pin);
        public string FormatCardNumber(string cardNumber);
        public string CardNumberFromFormatted(string cardNumber);
        public Task<Decimal> GetCardBalanceAsync(string cardNumber);
        public Task<WithdrawResult> WithdrawAsync(string cardNumber, decimal ammount);
    }
}