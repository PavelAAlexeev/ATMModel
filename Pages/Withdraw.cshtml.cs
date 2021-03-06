using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using ATMModel.Logic.Abstract;

namespace ATMModel.Pages
{
    public class WithdrawModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ICardLogic _cardLogic;
        private readonly IAccessTokenLogic _accessTokenLogic;
        public WithdrawModel(
            ILogger<IndexModel> logger,
            ICardLogic cardLogic,
            IAccessTokenLogic accessTokenLogic)
        {
            _logger = logger;
            _cardLogic = cardLogic;
            _accessTokenLogic = accessTokenLogic;
        }

        [BindProperty]
        public string AccessToken { get; set; }
        [BindProperty]
        public Decimal Amount { get; set; }

        public IActionResult OnGet(string AccessToken)
        {
            if(!_accessTokenLogic.IsAccessTokenValid(AccessToken))
            {
                return RedirectToPage("./Error", new {errorMessage = "Сессия завершена"});
            }
            this.AccessToken = _accessTokenLogic.RenewAccessToken(AccessToken);

            return Page();
        }


        public IActionResult OnPostLogout()
        {
            return RedirectToPage("./Index");
        }
        public async Task<IActionResult> OnPostOKAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
           
            var cardNumber = _accessTokenLogic.GetCardNumberFromAccessToken(AccessToken);
            if(await _cardLogic.IsCardBlockedAsync(cardNumber))
            {
                return RedirectToPage("./Error", new {errorMessage = "Карта заблокирована", AccessToken = AccessToken});
            }

            var withdrawResult = await _cardLogic.WithdrawAsync(cardNumber, Amount);
            if(!withdrawResult.Result)
            {
                return RedirectToPage("./Error", new {ErrorMessage = "Не удалось снять деньги", AccessToken = AccessToken});
            }
            
            string url = Url.Page("WithdrawReport", new 
            {
                AccessToken=AccessToken,
                OperationDateTime = DateTime.UtcNow,
                Amount = Amount,
                Balance = withdrawResult.NewBalance
            });
            return Redirect(url);
        }
    }
}
