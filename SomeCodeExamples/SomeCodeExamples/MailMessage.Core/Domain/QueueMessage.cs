using System;
using MailMessage.Core.Data.DTOs;
using MailMessage.Core.ValueObjects;

namespace MailMessage.Core.Domain
{
	public class QueueMessage : IEquatable<QueueMessage>
	{
		public Guid Id { get; }
		public string MessageId { get; }
		public EmailAddress Sender { get; }
		public GeneratedEmailAddress Recipient { get; }
		public MessageStatuses Status { get; private set; }
		public MailMessageType MailMessageType { get; }
		public DateTime? StartTime { get; private set; }
		public DateTime? EndTime { get; private set; }

		public QueueMessage(string messageId, EmailAddress sender, GeneratedEmailAddress recipient, MailMessageType mailMessageType)
		{
			Guard.NotNullOrEmpty(messageId, nameof(messageId));
			Guard.NotNull(sender, nameof(sender));
			Guard.NotNull(recipient, nameof(recipient));
			Guard.NotNull(mailMessageType, nameof(mailMessageType));

			MessageId = messageId;
			Sender = sender;
			Recipient = recipient;
			Status = MessageStatuses.Waiting;
			MailMessageType = mailMessageType;
		}

		public QueueMessage(QueueMessageDto queueMessageDto)
		{
			Id = queueMessageDto.Id;
			MessageId = queueMessageDto.MessageId;
			Sender = EmailAddress.CreateFromDto(queueMessageDto.Sender);
			Recipient = GeneratedEmailAddress.CreateFromDto(queueMessageDto.Recipient);
			Status = queueMessageDto.Status;
			MailMessageType = MailMessageType.CreateFromDto(queueMessageDto.MailMessageType);
			StartTime = queueMessageDto.StartTime;
			EndTime = queueMessageDto.EndTime;
		}

		public void SetStatusToBusy(DateTime dateTime)
		{
			StartTime = dateTime;
			Status = MessageStatuses.Busy;
		}

		public void SetStatusToFinished(DateTime dateTime)
		{
			EndTime = dateTime;
			Status = MessageStatuses.Finished;
		}

		public void SetStatusToFunctionalError(DateTime dateTime)
		{
			EndTime = dateTime;
			Status = MessageStatuses.ErrorFunctional;
		}

		public void SetStatusToTechnicalError(DateTime dateTime)
		{
			EndTime = dateTime;
			Status = MessageStatuses.ErrorTechnical;
		}

		#region IEquatable
		public bool Equals(QueueMessage other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return string.Equals(MessageId, other.MessageId);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != GetType()) return false;
			return Equals((QueueMessage) obj);
		}

		public override int GetHashCode()
		{
			return MessageId != null ? MessageId.GetHashCode() : 0;
		}
		#endregion
	}
}
