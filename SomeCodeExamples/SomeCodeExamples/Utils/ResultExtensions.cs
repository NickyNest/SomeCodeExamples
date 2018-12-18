using System;

namespace Utils
{
	public static class ResultExtensions
	{
		public static Result OnSuccess(this Result result, Func<Result> nextFunc)
		{
			return result.IsFail ? result : nextFunc();
		}

		public static Result<TNewValue> OnSuccess<TValue, TNewValue>(this Result<TValue> result, Func<TValue, Result<TNewValue>> nextFunc)
		{
			return result.IsFail ? Result.Fail<TNewValue>(result) : nextFunc(result.Value);
		}

		public static Result OnSuccess<T>(this Result<T> result, Func<T, Result> nextFunc)
		{
			return result.IsFail ? Result.Fail<T>(result) : nextFunc(result.Value);
		}

		public static Result<T> OnSuccess<T>(this Result result, Func<Result<T>> nextFunc)
		{
			return result.IsFail ? Result.Fail<T>(result) : nextFunc();
		}

		public static Result OnSuccess(this Result result, Action nextAction)
		{
			if (result.IsFail)
			{
				return result;
			}

			nextAction();

			return Result.Ok();
		}

		public static Result<T> OnSuccess<T>(this Result<T> result, Action<T> nextAction)
		{
			if (result.IsFail)
			{
				return result;
			}

			nextAction(result.Value);

			return result;
		}

		public static Result OnFailure<T>(this Result result, Func<Result> nextFunc)
		{
			return result.IsFail && result.HasException && result.Exception is T ? nextFunc() : result;
		}

		public static T Then<T>(this Result result, Func<Result, T> nextAction)
		{
			return nextAction(result);
		}
	}
}
