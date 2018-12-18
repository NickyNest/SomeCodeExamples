using Utils;

namespace MailMessage.Framework.Utils.Interfaces
{
	public interface IS3ObjectParser
	{
		Result<S3Message> Parse(string s3Object);
	}
}