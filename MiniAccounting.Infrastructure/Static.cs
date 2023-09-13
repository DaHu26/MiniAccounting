using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniAccounting.Infrastructure
{
    public class Static
    {
        public static readonly Encoding Encoding = Encoding.UTF8;

        public static string ToStringPropsWithReflection(object obj)
        {
            var list = new List<string>();
            foreach (var prop in obj.GetType().GetProperties())
            {
                list.Add($"{prop.Name}={prop.GetValue(obj)}");
            }
            return string.Join("; ", list);
        }
    }
}
