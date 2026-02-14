using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace IronPdfMVC.Helpers
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Returns the <see cref="DisplayAttribute.Name"/> for the enum member if present;
        /// otherwise returns the enum member's <see cref="Enum.ToString"/>.
        /// </summary>
        public static string ToDisplayName(this Enum value)
        {
            if (value == null) return string.Empty;

            var type = value.GetType();
            var member = type.GetMember(value.ToString()).FirstOrDefault();
            if (member != null)
            {
                var display = member.GetCustomAttribute<DisplayAttribute>();
                var name = display?.GetName();
                if (!string.IsNullOrEmpty(name))
                    return name;
            }

            return value.ToString();
        }

        /// <summary>
        /// Creates a list of <see cref="SelectListItem"/> objects representing the values of the specified enumeration
        /// type, with an optional selected value.
        /// </summary>
        /// <remarks>The text for each item is determined by calling <c>ToDisplayName()</c> on each enum
        /// value. This method is commonly used to populate dropdown lists in ASP.NET MVC or Razor Pages
        /// views.</remarks>
        /// <param name="enumType">The enumeration <see cref="Type"/> to generate the select list from. Must represent an enum type.</param>
        /// <param name="selectedValue">The value to be marked as selected in the resulting list. If null, no item is selected.</param>
        /// <returns>A list of <see cref="SelectListItem"/> objects for each value in the enumeration. The item corresponding to
        /// <paramref name="selectedValue"/> is marked as selected if provided.</returns>
        /// <exception cref="ArgumentException">Thrown if <paramref name="enumType"/> does not represent an enumeration type.</exception>
        public static IList<SelectListItem> ToSelectList(this Type enumType, Enum selectedValue = null)
        {
            if (!enumType.IsEnum)
                throw new ArgumentException("Type must be an enum.");

            return Enum.GetValues(enumType)
                .Cast<Enum>()
                .Select(e => new SelectListItem
                {
                    Text = e.ToDisplayName(),
                    Value = e.ToString(),
                    Selected = selectedValue != null && e.Equals(selectedValue)
                })
                .ToList();
        }
    }
}