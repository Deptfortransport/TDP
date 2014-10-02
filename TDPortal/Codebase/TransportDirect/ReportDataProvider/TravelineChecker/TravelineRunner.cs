// *********************************************** 
// NAME                 : TravelineChecker.cs
// AUTHOR               : Patrick ASSUIED
// DATE CREATED         : 15/11/2004 
// DESCRIPTION  : This class accesses the given travelines and runs check 
// journeyRequests return journes for each of them
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/TravelineChecker/TravelineRunner.cs-arc  $ 
//
//   Rev 1.1   Mar 16 2009 12:24:06   build
//Automatically merged from branch for stream5215
//
//   Rev 1.0.1.1   Feb 19 2009 15:40:58   mturner
//Changes to implement via proxy server functionality.
//
//   Rev 1.0.1.0   Jan 12 2009 16:06:38   mturner
//Removed IsAim flag as no longer needed
//Resolution for 5215: Workstream for RS620
//
//   Rev 1.0   Nov 08 2007 12:40:48   mturner
//Initial revision.
//
//   Rev 1.3   Nov 25 2004 13:20:42   passuied
//changes so throwing an exception while checking a traveline doesn't stop the whole process but just sends a warning and report the faulty traveline.
//
//   Rev 1.2   Nov 17 2004 16:00:10   passuied
//changes for FxCop
//
//   Rev 1.1   Nov 17 2004 11:26:24   passuied
//First working version
//
//   Rev 1.0   Nov 15 2004 17:32:46   passuied
//Initial revision.


using System;
using System.Text;
using System.Collections;
using System.Diagnostics;

using Logger = System.Diagnostics.Trace;

using TransportDirect.Common;	
using TransportDirect.Common.Logging;
using TransportDirect.ReportDataProvider.TransactionHelper;



namespace TransportDirect.ReportDataProvider.TravelineChecker
{
	/// <summary>
	/// This component accesses the given travelines and checks 
	/// journeyRequests return journes for each of them
	/// </summary>
	public abstract class TravelineRunner
	{

		/// <summary>
		/// Runs through traveline and  check them
		/// </summary>
		/// <param name="request"></param>
		/// <param name="result"></param>
		/// <returns></returns>
		public static bool Run(RequestTravelineParams request, out string result)
		{
			bool success = true;

			// Build Journey Web Request
			TLJourneyWebRequest jwr = new TLJourneyWebRequest(
				request.OriginLocation, 
				request.DestinationLocation);

			// for each traveline, check if journey is working
			ArrayList faultyTravelines = new ArrayList( request.Travelines.Length);
			foreach ( Traveline tl in request.Travelines)
			{

				try
				{
					ITLChecker checker = null;
					checker = new TLChecker();

					// Initialise checker
					checker.JourneyWebRequest = jwr;
					checker.TravelineUrl = tl.Url;
                    checker.UseProxy = tl.UseProxy;
                    checker.ProxyUrl = tl.ProxyName;

					try
					{
						// if Check method returns false, add faulty traveline to list
						if (checker.Check() == 0)
						{
							faultyTravelines.Add(tl);
							success = false;
						}
					}
						// if exception thrown trying to access traveline, consider it as faulty bu log it as an error.
					catch (TDException e)
					{
						Logger.Write(
							new OperationalEvent(TDEventCategory.Infrastructure, 
													TDTraceLevel.Warning, 
													e.Message)
							);									

						faultyTravelines.Add(tl);
						success = false;
					}

				}
				catch (TDException e)
				{
					result = string.Format(Messages.Travelinecheck_Error, tl.Url, e.Message);
					return false;
				}
				
			}

			if (success)
			{
				result = Messages.TravelineCheck_Success;
			}
			else
			{
				result = string.Format(Messages.TravelineCheck_Failed,  
					GetFaultyTravelineList(faultyTravelines));
			}


			return success;
		
		}

		/// <summary>
		/// Gets a string description list of the given traveline
		/// </summary>
		/// <param name="faultyTravelines">Array list to create the string from</param>
		/// <returns></returns>
		private static string GetFaultyTravelineList( ArrayList faultyTravelines)
		{
			StringBuilder sb = new StringBuilder();
			const string format = "\nTraveline Url=[{0}]";
			foreach ( Traveline tl in faultyTravelines)
			{
				sb.AppendFormat(format, tl.Url);
			}

			return sb.ToString();
		}
	}
}
