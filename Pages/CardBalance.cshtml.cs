using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using ATMModel.Models;
using ATMModel.Logic.Abstract;

namespace ATMModel.Pages
{
    public class CardBalanceModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ICardLogic _cardLogic;
        private readonly IAccessTokenLogic _accessTokenLogic;

        public CardBalanceModel(
            ILogger<IndexModel> logger,
            ICardLogic cardLogic,
            IAccessTokenLogic accessTokenLogic)
        {
            _logger = logger;
            _cardLogic = cardLogic;
            _accessTokenLogic = accessTokenLogic;
        }

        [BindProperty]
        public string CardNumber { get; set; }

        [BindProperty]
        public string AccessToken { get; set; }
        
        [BindProperty]
        public DateTime CurrentDateTime { get; set; }

        [BindProperty]
        public Decimal Ammount { get; set; }

        public async Task<IActionResult> OnGetAsync(string accessToken)
        {
            if(!_accessTokenLogic.IsAccessTokenValid(accessToken))
            {
                return BadRequest();
            }

            var cardNumber = _accessTokenLogic.GetCardNumberFromAccessToken(accessToken);
            var card = await _cardLogic.GetCardAsync(cardNumber);
            var formattedCardNumber = _cardLogic.FormatCardNumber(cardNumber);

            this.AccessToken = _accessTokenLogic.RenewAccessToken(accessToken);
            this.CardNumber = formattedCardNumber;
            this.CurrentDateTime = DateTime.UtcNow;
            this.Ammount = card.Balance;


            return Page();
        }

        public IActionResult OnPostBack()
        {
            return RedirectToPage("./SelectOperation",  new {AccessToken = AccessToken});
        }

        public IActionResult OnPostLogout()
        {
            return RedirectToPage("./Index");
        }
    }
}
