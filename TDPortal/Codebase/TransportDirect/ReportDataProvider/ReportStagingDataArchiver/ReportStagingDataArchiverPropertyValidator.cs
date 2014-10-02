// *********************************************** 
// NAME                 : ReportStagingDataArchiverPropertyValidator.cs
// AUTHOR               : Gary Eaton
// DATE CREATED         : 10/10/2003 
// DESCRIPTION  :  Validates properties of the database publishers.
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/ReportStagingDataArchiver/ReportStagingDataArchiverPropertyValidator.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:39:14   mturner
//Initial revision.
//
//   Rev 1.2   Nov 21 2003 12:16:48   geaton
//Removed redundant using statements.

using System;
using System.Collections;
using System.Diagnostics;

using TransportDirect.Common;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.Logging;


namespace TransportDirect.ReportDataProvider.ReportStagingDataArchiver
{
	/// <summary>
	/// Class for validating properties of the custom database publishers.
	/// </summary>
	public class ReportStagingDataArchiverPropertyValidator : PropertyValidator
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <param name="properties">Properties.</param>
		public ReportStagingDataArchiverPropertyValidator( IPropertyProvider properties ) : base( properties )
		{}

		/// <summary>
		/// Validates the given property.
		/// </summary>
		/// <param name="key">Property name to validate.</param>
		/// <param name="errors">List to append any errors.</param>
		/// <returns>true on success, false on validation failure.</returns>
		public override bool ValidateProperty( string key, ArrayList errors )
		{
			if (key == Keys.ReportDataStagingDB)
				return ValidateExistence(key, Optionality.Mandatory, errors);				
			else
			{
				throw new TDException(String.Format(Messages.Init_UnknownPropertyKey, key), false, TDExceptionIdentifier.RDPStagingArchiverUnknownPropertyKey );
			}
		}

	}
}

