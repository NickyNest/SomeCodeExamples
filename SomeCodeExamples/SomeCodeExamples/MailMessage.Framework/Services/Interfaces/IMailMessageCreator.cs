using System.Collections.Generic;
using Utils;

namespace MailMessage.Framework.Services.Interfaces
{
	public interface IMailMessageCreator
	{
		Result Create(IReadOnlyCollection<MailMessage> mailMessages);
	}
}