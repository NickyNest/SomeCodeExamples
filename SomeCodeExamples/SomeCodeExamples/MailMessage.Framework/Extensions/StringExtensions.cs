using System;
using System.Collections.Generic;
using System.Linq;

namespace MailMessage.Framework.Extensions
{
	internal static class StringExtensions
	{
		internal static IEnumerable<string> SplitAndTrim(this string value, char separator)
		{
			return string.IsNullOrWhiteSpace(value)
				? Enumerable.Empty<string>()
				: value.Split(new[] { separator }, StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim());
		}
	}
}
