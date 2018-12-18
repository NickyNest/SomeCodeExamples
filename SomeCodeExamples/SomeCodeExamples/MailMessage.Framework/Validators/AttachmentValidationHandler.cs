using System;
using System.Collections.Generic;
using System.Linq;
using MailMessage.Core.Domain;
using MailMessage.Framework.Validators.Interfaces;

namespace MailMessage.Framework.Validators
{
	public class AttachmentValidationHandler : IAttachmentValidationHandler
	{
		private IAttachmentValidator _validationStrategy;

		public void SetValidationStrategy(IAttachmentValidator validator)
		{
			_validationStrategy = validator;
		}

		public AttachmentValidationResult Validate(IEnumerable<Mail2EolAttachment> attachments)
		{
			if (_validationStrategy == null)
			{
				throw new InvalidOperationException("Validation strategy is not set");
			}

			var attachmentsWithResults = attachments.Select(attachment =>
				new { Attachment = attachment, IsValid = _validationStrategy.IsValid(attachment) }).ToList();

			return new AttachmentValidationResult(
				valid: attachmentsWithResults.Where(a => a.IsValid)
					.Select(a => a.Attachment).ToList().AsReadOnly(),

				invalid: attachmentsWithResults.Where(a => !a.IsValid)
					.Select(a => new InvalidAttachment(
						a.Attachment.Name.Value, a.Attachment.Size, _validationStrategy.ErrorMessageToLog)).ToList().AsReadOnly());
		}
	}
}
