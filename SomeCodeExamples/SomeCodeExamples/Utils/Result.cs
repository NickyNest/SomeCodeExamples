using System;

namespace Utils
{
	public class Result
	{
		public bool IsSuccess { get; }
		public string Error { get; }
		public Exception Exception { get; }
		public bool IsFail => !IsSuccess;
		public bool HasException => Exception != null;

		protected Result(bool isSuccess, string error)
		{
			if (isSuccess && !string.IsNullOrWhiteSpace(error))
			{
				throw new InvalidOperationException("Success result can't come with an error");
			}

			if (!isSuccess && string.IsNullOrWhiteSpace(error))
			{
				throw new InvalidOperationException("Not success result should have an error");
			}

			IsSuccess = isSuccess;
			Error = error;
		}

		protected Result(bool isSuccess, Exception exception) : this(isSuccess, exception.Message)
		{
			Exception = exception;
		}

		public static Result Ok()
		{
			return new Result(true, string.Empty);
		}

		public static Result<T> Ok<T>(T value)
		{
			return new Result<T>(value, true, string.Empty);
		}

		public static Result Fail(string message)
		{
			return new Result(false, message);
		}

		public static Result Fail(Exception exception)
		{
			return new Result(false, exception);
		}

		public static Result Fail(Result previousResult)
		{
			return previousResult.HasException
				? new Result(false, previousResult.Exception)
				: new Result(false, previousResult.Error);
		}

		public static Result<T> Fail<T>(string message)
		{
			return new Result<T>(default(T), false, message);
		}

		public static Result<T> Fail<T>(Exception exception)
		{
			return new Result<T>(default(T), false, exception);
		}

		public static Result<T> Fail<T>(Result previousResult)
		{
			return previousResult.HasException
				? Fail<T>(previousResult.Exception)
				: Fail<T>(previousResult.Error);
		}

		public static Result Combine(params Result[] results)
		{
			foreach (Result result in results)
			{
				if (result.IsFail)
				{
					return result;
				}
			}

			return Ok();
		}
	}

	public class Result<T> : Result
	{
		public T Value { get; }

		protected internal Result(T value, bool isSuccess, string error)
			: base(isSuccess, error)
		{
			Value = value;
		}

		protected internal Result(T value, bool isSuccess, Exception exception)
			: base(isSuccess, exception)
		{
			Value = value;
		}
	}
}
