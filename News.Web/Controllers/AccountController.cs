using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using News.Core;
using News.Core.DTOs;
using News.Core.Services.Interfaces;
using News.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace News.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<Users> _userService;
        private readonly SignInManager<Users> _signInManager;
        private readonly IViewRenderService _render;
        public AccountController(UserManager<Users> userService, SignInManager<Users> signInManager, IViewRenderService render)
        {
            this._render = render;
            this._signInManager = signInManager;
            this._userService = userService;
        }

        #region Register
        [Route("/Register")]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [Route("/Register")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var User = new Users
                {
                    Email = model.Email,
                    UserName = model.UserName,
                    CreateDate = DateTime.Now,
                };
                var res = await _userService.CreateAsync(User, model.Password);
                if (res.Succeeded)
                {
                    var token = await _userService.GenerateEmailConfirmationTokenAsync(User);
                    var modelEmail = new EmailActive()
                    {
                        URL = Url.Action(nameof(ConfirmEmailActivate), "Account", new { username = User.UserName, token }, Request.Scheme)
                    };
                    string emailMessage = await _render.RenderToStringAsync("_sendEmail", modelEmail);
                    SendEmailME.Send(model.Email, "فعال سازی حساب کاربری", emailMessage);
                    return View();
                }
                else
                {
                    ViewBag.ErrRegister = res.Errors;
                    return View(model);
                }
            }
            return View(model);
        }
        #endregion

        #region Login
        [Route("/Login")]
        public IActionResult Login(string ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }

        [Route("/Login")]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel, string ReturnUrl = "/")
        {
            if (ModelState.IsValid)
            {
                var user = await _userService.FindByNameAsync(loginViewModel.UserName);
                var checkPassword = await _userService.CheckPasswordAsync(user, loginViewModel.Password);
                if (user != null && checkPassword)
                {
                    var res = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, loginViewModel.RememberMe, false);
                    if (res.Succeeded)
                    {
                        if (Url.IsLocalUrl(ReturnUrl))
                            return Redirect(ReturnUrl);
                        return Redirect("/");
                    }
                    else
                    {
                        ViewBag.SuccessLogin = false;
                        return View();
                    }
                }
                else
                {
                    ModelState.AddModelError("UserName", "کاربری با این مشخصات یافت نشد");
                }
            }
            ViewBag.SuccessLogin = false;
            return View(loginViewModel);
        }
        #endregion

        #region LogOut
        [Route("/LogOut")]
        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return Redirect("/");
        }
        #endregion

        #region Active Account
        [HttpGet]
        public async Task<ActionResult<bool>> ConfirmEmailActivate(string userName, string token)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(token))
            {
                return BadRequest();
            }
            var user = await _userService.FindByNameAsync(userName);
            if (user == null)
            {
                return BadRequest();
            }
            var result = await _userService.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, true);
                return Redirect("/");
            };
            return Redirect("/NotFound");
        }
        #endregion
    }
}
