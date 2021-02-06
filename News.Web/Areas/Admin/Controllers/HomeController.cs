using Microsoft.AspNetCore.Mvc;
using News.Core.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        [Route("Admin/Dashboard")]
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
