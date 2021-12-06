using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChessGame.Pages
{
    public class LoginModel : PageModel
    {
        public LoginModel()
        {
            Username = "";
            Password = "";
        }

        [BindProperty] public string Username { get; set; }
        [BindProperty] public string Password { get; set; }

        public void OnGet()
        {
            GlobalOptions.IsLoggedIn = false;
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                foreach (string line in System.IO.File.ReadLines(@"./Backend/Users"))
                {
                    var args = line.Split(" ");
                    if (args[1].Equals(Username) && args[2].Equals(Password))
                    {
                        GlobalOptions.IsLoggedIn = true;
                        GlobalOptions.Username = Username;
                        GlobalOptions.Score = int.Parse(args[0]);
                        return RedirectToPage("/Menu");
                    }
                }
            }
            return Page();
        }
    }
}