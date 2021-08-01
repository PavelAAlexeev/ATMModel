using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

using ATMModel.Logic.Abstract;
using ATMModel.Models;

namespace ATMModel.Logic.Implementation
{
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
    }
}