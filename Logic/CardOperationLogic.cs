using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using ATMModel.Logic.Abstract;
using ATMModel.Models;

namespace ATMModel.Logic.Implementation
{
    public class CardOperationLogic : ICardOperationLogic
    {
        private readonly ILogger<CardLogic> _logger;

        private readonly ATMModelContext _context;


        public CardOperationLogic(ILogger<CardLogic> logger, ATMModelContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task AddCheckBalanceAsync(int cardId)
        {
            var operation = new Operation
            {
                CardId = cardId,
                BalanceChange = 0,
                Date = DateTime.UtcNow,
                OperationType = OperationType.CheckBalance
            };

            await _context.AddAsync(operation);
        }
        public async Task AddWithdrawAsync(int cardId, Decimal change)
        {
            var operation = new Operation
            {
                CardId = cardId,
                BalanceChange = change,
                Date = DateTime.UtcNow,
                OperationType = OperationType.Withdraw
            };
            await _context.AddAsync(operation);
       }
   }
}