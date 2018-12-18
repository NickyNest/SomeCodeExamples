using MailMessage.Core.Domain;
using MailMessage.Core.Resources;
using MailMessage.Core.ValueObjects;
using MailMessage.Framework.Validators;
using MailMessage.Framework.Validators.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MailMessage.Core.Framework.Validators
{
	[TestClass]
	public class PdfFileValidatorTest
	{
		private TermStub _term;
		private IAttachmentValidator _validator;

		[TestInitialize]
		public void Initialize()
		{
			_term = new TermStub();
			_validator = new PdfFileValidator(_term);
		}

		[TestMethod]
		public void IsValid_PdfFileWithoutSignature_ReturnsFalseAndSetsCorrectMessages()
		{
			//Arrange
			Mail2EolAttachment attachment = Create(TestFiles.WithoutPdfSignature);
			string errorMessage = "Invalid PDF file";

			//Act
			var actual = _validator.IsValid(attachment);

			//Assert
			Assert.IsFalse(actual);
			Assert.AreEqual(errorMessage, _validator.ErrorMessageToLog);
			Assert.AreEqual(_term.String(67245, errorMessage), _validator.ErrorMessageToDisplay);
		}

		[TestMethod]
		public void IsValid_PdfFileWithMoreThan150Pages_ReturnsFalseAndSetsCorrectMessages()
		{
			//Arrange
			Mail2EolAttachment attachment = Create(TestFiles.WithMoreThan150Pages);
			string errorMessage = "The maximum number of pages allowed is 150 pages for PDF file.";

			//Act
			var actual = _validator.IsValid(attachment);

			//Assert
			Assert.IsFalse(actual);
			Assert.AreEqual(errorMessage, _validator.ErrorMessageToLog);
			Assert.AreEqual(_term.String(67241, errorMessage), _validator.ErrorMessageToDisplay);
		}

		[TestMethod]
		public void IsValid_ValidPdfFile_ReturnsTrueAndDoesNotSetMessages()
		{
			//Arrange
			Mail2EolAttachment attachment = Create(TestFiles.PurchaseInvoice);

			//Act
			var actual = _validator.IsValid(attachment);

			//Assert
			Assert.IsTrue(actual);
			Assert.IsTrue(string.IsNullOrEmpty(_validator.ErrorMessageToLog));
			Assert.IsTrue(string.IsNullOrEmpty(_validator.ErrorMessageToDisplay));
		}

		private Mail2EolAttachment Create(byte[] content)
		{
			return new Mail2EolAttachment(FileName.Create(Dummy.String).Value, Dummy.Int, content);
		}
	}
}
