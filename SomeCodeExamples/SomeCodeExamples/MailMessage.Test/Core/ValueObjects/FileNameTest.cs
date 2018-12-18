using MailMessage.Core.ValueObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MailMessage.Core.Core.ValueObjects
{
	[TestClass]
	public class FileNameTest
	{
		private string _fileName;

		[TestMethod]
		public void Create_FileNameIsEmptyString_ReturnsFailedResult()
		{
			//Arrange
			_fileName = string.Empty;
			var expectedMessage = "File name is empty.";

			//Act
			var result = FileName.Create(_fileName);

			//Assert
			Assert.IsTrue(result.IsFail);
			Assert.AreEqual(expectedMessage, result.Error);
		}

		[TestMethod]
		public void Create_FileNameContainsInvalidChar_ReturnsFailedResult()
		{
			//Arrange
			_fileName = "Cust?mName";
			var expectedMessage = $"File name {_fileName} contains invalid characters.";

			//Act
			var result = FileName.Create(_fileName);

			//Assert
			Assert.IsTrue(result.IsFail);
			Assert.AreEqual(expectedMessage, result.Error);
		}

		[TestMethod]
		public void Create_FileNameDoesNotContainExtension_ReturnsFailedResult()
		{
			//Arrange
			_fileName = "CustomName";
			var expectedMessage = $"File name {_fileName} does not contain extension.";

			//Act
			var result = FileName.Create(_fileName);

			//Assert
			Assert.IsTrue(result.IsFail);
			Assert.AreEqual(expectedMessage, result.Error);
		}

		[TestMethod]
		public void Create_FileNameIsValid_ReturnsOkResult()
		{
			//Arrange
			_fileName = "Good Name.pdf";

			//Act
			var result = FileName.Create(_fileName);

			//Assert
			Assert.IsTrue(result.IsSuccess);
			Assert.AreEqual(_fileName, result.Value.Value);
			Assert.AreEqual("pdf", result.Value.Extension);
		}
	}
}
