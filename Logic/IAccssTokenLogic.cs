using System.Threading.Tasks;

namespace ATMModel.Logic.Abstract
{
    public interface IAccessTokenLogic
    {
        public bool IsAccessTokenValid(string token);
        public string NewAccessToken(string cardNumber);
        public string RenewAccessToken(string token);
        public string GetCardNumberFromAccessToken(string token);
    }
}