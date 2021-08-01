using System.Threading.Tasks;

using ATMModel.Models;
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

        public Task<Card> GetCardAsync(string cardNumber);
    }
}