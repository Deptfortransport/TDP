// *********************************************** 
// NAME                 : DatabasePublishersPropertyValidator.cs 
// AUTHOR               : Gary Eaton
// DATE CREATED         : 10/10/2003 
// DESCRIPTION  :  Validates properties of the database
// publishers.
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/DatabasePublishers/DatabasePublishersPropertyValidator.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:38:14   mturner
//Initial revision.
//
//   Rev 1.1   Nov 13 2003 21:59:58   geaton
//Use DefaultDB instead of ReportStagingDB which has been dropped.
//
//   Rev 1.0   Oct 10 2003 15:24:30   geaton
//Initial Revision

using System;
using System.Collections;
using System.Diagnostics;

using TransportDirect.Common;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.Logging;
using TransportDirect.Common.DatabaseInfrastructure;
using Logger = System.Diagnostics.Trace;

namespace TransportDirect.ReportDataProvider.DatabasePublishers
{
	/// <summary>
	/// Class for validating properties of the custom database publishers.
	/// </summary>
	public class DatabasePublisherPropertyValidator : PropertyValidator
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <param name="properties">Properties.</param>
		public DatabasePublisherPropertyValidator( IPropertyProvider properties ) : base( properties )
		{}

		/// <summary>
		/// Validates the given property.
		/// </summary>
		/// <param name="key">Property name to validate.</param>
		/// <param name="errors">List to append any errors.</param>
		/// <returns>true on success, false on validation failure.</returns>
		public override bool ValidateProperty( string key, ArrayList errors )
		{
			if( key == SqlHelperDatabase.DefaultDB.ToString() )
				return ValidateTargetDB( errors );				
			else
			{
				// by default validate the key exists
				return ValidateExistence( key, Optionality.Mandatory, errors );
			}

		}

		/// <summary>
		/// Checks target database key containing connection string is valid
		/// </summary>
		/// <param name="errors">Error list.</param>
		/// <returns>true on success, false on validation failure.</returns>
		private bool ValidateTargetDB( ArrayList errors )
		{
			int errorsBefore = errors.Count;			
					
			ValidateExistence( SqlHelperDatabase.DefaultDB.ToString(), Optionality.Mandatory, errors );

			return (errorsBefore == errors.Count);
		}
	}
}

