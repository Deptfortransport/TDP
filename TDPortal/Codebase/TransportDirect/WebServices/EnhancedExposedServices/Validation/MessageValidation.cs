// *********************************************** 
// NAME                 : MessageValidation.cs
// AUTHOR               : Marcus Tillett
// DATE CREATED         : 20/12/2005
// DESCRIPTION			: Provides validation of SOAP request message against WSDL document
//
//  Orignal code samples obtained from MSDN and used under EULA
//	http://msdn.microsoft.com/msdnmag/issues/03/07/xmlschemavalidation/default.aspx
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/WebServices/EnhancedExposedServices/Validation/MessageValidation.cs-arc  $
//
//   Rev 1.2   Dec 12 2007 15:42:02   jfrank
//Updated to use WSE 3.0
//
//   Rev 1.1   Dec 07 2007 11:17:34   mmodi
//Corrected loading Schema's problem following .NET 2.0 changes
//
//   Rev 1.0   Nov 08 2007 13:52:30   mturner
//Initial revision.
//
//   Rev 1.5   Feb 22 2006 15:46:24   RWilby
//Removed constant variable 'DN' holding a url to the DevelopMentor web site, and replace with constant 'TD' holding the TransportDirect url.
//Resolution for 3129: Enhanced  Exposed Services Framework Enhancements
//
//   Rev 1.4   Jan 12 2006 18:12:58   mtillett
//Update MessageValidation to ensure that the SOAP response is validated aginast the WSDL in schema folder
//Resolution for 3129: Enhanced  Exposed Services Framework Enhancements
//
//   Rev 1.3   Jan 11 2006 09:17:14   mtillett
//Move soap exception helper to SoapFault/V1 namespace and fix up references to methods and unit tests
//Resolution for 3129: Enhanced  Exposed Services Framework Enhancements
//
//   Rev 1.2   Jan 05 2006 14:54:46   mtillett
//Update throw of exceptions to use the healper classes
//Resolution for 3129: Enhanced  Exposed Services Framework Enhancements
//
//   Rev 1.1   Jan 04 2006 12:37:50   mtillett
//Move Helper classes into the TransportDirect.EnhancedExposedServices.Helpers namespace
//Resolution for 3129: Enhanced  Exposed Services Framework Enhancements
//
//   Rev 1.0   Jan 04 2006 09:34:54   mtillett
//Initial revision.

using System;
using System.Globalization;
using System.IO;
using System.Resources;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Xml.XPath;
using System.Web;
using System.Web.Services.Protocols;
using System.Web.Services.Description;
using System.Web.Services.Configuration;
using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.EnhancedExposedServices.Helpers;
using TransportDirect.EnhancedExposedServices.SoapFault.V1;
using Logger = System.Diagnostics.Trace;

namespace TransportDirect.EnhancedExposedServices.Validation
{
	/// <summary>
	/// XML namespaces used in assembly
	/// </summary>
	public class Namespaces
	{
		public const string TD = "http://www.transportdirect.info/";
		public const string XMLNS = "http://www.w3.org/2000/xmlns/";
	}	

	/// <summary>
	/// intializer object generated by GetInitializer, stored by the 
	/// infrastructure, and provided to each call to Initialize
	/// holds state used to perform schema/assertion validation
	/// </summary>
	internal class ValidationContext
	{
		internal XmlNamespaceManager NamespaceManager;
		internal AssertAttribute[] AssertAttributes;
		internal XPathExpression CompleteRuleExpression;
		internal ValidationAttribute ValidationAttribute;
		internal XmlSchemaSet SchemaSet;
	}

	/// <summary>
	/// ValidationExtension implements both schema validation and assertion processing
	/// </summary>
	public class ValidationExtension :  SoapExtension
	{
		// initializer object (context for each request)
		// generated by GetInitializer
		ValidationContext _context;

		/// <summary>
		/// helpers for loading schema files from different locations
		/// </summary>
		/// <param name="sc"></param>
		/// <param name="filePath"></param>
		internal void LoadSchemaFromFile(XmlSchemaSet sc, string filePath)
		{
			XmlTextReader schemaReader = new XmlTextReader(filePath);
			XmlSchema schema = XmlSchema.Read(schemaReader, null);
			sc.Add(schema);
		}

		/// <summary>
		/// helpers for loading schema files from different locations
		/// </summary>
		/// <param name="sc"></param>
		/// <param name="sd"></param>
		internal void LoadSchemasFromServiceDescriptions(XmlSchemaSet sc, ServiceDescription sd)
		{
			foreach(XmlSchema embeddedXsd in sd.Types.Schemas)
				sc.Add(embeddedXsd);				
		}

		/// <summary>
		/// helpers for loading schema files from different locations
		/// </summary>
		/// <param name="sc"></param>
		/// <param name="relativeDir"></param>
		internal void LoadSchemasFromDirectory(XmlSchemaSet sc, string relativeDir)
		{
			if (Directory.Exists(HttpContext.Current.Server.MapPath(relativeDir)))
			{
				string[] schemaFiles = Directory.GetFiles(HttpContext.Current.Server.MapPath(relativeDir), "*.xsd");
				foreach (string schemaFile in schemaFiles)
					LoadSchemaFromFile(sc, schemaFile);
				string[] wsdlFiles = Directory.GetFiles(HttpContext.Current.Server.MapPath(relativeDir), "*.wsdl");
				foreach (string wsdlFile in wsdlFiles)
				{
					ServiceDescription sd = ServiceDescription.Read(wsdlFile);
					LoadSchemasFromServiceDescriptions(sc, sd);
				}
			}
		}

		/// <summary>
		/// helpers for loading schema files from different locations
		/// </summary>
		/// <param name="sc"></param>
		/// <param name="type"></param>
		/// <param name="url"></param>
		internal void LoadReflectedSchemas(XmlSchemaSet sc, Type type, string url)
		{
			ServiceDescriptionReflector r = new ServiceDescriptionReflector();
			r.Reflect(type, url);
			foreach (XmlSchema xsd in r.Schemas)
				sc.Add(xsd);
			foreach (ServiceDescription sd in r.ServiceDescriptions)
				LoadSchemasFromServiceDescriptions(sc, sd);
		}

		/// <summary>
		/// helpers for loading schema files from different locations
		/// </summary>
		/// <param name="sc"></param>
		/// <param name="root"></param>
		/// <param name="assemblyName"></param>
		/// <param name="resourceName"></param>
		internal void LoadSchemaFromResourceFile(XmlSchemaSet sc, string root, string assemblyName, string resourceName)
		{
			// load SOAP 1.1 schema from assembly resource file		
			ResourceManager resmgr = new ResourceManager(root, System.Reflection.Assembly.Load(assemblyName));
			string resSchema = resmgr.GetString(resourceName);
            // Following changes for .NET 2.0, the SOAP schema is no longer being read from the resx file.
            // THIS MAY NEED TO BE FIXED
            if (!string.IsNullOrEmpty(resSchema))
            {
                StringReader sreader = new StringReader(resSchema);
                XmlTextReader xtr = new XmlTextReader(sreader);
                XmlSchema soapSchema = XmlSchema.Read(xtr, null);
                sc.Add(soapSchema);
                xtr.Close();
            }
		}

		/// <summary>
		/// called by both versions of GetInitializer to generated initializer object (ValidationContext)
		/// which serves as the context for future requests
		/// </summary>
		/// <param name="serviceType"></param>
		/// <param name="methodInfo"></param>
		/// <param name="attribute"></param>
		/// <returns></returns>
		public object GetInitializerHelper(Type serviceType, LogicalMethodInfo methodInfo, SoapExtensionAttribute attribute)
		{
			ValidationContext ctx = new ValidationContext();
			HttpContext httpctx = HttpContext.Current;

			// cache ValidationAttribute for future use
			ctx.ValidationAttribute = (ValidationAttribute)attribute;

			// temporary document/navigator for compiling XPath expressions
			XmlDocument doc = new XmlDocument();
			XPathNavigator nav = doc.CreateNavigator();
			// namespace manager for holding all namespace bindings
			XmlNamespaceManager ns = new XmlNamespaceManager(nav.NameTable);

			// retrieve (user-provided) namespace binding attributes
			object[] namespaceAtts = serviceType.GetCustomAttributes(typeof(AssertNamespaceBindingAttribute), true);
			foreach(AssertNamespaceBindingAttribute nsa in namespaceAtts)
				ns.AddNamespace(nsa.Prefix, nsa.Namespace);
			// store namespace manager in context for future use
			ctx.NamespaceManager = ns;

			// retrieve (user-provided) assertion attributes
			AssertAttribute[] allRuleAtts = null;
			object[] classRuleAtts = serviceType.GetCustomAttributes(typeof(AssertAttribute), true);
			if (methodInfo != null) // enabled via custom SoapExtensionAttribute
			{
				// retrieve (user-provided) method and class-level assert attributes
				object[] methodRuleAtts = methodInfo.GetCustomAttributes(typeof(AssertAttribute));
				allRuleAtts = new AssertAttribute[methodRuleAtts.Length + classRuleAtts.Length];
				methodRuleAtts.CopyTo(allRuleAtts, 0);
				classRuleAtts.CopyTo(allRuleAtts, methodRuleAtts.Length);
			}
			else
				// just retrieve (user-provided) class-level assertion attributes
				allRuleAtts = (AssertAttribute[])classRuleAtts;

			// store all assertions in context
			ctx.AssertAttributes = allRuleAtts;
			
			// generate, compile, and cache XPath expressions for future use
			StringBuilder completeExpression = new StringBuilder();
			string and = "";
			foreach(AssertAttribute ra in allRuleAtts)
			{
				string rule = String.Format(CultureInfo.InvariantCulture, "boolean({0})", ra.Rule);
				// cache compiled expression for future use
				ra.Expression = nav.Compile(rule);
				ra.Expression.SetContext(ns);
				completeExpression.Append(and);
				completeExpression.Append(rule);
				and = " and ";
			}
			if (completeExpression.Length != 0)
			{
				// complete expression (combination off all asserts) for quick success/failure check
				ctx.CompleteRuleExpression = nav.Compile(completeExpression.ToString());
				ctx.CompleteRuleExpression.SetContext(ns);
			}

			// XML Schema cache for schema validation
			XmlSchemaSet sc = new XmlSchemaSet();
			
			// load SOAP 1.1 schema from assembly resource file
			LoadSchemaFromResourceFile(sc, "TransportDirect.EnhancedExposedServices.Validation.ValidationResources", "td.enhancedexposedservices", "SOAP1.1");

			// load automatically-generated XML Schema for current endpoint (in case it hasn't already been loaded)
			LoadReflectedSchemas(sc, serviceType, httpctx.Request.RawUrl);

			// load from xsd directory by default
			LoadSchemasFromDirectory(sc, "xsd"); 
			// load schemas from user-defined vroot caches
			object[] schemaCacheAtts = serviceType.GetCustomAttributes(typeof(ValidationSchemaCacheAttribute), true);
			foreach (ValidationSchemaCacheAttribute schemaCacheLoc in schemaCacheAtts)
				LoadSchemasFromDirectory(sc, schemaCacheLoc.RelativeDirectory);

			// load schemas from explicit (user-provided) locations
			object[] schemaLocations = serviceType.GetCustomAttributes(typeof(ValidationSchemaAttribute), true);
			foreach(ValidationSchemaAttribute vsa in schemaLocations)
				LoadSchemaFromFile(sc, httpctx.Server.MapPath(vsa.Location));

			// save schema cache for future use
			ctx.SchemaSet = sc;

			// the System.Web.Services infrastructure will cache this object
			// and supply it in each future call to Initialize
			return ctx;
		}

		/// <summary>
		/// this GetInitializer is called when the SoapExtension is registered
		/// in the config file (web/machine.config), it's called the first time
		/// the class is invoked
		/// </summary>
		/// <param name="serviceType"></param>
		/// <returns></returns>
		public override object GetInitializer(System.Type serviceType)
		{
			return GetInitializerHelper(serviceType, null, null);
		}

		/// <summary>
		/// this GetInitializer is called when the SoapExtension is configured
		/// via a custom SoapExtensionAttribute, it's called the first time the
		/// WebMethod is invoked
		/// </summary>
		/// <param name="methodInfo"></param>
		/// <param name="attribute"></param>
		/// <returns></returns>
		public override object GetInitializer(LogicalMethodInfo methodInfo, SoapExtensionAttribute attribute)
		{
			return GetInitializerHelper(methodInfo.DeclaringType, methodInfo, attribute);
		}

		/// <summary>
		/// called each a givne [WebMethod] is used (will recieve a ValidationContext object)
		/// </summary>
		/// <param name="initializer"></param>
		public override void Initialize(object initializer)
		{
			// business rules passed in by initialize
			_context = (ValidationContext)initializer;
		}

		/// <summary>
		/// this will be called four times (before/after serialize/deserialize) for each [WebMethod] 
		/// </summary>
		/// <param name="message"></param>
		public override void ProcessMessage(SoapMessage message)
		{
			// this extension only cares about the BeforeSerialize stage where it
			// performs schema/assertion validation
            if (message.Stage == SoapMessageStage.BeforeDeserialize
                && _context != null)
			{
				try
				{
					// create an XPathNavigator on the request stream
					XmlReader reader = null;
					// check to see if the user wants schema validation
                    if ((_context.ValidationAttribute != null && _context.ValidationAttribute.SchemaValidation == true))
                    {
                        //move message stream position to start for read
                        message.Stream.Position = 0L;

                        XmlReaderSettings settings = new XmlReaderSettings();
                        settings.ValidationType = ValidationType.Schema;

                        // Ensure we only add unique schema's otherwise XmlSchemaSet throws error
                        foreach (XmlSchema xs in _context.SchemaSet.Schemas())
                        {
                            if (!string.IsNullOrEmpty(xs.TargetNamespace)
                                && !settings.Schemas.Contains(xs.TargetNamespace))
                            {
                                settings.Schemas.Add(xs);
                            }
                        }

                        XmlReader vr = XmlReader.Create(message.Stream, settings);

                        // if the user doesn't want to check assertions, just 
                        // rip through the stream to do schema validation
                        // and return
                        if (_context.ValidationAttribute.CheckAssertions == false)
                        {
                            while (vr.Read()) ; // read through stream
                            return;
                        }
                        reader = vr;
                    }
                    else
                        reader = new XmlTextReader(message.Stream);

					// turn the message stream into an XPathDocument
					// (will validate if it's an XmlValidatingReader)
					XPathDocument doc = new XPathDocument(reader);
					XPathNavigator nav = doc.CreateNavigator();

					// evaluate the XPath assertions - if complete expression returns true
					// let the System.Web.Services infrastructure finish processing 
					if ((_context.ValidationAttribute != null) &&
						(_context.ValidationAttribute.CheckAssertions == true) && 
						(_context.CompleteRuleExpression != null) && 
						(false == (bool)nav.Evaluate(_context.CompleteRuleExpression)))
					{
						// generate namespace declarations required by assert expressions
						foreach (string prefix in _context.NamespaceManager)
						{
							if (prefix.Equals("xml") || prefix.Equals("xmlns") || prefix.Length == 0)
							{
								continue;
							}
							string errormessage = _context.NamespaceManager.LookupNamespace(prefix);
							bool clienterror = (message.Stage == SoapMessageStage.BeforeDeserialize);
							throw LogErrorAndCreateThrowableException(errormessage, clienterror);
						}

						// check to see which assertions failed and list in failedRules
						foreach(AssertAttribute aa in _context.AssertAttributes)
						{
							if(false == (bool)nav.Evaluate(aa.Expression))	
							{
								string errormessage = aa.Rule + aa.Description;
								bool clienterror = (message.Stage == SoapMessageStage.BeforeDeserialize);
								throw LogErrorAndCreateThrowableException(errormessage, clienterror);
							}
						}
					}
				}				
				catch (Exception exception)
				{
					bool clienterror = (message.Stage == SoapMessageStage.BeforeDeserialize);
					throw LogErrorAndCreateThrowableException(exception.ToString(), clienterror);
				}
				finally
				{
					// make sure you leave the stream the way you found it
					// move it to start for BeforeDeserialize
					// leave at end for AfterSerialize
					if (message.Stage == SoapMessageStage.BeforeDeserialize)
					{
						message.Stream.Position=0L;
					}
				}
			}
		}
		/// <summary>
		/// Helper method to log error and create SoapException to throw
		/// </summary>
		/// <param name="detailederrormessage">Error message for SOAP fault Details block</param>
		/// <param name="clienterror">Whether client or server error</param>
		/// <returns>SoapException</returns>
		private SoapException LogErrorAndCreateThrowableException(string detailederrormessage, bool clienterror)
		{
			Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, detailederrormessage));
			if (clienterror)
			{
				TDException tdex = new TDException(detailederrormessage, true, TDExceptionIdentifier.EESWSDLRequestValidationFailed);
				return SoapExceptionHelper.ThrowClientSoapException(
					EnhancedExposedServicesMessages.WSDLRequestValidationError,
					tdex);
			}
			else
			{
				TDException tdex = new TDException(detailederrormessage, true, TDExceptionIdentifier.EESWSDLResponseValidationFailed);
				return SoapExceptionHelper.ThrowSoapException(
					EnhancedExposedServicesMessages.WSDLResponseValidationError,
					tdex);
			}
		}
	}

	/// <summary>
	/// ValidationAttribute triggers schema/assert validation through
	/// the ValidationExtension class
	/// </summary>
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
	public sealed class ValidationAttribute :  SoapExtensionAttribute
	{
		int priority;
		bool _schemaValidation = true;
		bool _checkAssertions = true;

		/// <summary>
		/// Read/write property whether to enable schema validation
		/// </summary>
		public bool SchemaValidation
		{
			get { return _schemaValidation; }
			set { _schemaValidation = value; }
		}

		/// <summary>
		/// Read/write property whether to enable assertion processing
		/// </summary>
		public bool CheckAssertions
		{
			get { return _checkAssertions; }
			set { _checkAssertions = value; }
		}

		/// <summary>
		/// Read only property used by soap extension to get the type
		/// of object to be created
		/// </summary>
		public override System.Type ExtensionType
		{
			get
			{
				// tell the System.Web.Services infrastructure
				// to use the ValidationExtension class
				return typeof(ValidationExtension);
			}
		}
		/// <summary>
		/// Read/write property for priority
		/// </summary>
		public override int Priority
		{
			get { return priority; }
			set { priority = value;	}
		}
	}
	
	/// <summary>
	/// Specifies (at the class-level) the location of a directory
	/// containing schema files (relative to vroot)
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	public sealed class ValidationSchemaCacheAttribute : System.Attribute
	{
		string _relativeDirectory;

		/// <summary>
		/// Default constructor
		/// </summary>
		/// <param name="relativeDirectory"></param>
		public ValidationSchemaCacheAttribute(string relativeDirectory)
		{
			_relativeDirectory = relativeDirectory;
		}

		/// <summary>
		/// Read/write property to specify relative path to schema directory
		/// </summary>
		public string RelativeDirectory
		{
			get { return _relativeDirectory; }
			set { _relativeDirectory = value; }
		}
	}

	/// <summary>
	/// Specifies (at the class-level) the exact location of a 
	/// schema file (relative to vroot)
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	public sealed class ValidationSchemaAttribute : System.Attribute
	{
		string _location;

		/// <summary>
		/// Default constructor
		/// </summary>
		/// <param name="location"></param>
		public ValidationSchemaAttribute(string location)
		{
			_location = location;
		}

		/// <summary>
		/// Read only property for schema location
		/// </summary>
		public string Location
		{
			get { return _location; }
		}
	}

	/// <summary>
	/// specifies an XPath expression evaluated as an assertion (must evaluate to true)
	/// can be used on an individual method or the entire class - if used on the class, 
	/// the expression applies to all WebMethods
	/// </summary>
	[AttributeUsage(AttributeTargets.Class|AttributeTargets.Method, AllowMultiple = true)]
	public sealed class AssertAttribute : System.Attribute
	{
		string _rule;
		string _description;
		XPathExpression _expression;

		/// <summary>
		/// Default constructor
		/// </summary>
		public AssertAttribute() : this("", "") { }

		/// <summary>
		/// Overloaded constructor
		/// </summary>
		/// <param name="rule"></param>
		/// <param name="description"></param>
		public AssertAttribute(string rule, string description)
		{
			_rule = rule;
			_description = description;
		}

		/// <summary>
		/// Overloaded constructor
		/// </summary>
		/// <param name="rule"></param>
		public AssertAttribute(string rule) : this(rule, "") {}

		/// <summary>
		/// Read/write property for XPath rule expression
		/// </summary>
		[XmlElement("expression")]
		public string Rule
		{
			get { return _rule; }
			set { _rule = value; }
		}

		/// <summary>
		/// Read/write property for description
		/// </summary>
		[XmlElement("description")]
		public string Description
		{
			get { return _description; }
			set { _description = value; }
		}

		/// <summary>
		/// Read/write property for XPathExpression
		/// </summary>
		internal XPathExpression Expression
		{
			get { return _expression; }
			set { _expression = value; }
		}
	}
	
	/// <summary>
	/// Specifies namespace bindings required by the XPath (assert)
	/// expressions
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	public sealed class AssertNamespaceBindingAttribute : System.Attribute
	{
		string _prefix;
		string _ns;

		/// <summary>
		/// Default constructor
		/// </summary>
		/// <param name="prefix"></param>
		/// <param name="ns"></param>
		public AssertNamespaceBindingAttribute(string prefix, string ns)
		{
			_prefix = prefix;
			_ns = ns;
		}

		/// <summary>
		/// Read only property for prefix
		/// </summary>
		public string Prefix
		{
			get { return _prefix; }
		}

		/// <summary>
		/// Read only property for namepsace
		/// </summary>
		public string Namespace
		{
			get { return _ns; }
		}
	}

	/// <summary>
	/// format extension used to customize automatic WSDL generation (.asmx?wsdl)
	/// </summary>
	[XmlFormatExtension("validation", Namespaces.TD, typeof(InputBinding))]
	[XmlFormatExtensionPrefix("td", Namespaces.TD)]
	public class ValidationFormatExtension : ServiceDescriptionFormatExtension
	{
		// holds array of AssertAttributes (expression + description)
		[XmlArray("assertions")]
		[XmlArrayItem("assert")]
		internal AssertAttribute[] assertions;
		// holds namespace declarations required by XPath expressions
		[XmlNamespaceDeclarations]
		internal XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
	}

	/// <summary>
	/// called by the runtime during WSDL generation (.asmx?wsdl)
	/// must be configured in the web/machine.config file
	/// </summary>
	public class ValidationExtensionReflector : SoapExtensionReflector
	{
		/// <summary>
		/// Overridden ReflectMethod
		/// </summary>
		public override void ReflectMethod()
		{
			ProtocolReflector reflector = ReflectionContext;	
			// ValidationFormatExtension represents an extension element
			ValidationFormatExtension fe = new ValidationFormatExtension();
		
			// populate with assertions and namespaces
			object va = reflector.Method.GetCustomAttribute(typeof(ValidationAttribute));
			if (va != null)
			{
				// retrieve assertions
				object[] classRuleAtts = reflector.Method.DeclaringType.GetCustomAttributes(typeof(AssertAttribute), true);
				object[] methodRuleAtts = reflector.Method.GetCustomAttributes(typeof(AssertAttribute));
				fe.assertions = new AssertAttribute[methodRuleAtts.Length + classRuleAtts.Length];
				methodRuleAtts.CopyTo(fe.assertions, 0);
				classRuleAtts.CopyTo(fe.assertions, methodRuleAtts.Length);

				// retrieve namespace bindings
				object[] namespaceAtts = reflector.Method.DeclaringType.GetCustomAttributes(typeof(AssertNamespaceBindingAttribute), true);
				foreach(AssertNamespaceBindingAttribute nsa in namespaceAtts)
					fe.namespaces.Add(nsa.Prefix, nsa.Namespace);
			}
			// inject extension element in the binding/input element
			reflector.OperationBinding.Input.Extensions.Add(fe);
		}
	}
}
