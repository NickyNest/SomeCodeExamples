using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MailMessage.Core.Framework.Extensions
{
	[TestClass]
	public class StringExtensionsTest
	{
		private const char Separator = ',';

		[TestMethod]
		public void SplitAndTrim_ValueIsNull_ReturnsEmptyEnumerable()
		{
			//Arrange
			string value = null;

			//Act
			IEnumerable<string> actual = value.SplitAndTrim(Separator);

			//Assert
			Assert.AreEqual(Enumerable.Empty<string>(), actual);
		}

		[TestMethod]
		public void SplitAndTrim_2ValuesSeparatedByComma_ReturnsCollectionWith2Values()
		{
			//Arrange
			string value = "value1,value2";
			IEnumerable<string> expected = new List<string> { "value1", "value2" };

			//Act
			IEnumerable<string> actual = value.SplitAndTrim(Separator);

			//Assert
			AssertionMethods.AssertCollections(expected, actual);
		}

		[TestMethod]
		public void SplitAndTrim_2ValuesSeparatedBySpace_ReturnsCollectionWith1Value()
		{
			//Arrange
			string value = "value1 value2";
			IEnumerable<string> expected = new List<string> { "value1 value2" };

			//Act
			IEnumerable<string> actual = value.SplitAndTrim(Separator);

			//Assert
			AssertionMethods.AssertCollections(expected, actual);
		}

		[TestMethod]
		public void SplitAndTrim_3ValuesSeparatedByCommaAndSpace_ReturnsCollectionWith3Values()
		{
			//Arrange
			string value = "value1, value2, value3";
			IEnumerable<string> expected = new List<string> { "value1", "value2", "value3" };

			//Act
			IEnumerable<string> actual = value.SplitAndTrim(Separator);

			//Assert
			AssertionMethods.AssertCollections(expected, actual);
		}

		[TestMethod]
		public void SplitAndTrim_ValueSurroundWithSpaces_ReturnsCollectionWithTrimmedValue()
		{
			//Arrange
			string value = " value1 ";
			IEnumerable<string> expected = new List<string> { "value1" };

			//Act
			IEnumerable<string> actual = value.SplitAndTrim(Separator);

			//Assert
			AssertionMethods.AssertCollections(expected, actual);
		}
	}
}
