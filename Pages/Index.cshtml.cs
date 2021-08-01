using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using ATMModel.Models;

namespace ATMModel.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        private readonly ATMModelContext _context;

        public IndexModel(ILogger<IndexModel> logger, ATMModelContext context)
        {
            _logger = logger;
            _context = context;
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
                return BadRequest();
            }

            CardNumber = CardNumber.Replace("-", "");

            if(string.IsNullOrEmpty(CardNumber) ||  CardNumber.Length != Card.CardNumberLength || 
                !CardNumber.All(x => Char.IsDigit(x)) )
            {
                return BadRequest();
            }

            bool isCardExist = await _context.Card.AnyAsync(x => x.CardNumber == CardNumber);
            if(!isCardExist) 
            {
                return BadRequest();
            }

            var isCardblocked = (await _context.Card.FirstOrDefaultAsync(x => x.CardNumber == CardNumber)).Blocked;
            if(isCardblocked) 
            {
                return BadRequest();
            }

            return RedirectToPage("./EnterPin");
        }
    }
}
