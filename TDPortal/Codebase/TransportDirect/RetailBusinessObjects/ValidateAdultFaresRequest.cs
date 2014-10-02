//********************************************************************************
//NAME         : ValidateAdultFaresRequest.cs
//AUTHOR       : SchlumbergerSema
//DATE CREATED : 10/05/2003
//DESCRIPTION  : 
//             : 
//DESIGN DOC   : DD034 Reatil Pricing
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/RetailBusinessObjects/ValidateAdultFaresRequest.cs-arc  $
//
//   Rev 1.1   Feb 18 2009 18:20:34   mmodi
//Updated following changes to use interface 0202
//Resolution for 5210: CCN487 - ZPBO Implementation workstream
//
//   Rev 1.0   Nov 08 2007 12:46:26   mturner
//Initial revision.
//
//   Rev 1.8   Apr 23 2007 16:15:38   asinclair
//Tidy up of if statement
//
//   Rev 1.7   Apr 23 2007 15:07:32   asinclair
//If validating an Outward fare, then ensure that only the outward train is taken into account - as a different TOC being used for the inward leg will cause TOC fares for the outward to be restricted.
//
//   Rev 1.6   Mar 22 2005 16:09:20   RPhilpott
//Addition of cost-based search for Del 7.
//
//   Rev 1.5   Jan 13 2004 13:13:24   CHosegood
//Now checks to see if a negative length output size has been supplied and throws an excpetion if this is the case.
//Resolution for 594: BO request do not check for a negative length output buffer.
//
//   Rev 1.4   Nov 07 2003 10:19:56   CHosegood
//Changed IgnoreSingles flag to Y
//
//   Rev 1.3   Oct 17 2003 10:14:06   CHosegood
//changed journey type
//
//   Rev 1.2   Oct 15 2003 20:06:26   CHosegood
//Now notifies the RBO if it is an outgoing or a return train
//
//   Rev 1.1   Oct 14 2003 12:26:40   CHosegood
//Intermediate version.  Not ready for build/test
//
//   Rev 1.0   Oct 13 2003 15:25:54   CHosegood
//Initial Revision

#region using
using System;
using System.Collections;
using System.Diagnostics;
using System.Text;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.UserPortal.PricingMessages;
#endregion

namespace TransportDirect.UserPortal.RetailBusinessObjects
{

    /// <summary>
    /// 
    /// </summary>
    public class ValidateAdultFaresRequest : BusinessObjectInput
    {
		//This is used to indicate that we only want to validate an Outward Journey.
		private bool isOutwardOnly;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="interfaceVersion"></param>
        /// <param name="outputLength"></param>
        /// <param name="request"></param>
        /// <param name="fares"></param>
        public ValidateAdultFaresRequest(string interfaceVersion,
            int outputLength, PricingRequestDto request, Fares fares, bool isOutwardOnly )
            : base("RE", "GA", interfaceVersion )
        {
			this.isOutwardOnly = isOutwardOnly;


            if ( outputLength < 0 ) 
            {
                string msg = "Output Lenght must be a non-negative number.  Output Length supplied::" + outputLength.ToString();
                TDException tde = new TDException(msg, true, TDExceptionIdentifier.PRHOutputLengthMustBeNonNegative );
                Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Error, msg, tde ) );
                throw tde;
            }

            HeaderInputParameter inputParameter = new HeaderInputParameter( this.BuildValidateAdultFaresRequest( request, fares ) );
            this.AddInputParameter( inputParameter, 0 );

            HeaderOutputParameter outputParameter = new HeaderOutputParameter( outputLength );
            this.AddOutputParameter( outputParameter, 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="fares"></param>
        /// <returns></returns>
        protected string BuildValidateAdultFaresRequest
            ( PricingRequestDto request, Fares fares) 
        {
            StringBuilder sb = new StringBuilder();
            sb.Append( request.Origin.Crs );        //Origin CRS
            sb.Append( " " );                       //Origin country
            sb.Append( request.Destination.Crs );   //destination CRS
            //Outward Date
            sb.Append( request.OutwardDate.ToString("yyyyMMdd") );
            // if the return date is known then append it to the StringBuilder
            sb.Append( request.ReturnDate==null?"        ":request.ReturnDate.ToString("yyyyMMdd") );
            sb.Append( "O" );                       //outbound reutrn indicator : not used
            sb.Append( "     " );                   //route-code : not used
            sb.Append( "     " );                   //alt route code : not used
            sb.Append( "T" );                       //TOC-OR-SECTOR-IND
            //ignore -singles (singles invalid if a return date is specified)
            sb.Append( "Y" );
            //The number of train enteries to follow
			sb.Append( request.Trains.Count.ToString().PadLeft(4, '0') );


            for ( int i = 0; i < 20; i++ )
            {
                if ( (request.Trains.Count == 0) || (i >= request.Trains.Count ) )
                {
                    sb.Append( "                                   " );
                } 
                else 
                { 
					bool appendTrainDetails;

                    TrainDto train = request.Trains[i] as TrainDto;
                    //outbound or return indicator
                    if ( Enum.Parse( typeof(ReturnIndicator), train.ReturnIndicator.ToString(), true).Equals(ReturnIndicator.Outbound) ) 
                    {
                        sb.Append( "O");
						appendTrainDetails = true;


                    } 
                    else if ( Enum.Parse( typeof(ReturnIndicator), train.ReturnIndicator.ToString(), true).Equals(ReturnIndicator.Return) ) 
                    {
						//In this case we don't want to validate any return (i.e. Inward) trains, 
						//as the Outward Single tickets will also be restricted.  This will cause problems
						//when different TOC's for outward and inward journeys offer different tickets.
						if (isOutwardOnly)
						{
							sb.Append( " " );
							appendTrainDetails = false;
						}
						else
						{
							sb.Append( "R" );
							appendTrainDetails = true;

						}
                    } 
                    else 
                    {
                        sb.Append( " " );
						appendTrainDetails = true;

                    }

					if(appendTrainDetails)
					{

						//Train unique identifier
						sb.Append( train.Uid.PadLeft(6, ' ') );
						//CRS of start of leg
						sb.Append( train.Board.Location.Crs.PadLeft(3,' ') );
						//CRS of end of leg
						sb.Append( train.Alight.Location.Crs.PadLeft(3,' ') );
						//Departure time
						sb.Append( train.Board.Departure.Hour.ToString().PadLeft(2,'0') + train.Board.Departure.Minute.ToString().PadLeft(2,'0') );
						//Arrival time
						sb.Append( train.Alight.Arrival.Hour.ToString().PadLeft(2,'0') + train.Alight.Arrival.Minute.ToString().PadLeft(2,'0') );
						//CRS of train destination
						sb.Append( train.Destination.Location.Crs.PadLeft(3,' ') );
						//Arrival time at train destination
						sb.Append( train.Destination.Arrival.Hour.ToString().PadLeft(2,'0') + train.Destination.Arrival.Minute.ToString().PadLeft(2,'0') );
						//Train class
						sb.Append( train.TrainClass.PadLeft(1,' ') );
						//Sleeper accommodation code of train
						sb.Append( train.Sleeper.PadLeft(1,' ')  );
						//TOC code(s) of train operator
						sb.Append( train.Tocs[0].Code.PadLeft(2,' ') );
						sb.Append( train.Tocs[1].Code.PadLeft(2,' ') );
						//sector : using TOC instead
						sb.Append( " " );
					}
					else
					{
						sb.Append("                                  " );
					}

                }   //if-else
            }

            ArrayList routes = new ArrayList();
            StringBuilder sb2 = new StringBuilder();
            sb2.Append( fares.Item.Count.ToString().PadLeft(4,'0') );
            foreach (Fare fare in fares.Item) 
            {
                if ( !routes.Contains( fare.Route.Code ) ) 
                {
                    routes.Add( fare.Route.Code );
                }

                sb2.Append( fare.TicketType.Code.PadLeft(3,' ') );
                sb2.Append( (routes.IndexOf( fare.Route.Code )+1).ToString().PadLeft(2,'0') );
                sb2.Append( fare.RestrictionCode.PadLeft(2,' ') );
                //ticket-repeat ( single or return )
                if ( fare.TicketType.Type.Equals(JourneyType.OutwardSingle) ) 
                {
                    sb2.Append( "S" );
                }
                else 
                {
                    sb2.Append( "R" );
                }
                sb2.Append( fare.TicketValidityCode.PadLeft(2,' ') );
            }

            // Interface version 0202 and later allows 99 route codes (otherwise 25)
            for ( int i=0;i< 99; i++) 
            {
                if ( (routes.Count == 0) || (i >= routes.Count ) )
                {
                    sb.Append( "     " );
                } 
                else 
                {
                    sb.Append( routes[i].ToString().PadLeft(5,' ') );
                }
            }

            sb.Append( sb2.ToString() );

            return sb.ToString();
        }
    }
}
