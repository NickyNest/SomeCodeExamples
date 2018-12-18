using MailMessage.Core.Domain;

namespace MailMessage.Framework.Validators.Interfaces
{
	public interface IAttachmentValidator
	{
		bool IsValid(Mail2EolAttachment attachment);
		string ErrorMessageToLog { get; }
		string ErrorMessageToDisplay { get; }
	}
}
