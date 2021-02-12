using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Sprout.Common
{
    public static class EnumExtensions
    {
        public static IEnumerable<SelectListItem> ToSelectList(this Type source)
        {
            if (!source.IsEnum) return Enumerable.Empty<SelectListItem>();

            return Enum.GetValues(source).Cast<Enum>()
                .Select(e => new SelectListItem(e.ToString(), Convert.ChangeType(e, e.GetTypeCode()).ToString()));
        }
    }
}
