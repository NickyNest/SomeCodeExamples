using System;
using System.Net.Mail;
using Utils;

namespace MailMessage.Core.ValueObjects
{
	public class EmailAddress
	{
		public readonly string Value;
		public readonly string Alias;
		public readonly string Domain;

		protected EmailAddress(string value, string alias, string domain)
		{
			Value = value;
			Alias = alias;
			Domain = domain;
		}

		public static Result<EmailAddress> Create(string emailAddress)
		{
			if (string.IsNullOrWhiteSpace(emailAddress))
			{
				return Result.Fail<EmailAddress>("Email address should not be empty");
			}

			MailAddress mailAddress;

			try
			{
				mailAddress = new MailAddress(emailAddress);
			}
			catch (FormatException)
			{
				return Result.Fail<EmailAddress>($"Email adress {emailAddress} is not in a recognized format");
			}

			return Result.Ok(new EmailAddress(mailAddress.Address, mailAddress.User, mailAddress.Host));
		}

		//In order not to perform any checks for data from the database
		internal static EmailAddress CreateFromDto(string emailAddress)
		{
			var mailAddress = new MailAddress(emailAddress);
			return new EmailAddress(mailAddress.Address, mailAddress.User, mailAddress.Host);
		}
	}
}
