// *********************************************** 
// NAME                 : TransactionInjectorFilePublisherPropertyValidator.cs 
// AUTHOR               : Gary Eaton
// DATE CREATED         : 12/2/2004
// DESCRIPTION  :  Validates properties of Transaction Injector File Publishers
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/TransactionInjector/TransactionInjectorFilePublisherPropertyValidator.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:40:14   mturner
//Initial revision.
//
//   Rev 1.0   Feb 16 2004 15:39:32   geaton
//Initial Revision

using System;
using System.Collections;
using System.Diagnostics;
using System.IO;

using TransportDirect.Common;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.Logging;


namespace TransportDirect.ReportDataProvider.TransactionInjector
{
	/// <summary>
	/// Class for validating properties of the TI file publishers
	/// </summary>
	public class TransactionInjectorFilePublisherPropertyValidator : PropertyValidator
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <param name="properties">Properties.</param>
		public TransactionInjectorFilePublisherPropertyValidator( IPropertyProvider properties ) : base( properties )
		{}

		/// <summary>
		/// Validates the given property.
		/// </summary>
		/// <param name="key">Property name to validate.</param>
		/// <param name="errors">List to append any errors.</param>
		/// <returns>true on success, false on validation failure.</returns>
		public override bool ValidateProperty(string key, ArrayList errors)
		{
			if (key == "Logging.Publisher.Custom.TinjFP.Directory")
				return ValidateDirectory(key, errors);
			else
			{
				// by default validate the key value exists
				return ValidateExistence( key, Optionality.Mandatory, errors );
			}

		}

		/// <summary>
		/// Checks target database key containing connection string is valid
		/// </summary>
		/// <param name="errors">Error list.</param>
		/// <returns>true on success, false on validation failure.</returns>
		private bool ValidateDirectory(string key, ArrayList errors )
		{
			int errorsBefore = errors.Count;			
					
			if (ValidateExistence(key.ToString(), Optionality.Mandatory, errors))
			{
				if (!Directory.Exists(properties[key.ToString()]))
					errors.Add(String.Format("Directory [{0}] of transaction injector file publisher specified in property [{1}] does not exist.", properties[key.ToString()], key.ToString()));
			}

			return (errorsBefore == errors.Count);
		}
	}
}

