using System.Threading.Tasks;

namespace ATMModel.Logic.Abstract
{
    public interface ICardLogic
    {
        public bool IsNumberValid(string cardNumber);
        public Task<bool> IsCardExistAsync(string cardNumber);
        public Task<bool> IsCardBlockedAsync(string cardNumber);
        public Task<bool> CheckPINAsync(string cardNumber, string pin);
    }
}