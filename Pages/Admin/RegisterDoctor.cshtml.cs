using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HospitalManagementSystem.Pages.Admin
{
    [Authorize(Roles = "Admin")]
    public class RegisterDoctorModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;

        public RegisterDoctorModel(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public string Message { get; set; }
        public string Error { get; set; }

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = new IdentityUser { UserName = Email, Email = Email };
            var result = await _userManager.CreateAsync(user, Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Doctor");
                Message = $"Doctor registered: {Email}";
            }
            else
            {
                Error = string.Join(" ", result.Errors.Select(e => e.Description));
            }

            return Page();
        }
    }
}
