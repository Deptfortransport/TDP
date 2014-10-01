// *********************************************** 
// NAME                 : RTTIXmlValidator.cs
// AUTHOR               : Sanjeev Chand
// DATE CREATED         : 04/01/2005
// DESCRIPTION			: This class is responsible for validating the response against the specified schema.
//						  This class schema collection which keeps the schema in cache.		
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/DepartureBoardService/RTTIManager/RTTIXmlValidator.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:21:42   mturner
//Initial revision.
//
//   Rev 1.1   Mar 01 2005 18:55:52   schand
//Code Review Fix (IR-1928)
//
//   Rev 1.0   Feb 28 2005 16:23:08   passuied
//Initial revision.
//
//   Rev 1.4   Feb 02 2005 15:33:36   schand
//applied FxCop rules
//
//   Rev 1.3   Jan 21 2005 14:22:38   schand
//Code clean-up and comments has been added

using System;
using System.IO;
using System.Xml; 
using System.Xml.Schema ; 
using Logger = System.Diagnostics.Trace;
using TransportDirect.Common.Logging ; 
namespace TransportDirect.UserPortal.DepartureBoardService.RTTIManager
{
	/// <summary>
	/// Summary description for RTTIXmlValidator.
	/// This class is responsible for validating the response against the specified schema.
	/// This class schema collection which keeps the schema in cache.
	/// </summary>
	public class RTTIXmlValidator
	{
		#region "Private Variables"
		private string xmlFragment=string.Empty ;
		private string schemaLocation= string.Empty;  
		private string defaultNameSpace = string.Empty ;
		private bool xmlValid=true;
		private int xmlValidationErrorCount = 0;
		private string xmlValidationErrorMessage = string.Empty;
		private XmlSchema xmlschema = new XmlSchema();  
		private XmlSchemaSet xmlSchemaSet = new XmlSchemaSet();  
		NameTable nt = null;
		XmlNamespaceManager nsmgr = null;
		XmlParserContext context = null;
		#endregion

		#region "Public Members"

		/// <summary>
		/// Class contructor which takes two parameters.
		/// </summary>
		/// <param name="xmlSchemaLocation">The full path of RTTI schema file  </param>
		/// <param name="xmlNameSpace">The namespace used in the schema file</param>
		public RTTIXmlValidator(string xmlSchemaLocation, string xmlNameSpace)
		{
			try
			{
				// Set Schema location 
				SchemaLocation = xmlSchemaLocation;

				// Set Default Namespce
				DefaultNameSpace = xmlNameSpace;
				
				// Add schema to the schema collection
				if (! AddSchema())
				{	// if fails then throw exception
					throw new Exception("Unable to add schema: " + SchemaLocation );  
				}				
				// Build validator object
				if (! BuildXmlValidator())
				{
					throw new Exception("Unable to create xmlValidator object"); 
				}
			}
			catch(Exception ex)
			{	// if exception encountered, then throw it back
				throw ex;
			}
		}
		
		
		/// <summary>
		/// Public property to get and set the xml string to be validated
		/// </summary>
		public string XmlFragment
		{
			get{return xmlFragment;}
			set{xmlFragment= (string)value;}
		}

		
		/// <summary>
		/// Public property for getting/setting the default schema of the validator class
		/// </summary>
		public XmlSchema DefaultSchema
		{
			get{return (XmlSchema) xmlschema;}
			set{xmlschema = (XmlSchema)value;}
		}

		
		/// <summary>
		/// Public property for getting/setting the schema location
		/// </summary>
		public string SchemaLocation
		{
			get{return schemaLocation;}
			set{schemaLocation= (string)value;}
		}
			
		
		/// <summary>
		/// Public property for getting/setting the default namespace used in specified schema
		/// </summary>
		public string DefaultNameSpace
		{
			get{return defaultNameSpace;}
			set{defaultNameSpace= (string)value;}
		}

		
		/// <summary>
		/// Public method which validated the given xml string against the specified schema.
		/// </summary>
		/// <param name="xmlResponse">xml string to be validated </param>
		/// <returns></returns>
		public bool IsValid(string xmlResponse)
		{
			try
			{

				// Check xmlResponse
				if (xmlResponse== null || xmlResponse.Length  == 0)
				{
					return false;
				}
				else
				{
					XmlFragment = xmlResponse ;
				}
				
				// initialising error count and error message
				xmlValidationErrorCount =0;
				xmlValidationErrorMessage = string.Empty ;
				// Initialising xmlValid=true, if error encountered or validation fails then xmlValid would be false
				xmlValid = true;

                XmlReaderSettings settings = new XmlReaderSettings();
                settings.ValidationType = ValidationType.Schema;
                settings.ValidationEventHandler += new ValidationEventHandler(this.XmlValidationEventHandler);
                settings.Schemas.Add(xmlSchemaSet);
                settings.ConformanceLevel = ConformanceLevel.Fragment;
                               
				//creating an instance of xmlValidator 
				XmlReader xmlValidator = XmlReader.Create(new StringReader(XmlFragment),settings);
				
				//Chek and validate XML document
				while (xmlValidator.Read())
				{
					if (xmlValidator.NodeType == XmlNodeType.EntityReference) xmlValidator.ResolveEntity();
				}
				// check if specified xml string is valid and 	xmlValidationErrorCount = 0 			
				if (xmlValid && xmlValidationErrorCount == 0 )return true;
				else return false;
								
			}
			catch(XmlException xEx)
			{	
				// log the error 
				string errMsg = "Error occured in IsValid " + xEx.Message;
				if (xmlValidationErrorMessage.Length != 0) errMsg += "\t" + xmlValidationErrorMessage;				
				Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, errMsg));  										 
				return false;
			}

		}

		
		/// <summary>
		/// Public method to release the object from memory.
		/// This method is not called in the current scenario as it degrades the performace.
		/// </summary>
		public void Dispose()
		{
			try
			{
			}
			catch(NullReferenceException)
			{// do nothing
			}
		}


		#endregion
		

		#region "Private Members"
		/// <summary>
		/// Method to build nametable, xml parser context and namespace object and add it to XmlValidator object
		/// </summary>
		/// <returns>returns true if successfully managed to build nametable, xml parser context and namespace object. </returns>
		private bool BuildXmlValidator()
		{
			try
			{	// if schema location is empty then return false
				if ( SchemaLocation.Length ==0 ) return false;

				// if namespace is blank then return false
				if (DefaultNameSpace.Length ==0 ) return false;				
					
				//Create the XmlNamespaceManager that is used to
				//look up namespace information.
				nt = new NameTable();
				nsmgr = new XmlNamespaceManager(nt);				
				
				//Create the XmlParserContext.
				context = new XmlParserContext(null, nsmgr, null, XmlSpace.None);
		
				return true;
			}
			catch (XmlException xEx) 
			{	
				// log the error 
				string errMsg = "Error occured in BuildXMLValidator " + xEx.Message;  
				Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, errMsg));  				
				return false;
			}
		}

		
		/// <summary>
		/// This method adds schema and namespace to xmlSchema collection object
		/// </summary>
		/// <returns>Returns true if successfully managed to add schema and namespace to xmlSchema collection object</returns>
		private bool AddSchema()
		{	
			try
			{
				xmlSchemaSet.Add(DefaultNameSpace, SchemaLocation);
				return true;
			}
			catch(XmlException xEx)
			{	
				// log the error 
				string errMsg = "Error occured in AddSchema " + xEx.Message;  
				Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, errMsg));  							
				return false;
			}
		}


		/// <summary>
		/// Any anomalies found in the XML document will be raised through here.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="args">xml validation event arguement</param>
		/// <returns>nothing</returns>
		private void XmlValidationEventHandler (object sender, ValidationEventArgs args)
		{
			xmlValidationErrorCount +=1;
			xmlValidationErrorMessage +=  args.Exception.Message.ToString() + "\t";   
			xmlValid = false;
		}

		#endregion
	}
}
