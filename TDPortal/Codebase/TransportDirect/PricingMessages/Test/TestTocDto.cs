//********************************************************************************
//NAME         : TestTocDto
//AUTHOR       : SchlumbergerSema
//DATE CREATED : 10/05/2003
//DESCRIPTION  : NUnit test script for TocDto
//             : 
//DESIGN DOC   : DD034 Reatil Pricing
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingMessages/Test/TestTocDto.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:36:02   mturner
//Initial revision.
//
//   Rev 1.3   Feb 07 2005 15:17:26   RScott
//Assertion changed to Assert
//
//   Rev 1.2   Nov 24 2003 15:04:40   CHosegood
//Added XML doco
//
//   Rev 1.1   Oct 15 2003 11:34:42   CHosegood
//TOC is 2 not 3 characters long
//
//   Rev 1.0   Oct 13 2003 13:26:56   CHosegood
//Initial Revision

using NUnit.Framework;

using System;
using System.Diagnostics;

using TransportDirect.Common;

namespace TransportDirect.UserPortal.PricingMessages
{
    /// <summary>
    /// NUnit test script for TocDto.
    /// </summary>
    [TestFixture]
	public class TestTocDto
	{

        /// <summary>
        /// NUnit test script for TocDto
        /// </summary>
		public TestTocDto() { }

        /// <summary>
        /// NUnit initialisation
        /// </summary>
        [SetUp] public void SetUp() { }

        /// <summary>
        /// NUnit Tear down
        /// </summary>
        [TearDown] public void TearDown() { }

        /// <summary>
        /// Test that the TOC may be set correctly
        /// </summary>
        [Test]
        public void TestValidToc() 
        {
            string tocCode = "TO";
            TocDto toc = new TocDto( tocCode );
            Assert.AreEqual( tocCode.PadLeft(2,' ') , toc.Code, "Correctly set TOC" );

            tocCode = "T";
            toc = new TocDto( tocCode );
            Assert.AreEqual( tocCode.PadLeft(2,' ') , toc.Code, "Correctly set TOC" );

            tocCode = "";
            toc = new TocDto( tocCode );
            Assert.AreEqual( tocCode.PadLeft(2,' ') , toc.Code, "Correctly set TOC" );

            tocCode = string.Empty;
            toc = new TocDto( tocCode );
            Assert.AreEqual( tocCode.PadLeft(2,' ') , toc.Code, "Correctly set TOC" );

            tocCode = null;
            toc = new TocDto( tocCode );
            Assert.AreEqual( string.Empty.PadLeft(2,' ') , toc.Code, "Correctly set TOC" );
        }

        /// <summary>
        /// Test that the expected exception is thrown if an attempt to set
        /// the TOC to an invalid value is made.
        /// </summary>
        [Test]
        [ExpectedException( typeof( TDException ) )]
        public void TestInvalidToc() 
        {
            string tocCode = "TOCO";
            TocDto toc = new TocDto( tocCode );
            Assert.AreEqual( tocCode.PadLeft(3,' ') , toc.Code, "Correctly set TOC" );
        }
	}
}
