using News.Core.DTOs;
using News.Core.Security;
using News.Core.Services.Interfaces;
using News.DataLayer.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace News.Core.Services
{
    public class UserService : IUserService
    {
        NewsContext _context;
        public UserService(NewsContext Context)
        {
            _context = Context;
        }

        
    }
}
