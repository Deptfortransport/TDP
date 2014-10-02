// *********************************************** 
// NAME                 :  
// AUTHOR               : Patrick ASSUIED
// DATE CREATED         : /2004 
// DESCRIPTION  : Provides class for a travelinechecker
// request reference transaction.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/TransactionInjector/TravelineCheckerTransaction.cs-arc  $ 
//
//   Rev 1.1   Mar 16 2009 12:24:06   build
//Automatically merged from branch for stream5215
//
//   Rev 1.0.1.1   Jan 13 2009 17:36:02   mturner
//Added logic to 'skip' transactions when neccessary
//
//   Rev 1.0.1.0   Jan 13 2009 14:59:04   mturner
//Updates for tech refresh
//Resolution for 5215: Workstream for RS620
//
//   Rev 1.0   Nov 08 2007 12:40:16   mturner
//Initial revision.
//
//   Rev 1.0   Nov 17 2004 11:17:26   passuied
//Initial revision.


using System;

using TransportDirect.UserPortal.LocationService;
using TransportDirect.ReportDataProvider.TransactionHelper;
using TransportDirect.ReportDataProvider.TravelineChecker;

namespace TransportDirect.ReportDataProvider.TransactionInjector
{
	/// <summary>
	/// Provides class for a travelinechecker
	/// request reference transaction.
	/// </summary>
	[Serializable]
	public class TravelineCheckerTransaction : TDTransaction
	{
		private TDLocation originLocation;
		private TDLocation destinationLocation;
		private Traveline[] travelines;

        private int frequency;
        private int offset;


		/// <summary>
		/// Read-Write property. Enables serialisation of transaction
		/// </summary>
		public TDLocation OriginLocation
		{
			get
			{
				return originLocation;
			}
			set
			{
				originLocation = value;
			}
		}

		/// <summary>
		/// Read-Write property. Enables serialisation of transaction
		/// </summary>
		public TDLocation DestinationLocation
		{
			get
			{
				return destinationLocation;
			}
			set
			{
				destinationLocation = value;
			}
		}

		/// <summary>
		/// Read-Write property. Enables serialisation of transaction
		/// </summary>
		public Traveline[] Travelines
		{
			get
			{
				return travelines;
			}
			set
			{
				travelines = value;
			}
		}

        /// <summary>
        /// Read-Write property. Sets frequency of transaction
        /// </summary>
        public int Frequency
        {
            get
            {
                return frequency;
            }
            set
            {
                frequency = value;
            }
        }

        /// <summary>
        /// Read-Write property. Sets offset of transaction
        /// </summary>
        public int Offset
        {
            get
            {
                return offset;
            }
            set
            {
                offset = value;
            }
        }

		/// <summary>
		/// Default constructor
		/// </summary>
		public TravelineCheckerTransaction() :base ()
		{
			
		}


		/// <summary>
		/// Executes the query, and logs the time take to run the query.
		/// </summary>
        public override void ExecuteTransaction()
        {
            if ((DateTime.Now.Minute - offset) % frequency == 0)
            {
                try
                {
                    string resultData = String.Empty;

                    // Using transaction properties, construct parameters to call web service.
                    RequestTravelineParams reqTraveline = new RequestTravelineParams();

                    reqTraveline.OriginLocation = this.OriginLocation;
                    reqTraveline.DestinationLocation = this.DestinationLocation;
                    reqTraveline.Travelines = this.Travelines;


                    bool success = false;

                    // Log the start of the call.
                    LogStart();


                    success = TravelineRunner.Run(reqTraveline, out resultData);


                    if (success)
                        LogEnd(resultData);
                    else
                        LogBadEnd(resultData);

                }
                catch (Exception exception)
                {
                    LogBadEnd(String.Format(Messages.Service_TravelineCheckerException, this.Category.ToString(), exception.Message));
                }
            }
            else
            {
                LogSkipped();
            }
        }
	}
}
