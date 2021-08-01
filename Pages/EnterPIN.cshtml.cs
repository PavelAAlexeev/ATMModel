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
    public class EnterPINModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        private readonly ICardLogic _cardLogic;
        private readonly IAccessTokenLogic _accessTokenLogic;

        public EnterPINModel(ILogger<IndexModel> logger, ICardLogic cardLogic, IAccessTokenLogic accessTokenLogic)
        {
            _logger = logger;
            _cardLogic = cardLogic;
            _accessTokenLogic = accessTokenLogic;
        }

        [BindProperty]
        public string CardNumber { get; set; }
        [BindProperty]
        public string PINNumber { get; set; }
        
        public async Task<IActionResult> OnGetAsync(string cardNumber)
        {
            //var cardNumber = id;

            if(! await _cardLogic.IsCardExistAsync(cardNumber) 
                && ! await _cardLogic.IsCardBlockedAsync(cardNumber) )
            {
                return BadRequest();
            }
            CardNumber = cardNumber;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            PINNumber = "0000";

            if(! await _cardLogic.CheckPINAsync(CardNumber, PINNumber)) 
            {
                return RedirectToPage("./Error", new {errorMessage = "ПИН-код введен неправильно"});
            }

            var AccessToken = _accessTokenLogic.NewAccessToken(CardNumber);
            return RedirectToPage("./SelectOperation",  new {AccessToken = AccessToken});
        }
    }
}
