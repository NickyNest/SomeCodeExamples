using System.IO;
using Utils;

namespace MailMessage.Core.ValueObjects
{
	public class FileName
	{
		public readonly string Value;
		public readonly string Extension;

		private FileName(string value)
		{
			Value = value;
			//Substring is needed because GetExtension returns extension within period e.g. ".pdf"
			Extension = Path.GetExtension(value).Substring(1);
		}

		public static Result<FileName> Create(string fileName)
		{
			if (string.IsNullOrWhiteSpace(fileName))
			{
				return Result.Fail<FileName>("File name is empty.");
			}

			if (fileName.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
			{
				return Result.Fail<FileName>($"File name {fileName} contains invalid characters.");
			}

			if (Path.GetExtension(fileName) == string.Empty)
			{
				return Result.Fail<FileName>($"File name {fileName} does not contain extension.");
			}

			return Result.Ok(new FileName(fileName));
		}

		public static FileName CreateInvalidAttachmentsReportFileName(ITerm term)
		{
			return new FileName(term.String(56230, "Attachments not downloaded") + ".txt");
		}
	}
}
