// ************************************************************************************
// NAME			: TransactionWebServicePropertyValidator.cs
// AUTHOR		: Gary Eaton
// DATE CREATED	: 04/10/2003 
// DESCRIPTION	: Used to validate properties of the Transaction Web Service component.
// ************************************************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/WebServices/TransactionWebServices/TransactionWebServicePropertyValidator.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 13:55:30   mturner
//Initial revision.
//
//   Rev 1.1   Nov 12 2003 16:12:04   geaton
//Added property validation to ensure that esri db has been specified.
//
//   Rev 1.0   Nov 05 2003 09:56:24   geaton
//Initial Revision

using System;
using System.IO;
using System.Collections;

using TransportDirect.Common;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.UserPortal.JourneyControl;

namespace TransportDirect.ReportDataProvider.TransactionWebService
{
	/// <summary>
	/// Validates configuration properties of the TransactionWebService component.
	/// </summary>
	public class TransactionWebServicePropertyValidator : PropertyValidator
	{
		/// <summary>
		/// Constructor, passes property object to base constructor.
		/// </summary>
		/// <param name="properties">Properties object.</param>
		public TransactionWebServicePropertyValidator( IPropertyProvider properties ) : base( properties )
		{
			
		}

		/// <summary>
		/// Validates a given property.
		/// </summary>
		/// <param name="key">Property to validate.</param>
		/// <param name="errors">Error list to append any new errors.</param>
		/// <returns>true on success, false on failure.</returns>
		public override bool ValidateProperty( string key, ArrayList errors )
		{			
			if (key == JourneyControlConstants.NumberOfPublicJourneys)
				return ValidateExistence(key, Optionality.Mandatory, errors);
			else if (key == JourneyControlConstants.LogAllRequests)
				return ValidateExistence(key, Optionality.Mandatory, errors);
			else if (key == JourneyControlConstants.LogAllResponses)
				return ValidateExistence(key, Optionality.Mandatory, errors);
			else if (key == JourneyControlConstants.CJPTimeoutMillisecs)
				return ValidateExistence(key, Optionality.Mandatory, errors);
			else if (key == Keys.LocationServerName)
				return ValidateExistence(key, Optionality.Mandatory, errors);
			else if (key == Keys.LocationServiceName)
				return ValidateExistence(key, Optionality.Mandatory, errors);
			else if (key == Keys.CJPConfig)
				return ValidateCJPConfigFile(errors);
			else if (key == Keys.DefaultDB)
				return ValidateExistence(key, Optionality.Mandatory, errors);
			else if (key == Keys.EsriDB)
				return ValidateExistence(key, Optionality.Mandatory, errors);
			else
			{
				throw new TDException(String.Format(Messages.Init_UnknownPropertyKey, key), false, TDExceptionIdentifier.RDPTransactionServiceUnknownPropertyKey);
			}						
		}

		private bool ValidateCJPConfigFile(ArrayList errors)
		{
			if (ValidateExistence(Keys.CJPConfig, Optionality.Mandatory, errors))
			{
				if (File.Exists(properties[Keys.CJPConfig]))
					return true;
				else
				{
					errors.Add("Missing configuration file: [" + properties[Keys.CJPConfig] + "] specified in property key [" + Keys.CJPConfig + "]");
					return false;
				}
			}
			else
				return false;
		}
	}
}

