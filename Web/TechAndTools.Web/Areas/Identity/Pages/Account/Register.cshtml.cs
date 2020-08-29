using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using TechAndTools.Commons.Constants;
using TechAndTools.Data.Models;
using TechAndTools.Services.Contracts;
using TechAndTools.Web.ViewModels.ShoppingCart;

namespace TechAndTools.Web.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<TechAndToolsUser> signInManager;
        private readonly UserManager<TechAndToolsUser> userManager;
        private readonly ILogger<RegisterModel> logger;
        private readonly IEmailSender emailSender;
        private readonly IUserService userService;
        private readonly IShoppingCartService shoppingCartService;

        public RegisterModel(
            UserManager<TechAndToolsUser> userManager,
            SignInManager<TechAndToolsUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            IUserService userService,
            IShoppingCartService shoppingCartService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = logger;
            this.emailSender = emailSender;
            this.userService = userService;
            this.shoppingCartService = shoppingCartService;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required]
            [Display(Name = "Username")]
            public string Username { get; set; }
            
            [Required]
            [Display(Name = "First Name")]
            public string FirstName { get; set; }
            
            [Required]
            [Display(Name = "Last Name")]
            public string LastName { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
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
                var user = new TechAndToolsUser
                {
                    UserName = Input.Username,
                    Email = Input.Email,
                    FirstName = Input.FirstName,
                    LastName = Input.LastName,
                    ShoppingCart = new ShoppingCart(),
                    CreatedOn = DateTime.UtcNow
                };

                var resultCreate = await userManager.CreateAsync(user, Input.Password);

                if (resultCreate.Succeeded)
                {
                    logger.LogInformation("User created a new account with password.");

                    var code = await userManager.GenerateEmailConfirmationTokenAsync(user);

                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { userId = user.Id, code = code },
                        protocol: Request.Scheme);

                    await userManager.AddToRoleAsync(user, GlobalConstants.UserRole);

                    await emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    await signInManager.SignInAsync(user, isPersistent: false);

                    
                    var cart = SessionHelper.GetObjectFromJson<List<ShoppingCartProductViewModel>>(HttpContext.Session, TechAndTools.Commons.Constants.GlobalConstants.SessionShoppingCartKey);

                    if (cart != null)
                    {
                        foreach (var product in cart)
                        {
                            await shoppingCartService.AddToShoppingCartAsync(product.Id, Input.Email, product.Quantity);
                        }

                        HttpContext.Session.Remove(GlobalConstants.SessionShoppingCartKey);
                    }

                    return LocalRedirect(returnUrl);
                }
                foreach (var error in resultCreate.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
