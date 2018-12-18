using System;

namespace MailMessage.Core.Domain
{
	public static class Mail2EolAttachmentExtensions
	{
		public static bool IsUblFile(this Mail2EolAttachment attachment)
		{
			return attachment.Name.Extension.Equals(AllowedExtensions.XmlFileExtension, StringComparison.OrdinalIgnoreCase);
		}

		public static bool IsPdfFile(this Mail2EolAttachment attachment)
		{
			return attachment.Name.Extension.Equals(AllowedExtensions.PdfFileExtension, StringComparison.OrdinalIgnoreCase);
		}

		public static bool FormUblInvoice(this Mail2EolAttachments attachments)
		{
			return attachments.Count == 2 && attachments.UblFiles.Count == 1;
		}
	}
}
