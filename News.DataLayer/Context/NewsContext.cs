using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using News.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace News.DataLayer.Context
{
    public class NewsContext : IdentityDbContext<Users>
    {
        public NewsContext(DbContextOptions<NewsContext> options) : base(options)
        {
        }

        #region Users
        public DbSet<Users> Users { get; set; }
        #endregion
    }
}
