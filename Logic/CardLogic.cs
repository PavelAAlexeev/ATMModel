using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using ATMModel.Logic.Abstract;
using ATMModel.Logic.Datatypes;

namespace ATMModel.Logic.Implementation
{
    public class CardLogic : ICardLogic
    {
        protected static int CardNumberLength = 16;
        protected static int AllowedWrongAttempts = 3;

        protected static string DigitsGroupDelimiter = "-";
        
        private readonly ILogger<CardLogic> _logger;

        private readonly ATMModelContext _context;
        private readonly ICardOperationLogic _cardOperationLogic;

        public CardLogic(
            ILogger<CardLogic> logger,
            ATMModelContext context,
            ICardOperationLogic cardOperationLogic)
        {
            _logger = logger;
            _context = context;
            _cardOperationLogic = cardOperationLogic;
        }
        public bool IsNumberValid(string cardNumber)
        {
            if(string.IsNullOrEmpty(cardNumber))
            {
                return false;
            }

            cardNumber = CardNumberFromFormatted(cardNumber);

            if(string.IsNullOrEmpty(cardNumber) ||  cardNumber.Length != CardNumberLength || 
                !cardNumber.All(x => Char.IsDigit(x)) )
            {
                return false;
            }

            return true;
        }

        public async Task<bool> IsCardExistAsync(string cardNumber)
        {

            return await _context.Card.AnyAsync(x => x.CardNumber == cardNumber);
        }

        public async Task<bool> IsCardBlockedAsync(string cardNumber)
        {
            var isCardBlocked = (await _context.Card.AsNoTracking()
                    .FirstOrDefaultAsync(x => x.CardNumber == cardNumber))
                .Blocked;
            return isCardBlocked;
        }

        public async Task<Decimal> GetCardBalanceAsync(string cardNumber)
        {
            var card = await _context.Card.AsNoTracking()
                .FirstOrDefaultAsync(x => x.CardNumber == cardNumber);
            await _cardOperationLogic.AddCheckBalanceAsync(card.ID);
            await _context.SaveChangesAsync();
            return card.Balance;
        }

        public async Task<WithdrawResult> WithdrawAsync(string cardNumber, decimal amount)
        {
            var card = await _context.Card
                .FirstOrDefaultAsync(x => x.CardNumber == cardNumber);
            if(amount > 0 && card.Balance >= amount)
            {
                await _cardOperationLogic.AddWithdrawAsync(card.ID, amount);
                card.Balance -= amount; 

                await _context.SaveChangesAsync();
                return new WithdrawResult{
                    Result = true,
                    NewBalance = card.Balance
                };
            }
            else
            {
                return new WithdrawResult{
                    Result = false,
                    NewBalance = card.Balance
                };
            }
        }


        public string FormatCardNumber(string cardNumber)
        {
            var formattedCardNumber = new StringBuilder();
            for(int i = 0; i < cardNumber.Length; i++)
            {
               formattedCardNumber.Append(cardNumber[i]);
               if((i > 0) &&  i < (CardNumberLength - 1) && (i%4 == 3))
               {
                   formattedCardNumber.Append(DigitsGroupDelimiter);
               } 
            }
            return formattedCardNumber.ToString();
        }

        public string CardNumberFromFormatted(string cardNumber)
        {
            return cardNumber.Replace(DigitsGroupDelimiter, "");
        }

        public string GenerateHashedPIN(string pin)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] pinBytes = Encoding.ASCII.GetBytes(pin);
                byte[] pinHash = sha256.ComputeHash(pinBytes);
                byte[] pinHash2 = sha256.ComputeHash(pinHash);
                Array.Clear(pinBytes, 0, pin.Length);
                Array.Clear(pinHash, 0, pinHash.Length);

                return BitConverter.ToString( pinHash2 );
            }
        }

        public async Task<bool> CheckPINAsync(string cardNumber, string pin)
        {
            var hashedPin = GenerateHashedPIN(pin);

            var card = await _context.Card.FirstOrDefaultAsync(x => x.CardNumber == cardNumber);
            if(card == null)
            {
                throw new Exception("No card with requested number");
            }
            
            if(card.Blocked)
            {
                throw new Exception("The requested card is blocked");
            }

            try
            {
                if(card.HashedPin == hashedPin)
                {
                    card.CountOfWrongTry = 0;
                    return true;
                }
                else
                {
                    card.CountOfWrongTry++;
                    if(card.CountOfWrongTry > AllowedWrongAttempts)
                    {
                        card.Blocked = true;
                    }
                    return false;
                }
            }
            finally
            {
                await _context.SaveChangesAsync();
            }
        }
    }
}