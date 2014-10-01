// ********************************************************************* 
// NAME                 : StopTaxiInformation.cs 
// AUTHOR               : Ken Josling
// DATE CREATED         : 2005-09-08
// DESCRIPTION			: Provides Taxi Operator information for a stop (train, coach, airport etc)
// ********************************************************************** 
//
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/AdditionalDataModule/StopTaxiInformation.cs-arc  $
//
//   Rev 1.2   Oct 17 2008 11:54:48   build
//Automatically merged from branch for stream0093
//
//   Rev 1.1.1.0   Aug 05 2008 14:06:00   mturner
//Reduced severity of error message when no info is found for a stop
//
//   Rev 1.1   Jun 16 2008 11:08:52   mturner
//Fix for IR 5021. - Dropped severity of one of the operational events and added a mjkissing space to the message text.
//
//   Rev 1.0   Nov 08 2007 12:18:10   mturner
//Initial revision.
//
//   Rev 1.10   Aug 31 2007 14:19:50   rbroddle
//CCN393, (Coach Taxi Info) work, merged in from stream4468
//
//   Rev 1.9.1.0   Aug 31 2007 13:47:12   rbroddle
//CCN393, stream4468 (Coach Taxi Info) work
//
//   Rev 1.9   Aug 31 2007 13:44:58   rbroddle
//CCN393, stream4468 (Coach Taxi Info) work
//Resolution for 4468: Coach Stop Taxi Enhancements
//
//   Rev 1.8   Oct 11 2005 14:56:42   kjosling
//Fixed accessible text visibility issue. 
//Resolution for 2851: DN65 - TrainTaxi: text truncated for Grateley station
//
//   Rev 1.7   Sep 01 2005 11:45:20   kjosling
//Updated following code review
//
//   Rev 1.6   Aug 15 2005 16:34:20   kjosling
//Changed properties to r/w
//
//   Rev 1.5   Aug 15 2005 09:40:30   kjosling
//Added code commentary
//
//   Rev 1.4   Aug 12 2005 15:01:48   kjosling
//Added Serializable attribute
//
//   Rev 1.3   Aug 12 2005 11:35:56   kjosling
//Initial version

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.DatabaseInfrastructure;

using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.AdditionalDataModule
{
	/// <summary>
	/// Provides Taxi Operator information for a stop (train, coach, airport etc)
	/// </summary>
	[Serializable]
	public class StopTaxiInformation
	{

		#region Private attributes

		//Constants to identify properties of a stop
		private const string COMMENT_LINE = "Comment";
		private const string TAXI_OPERATOR_LINE = "TaxiPHOperator";
		private const string GOTO_LINE = "GOTO";
		private const string ACCESSIBLE_LINE = "ACCESSIBLE";
		private const char SEPARATOR = '|';

		//Constants to identify columns in the source data
		private const string TYPE_COLUMN_NAME = "Type";
		private const string VALUE_COLUMN_NAME = "Value";
		private const string PDSKEY_COLUMN_NAME = "PDS Key";

		private string stopNaptan;
		private bool processAlternativeStops;
		private string comment;
		private string accessibleText = String.Empty;
		private string stopName;
		private bool accessibleOperatorPresent;
		private ArrayList operators;
		private ArrayList alternativeStops;

		//Enum to identify fields in the Atos DB stop info dataset
		private enum AtosTaxiFields
		{
			NaptanCode, DestinationName, TraintaxiLink, CommentLine, AccessibilityText, 
			Firm1Name, Firm2Name, Firm3Name, Firm4Name, 
			Firm1Phone, Firm2Phone, Firm3Phone, Firm4Phone, 
			Firm1Accessibility, Firm2Accessibility, Firm3Accessibility, Firm4Accessibility, 
			GOTO1Name, GOTO2Name, GOTO3Name, GOTO4Name, GOTO1Link, GOTO2Link, GOTO3Link, GOTO4Link, 
			Copyright, Type, Source, Version
		}


		IAdditionalData addData;

		#endregion

		#region Constructor

		/// <summary>
		/// Creates a new StopTaxiInformation class
		/// </summary>
		/// <param name="naptan">A unique identifier for the stop</param>
		/// <param name="processAlternativeStops">true if alternative stops should be processed, false otherwise</param>
		public StopTaxiInformation(string naptan, bool processAlternativeStops)
		{
			this.addData = (IAdditionalData)TDServiceDiscovery.Current[ServiceDiscoveryKey.AdditionalData];
			this.stopNaptan = naptan;
			this.processAlternativeStops = processAlternativeStops;
			this.stopName = addData.LookupStationNameForNaptan(naptan);
			operators = new ArrayList();
			alternativeStops = new ArrayList();

			DataSet ds = GetDataSetForNaptan(naptan);
			LoadData(ds);

			if	(ds == null || ds.Tables[0].Rows.Count == 0) 
			{
				ds = GetAtosDataSetForNaptan(naptan);
				LoadDataFromAtosDB(ds);
			}

		}

		/// <summary>
		/// Default public constructor for serialization
		/// </summary>
		public StopTaxiInformation(){}

		#endregion

		#region Public readonly attributes

		/// <summary>
		/// (Read-Write) Gets or sets the name of the stop
		/// </summary>
		public string StopName
		{
			get{	return stopName;	}
			set{	stopName = value;	}
		}

		/// <summary>
		/// (Read-Write) Gets or sets a unique identifier for the stop
		/// </summary>
		public string StopNaptan
		{
			get{	return stopNaptan;	}
			set{	stopNaptan = value;	}
		}

		/// <summary>
		/// (Read-Write) Gets or sets a textual commentary for the stop
		/// </summary>
		public string Comment
		{
			get{	return comment;		}
			set{	comment = value;	}
		}

		/// <summary>
		/// (Read-Write) Gets or sets a list of TaxiOperator objects representing the taxi operators associated with
		/// this stop
		/// </summary>
		public TaxiOperator[] Operators
		{
			get{	return (TaxiOperator[])operators.ToArray(typeof(TaxiOperator));	}
			set{	this.Operators = value;	}
		}

		/// <summary>
		/// (Read-Write) Gets or sets a list of StopTaxiInformation objects representing any alternative stops (if the
		/// stop has no immediate taxi operators associated with it)
		/// </summary>
		public StopTaxiInformation[] AlternativeStops
		{
			get{	return (StopTaxiInformation[])alternativeStops.ToArray(typeof(StopTaxiInformation));	}
			set{	this.AlternativeStops = value;	}
		}

		/// <summary>
		/// (Read-Write) Gets or sets a boolean indicating whether the stop (or one or more associated stops) has an accessible operator
		/// </summary>
		public bool AccessibleOperatorPresent
		{
			get{	return accessibleOperatorPresent;	}
			set{	accessibleOperatorPresent = value;	}
		}

		/// <summary>
		/// (Read-Write) Gets or sets a textual commentary applied to any accessible operator 
		/// </summary>
		public string AccessibleText
		{
			get{	return accessibleText;	}
			set{	accessibleText = value; }
		}

		/// <summary>
		/// (Read-Write) Gets or sets a boolean determining if there is any valid information for this stop to display
		/// </summary>
		public bool InformationAvailable
		{
			get
			{
				if(comment == null && operators.Count == 0 && alternativeStops.Count == 0)
				{
					return false;
				}
				else
				{
					return true;
				}
			}
			set{	InformationAvailable = value;	}
		}

		#endregion

		#region Private methods

		/// <summary>
		/// Parses incoming reference data and processes the content to populate the class properties
		/// </summary>
		/// <param name="ds">A Dataset containing the reference data for the stop</param>
		private void LoadData(DataSet ds)
		{	
			if	(ds == null || ds.Tables[0].Rows.Count == 0) 
			{
				OperationalEvent oe = new OperationalEvent
					(TDEventCategory.Business, TDTraceLevel.Info, "No data available for " + stopNaptan);
				Logger.Write(oe);
				return;
			}

			foreach(DataRow row in ds.Tables[0].Rows)
			{
				switch (row[TYPE_COLUMN_NAME].ToString())
				{
					case COMMENT_LINE:
						//Handle the stop description
						this.comment = FormatCommentLine(row[VALUE_COLUMN_NAME].ToString(), row[PDSKEY_COLUMN_NAME].ToString());				
						break;

					case TAXI_OPERATOR_LINE:
						//Handle a taxi operator
						AddTaxiOperator(row[VALUE_COLUMN_NAME].ToString());
						break;

					case GOTO_LINE:
						//Handle an alternative stop
						if	(processAlternativeStops)
						{
							AddAlternativeStop(row[VALUE_COLUMN_NAME].ToString());
						}
						break;

					case ACCESSIBLE_LINE:
						//Handle the accessibility text
						//(KJ) FIX: Stops the parent stop overwriting accessible text if an alternative stop has already overwritten it
						if(accessibleText == String.Empty)
						{
							accessibleText = row[VALUE_COLUMN_NAME].ToString();
						}
						break;

					default:
	
						OperationalEvent oe = new OperationalEvent
							(TDEventCategory.Business, TDTraceLevel.Error, "Unexpected data type " + row[TYPE_COLUMN_NAME].ToString() + " in TrainTaxi info for " + stopNaptan);
						Logger.Write(oe);
						break;
				}
			}
		}

		/// <summary>
		/// Creates an alternative stop for the current object and adds it to the AlternativeStops collection
		/// </summary>
		/// <param name="info">The name and naptan of the alternative stop</param>
		private void AddAlternativeStop(string info)
		{
			string gotoNapatan = info.Substring(0, info.IndexOf(SEPARATOR));

			if	(gotoNapatan.Length > 0)
			{
				StopTaxiInformation alternativeStop = new StopTaxiInformation(gotoNapatan, false);
				this.alternativeStops.Add(alternativeStop);
				if(alternativeStop.AccessibleOperatorPresent)
				{
					if(!this.accessibleOperatorPresent)
					{
						//FIX: (KJ) set the accessible text of the parent if one or more of its alternative stops is accessible
						this.accessibleOperatorPresent = true;
						this.accessibleText = alternativeStop.accessibleText;
					}
				}
			}
		}
		
		/// <summary>
		/// Creates a TaxiOperator for the current object and adds it to the Operators collection
		/// </summary>
		/// <param name="info">The name, telephone number and a flag indicated whether the operator is an accessible
		/// operator</param>
		private void AddTaxiOperator(string info)
		{
			//Retrieve information from the source data and use it to construct a TaxiOperator
			string[] operatorProps = info.Split(new char[] {SEPARATOR}, 3);
			TaxiOperator newOperator;
			
			if(operatorProps.Length == 3)
			{
				newOperator = new TaxiOperator(operatorProps[0],
					operatorProps[1],
					operatorProps[2]);				
			}
			else
			{
				newOperator = new TaxiOperator(operatorProps[0],
					operatorProps[1], String.Empty);
			}

			//Determine if this stop has one or more accessible Operators
			if(newOperator.Accessible == true)
			{	
				this.accessibleOperatorPresent = true;
			}
			//Add the TaxiOperator to the collection
			operators.Add(newOperator);
		}

		/// <summary>
		/// Retrives the reference data for the current stop
		/// </summary>
		/// <param name="naptan">A unique identifier for the current stop</param>
		/// <returns>The reference data loaded into a DataSet</returns>
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
					(TDEventCategory.Business, TDTraceLevel.Error, "Exception on retrieving Stop info for " + naptan, ex);
				Logger.Write(oe);
				
				ds = null;
				throw;
			}

			return ds;
		}


		/// <summary>
		/// Retrives the reference data for the current stop from the AtosAdditionalData DB
		/// Used where GetDataSetForNaptan has found no results
		/// </summary>
		/// <param name="naptan">A unique identifier for the current stop</param>
		/// <returns>The reference data loaded into a DataSet</returns>
		private DataSet GetAtosDataSetForNaptan(string naptan)
		{
			DataSet ds = null;
			SqlHelper helper = new SqlHelper();

			try
			{
				// Initialise a SqlHelper and connect to the database.
				Logger.Write( new OperationalEvent( TDEventCategory.Business,
					TDTraceLevel.Info, "Opening database connection to " + SqlHelperDatabase.AtosAdditionalDataDB.ToString() ));
				
				helper.ConnOpen(SqlHelperDatabase.AtosAdditionalDataDB);
				
				if ( TDTraceSwitch.TraceVerbose ) 
				{
					Logger.Write( new OperationalEvent( TDEventCategory.Business,
						TDTraceLevel.Verbose, "Obtaining TrainTaxi data from "  + SqlHelperDatabase.AtosAdditionalDataDB.ToString()));
				}

				//Obtain dataset using usp_GetTrainTaxiData stored proc
				// Build the Hash tables for parameters and types
				Hashtable parameter = new Hashtable(1);
				parameter.Add("@Naptan",naptan);
				Hashtable parametertype = new Hashtable(1);
				parametertype.Add("@Naptan",SqlDbType.VarChar);

				// Execute the usp_GetTrainTaxiData stored procedure.
				// This returns a result set containing all the TrainTaxi details for this NaPTAN
				ds = helper.GetDataSet("usp_GetTrainTaxiData",parameter,parametertype);
				
				// Log the fact that the data was loaded.
				if ( TDTraceSwitch.TraceVerbose ) 
				{
					Logger.Write( new OperationalEvent( TDEventCategory.Business,
						TDTraceLevel.Verbose, "Successfully obtained TrainTaxi data from "  + SqlHelperDatabase.AtosAdditionalDataDB.ToString() ));
				}
			
			}
			catch (SqlException sqle)
			{
				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, "An SQL exception occurred in Obtaining TrainTaxi data from "  + SqlHelperDatabase.AtosAdditionalDataDB.ToString(), sqle));
			}
			finally
			{
				//close the database connection
				helper.ConnClose();
			}
			return ds;
		}

		/// <summary>
		/// Parses incoming reference data and processes the content to populate the class properties
		/// </summary>
		/// <param name="ds">A Dataset containing the reference data for the stop</param>
		private void LoadDataFromAtosDB(DataSet ds)
		{	
			if	(ds == null || ds.Tables[0].Rows.Count == 0) 
			{
				OperationalEvent oe = new OperationalEvent
					(TDEventCategory.Business, TDTraceLevel.Info, "No data available for " + stopNaptan);
				Logger.Write(oe);
				return;
			}

			foreach(DataRow row in ds.Tables[0].Rows)
			{
				if (this.stopNaptan == row[(int)AtosTaxiFields.NaptanCode].ToString())
				{
					//Naptan matches stop Naptan - its the stop rather than an alternative, or "GOTO link"

					if (this.stopName == null || this.stopName == "")
					{
						//Get name from the TrainTaxi data if not already found in the lookup
						this.stopName = row[(int)AtosTaxiFields.DestinationName].ToString();
					}
					//Handle the stop description
					this.comment = FormatCommentLine(row[(int)AtosTaxiFields.CommentLine].ToString(), row[(int)AtosTaxiFields.NaptanCode].ToString());	
				
					#region HandleTaxiOperators

					//Handle taxi operators
					string taxiOperator = String.Empty;
					if (row[(int)AtosTaxiFields.Firm1Name].ToString() != "")
					{
						taxiOperator = String.Empty;
						taxiOperator = row[(int)AtosTaxiFields.Firm1Name].ToString() + SEPARATOR +
							row[(int)AtosTaxiFields.Firm1Phone].ToString() + SEPARATOR +
							row[(int)AtosTaxiFields.Firm1Accessibility].ToString();
						taxiOperator = taxiOperator.Trim();
						AddTaxiOperator(taxiOperator);
					}
					if (row[(int)AtosTaxiFields.Firm2Name].ToString() != "")
					{
						taxiOperator = String.Empty;
						taxiOperator = row[(int)AtosTaxiFields.Firm2Name].ToString() + SEPARATOR +
							row[(int)AtosTaxiFields.Firm2Phone].ToString() + SEPARATOR +
							row[(int)AtosTaxiFields.Firm2Accessibility].ToString();
						taxiOperator = taxiOperator.Trim();
						AddTaxiOperator(taxiOperator);
					}
					if (row[(int)AtosTaxiFields.Firm3Name].ToString() != "")
					{
						taxiOperator = String.Empty;
						taxiOperator = row[(int)AtosTaxiFields.Firm3Name].ToString() + SEPARATOR +
							row[(int)AtosTaxiFields.Firm3Phone].ToString() + SEPARATOR +
							row[(int)AtosTaxiFields.Firm3Accessibility].ToString();
						taxiOperator = taxiOperator.Trim();
						AddTaxiOperator(taxiOperator);
					}
					if (row[(int)AtosTaxiFields.Firm4Name].ToString() != "")
					{
						taxiOperator = String.Empty;
						taxiOperator = row[(int)AtosTaxiFields.Firm4Name].ToString() + SEPARATOR +
							row[(int)AtosTaxiFields.Firm4Phone].ToString() + SEPARATOR +
							row[(int)AtosTaxiFields.Firm4Accessibility].ToString();
						taxiOperator = taxiOperator.Trim();
						AddTaxiOperator(taxiOperator);
					}
					#endregion

					//Handle the accessibility text
					//Make sure the parent stop does not overwrite accessible text if an alternative stop has already overwritten it
					if(accessibleText == String.Empty)
					{
						accessibleText = row[(int)AtosTaxiFields.AccessibilityText].ToString();
					}
				}
				else
				{
					//Naptan is not stop Naptan - must be an alternative stop
					if	(processAlternativeStops)
					{
						string alternativeStopInfo = String.Empty;
						alternativeStopInfo = row[(int)AtosTaxiFields.NaptanCode].ToString() + SEPARATOR + 
							row[(int)AtosTaxiFields.DestinationName].ToString();
						AddAlternativeStop(alternativeStopInfo);
					}
				}
			}
		}

		/// <summary>
		/// Helper function to create the stop description text
		/// </summary>
		/// <param name="suppliedComment">The textual commentary for the stop</param>
		/// <param name="naptanForRow">The unique identifier for the stop</param>
		/// <returns>The name of the stop and the textual commentary appended</returns>
		private string FormatCommentLine(string suppliedComment, string naptanForRow)
		{
			if (this.StopName == "" || this.StopName == null)
			{
				return addData.LookupStationNameForNaptan(naptanForRow) + " " + suppliedComment;	
			}
			else
			{
				//Trim word "airport" off start to avoid getting "Heathrow Airport airport..." etc.
				if (suppliedComment.Substring(0,7) == "airport")
				{
					suppliedComment = suppliedComment.Substring(7);
				}
				return this.StopName + " " + suppliedComment;	
			}
		}

		#endregion
	}
}
