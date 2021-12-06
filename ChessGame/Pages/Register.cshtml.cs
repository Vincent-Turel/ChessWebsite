using System.ComponentModel.DataAnnotations;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace ChessGame.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly ILogger<RegisterModel> _logger;
        [BindProperty, Required, MinLength(2, ErrorMessage = "Must be at least 2-character long.")]  
        [RegularExpression(@"[\S]+$", ErrorMessage = "Should not contains spaces.")] public string Username { get; set; }
        [BindProperty, Required, MinLength(6, ErrorMessage = "Must be at least 6-character long.")] 
        [RegularExpression(@"[\S]+$", ErrorMessage = "Should not contains spaces.")] public string Password { get; set; }
        [BindProperty, Required, Compare("Password", ErrorMessage = "Confirm password doesn't match.")] public string Confirm { get; set; }

        public RegisterModel(ILogger<RegisterModel> logger)
        {
            _logger = logger;
            Username = "";
            Password = "";
            Confirm = "";
        }

        public void OnGet()
        {
            GlobalOptions.IsLoggedIn = false;
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid) return Page();
            foreach (string line in System.IO.File.ReadLines(@"./Backend/Users"))
            {
                var args = line.Split(" ");
                if (args[1].Equals(Username) || args[2].Equals(Password))
                {
                    return Page();
                }
            }

            using (StreamWriter writer = new(@"./Backend/Users", true))
            {
                writer.WriteLine(500 + " " + Username + " " + Password);
            }
            GlobalOptions.IsLoggedIn = true;
            GlobalOptions.Username = Username;
            GlobalOptions.Score = 500;
            return RedirectToPage("/Menu");
        }
    }
}