using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MailMessage.Core.Domain
{
	public class Mail2EolAttachments : IReadOnlyCollection<Mail2EolAttachment>
	{
		public int Count => _attachments.Count;

		public IReadOnlyCollection<Mail2EolAttachment> UblFiles =>
			_attachments.Where(attachment => attachment.IsUblFile()).ToList().AsReadOnly();

		public IReadOnlyCollection<InvalidAttachment> Invalid { get; }
		public bool HasInvalid => Invalid.Count > 0;

		public IReadOnlyCollection<string> InvalidAttachmentsInfo =>
			Invalid.Select(attachment => attachment.Report).ToList().AsReadOnly();

		public string ValidExtensiveReport =>
			string.Join(Environment.NewLine, this.Select(attachment => attachment.ToString()));

		private readonly IReadOnlyCollection<Mail2EolAttachment> _attachments;

		public Mail2EolAttachments(IReadOnlyCollection<Mail2EolAttachment> attachments,
			IReadOnlyCollection<InvalidAttachment> invalidAttachments)
		{
			_attachments = attachments;
			Invalid = invalidAttachments;
		}

		public IEnumerator<Mail2EolAttachment> GetEnumerator()
		{
			return _attachments.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
