using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using ATMModel.Logic.Abstract;
using ATMModel.Models;

namespace ATMModel.Logic.Implementation
{
    public class CardLogic : ICardLogic
    {
        
        private readonly ILogger<CardLogic> _logger;

        private readonly ATMModelContext _context;

        public CardLogic(ILogger<CardLogic> logger, ATMModelContext context)
        {
            _logger = logger;
            _context = context;
        }
        public bool IsNumberValid(string cardNumber)
        {
            if(string.IsNullOrEmpty(cardNumber))
            {
                return false;
            }

            cardNumber = cardNumber.Replace("-", "");

            if(string.IsNullOrEmpty(cardNumber) ||  cardNumber.Length != Card.CardNumberLength || 
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

        public async Task<bool> CheckPINAsync(string cardNumber, string pin)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] pinBytes = Encoding.ASCII.GetBytes(pin);
                byte[] pinHash = sha256.ComputeHash(pinBytes);
                byte[] pinHash2 = sha256.ComputeHash(pinHash);
                Array.Clear(pinBytes, 0, pin.Length);
                Array.Clear(pinHash, 0, pinHash.Length);
                

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
                    if(card.HashedPin ==  BitConverter.ToString( pinHash2 ))
                    {
                        card.CountOfWrongTry = 0;
                        return true;
                    }
                    else
                    {
                        card.CountOfWrongTry++;
                        if(card.CountOfWrongTry > Card.AllowedWrongAttempts)
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
}