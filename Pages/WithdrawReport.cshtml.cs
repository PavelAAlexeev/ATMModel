using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using ATMModel.Logic.Abstract;

namespace ATMModel.Pages
{
    public class WithdrawReportModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ICardLogic _cardLogic;
        private readonly IAccessTokenLogic _accessTokenLogic;

        public WithdrawReportModel(
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
        public DateTime OperationDateTime { get; set; }

        [BindProperty]
        public Decimal Amount { get; set; }
        
        [BindProperty]
        public Decimal Balance { get; set; }

        public IActionResult OnGet(
            string accessToken,
            DateTime dateTime,
            decimal amount,
            decimal balance
            )
        {
            if(!_accessTokenLogic.IsAccessTokenValid(accessToken))
            {
                return RedirectToPage("./Error", new {errorMessage = "Сессия завершена"});
            }
            this.AccessToken = _accessTokenLogic.RenewAccessToken(accessToken);

            var cardNumber = _accessTokenLogic.GetCardNumberFromAccessToken(accessToken);
            var cardBalance = balance;
            var formattedCardNumber = _cardLogic.FormatCardNumber(cardNumber);


            this.CardNumber = formattedCardNumber;
            this.OperationDateTime = dateTime;
            this.Amount = amount;
            this.Balance = balance;

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
