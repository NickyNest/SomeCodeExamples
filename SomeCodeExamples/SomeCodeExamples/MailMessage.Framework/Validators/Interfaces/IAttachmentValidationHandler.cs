using System.Collections.Generic;
using MailMessage.Core.Domain;

namespace MailMessage.Framework.Validators.Interfaces
{
	public interface IAttachmentValidationHandler
	{
		void SetValidationStrategy(IAttachmentValidator validator);
		AttachmentValidationResult Validate(IEnumerable<Mail2EolAttachment> attachments);
	}
}