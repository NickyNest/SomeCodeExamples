using System;
using MailMessage.Core.Data.DTOs;

namespace MailMessage.Core.Data.Interfaces
{
	public interface IMailboxStore
	{
		MailboxDto Get(Guid id);
	}
}