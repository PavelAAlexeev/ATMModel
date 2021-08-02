using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using ATMModel.Logic.Abstract;

namespace ATMModel.Pages
{
    public class SelectOperationModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ICardLogic _cardLogic;
        private readonly IAccessTokenLogic _accessTokenLogic;

        public SelectOperationModel(
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

        public IActionResult OnGet(string AccessToken)
        {
            if(!_accessTokenLogic.IsAccessTokenValid(AccessToken))
            {
                return RedirectToPage("./Error", new {errorMessage = "Сессия завершена"});
            }

            this.AccessToken = _accessTokenLogic.RenewAccessToken(AccessToken);

            return Page();
        }

        public IActionResult OnPostCardBalance()
        {
            return RedirectToPage("./CardBalance",  new {accessToken = AccessToken});
        }

        public IActionResult OnPostWithdraw()
        {
            return RedirectToPage("./Withdraw",  new {accessToken = AccessToken});
        }

        public IActionResult OnPostLogout()
        {
            return RedirectToPage("./Index");
        }
    }
}
