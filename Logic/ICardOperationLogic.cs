using System;
using System.Threading.Tasks;

namespace ATMModel.Logic.Abstract
{
    public interface ICardOperationLogic
    {
        public Task AddCheckBalanceAsync(int cardId);
        public Task AddWithdrawAsync(int cardId, Decimal change);
   }
}