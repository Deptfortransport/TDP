// *********************************************** 
// NAME             : RetailHandoffSchema.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 08 Apr 2011
// DESCRIPTION  	: RetailHandoffSchema class responsible for loading and returning the XML schema used to
// validate XML documents generated for retailer handoff. 
// ************************************************
// 

using System.IO;
using System.Xml;
using System.Xml.Schema;
using TDP.Common;
using TDP.Common.EventLogging;
using TDP.Common.PropertyManager;
using Logger = System.Diagnostics.Trace;

namespace TDP.UserPortal.Retail
{
    /// <summary>
    /// RetailHandoffSchema class responsible for loading and returning the XML schema used to
    /// validate XML documents generated for retailer handoff. 
    /// </summary>
    public class RetailHandoffSchema
    {
        #region Private members

        /// <summary>
	    /// The XML schema used to validate XML documents generated for retailer handoff
	    /// </summary>
	    private XmlSchema schema;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor. Clients should not use this constructor to create instances.
        /// Instead the Current property should be used to obtain a singleton instance.
        /// </summary>
        public RetailHandoffSchema()
        {
            LoadRetailHandoffSchema();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// The XML schema used to validate XML documents generated for retailer handoff
        /// </summary>
        public XmlSchema Schema 
        {
            get {return schema;}
        }

        #endregion

        #region Private Methods                    
        
        /// <summary>
        /// Loads and stores the schema. Any IO errors or schema validation errors are
        /// logged and a exception is thrown.
        /// </summary>
        private void LoadRetailHandoffSchema() 
        {
        
            IPropertyProvider pp = Properties.Current;

            string schemaFileName = pp["Retail.RetailHandoffXml.Schema.Path"];
            
            try
			{
				// Read in the schema
                using (FileStream schemaStream = new FileStream(schemaFileName, FileMode.Open, FileAccess.Read))
                {
                    XmlTextReader textReader = new XmlTextReader(schemaStream);
                    schema = XmlSchema.Read(textReader, null);
                    
                    // Add the schema to an XmlSchemaCollection - this will validate it
                    XmlSchemaSet xsc = new XmlSchemaSet();
                    xsc.Add(schema);

                }
			}
			catch( IOException e )
			{
			    string message = "Error occurred reading Retailer Xml schema";
                Logger.Write(new OperationalEvent(TDPEventCategory.Business, TDPTraceLevel.Error, message, e));
			    
                schema = null;

                throw new TDPException(message, e, true, TDPExceptionIdentifier.REErrorReadingRetailXmlSchema);
			}
            catch( XmlSchemaException e )
            {
                string message = "Error occurred validating the Retailer Xml schema";
                Logger.Write(new OperationalEvent(TDPEventCategory.Business, TDPTraceLevel.Error, message, e));
                
                schema = null;

                throw new TDPException(message, e, true, TDPExceptionIdentifier.REErrorValidatingRetailXmlSchema);
            }
            catch( XmlException e )
            {
                string message = "Error occurred validating the Retailer Xml schema";
                Logger.Write(new OperationalEvent(TDPEventCategory.Business, TDPTraceLevel.Error, message, e));
                
                schema = null;

                throw new TDPException(message, e, true, TDPExceptionIdentifier.REErrorValidatingRetailXmlSchema);
            }
        }
        
        #endregion
    }
}
