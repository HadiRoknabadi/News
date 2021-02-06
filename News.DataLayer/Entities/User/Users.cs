using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace News.DataLayer.Entities
{
    public class Users : IdentityUser
    {
        public Users()
        {

        }
        [Display(Name = "تاریخ ثبت نام")]
        public DateTime CreateDate { get; set; }
        #region Relations
        #endregion
    }
}
