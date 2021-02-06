using Microsoft.AspNetCore.Mvc;
using News.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Route("Error/{StatusCode}")]
        public IActionResult Error(int StatusCode)
        {
            ErrorViewModel errorViewModel = new ErrorViewModel();
            String Message = string.Empty;
            switch (StatusCode)
            {
                case 404:
                    {
                        Message = "صفحه موردنظر شما یافت نشد";
                        errorViewModel.ErrorCode = 404;
                        errorViewModel.ErrorMessage = Message;
                        break;
                    }
                case 401:
                    {
                        Message = "شما به این صفحه دسترسی ندارید";
                        errorViewModel.ErrorCode = 401;
                        errorViewModel.ErrorMessage = Message;
                        break;
                    }
                case 500:
                    {
                        Message = "خطای سرور داخلی";
                        errorViewModel.ErrorCode = 500;
                        errorViewModel.ErrorMessage = Message;
                        break;
                    }
                case 504:
                    {
                        Message = "سایت از دسترس خارج شده است";
                        errorViewModel.ErrorCode = 504;
                        errorViewModel.ErrorMessage = Message;
                        break;
                    }
                case 503:
                    {
                        Message = "سرویس در دسترس نیست";
                        errorViewModel.ErrorCode = 503;
                        errorViewModel.ErrorMessage = Message;
                        break;
                    }
                default:
                    {
                        Message = "خطایی رخ داد :(";
                        errorViewModel.ErrorMessage = Message;
                        break;
                    }
            }
            return View("Error", errorViewModel);
        }
    }
}
