using System;
using System.Collections.Generic;
using MailMessage.Framework.Services.Interfaces;
using Utils;

namespace MailMessage.Framework.Services
{
	public class MailMessageCreator : IMailMessageCreator
	{
		private readonly ITransactionFactory _transactionFactory;
		private readonly IMailMessageStore _store;

		public MailMessageCreator(ITransactionFactory transactionFactory, IMailMessageStore store)
		{
			_transactionFactory = transactionFactory;
			_store = store;
		}

		public Result Create(IReadOnlyCollection<MailMessage> mailMessages)
		{
			using (ITransaction transaction = _transactionFactory.Create())
			{
				try
				{
					foreach (MailMessage mailMessage in mailMessages)
					{
						_store.Create(mailMessage, divisionEnvironment);
					}
				}
				catch (Exception ex)
				{
					return Result.Fail(ex);
				}

				transaction.Commit();
			}

			return Result.Ok();
		}
	}
}
