// *********************************************** 
// NAME             : StringExtendersTest.cs      
// AUTHOR           : Amit Patel
// DATE CREATED     : 20 Jun 2011
// DESCRIPTION  	: Unit tests for StringExtenders
// ************************************************
                
                
using TDP.Common.Extenders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Globalization;

namespace TDP.TestProject.Extenders
{
    
    
    /// <summary>
    ///This is a test class for StringExtendersTest and is intended
    ///to contain all StringExtendersTest Unit Tests
    ///</summary>
    [TestClass()]
    public class StringExtendersTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for IsValidEmailAddress
        ///</summary>
        [TestMethod()]
        public void IsValidEmailAddressTest()
        {
            string address = "abc@test.com";  
            bool expected = true; 
            bool actual;
            actual = StringExtenders.IsValidEmailAddress(address);
            Assert.AreEqual(expected, actual);

            // Invalid email address
            address = "abc@test"; 
            expected = false;
            actual = StringExtenders.IsValidEmailAddress(address);
            Assert.AreEqual(expected, actual);
           
        }

        /// <summary>
        ///A test for IsValidPostcode
        ///</summary>
        [TestMethod()]
        public void IsValidPostcodeTest()
        {
            string postcode = string.Empty;
            
            // Valid postcodes
            postcode = "SE9 1AB"; 
            Assert.AreEqual(true, StringExtenders.IsValidPostcode(postcode));

            postcode = "CW3 9SS";
            Assert.AreEqual(true, StringExtenders.IsValidPostcode(postcode));

            postcode = "SE5 0EG";
            Assert.AreEqual(true, StringExtenders.IsValidPostcode(postcode));
    
            postcode = "SE50EG";
            Assert.AreEqual(true, StringExtenders.IsValidPostcode(postcode));
    
            postcode = "se5 0eg";
            Assert.AreEqual(true, StringExtenders.IsValidPostcode(postcode));
            
            postcode = "WC2H 7LT";
            Assert.AreEqual(true, StringExtenders.IsValidPostcode(postcode));

            postcode = "W1D 3JW";
            Assert.AreEqual(true, StringExtenders.IsValidPostcode(postcode));

            postcode = "w1D 3JW";
            Assert.AreEqual(true, StringExtenders.IsValidPostcode(postcode));

            postcode = "GIR 0AA";
            Assert.AreEqual(true, StringExtenders.IsValidPostcode(postcode));

            // Invalid postcodes
            postcode = string.Empty;
            Assert.AreEqual(false, StringExtenders.IsValidPostcode(postcode));

            postcode = "SE9ABC";
            Assert.AreEqual(false, StringExtenders.IsValidPostcode(postcode));
            
            postcode = "aWC2H 7LT";
            Assert.AreEqual(false, StringExtenders.IsValidPostcode(postcode));
    
            postcode = "WC2H 7LTa";
            Assert.AreEqual(false, StringExtenders.IsValidPostcode(postcode));
            
            postcode = "WC2H";
            Assert.AreEqual(false, StringExtenders.IsValidPostcode(postcode));

            postcode = "SE9";
            Assert.AreEqual(false, StringExtenders.IsValidPostcode(postcode));
            
            postcode = "O2";
            Assert.AreEqual(false, StringExtenders.IsValidPostcode(postcode));

            postcode = "123 Street Name, CW3 9SS";
            Assert.AreEqual(false, StringExtenders.IsValidPostcode(postcode));
        }

        /// <summary>
        ///A test for IsNotSingleWord
        ///</summary>
        [TestMethod()]
        public void IsNotSingleWord()
        {
            string word = string.Empty;

            // Valid
            word = "word word";
            Assert.AreEqual(true, StringExtenders.IsNotSingleWord(word));

            word = "123 word";
            Assert.AreEqual(true, StringExtenders.IsNotSingleWord(word));

            word = "street SE9 1AB";
            Assert.AreEqual(true, StringExtenders.IsNotSingleWord(word));

            word = "123 street name, SE9 1AB";
            Assert.AreEqual(true, StringExtenders.IsNotSingleWord(word));

            // Invalid
            word = string.Empty;
            Assert.AreEqual(false, StringExtenders.IsNotSingleWord(word));

            word = "word";
            Assert.AreEqual(false, StringExtenders.IsNotSingleWord(word));

            word = "123word";
            Assert.AreEqual(false, StringExtenders.IsNotSingleWord(word));
        }

        /// <summary>
        ///A test for IsValidPartPostcodeTest
        ///</summary>
        [TestMethod()]
        public void IsValidPartPostcodeTest()
        {
            string postcode = string.Empty;

            // Valid part postcodes (out)
            postcode = "SE9";
            Assert.AreEqual(true, StringExtenders.IsValidPartPostcode(postcode));

            postcode = "S1";
            Assert.AreEqual(true, StringExtenders.IsValidPartPostcode(postcode));

            postcode = "EC1R";
            Assert.AreEqual(true, StringExtenders.IsValidPartPostcode(postcode));

            // Valid part postcodes (in)
            postcode = "1DJ";
            Assert.AreEqual(true, StringExtenders.IsValidPartPostcode(postcode));

            // Invalid part postcodes (out)
            postcode = string.Empty;
            Assert.AreEqual(false, StringExtenders.IsValidPartPostcode(postcode));

            postcode = "EC1R 3HN";
            Assert.AreEqual(false, StringExtenders.IsValidPartPostcode(postcode));

            postcode = "EC1R 3";
            Assert.AreEqual(false, StringExtenders.IsValidPartPostcode(postcode));

            postcode = "M123";
            Assert.AreEqual(false, StringExtenders.IsValidPartPostcode(postcode));

            postcode = "12DJ";
            Assert.AreEqual(false, StringExtenders.IsValidPartPostcode(postcode));

            postcode = "123DJ";
            Assert.AreEqual(false, StringExtenders.IsValidPartPostcode(postcode));

            postcode = "1DJA";
            Assert.AreEqual(false, StringExtenders.IsValidPartPostcode(postcode));

            postcode = "1";
            Assert.AreEqual(false, StringExtenders.IsValidPartPostcode(postcode));

            postcode = "12";
            Assert.AreEqual(false, StringExtenders.IsValidPartPostcode(postcode));

            postcode = "123";
            Assert.AreEqual(false, StringExtenders.IsValidPartPostcode(postcode));

            postcode = "WC2H 7LTa";
            Assert.AreEqual(false, StringExtenders.IsValidPartPostcode(postcode));
        }

        /// <summary>
        ///A test for IsContainsPostcode
        ///</summary>
        [TestMethod()]
        public void IsContainsPostcodeTest()
        {
            string postcode = string.Empty;

            // Valid postcodes
            postcode = "SE9 1AB";
            Assert.AreEqual(true, StringExtenders.IsContainsPostcode(postcode));

            postcode = "123 CW3 9SS";
            Assert.AreEqual(true, StringExtenders.IsContainsPostcode(postcode));

            postcode = "123 Street Name CW3 9SS";
            Assert.AreEqual(true, StringExtenders.IsContainsPostcode(postcode));

            postcode = "123 Street Name, CW3 9SS";
            Assert.AreEqual(true, StringExtenders.IsContainsPostcode(postcode));

            postcode = "123 Street Name, City CW3 9SS";
            Assert.AreEqual(true, StringExtenders.IsContainsPostcode(postcode));

            postcode = "123 Street Name, City, CW3 9SS";
            Assert.AreEqual(true, StringExtenders.IsContainsPostcode(postcode));

            postcode = "WC2H 7LT, Street Name";
            Assert.AreEqual(true, StringExtenders.IsContainsPostcode(postcode));

            // Invalid postcodes
            postcode = string.Empty;
            Assert.AreEqual(false, StringExtenders.IsContainsPostcode(postcode));

            postcode = "123 Street Name SE9ABC";
            Assert.AreEqual(false, StringExtenders.IsContainsPostcode(postcode));

            postcode = "123 Street Name aWC2H 7LT";
            Assert.AreEqual(false, StringExtenders.IsContainsPostcode(postcode));

            postcode = "123 Street Name WC2H 7LTa";
            Assert.AreEqual(false, StringExtenders.IsContainsPostcode(postcode));

            postcode = "123 Street Name WC2H";
            Assert.AreEqual(false, StringExtenders.IsContainsPostcode(postcode));

            postcode = "123 Street Name SE9";
            Assert.AreEqual(false, StringExtenders.IsContainsPostcode(postcode));

            postcode = "O2 Arena";
            Assert.AreEqual(false, StringExtenders.IsContainsPostcode(postcode));
        }

        /// <summary>
        ///A test for LowercaseFirst
        ///</summary>
        [TestMethod()]
        public void LowercaseFirstTest()
        {
            string text = "ABCDE"; 
            string expected = "aBCDE"; 
            string actual;
            actual = StringExtenders.LowercaseFirst(text);
            Assert.AreEqual(expected, actual);

            // empty string
            actual = StringExtenders.LowercaseFirst(string.Empty);
            Assert.AreEqual(string.Empty, actual);
        }

        /// <summary>
        ///A test for MatchesRegex
        ///</summary>
        [TestMethod()]
        public void MatchesRegexTest()
        {
            string text = "567"; 
            string regex = @"^\d{3}$"; 
            bool expected = true; 
            bool actual;
            actual = StringExtenders.MatchesRegex(text, regex);
            Assert.AreEqual(expected, actual);

            // regex not matched
            text = "5678"; 
            expected = false;
            actual = StringExtenders.MatchesRegex(text, regex);
            Assert.AreEqual(expected, actual);
            
            // no regex
            actual = StringExtenders.MatchesRegex(text, null);
            Assert.AreEqual(false, actual);

            // no text
            actual = StringExtenders.MatchesRegex(null, regex);
            Assert.AreEqual(false, actual);
        }

        /// <summary>
        ///A test for Parse
        ///</summary>
        [TestMethod()]
        public void ParseTest()
        {
            string value = "23"; 
            int defaultValue = 5;
            int expected = 23; 
            int actual;
            actual = StringExtenders.Parse<int>(value, defaultValue);
            Assert.AreEqual(expected, actual);

            // Default Value
            value = "ab";
            expected = 5;
            actual = StringExtenders.Parse<int>(value, defaultValue);
            Assert.AreEqual(expected, actual);

            // Default Value
            value = string.Empty;
            expected = 5;
            actual = StringExtenders.Parse<int>(value, defaultValue);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Parse where no default value specified
        ///</summary>
        [TestMethod()]
        public void ParseTest1()
        {
            string value = "23";
            int expected = 23;
            int actual;
            actual = StringExtenders.Parse<int>(value);
            Assert.AreEqual(expected, actual);

            // Default Value
            value = "ab";
            expected = 0;
            actual = StringExtenders.Parse<int>(value);
            Assert.AreEqual(expected, actual);

            // Default Value
            value = string.Empty;
            expected = 0;
            actual = StringExtenders.Parse<int>(value);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Parse where culture info specified
        ///</summary>
        [TestMethod()]
        public void ParseTest2()
        {
            CultureInfo cultureInfo = new CultureInfo("en-GB"); 
            string value = "23"; 
            int defaultValue = 5;
            int expected = 23; 
            int actual;
            actual = StringExtenders.Parse<int>(value, defaultValue, cultureInfo);
            Assert.AreEqual(expected, actual);

            // Default Value
            value = "ab";
            expected = 5;
            actual = StringExtenders.Parse<int>(value, defaultValue, cultureInfo);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for SubstringFirst
        ///</summary>
        [TestMethod()]
        public void SubstringFirst1()
        {
            string text = "ABCDE";
            string expected = "ABC";

            string actual;
            actual = StringExtenders.SubstringFirst(text, 3);
            Assert.AreEqual(expected, actual);

            // empty string
            actual = StringExtenders.SubstringFirst(string.Empty, 3);
            Assert.AreEqual(string.Empty, actual);

            // short string
            actual = StringExtenders.SubstringFirst(text, 100);
            Assert.AreEqual(text, actual);
        }

        /// <summary>
        ///A test for SubstringFirst
        ///</summary>
        [TestMethod()]
        public void SubstringFirst2()
        {
            string text = "ABCDE";
            string expected = "ABC";

            string actual;
            actual = StringExtenders.SubstringFirst(text, 'D');
            Assert.AreEqual(expected, actual);

            // empty string
            actual = StringExtenders.SubstringFirst(string.Empty, 'X');
            Assert.AreEqual(string.Empty, actual);

            // short string
            actual = StringExtenders.SubstringFirst(text, 'X');
            Assert.AreEqual(text, actual);
        }

        /// <summary>
        ///A test for AddressFormat
        ///</summary>
        [TestMethod()]
        public void AddressFormatTest()
        {
            string text = "123 London Road,London,AB1 2CD";
            string expected = "123 London Road, London, AB1 2CD";

            string actual;
            actual = StringExtenders.AddressFormat(text);
            Assert.AreEqual(expected, actual);

            // empty string
            actual = StringExtenders.AddressFormat(string.Empty);
            Assert.AreEqual(string.Empty, actual);
        }
    }
}
