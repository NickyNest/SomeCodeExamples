using MailMessage.Core.Domain;
using MailMessage.Core.ValueObjects;
using MailMessage.Framework.Builders.Interfaces;
using Utils;

namespace MailMessage.Framework.Builders
{
	public class QueueMessageBuilder : IQueueMessageBuilder
	{
		public Result<QueueMessage> Build(SqsMessageBody messageBody)
		{
			Result<EmailAddress> senderAddressResult = EmailAddress.Create(messageBody.Sender);
			Result<GeneratedEmailAddress> recipientAddressResult = GeneratedEmailAddress.Create(messageBody.Recipients.FirstOrDefault());
			Result<MailMessageType> mailMessageTypeResult = MailMessageType.Create(recipientAddressResult);
			
			return Result.Combine(senderAddressResult, recipientAddressResult, mailMessageTypeResult)
				.OnSuccess(() =>
					string.IsNullOrWhiteSpace(messageBody.Id) 
						? Result.Fail("Message Id should not be empty")
						: Result.Ok())
				.Then(result =>
					result.IsSuccess
						? Result.Ok(new QueueMessage(
							messageBody.Id, senderAddressResult.Value, recipientAddressResult.Value, mailMessageTypeResult.Value))
						: Result.Fail<QueueMessage>(result.Error));
		}
	}
}
