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
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ICardLogic _cardLogic;

        public IndexModel(ILogger<IndexModel> logger, ICardLogic cardLogic)
        {
            _logger = logger;
            _cardLogic = cardLogic;
        }

        [BindProperty]
        public string CardNumber { get; set; }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            if(string.IsNullOrEmpty(CardNumber))
            {
                return RedirectToPage("./Error", new {errorMessage = "Номер карты не введен"});
            }

            CardNumber = _cardLogic.CardNumberFromFormatted(CardNumber);

            if(!_cardLogic.IsNumberValid(CardNumber))
            {
                return RedirectToPage("./Error", new {errorMessage = "Неправильный формат номера карты"});
            }

            if(! await _cardLogic.IsCardExistAsync(CardNumber))
            {
                return RedirectToPage("./Error", new {errorMessage = "Карты с таким номером не существует"});
            }

            if(await _cardLogic.IsCardBlockedAsync(CardNumber)) 
            {
                return RedirectToPage("./Error", new {errorMessage = "Карта заблокирована"});
            }
            
            string url = Url.Page("EnterPin", new {cardNumber=CardNumber});
            return Redirect(url);

        }
    }
}
