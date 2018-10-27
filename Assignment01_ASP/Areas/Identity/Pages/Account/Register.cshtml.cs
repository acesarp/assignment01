using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Assignment01_ASP.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment01_ASP.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        public RegisterModel(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel {

            //[Key]
            //[DatabaseGenerated(DatabaseGeneratedOption.None)]
            [Required]
            [Display(Name = "Username")]
            [DataType(DataType.Text)]
            [StringLength(20, MinimumLength = 6, ErrorMessage = "Username must be at least {0} and max {1} characteres long.")]
            public string UserName { get; set; }

            [DataType(DataType.PhoneNumber)]
            [Display(Name = "Cell number")]
            public long MobileNumber { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 8)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [MinLength(2)]
            [Required]
            [Display(Name = "Your first name")]
            [DataType(DataType.Text)]
            public string FirstName { get; set; }

            [MinLength(2)]
            [Required]
            [Display(Name = "Your last name")]
            [DataType(DataType.Text)]
            public string LastName { get; set; }

            [Required]
            [Display(Name = "Street")]
            [DataType(DataType.Text)]
            public string Street { get; set; }

            [Required]
            [MinLength(4)]
            [Display(Name = "City")]
            [DataType(DataType.Text)]
            public string City { get; set; }

            [Required]
            [StringLength(2)]
            [Display(Name ="Province")]
            [DataType(DataType.Text)]
            public string Province { get; set; }

            [StringLength(6)]
            [Display(Name = "Postal code")]
            [DataType(DataType.Text)]
            public string PostalCode { get; set; }

            [Required]
            [MinLength(3)]
            [Display(Name = "Country")]
            [DataType(DataType.Text)]
            public string Country { get; set; }
        }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                var user = new AppUser { UserName =Input.UserName,
                                            Email = Input.Email,
                                            MobileNumber = Input.MobileNumber,
                                            Password = Input.Password,
                                            FirstName = Input.FirstName,
                                            LastName = Input.LastName,
                                            Street = Input.Street,
                                            City = Input.City,
                                            Province = Input.Province,
                                            PostalCode = Input.PostalCode,
                                            Country = Input.Country
                };

                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { userId = user.Id, code = code },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnUrl);
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
