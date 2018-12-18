using MailMessage.Framework.Utils.Interfaces;
using Utils;

namespace MailMessage.Framework
{
	public class QueueMessageProcessor
	{
		private readonly IMessageProcessorConfigurationValidator _configurationValidator;
		private readonly IS3Client _s3Client;
		private readonly IS3ObjectParser _s3ObjectParser;
		private readonly IMail2EolMessageBuilder _mail2EolMessageBuilder;
		private readonly IEntityServiceProvider _entityServiceProvider;
		private readonly IActivityLogger _activityLogger;

		public QueueMessageProcessor(
			IMessageProcessorConfigurationValidator configurationValidator,
			IS3Client s3Client,
			IS3ObjectParser s3ObjectParser,
			IMail2EolMessageBuilder mail2EolMessageBuilder,
			IEntityServiceProvider entityServiceProvider,
			IActivityLogger activityLogger)
		{
			_configurationValidator = configurationValidator;
			_s3Client = s3Client;
			_s3ObjectParser = s3ObjectParser;
			_mail2EolMessageBuilder = mail2EolMessageBuilder;
			_entityServiceProvider = entityServiceProvider;
			_activityLogger = activityLogger;
		}

		public Result Process(QueueItem queueItem)
		{
			IEntityService entityService = _entityServiceProvider.Get(queueItem.MailMessageType.Value);

			return _configurationValidator.IsValid(queueItem.Division)
				.OnSuccess(() => _s3Client.GetObject(queueItem.MessageId))
				.OnSuccess(s3Object => _s3ObjectParser.Parse(s3Object))
				.OnSuccess(s3Message => _activityLogger.Info(ActivityType.GetMessageSize, $"Got {s3Message.ToString()}"))
				.OnSuccess(s3Message => _mail2EolMessageBuilder.Build(queueItem, s3Message))
				.OnSuccess(mail2EolMessage => entityService.Create(mail2EolMessage));
		}
	}
}
