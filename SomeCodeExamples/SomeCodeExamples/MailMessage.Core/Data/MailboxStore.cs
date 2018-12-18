using System;
using MailMessage.Core.Data.DTOs;
using MailMessage.Core.Data.Interfaces;

namespace MailMessage.Core.Data
{
	public class MailboxStore : IMailboxStore
	{
		public MailboxDto Get(Guid id)
		{
			return ResourceQueryBuilder
				.CreateFromSql(Connection, Sql.GetMailbox)
				.QueryRow<MailboxDto>(new IdParameter(id));
		}
	}
}
