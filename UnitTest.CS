using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace TextFileGenerator.Tests
{
    [TestClass]
    public class ProgramTests
    {
        [TestMethod]
        public void TestValidateInput_ValidInput_DoesNotThrowException()
        {
            string input = "Valid Input";
            int maxLength = 50;

            Program.ValidateInput(input, maxLength, "TestField");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestValidateInput_InvalidInput_ThrowsException()
        {
            string input = new string('a', 51); // Exceeds maxLength of 50
            int maxLength = 50;

            Program.ValidateInput(input, maxLength, "TestField");
        }

        [TestMethod]
        public void TestFormatField_LeftPadding_CorrectlyFormatted()
        {
            string input = "123";
            int maxLength = 10;
            char replacementChar = '0';
            string direction = "left";

            string result = Program.FormatField(input, maxLength, replacementChar, direction);

            Assert.AreEqual("0000000123", result);
        }

        [TestMethod]
        public void TestFormatField_RightPadding_CorrectlyFormatted()
        {
            string input = "123";
            int maxLength = 10;
            char replacementChar = ' ';
            string direction = "right";

            string result = Program.FormatField(input, maxLength, replacementChar, direction);

            Assert.AreEqual("123       ", result);
        }

        [TestMethod]
        public void TestFormatText_CorrectFormattingBasedOnXml()
        {
            string xmlContent = @"
                <Configuration>
                  <Header>
                    <Column Name='Name' MaxLength='10' ReplacementChar=' ' Direction='right' />
                    <Column Name='Email' MaxLength='20' ReplacementChar=' ' Direction='right' />
                    <Column Name='Age' MaxLength='5' ReplacementChar=' ' Direction='right' />
                    <Column Name='Address' MaxLength='30' ReplacementChar=' ' Direction='right' />
                    <Column Name='Observation' MaxLength='50' ReplacementChar=' ' Direction='right' />
                    <Column Name='Money' MaxLength='15' ReplacementChar=' ' Direction='right' />
                  </Header>
                  <Info>
                    <Field Name='Name' MaxLength='10' ReplacementChar=' ' Direction='right' />
                    <Field Name='Email' MaxLength='20' ReplacementChar='x' Direction='right' />
                    <Field Name='Age' MaxLength='5' ReplacementChar=' ' Direction='right' />
                    <Field Name='Address' MaxLength='30' ReplacementChar=' ' Direction='right' />
                    <Field Name='Observation' MaxLength='50' ReplacementChar=' ' Direction='right' />
                    <Field Name='Money' MaxLength='15' ReplacementChar='0' Direction='left' />
                  </Info>
                </Configuration>";
            XElement config = XElement.Parse(xmlContent);

            string name = "John";
            string email = "john@example.com";
            string age = "35";
            string address = "123 St, City";
            string obs = "This is an observation.";
            string money = "500";

            string result = Program.FormatText(config, name, email, age, address, obs, money);

            string expectedHeader = "Name      Email               Age  Address                       Observation                                        Money          \n";
            string expectedInfo = "John      john@example.comxxxxxx35   123 St, City                 This is an observation.                            000000000000500\n";
            string expected = expectedHeader + expectedInfo;
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestGenerateTextFile_CreatesCorrectFile()
        {
            string xmlContent = @"
                <Configuration>
                  <Header>
                    <Column Name='Name' MaxLength='10' ReplacementChar=' ' Direction='right' />
                    <Column Name='Email' MaxLength='20' ReplacementChar=' ' Direction='right' />
                    <Column Name='Age' MaxLength='5' ReplacementChar=' ' Direction='right' />
                    <Column Name='Address' MaxLength='30' ReplacementChar=' ' Direction='right' />
                    <Column Name='Observation' MaxLength='50' ReplacementChar=' ' Direction='right' />
                    <Column Name='Money' MaxLength='15' ReplacementChar=' ' Direction='right' />
                  </Header>
                  <Info>
                    <Field Name='Name' MaxLength='10' ReplacementChar=' ' Direction='right' />
                    <Field Name='Email' MaxLength='20' ReplacementChar='x' Direction='right' />
                    <Field Name='Age' MaxLength='5' ReplacementChar=' ' Direction='right' />
                    <Field Name='Address' MaxLength='30' ReplacementChar=' ' Direction='right' />
                    <Field Name='Observation' MaxLength='50' ReplacementChar=' ' Direction='right' />
                    <Field Name='Money' MaxLength='15' ReplacementChar='0' Direction='left' />
                  </Info>
                </Configuration>";
            XElement config = XElement.Parse(xmlContent);

            string name = "John";
            string email = "john@example.com";
            string age = "35";
            string address = "123 St, City";
            string obs = "This is an observation.";
            string money = "500";
            string filePath = "test_output.txt";

            Program.GenerateTextFile(config, name, email, age, address, obs, money, filePath);

            Assert.IsTrue(File.Exists(filePath));
            string fileContent = File.ReadAllText(filePath);

            string expectedHeader = "Name      Email               Age  Address                       Observation                                        Money          \n";
            string expectedInfo = "John      john@example.comxxxxxx35   123 St, City                 This is an observation.                            000000000000500\n";
            string expectedContent = expectedHeader + expectedInfo + expectedHeader + expectedInfo;
            Assert.AreEqual(expectedContent, fileContent);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}
