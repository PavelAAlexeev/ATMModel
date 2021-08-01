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
            
            CardNumber = "1111-1111-1111-1111";
            
            if(string.IsNullOrEmpty(CardNumber))
            {
                return BadRequest();
            }

            CardNumber = CardNumber.Replace("-", "");

            if(!_cardLogic.IsNumberValid(CardNumber))
            {
                return BadRequest();
            }

            if(! await _cardLogic.IsCardExistAsync(CardNumber))
            {
                return BadRequest();
            }

            if(await _cardLogic.IsCardBlockedAsync(CardNumber)) 
            {
                return BadRequest();
            }
            string url = Url.Page("EnterPin", new {cardNumber=CardNumber});
            return Redirect(url);
            //return RedirectToPage("./EnterPin", CardNumber);
        }
    }
}
