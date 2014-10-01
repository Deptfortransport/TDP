// ********************************************************************* 
// NAME                 : TrainTaxiInfo.cs 
// AUTHOR               : Richard Philpott
// DATE CREATED         : 2003-11-06
// DESCRIPTION			: Details of TrainTaxi links for single rail station
// ********************************************************************** 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/AdditionalDataModule/TrainTaxiInfo.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:18:10   mturner
//Initial revision.
//
//   Rev 1.1   Nov 07 2003 16:29:26   RPhilpott
//Changes to accomodate removal of station name lookup by Atkins.
//
//   Rev 1.0   Nov 07 2003 14:06:46   RPhilpott
//Initial Revision
//

using System;
using System.Data;
using System.Collections;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;

using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.AdditionalDataModule
{
	/// <summary>
	/// TrainTaxiInfo handles the retrieval and interpretation 
	/// of train/taxi link details for a single rail station.
	/// </summary>
	public class TrainTaxiInfo
	{

		// The following string is not in the langStrings resource file
		// because it will be mixed up with English-language only text 
		// originating from the database and therefore should not be 
		// translated into Welsh ... 

		private const string ALTERNATIVE_STATION = "Alternative station: ";

		private const string COMMENT_LINE = "Comment";
		private const string TAXI_OPERATOR_LINE = "TaxiPHOperator";
		private const string GOTO_LINE = "GOTO";
		private const char SEPARATOR = '|';

		private const string PDSKEY_COLUMN_NAME = "PDS Key";
		private const string VALUE_COLUMN_NAME = "Value";
		private const string TYPE_COLUMN_NAME = "Type";

		private string naptan = string.Empty;
		IAdditionalData addData = null;

		public TrainTaxiInfo(string naptan)
		{
			this.addData = (IAdditionalData)TDServiceDiscovery.Current[ServiceDiscoveryKey.AdditionalData];
			this.naptan = naptan;
		}
		
		public string[] GetInfoLines()
		{

			DataSet ds = GetDataSetForNaptan(naptan);

			if	(ds == null || ds.Tables[0].Rows.Count == 0) 
			{
				return new string[0];
			}

			return ParseTrainTaxiLines(ds, true);
		}
			

		private DataSet GetDataSetForNaptan(string naptan)
		{
			DataSet ds = null;

			try
			{
				ds = addData.RetrieveTrainTaxiInfoForNaptan(naptan); 
			}
			catch (Exception ex)
			{
				OperationalEvent oe = new OperationalEvent
					(TDEventCategory.Business, TDTraceLevel.Error, "Exception on retrieving TrainTaxi info for " + naptan, ex);
				Logger.Write(oe);
				
				ds = null;
			}

			return ds;
		}


		private string[] ParseTrainTaxiLines(DataSet ds, bool recurseIntoGotos) 
		{
			ArrayList stringList = new ArrayList();

			foreach(DataRow row in ds.Tables[0].Rows)
			{
				switch (row[TYPE_COLUMN_NAME].ToString())
				{
					case COMMENT_LINE:
						stringList.Add(FormatCommentLine(row[VALUE_COLUMN_NAME].ToString(), row[PDSKEY_COLUMN_NAME].ToString()));
						break;

					case TAXI_OPERATOR_LINE:
						stringList.Add(FormatOperatorLine(row[VALUE_COLUMN_NAME].ToString()));
						break;

					case GOTO_LINE:
						if	(recurseIntoGotos)
						{
							stringList.AddRange(GetGotoDetails(row[VALUE_COLUMN_NAME].ToString()));
						}
						else
						{
							stringList.Clear();
						}
						break;

					default:
	
						OperationalEvent oe = new OperationalEvent
							(TDEventCategory.Business, TDTraceLevel.Error, "Unexpected data type " + row[TYPE_COLUMN_NAME].ToString() + " in TrainTaxi info for " + naptan);
						Logger.Write(oe);
						break;
				}
			}

			return (string[])stringList.ToArray(typeof(string));
		}


		private string FormatCommentLine(string suppliedComment, string naptanForRow)
		{
			return addData.LookupStationNameForNaptan(naptanForRow) + " " + suppliedComment;			
		}


		private string FormatOperatorLine(string operatorText)
		{
			return operatorText.Replace(SEPARATOR, ' ');
		}


		private ArrayList GetGotoDetails(string gotoText)
		{
			ArrayList gotoDetails = new ArrayList();

			int sepIndex = gotoText.IndexOf(SEPARATOR);
			string gotoNapatan = gotoText.Substring(0, sepIndex);
			string gotoName = addData.LookupStationNameForNaptan(gotoNapatan);

			if	(gotoName != string.Empty)
			{
				DataSet ds = GetDataSetForNaptan(gotoNapatan);

				if	(ds == null || ds.Tables[0].Rows.Count == 0) 
				{
					return gotoDetails;
				}

				string[] gotoStrings = ParseTrainTaxiLines(ds, false);

				if (gotoStrings.Length > 0) 
				{
					gotoDetails.Add(ALTERNATIVE_STATION + gotoName);
					gotoDetails.AddRange(gotoStrings);	
				}
			}
			
			return gotoDetails;
		}

	}
}
