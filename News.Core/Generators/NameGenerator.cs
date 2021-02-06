using System;
using System.Collections.Generic;
using System.Text;

namespace News.Core.Generators
{
    public class NameGenerator
    {
        public static string GetUniqName()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }
    }
}
