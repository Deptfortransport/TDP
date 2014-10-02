//********************************************************************************
//NAME         : TestFilePropertyProvider.cs
//AUTHOR       : Phil Scott
//DATE CREATED : 02/07/2003
//DESCRIPTION  : Nunit Test class to test FilePropertyProvider 
//DESIGN DOC   : DD00601? Properties Service
//********************************************************************************
// Version   Ref        Name        Date         Description
// V1.0      DD00601    Phil Scott  02/07/2003   Initial version
//
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PropertiesService/FilePropertyProvider/TestFilePropertyProvider.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:37:44   mturner
//Initial revision.
//
//   Rev 1.5   Feb 07 2005 10:15:16   RScott
//Assertion changed to Assert
//
//   Rev 1.4   Apr 05 2004 11:50:58   rgreenwood
//Added SetAttributes to ensure that testproperties.xml is not read-only at start of test, and then set back at end of test.
//
//   Rev 1.3   Jul 23 2003 10:22:56   passuied
//Changes after PropertyService namespaces / dll renaming
//
//   Rev 1.2   Jul 17 2003 14:59:20   passuied
//updated after code review

using System;
using System.Configuration;
using NUnit.Framework;
using System.Xml;
using TransportDirect.Common.PropertyService.FilePropertyProvider;
using TransportDirect.Common.PropertyService.Properties;
using System.IO;

namespace TransportDirectNUnit
{
	/// <summary>
	/// NUnit test class for test of FilePropertProvider functionality.
	/// Uses test properties file testproperties.xml
	/// starts with global property and tests that newly created 
	/// properties at groupid level and application level are only
	/// implemented when version number is updated.
	/// </summary>
	/// 
	[TestFixture]
	public class TestFilePropertyProvider
	{
		string providerFile;
	
		private FilePropertyProvider fileProviderObject;
		public TestFilePropertyProvider()
		{
		}
		
		[SetUp]
		public void Init()
		{
			fileProviderObject = new FilePropertyProvider();
			providerFile = (string)ConfigurationManager.AppSettings["propertyservice.providers.fileprovider.filepath"];
			//make sure the xml file is not read-only
			File.SetAttributes(providerFile, File.GetAttributes(providerFile) & ~FileAttributes.ReadOnly);
		}

		[TearDown]
		public void CleanUp()
		{
			// reset the properties file
			XmlDocument xdLookup = new XmlDocument();
			XmlNode xnQuery;
			XmlNodeList xNodelist;
			xdLookup.Load(providerFile);
			xnQuery = xdLookup.DocumentElement.SelectSingleNode("//property[@name=\"propertyservice.version\"]");
			Assert.IsNotNull(xnQuery,"Xml version error");
			xnQuery.InnerText ="15";
			xNodelist = xdLookup.DocumentElement.SelectNodes("//property[@AID=\"TestAppID\"]");
			foreach (XmlNode node in xNodelist)
			{
				xdLookup.DocumentElement.RemoveChild(node);
			}
			xNodelist = xdLookup.DocumentElement.SelectNodes("//property[@GID=\"TestGroupID\"]");
			foreach (XmlNode node in xNodelist)
			{
				xdLookup.DocumentElement.RemoveChild(node);
			}
			xdLookup.Save(providerFile);
			// set xml file back to read-only
			File.SetAttributes(providerFile, File.GetAttributes(providerFile) & FileAttributes.ReadOnly);
		}


		[Test]
		public void TestLoad()
		{
            //*****************************************************************
			// Test 1:  test the load with initial version
			//*****************************************************************
			IPropertyProvider result = (FilePropertyProvider)fileProviderObject.Load();


			// check that you get all the properties
			//			Assertion.Assert("Properties returned is null", CurrProperties != null);
			Assert.IsNotNull(result,"No Properties returned");
			Assert.AreEqual((string)result["propertyservice.version"], "15", "Error retrieving property");
			Assert.AreEqual((string)result["propertyservice.refreshrate"], "30000", "Error retrieving property");
			Assert.AreEqual((string)result["propertyservice.property"], "globalproperty", "Error retrieving property");
			//*****************************************************************



			//*****************************************************************
			// Test 2:  test the load with changed properties but initial version
			//*****************************************************************
			// Add a group property of same name which should take precedent
			XmlDocument xdLookup = new XmlDocument();
			xdLookup.Load(providerFile);
			

			XmlElement xmlElement = xdLookup.CreateElement("property");

			XmlAttribute xmlAttribute = xdLookup.CreateAttribute("name");
			xmlAttribute.InnerText = "propertyservice.property";
			xmlElement.Attributes.Append(xmlAttribute);

			xmlAttribute = xdLookup.CreateAttribute("GID");
			xmlAttribute.InnerText = "TestGroupID";
			xmlElement.Attributes.Append(xmlAttribute);

			xmlAttribute = xdLookup.CreateAttribute("AID");
			xmlAttribute.InnerText = "";
			xmlElement.Attributes.Append(xmlAttribute);

			xmlElement.InnerText = "groupproperty";

			xdLookup.DocumentElement.AppendChild(xmlElement);
			xdLookup.Save(providerFile);

			// call the load function again
			result = fileProviderObject.Load();
			// check that no properties returned  ( as version not changed)
		
			Assert.IsNull(result,"Properties returned when version not updated");

			//*****************************************************************



			//*****************************************************************
			// Test 3:  test the load with changed properties and updated version
			//*****************************************************************
			// change ther version property		
			
			xdLookup.Load(providerFile);
			XmlNode xnQuery;
			xnQuery = xdLookup.DocumentElement.SelectSingleNode("//property[@name=\"propertyservice.version\"]");
			Assert.IsNotNull(xnQuery, "Xml version error");
			xnQuery.InnerText ="16";
			xdLookup.Save(providerFile);

			// call the load function again
			fileProviderObject = (FilePropertyProvider) fileProviderObject.Load();
			result = (IPropertyProvider) fileProviderObject;
			// check that properties returned are updated ( as version has changed)
		
			Assert.IsNotNull(result, "No Properties returned");
			Assert.AreEqual((string)result["propertyservice.version"], "16", "Error retrieving property");
			Assert.AreEqual((string)result["propertyservice.refreshrate"], "30000", "Error retrieving property");
			Assert.AreEqual((string)result["propertyservice.property"], "groupproperty", "Error retrieving property");
			//*****************************************************************

			//*****************************************************************



			//*****************************************************************
			// Test 4:  test the load with changed properties but initial version
			//*****************************************************************
			// Add an application property of same name which should take precedent
			xdLookup = new XmlDocument();
			xdLookup.Load(providerFile);
			

			xmlElement = xdLookup.CreateElement("property");

			xmlAttribute = xdLookup.CreateAttribute("name");
			xmlAttribute.Value = "propertyservice.property";
			xmlElement.Attributes.Append(xmlAttribute);

			xmlAttribute = xdLookup.CreateAttribute("GID");
			xmlAttribute.InnerText = "TestGroupID";
			xmlElement.Attributes.Append(xmlAttribute);

			xmlAttribute = xdLookup.CreateAttribute("AID");
			xmlAttribute.InnerText = "TestAppID";
			xmlElement.Attributes.Append(xmlAttribute);

			xmlElement.InnerText = "applicationproperty";

			xdLookup.DocumentElement.AppendChild(xmlElement);

			xdLookup.Save(providerFile);

			// call the load function again
			result = fileProviderObject.Load();
			// check no props returned as no version update	
			Assert.IsNull(result, "Properties returned when version not updated");

			//*****************************************************************



			//*****************************************************************
			// Test 5:  test the load with changed properties and updated version
			//*****************************************************************
			// change ther version property		
			// Add an application property of same name which should take precedent
			xdLookup.Load(providerFile);
			xnQuery = xdLookup.DocumentElement.SelectSingleNode("//property[@name=\"propertyservice.version\"]");
			Assert.IsNotNull(xnQuery, "Xml version error");
			xnQuery.InnerText ="17";
			xdLookup.Save(providerFile);

			// call the load function again
			fileProviderObject = (FilePropertyProvider)fileProviderObject.Load();
			result = (IPropertyProvider)fileProviderObject;
			// check that properties returned are updated ( as version has changed)
		
			Assert.IsNotNull(result, "No Properties returned");
			Assert.AreEqual((string)result["propertyservice.version"], "17", "Error retrieving property");
			Assert.AreEqual((string)result["propertyservice.refreshrate"], "30000", "Error retrieving property");
			Assert.AreEqual((string)result["propertyservice.property"], "applicationproperty", "Error retrieving property");
		}
	}
}
