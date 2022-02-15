// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication6.Areas.Identity.Data;

namespace TimeTracker.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<TimeSystemUser> _userManager;
        private readonly SignInManager<TimeSystemUser> _signInManager;

        public IndexModel(
            UserManager<TimeSystemUser> userManager,
            SignInManager<TimeSystemUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

       
        public string Username { get; set; }

        
        [TempData]
        public string StatusMessage { get; set; }

        
        [BindProperty]
        public InputModel Input { get; set; }

       
        public class InputModel
        {
            
            [Display(Name ="User Name")]
            public string UserName { get; set; }

            [Display(Name ="First Name")]
            public string FirstName { get;set; }

            [Display (Name ="Last Name")]
            public string LastName { get; set; }

            [EmailAddress]
            [Display(Name ="Email")]
            public string Email { get; set; }

            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }
        }

        private async Task LoadAsync(TimeSystemUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;

            Input = new InputModel
            {
                UserName = userName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = phoneNumber
            };
        }



        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            if (Input.FirstName != user.FirstName)
            {
                user.FirstName = Input.FirstName;
            }
            if (Input.LastName != user.LastName)
            {
                user.LastName = Input.LastName;
            }
            if (Input.Email != user.Email)
            {
                user.Email = Input.Email;
            }
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }

            await _userManager.UpdateAsync(user);
            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
