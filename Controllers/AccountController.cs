using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using shoes_final_exam.Models;
using shoes_final_exam.Models.AuthenticationModels;
using shoes_final_exam.Models.MailModels;
using shoes_final_exam.Repositories;
using System.Security.Claims;

namespace shoes_final_exam.Controllers
{
    public class AccountController : Controller
    {
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
		private readonly IEmailSender _emailSender;

		public AccountController(IMapper mapper,
            UserManager<AppUser> userManager, 
            SignInManager<AppUser> signInManager,
            IEmailSender emailSender
            )
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
		}

        //--------------------------- REGISTER ---------------------------

		[HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserRegistrationModel userModel)
        {
            if (!ModelState.IsValid)
            {
                return View(userModel);
            }
            var user = _mapper.Map<AppUser>(userModel);
            var result = await _userManager.CreateAsync(user, userModel.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return View(userModel);
            }

            await _userManager.AddToRoleAsync(user, "User");

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

		// --------------------------- LOGIN ---------------------------
		[HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLoginModel userModel)
        {
            if (!ModelState.IsValid)
            {
                return View(userModel);
            }
            var result = await _signInManager.PasswordSignInAsync(userModel.Email, userModel.Password, true, false);
            if (result.Succeeded)
            {
				return RedirectToAction(nameof(HomeController.Index), "Home");
			}
            else
            {
                ModelState.AddModelError("", "Không tồn tại tài khoản hoặc mật khẩu");
                return View();
            }
        }

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Logout()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction(nameof(HomeController.Index), "Home");
		}

		// --------------------------- FORGOT PASSWORD ---------------------------
		[HttpGet]
		public IActionResult ForgotPassword()
		{
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> ForgotPassword(ForgotPasswordModel forgotPasswordModel)
		{
			if (!ModelState.IsValid)
				return View(forgotPasswordModel);
			var user = await _userManager.FindByEmailAsync(forgotPasswordModel.Email);
			if (user == null)
				return RedirectToAction(nameof(ForgotPasswordConfirmation));

			var token = await _userManager.GeneratePasswordResetTokenAsync(user);
			var callback = Url.Action(nameof(ResetPassword), "Account", new { token, email = user.Email }, Request.Scheme);
			var message = new Message(new string[] { user.Email }, "Reset password token", callback, null);
			await _emailSender.SendEmailAsync(message);
			return RedirectToAction(nameof(ForgotPasswordConfirmation));
		}
		public IActionResult ForgotPasswordConfirmation()
		{
			return View();
		}
        // --------------------------- RESET PASSWORD ---------------------------
        [HttpGet]
		public IActionResult ResetPassword(string token, string email)
		{
			var model = new ResetPasswordModel { Token = token, Email = email };
			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> ResetPassword(ResetPasswordModel resetPasswordModel)
		{
			if (!ModelState.IsValid)
				return View(resetPasswordModel);
			var user = await _userManager.FindByEmailAsync(resetPasswordModel.Email);
			if (user == null)
				RedirectToAction(nameof(ResetPasswordConfirmation));
			var resetPassResult = await _userManager.ResetPasswordAsync(user, resetPasswordModel.Token, resetPasswordModel.Password);
			if (!resetPassResult.Succeeded)
			{
				foreach (var error in resetPassResult.Errors)
				{
					ModelState.TryAddModelError(error.Code, error.Description);
				}
				return View();
			}
			return RedirectToAction(nameof(ResetPasswordConfirmation));
		}

		[HttpGet]
		public IActionResult ResetPasswordConfirmation()
		{
			return View();
		}
        // --------------------------- CHANGE PASSWORD ---------------------------
        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel changePasswordModel)
        {
            if (!ModelState.IsValid)
                return View(changePasswordModel);
            var user = await _userManager.FindByEmailAsync(changePasswordModel.Email);
            if (user == null)
                View(changePasswordModel);
            var isCurrentPasswordValid = await _userManager.CheckPasswordAsync(user, changePasswordModel.PasswordCurrent);

            if (!isCurrentPasswordValid)
            {
                ModelState.AddModelError("PasswordCurrent", "Mật khẩu hiện tại không đúng.");
                return View(changePasswordModel);
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var resetPassResult = await _userManager.ResetPasswordAsync(user, token, changePasswordModel.Password);
            if (!resetPassResult.Succeeded)
            {
                foreach (var error in resetPassResult.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return View();
            }
            return RedirectToAction(nameof(ResetPasswordConfirmation));
        }
    }
}
