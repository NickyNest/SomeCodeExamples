using System;
using System.IO;
using System.Text;
using MailMessage.Framework.Utils.Interfaces;
using Utils;

namespace MailMessage.Framework.Utils
{
	public class S3ObjectParser : IS3ObjectParser
	{
		public Result<S3Message> Parse(string s3Object)
		{
			if (string.IsNullOrWhiteSpace(s3Object))
			{
				return Result.Fail<S3Message>("S3 object is empty");
			}

			byte[] s3ObjectInBytes = Encoding.UTF8.GetBytes(s3Object);

			try
			{
				S3Message s3Message;
				using (var memoryStream = new MemoryStream(s3ObjectInBytes))
				{
					MimeMessage mimeMessage = MimeMessage.Load(memoryStream);
					s3Message = new S3Message
					(
						subject: mimeMessage.Subject,
						body: mimeMessage.HtmlBody,
						size: s3ObjectInBytes.Length,
						attachments: mimeMessage.BodyParts.OfType<MimePart>()
							.Where(part => !string.IsNullOrEmpty(part.FileName))
							.Select(Create).ToList()
					);
				}

				return Result.Ok(s3Message);
			}
			catch (Exception ex)
			{
				return Result.Fail<S3Message>(ex);
			}
		}

		private S3MessageAttachment Create(MimePart mimePart)
		{
			byte[] content = GetContent(mimePart);
			return new S3MessageAttachment
			{
				Name = mimePart.ContentDisposition?.FileName ?? mimePart.ContentType.Name,
				Size = content?.Length ?? 0,
				Inline = !mimePart.IsAttachment,
				Content = content
			};
		}

		private byte[] GetContent(MimePart attachment)
		{
			using (var memory = new MemoryStream())
			{
				attachment.Content.DecodeTo(memory);
				return memory.ToArray();
			}
		}
	}
}
