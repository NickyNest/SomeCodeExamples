using System;
using System.Collections.Generic;
using System.Text;
using MailMessage.Core.ValueObjects;

namespace MailMessage.Core.Domain
{
	public class Mail2EolAttachment : IEqualityComparer<Mail2EolAttachment>
	{
		public FileName Name { get; }
		public long Size { get; }
		public byte[] Content { get; }
		public string Utf8Content => Content == null ? string.Empty : Encoding.UTF8.GetString(Content);

		public Mail2EolAttachment(FileName name, long size, byte[] content)
		{
			Name = name;
			Size = size;
			Content = content;
		}

		public bool Equals(Mail2EolAttachment x, Mail2EolAttachment y)
		{
			if (ReferenceEquals(x, y))
			{
				return true;
			}

			if (ReferenceEquals(x, null) || ReferenceEquals(y, null))
			{
				return false;
			}

			return x.Name.Value.Equals(y.Name.Value, StringComparison.OrdinalIgnoreCase) && x.Size == y.Size;
		}

		public int GetHashCode(Mail2EolAttachment obj)
		{
			return new { obj.Name.Value, obj.Size }.GetHashCode();
		}

		public override string ToString()
		{
			return $"{Name.Value} ({FileSizeConverter.ConvertToString(Size)})";
		}
	}
}
