using ATMModel.Logic.Abstract;

namespace ATMModel.Logic.Implementation
{

    //TODO IMPLEMENT TOKEN. Now it is just CardNumber. Need to pack it (and issueing time) into JWT 
    public class AccessTokenLogic : IAccessTokenLogic  
    {
        public bool IsAccessTokenValid(string token)
        {
            return true;
        }
        public string RenewAccessToken(string token)
        {
            return token;
        }

        public string NewAccessToken(string cardNumber)
        {
            return cardNumber;
        }

        public string GetCardNumberFromAccessToken(string token)
        {
            return token;
        }
    }
}