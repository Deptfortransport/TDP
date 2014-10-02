// *********************************************** 
// NAME			: RetailXmlSchema.cs
// AUTHOR		: C.M. Owczarek
// DATE CREATED	: 27/11/03
// DESCRIPTION	: This class is responsible for loading and returning the XML schema used to
// validate XML documents generated for retailer handoff. 
// ************************************************

// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/RetailXmlHandoff/RetailXmlSchema.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:37:14   mturner
//Initial revision.
//
//   Rev 1.0   Nov 28 2003 15:49:06   COwczarek
//Initial revision.
//Resolution for 451: Retail Handoff does not need to read XML schema for each request

using System;
using System.Xml;
using System.Xml.Schema;
using System.IO;
using System.Xml.Serialization;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ServiceDiscovery;

using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.PricingRetail.RetailXmlHandoff
{
	/// <summary>
	/// This class is responsible for loading and returning the XML schema used to
	/// validate XML documents generated for retailer handoff. 
	/// </summary>
	public class RetailXmlSchema
	{
	
	    /// <summary>
	    /// The XML schema used to validate XML documents generated for retailer handoff
	    /// </summary>
	    private XmlSchema schema;
	    
        /// <summary>
        /// Constructor. Clients should not use this constructor to create instances.
        /// Instead the Current property should be used to obtain a singleton instance.
        /// </summary>
        internal RetailXmlSchema()
        {
            loadRetailXmlSchema();
        }
        
        #region Properties
        
        /// <summary>
        /// The XML schema used to validate XML documents generated for retailer handoff
        /// </summary>
        public XmlSchema Schema 
        {
            get {return schema;}
        }
        #endregion Properties        

        #region Public Methods            
        
        /// <summary>
        /// Returns an instance of RetailXmlSchema using the Service Discovery component.
        /// </summary>
        /// <return>An instance of RetailXmlSchema</return>
        public static RetailXmlSchema Current
        {
            get
            {
                return (RetailXmlSchema)TDServiceDiscovery.Current[ServiceDiscoveryKey.RetailXmlSchema];
            }
        }
        
        #endregion Public Methods            

        #region Private Methods                    
        
        /// <summary>
        /// Loads and stores the schema. Any IO errors or schema validation errors are
        /// logged and a TDException is thrown.
        /// </summary>
        private void loadRetailXmlSchema() 
        {
        
            IPropertyProvider pp = Properties.Current;

            string schemaFileName = pp["PricingRetail.RetailXmlDocument.SchemaFilename"];
            
            try
			{
				// Read in the schema
				FileStream schemaStream = new FileStream( schemaFileName, FileMode.Open, FileAccess.Read );
				XmlTextReader textReader = new XmlTextReader( schemaStream );
			    schema = XmlSchema.Read( textReader, null );
				textReader.Close();

                // Add the schema to an XmlSchemaCollection - this will validate it
                XmlSchemaSet xsc = new XmlSchemaSet();
                xsc.Add(schema);
			}
			catch( IOException e )
			{
			    string message = "Error occurred reading Retailer Xml schema";
				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, 
					    message,e));
			    schema = null;
				throw new TDException(message,e,true,
				        TDExceptionIdentifier.PRHErrorReadingRetailXmlSchema);
			}
            catch( XmlSchemaException e )
            {
                string message = "Error occurred validating the Retailer Xml schema";
                Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, 
                    message,e));
                schema = null;
                throw new TDException(message,e,true,
                    TDExceptionIdentifier.PRHErrorValidatingRetailXmlSchema);
            }
            catch( XmlException e )
            {
                string message = "Error occurred validating the Retailer Xml schema";
                Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, 
                    message,e));
                schema = null;
                throw new TDException(message,e,true,
                    TDExceptionIdentifier.PRHErrorValidatingRetailXmlSchema);
            }
        
        }
        
        #endregion Private Methods                    
        
    }
}
