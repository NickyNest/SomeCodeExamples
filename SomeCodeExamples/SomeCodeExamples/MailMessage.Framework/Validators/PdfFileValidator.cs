using System.Text.RegularExpressions;
using MailMessage.Core.Domain;
using MailMessage.Framework.Validators.Interfaces;

namespace MailMessage.Framework.Validators
{
	public class PdfFileValidator : IAttachmentValidator
	{
		public string ErrorMessageToLog { get; private set; }
		public string ErrorMessageToDisplay { get; private set; }

		private readonly ITerm _term;
		public PdfFileValidator(ITerm term)
		{
			_term = term;
		}

		public bool IsValid(Mail2EolAttachment attachment)
		{
			string fileContent = attachment.Utf8Content;

			if (!HasPdfFileSignature(fileContent))
			{
				ErrorMessageToLog = "Invalid PDF file";
				ErrorMessageToDisplay = _term.String(67245, ErrorMessageToLog);

				return false;
			}

			if (!IsAllowedTotalPagesCount(fileContent))
			{
				ErrorMessageToLog = "The maximum number of pages allowed is 150 pages for PDF file.";
				ErrorMessageToDisplay = _term.String(67241, ErrorMessageToLog);

				return false;
			}

			return true;
		}

		private bool HasPdfFileSignature(string fileContent)
		{
			return fileContent.Substring(0, 5) == Core.Constants.PdfFileSignature;
		}

		private bool IsAllowedTotalPagesCount(string fileContent)
		{
			Regex regex = new Regex(@"/Type\s*/Page[^s]");
			MatchCollection matches = regex.Matches(fileContent);

			return matches.Count <= Core.Constants.MaxPdfPagesCount;
		}
	}
}
