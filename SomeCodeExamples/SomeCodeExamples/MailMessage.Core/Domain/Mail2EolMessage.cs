using System;
using System.Collections.Generic;
using MailMessage.Core.ValueObjects;

namespace MailMessage.Core.Domain
{
	public class Mail2EolMessage
	{
		public int Division { get; }
		public EmailAddress Sender { get; }
		public GeneratedEmailAddress Recipient { get; }
		public MailMessageType MailMessageType { get; }
		public Guid Mailbox { get; }
		public string Subject { get; }
		public string Body { get; }
		public Guid MailboxAccount { get; }
		public Mail2EolAttachments Attachments { get; }
		public IReadOnlyCollection<string> OrderedEmails
		{
			get
			{
				var orderedEmails = new List<string> { Recipient.Value };

				orderedEmails.AddRange(_emailsFromBody.Value);
				orderedEmails.Add(Sender.Value);

				return orderedEmails.AsReadOnly();
			}
		}

		private readonly Lazy<IReadOnlyCollection<string>> _emailsFromBody;

		public Mail2EolMessage(QueueItem queueItem, string subject, string body, Guid mailboxAccount,
			Mail2EolAttachments attachments, Func<string, bool, IReadOnlyCollection<string>> bodyParser)
		{
			Division = queueItem.Division;
			Sender = queueItem.Sender;
			Recipient = queueItem.Recipient;
			MailMessageType = queueItem.MailMessageType;
			Mailbox = queueItem.Mailbox;
			Subject = subject;
			Body = body;
			MailboxAccount = mailboxAccount;
			Attachments = attachments;
			_emailsFromBody = new Lazy<IReadOnlyCollection<string>>(() => bodyParser.Invoke(Body, true));
		}
	}
}
