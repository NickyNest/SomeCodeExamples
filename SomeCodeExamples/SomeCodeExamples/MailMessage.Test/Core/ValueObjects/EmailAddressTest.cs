using MailMessage.Core.ValueObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MailMessage.Core.Core.ValueObjects
{
	[TestClass]
	public class EmailAddressTest
	{
		private string _emailAddress;

		[TestMethod]
		public void Create_EmailAddressIsEmpty_ReturnsFailedResult()
		{
			//Arrange
			_emailAddress = string.Empty;
			var expectedMessage = "Email address should not be empty";

			//Act
			var result = EmailAddress.Create(_emailAddress);

			//Assert
			Assert.IsTrue(result.IsFail);
			Assert.AreEqual(expectedMessage, result.Error);
		}

		[TestMethod]
		public void Create_EmailAddressHasIncorrectFormat_ReturnsFailedResult()
		{
			//Arrange
			_emailAddress = "UsualString";
			var expectedMessage = $"Email adress {_emailAddress} is not in a recognized format";

			//Act
			var result = EmailAddress.Create(_emailAddress);

			//Assert
			Assert.IsTrue(result.IsFail);
			Assert.AreEqual(expectedMessage, result.Error);
		}

		[TestMethod]
		public void Create_EmailAddressHasCorrectFormat_ReturnsOkResult()
		{
			//Arrange
			_emailAddress = "example@test.com";

			//Act
			var result = EmailAddress.Create(_emailAddress);

			//Assert
			Assert.IsTrue(result.IsSuccess);
			Assert.AreEqual(_emailAddress, result.Value.Value);
			Assert.AreEqual("example", result.Value.Alias);
		}
	}
}
