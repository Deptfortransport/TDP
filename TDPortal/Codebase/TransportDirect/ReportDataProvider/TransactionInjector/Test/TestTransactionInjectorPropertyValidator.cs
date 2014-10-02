// *********************************************** 
// NAME			: TestTransactionInjectorPropertyValidator.cs
// AUTHOR		: Jatinder S. Toor
// DATE CREATED	: 03/09/2003 
// DESCRIPTION	: Tests validation of properties.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/TransactionInjector/TestTransactionInjectorPropertyValidator.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:40:10   mturner
//Initial revision.
//
//   Rev 1.11   Feb 10 2006 09:18:00   kjosling
//Turned off failed unit test
//
//   Rev 1.10   May 24 2005 15:09:02   NMoorhouse
//Post Del7 NUnit Updates
//
//   Rev 1.9   Feb 08 2005 13:16:44   RScott
//Assertion changed to Assert
//
//   Rev 1.8   Jun 21 2004 15:25:10   passuied
//Changes for del6-del5.4.1
//
//   Rev 1.7   Nov 13 2003 12:31:42   geaton
//Updated to use new mock properties.

using System;
using System.IO;
using System.Collections;
using System.Diagnostics;

using NUnit.Framework;

namespace TransportDirect.ReportDataProvider.TransactionInjector
{
	/// <summary>
	/// Tests the TransactionInjectorPropertyValidator class.
	/// </summary>
	[TestFixture]
	public class TestTransactionInjectorPropertyValidator
	{
		[SetUp]
		public void Init()
		{

		}

		[TearDown]
		public void CleanUp()
		{

		} 

		/// <summary>
		/// Tests the validation of valid properties.
		/// </summary>
		[Test]
		[Ignore("ProjectNewkirk")]
		public void TestGoodProperties()
		{			
			MockGoodProperties properties  = new MockGoodProperties("transactioninjector1");			
			TransactionInjectorPropertyValidator validator = new TransactionInjectorPropertyValidator( properties, "transactioninjector1" );	
			ArrayList errors = new ArrayList();

			validator.ValidateProperty(Keys.TransactionInjectorTransaction, errors);
			validator.ValidateProperty(Keys.TransactionInjectorFrequency, errors);
			validator.ValidateProperty(Keys.TransactionInjectorWebService, errors);
			validator.ValidateProperty(Keys.TransactionInjectorTemplateFileDirectory, errors);

			Assert.IsTrue(errors.Count == 0);
		}

		[Test]
		public void TestBadProperties()
		{
			MockBadProperties properties  = new MockBadProperties("transactioninjector1");			
			TransactionInjectorPropertyValidator validator = new TransactionInjectorPropertyValidator( properties, "transactioninjector1" );					
			ArrayList errors = new ArrayList();

			validator.ValidateProperty(Keys.TransactionInjectorTransaction, errors);
			validator.ValidateProperty(Keys.TransactionInjectorFrequency, errors);
			validator.ValidateProperty(Keys.TransactionInjectorWebService, errors);
			validator.ValidateProperty(Keys.TransactionInjectorTemplateFileDirectory, errors);

			Assert.IsTrue(errors.Count == 8);
		}

	}
}
