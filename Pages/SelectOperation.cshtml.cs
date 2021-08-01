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

        public IActionResult OnGetAsync(string AccessToken)
        {
            if(!_accessTokenLogic.IsAccessTokenValid(AccessToken))
            {
                return BadRequest();
            }

            this.AccessToken = AccessToken;

            return Page();
        }

        /*public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            return Page();
        }*/

        //[HttpPost("button1")]
        public IActionResult OnPostAccount()
        {
                return Page();
            //...
        }

        //[HttpPost("button1")]
        public IActionResult OnPostWithdraw()
        {
                return Page();
            //...
        }

        [BindProperty]
        public string AccessToken { get; set; }

    }
}
