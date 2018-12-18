using MailMessage.Core.Domain;
using Utils;

namespace MailMessage.Framework.Builders.Interfaces
{
	public interface IQueueMessageBuilder
	{
		Result<QueueMessage> Build(SqsMessageBody messageBody);
	}
}