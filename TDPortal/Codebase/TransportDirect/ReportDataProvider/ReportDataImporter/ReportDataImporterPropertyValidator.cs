// ********************************************************** 
// NAME                 : ReportDataImporterPropertyValidator
// AUTHOR               : Andy Lole
// DATE CREATED         : 02/10/2003 
// DESCRIPTION			: Validates Importer properties.
// ********************************************************** 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/ReportDataImporter/ReportDataImporterPropertyValidator.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:39:02   mturner
//Initial revision.
//
//   Rev 1.4   Nov 26 2003 13:56:08   geaton
//Removed need to use separate connection string (stored as property) for initialising link to Reporting database. - reuse string stored in another property.
//
//   Rev 1.3   Nov 23 2003 14:43:58   geaton
//Added timeout support.
//
//   Rev 1.2   Nov 18 2003 21:28:12   geaton
//Added extra validation.
//
//   Rev 1.1   Oct 07 2003 12:48:46   PScott
//exception enumerations
//
//   Rev 1.0   Oct 02 2003 17:27:28   ALole
//Initial Revision

using System;
using System.IO;
using System.Collections;
using System.Text.RegularExpressions;

using TransportDirect.Common;
using TransportDirect.Common.PropertyService.Properties;

namespace TransportDirect.ReportDataProvider.ReportDataImporter
{
	/// <summary>
	/// Validates properties specific to the report data importer component.
	/// </summary>
	public class ReportDataImporterPropertyValidator : PropertyValidator
	{
		public ReportDataImporterPropertyValidator( IPropertyProvider properties ) : base ( properties )
		{
		}

		public override bool ValidateProperty ( string key, ArrayList errors )
		{
			if (key == Keys.CJPWebRequestWindow)
				return ValidateCJPWebRequestWindow(key, errors);
			else if (key == Keys.ReportDatabase)
				return ValidateReportDatabase(key, errors);
			else if (key == Keys.ImportTimeout)
				return ValidateTimeout(key, errors);
			else
			{
				throw new TDException(String.Format(Messages.Init_UnknownPropertyKey, key), false, TDExceptionIdentifier.RDPDataImporterUnknownPropertyKey );
			}
		}

		private bool ValidateTimeout(string key, ArrayList errors)
		{
			int errorsBefore = errors.Count;

			if (ValidateExistence(key, Optionality.Mandatory, errors))
			{		
				if (!IsWholeNumber(properties[key]))
					errors.Add(String.Format(Messages.Validation_BadTimeout, properties[key], key));
			}

			return (errorsBefore == errors.Count); 
		}

		private bool ValidateCJPWebRequestWindow(string key, ArrayList errors)
		{
			int errorsBefore = errors.Count;

			if (ValidateExistence(key, Optionality.Mandatory, errors))
			{		
				if (!IsWholeNumber(properties[key]))
					errors.Add(String.Format(Messages.Validation_BadWebRequestWindow, properties[key], key));
			}

			return (errorsBefore == errors.Count); 
		}

		private bool ValidateReportDatabase(string key, ArrayList errors)
		{
			return (ValidateExistence(key, Optionality.Mandatory, errors)); 
		}

	}
}
