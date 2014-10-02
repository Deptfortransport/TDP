//********************************************************************************
//NAME         : ValidateSpecificFareRequest.cs
//AUTHOR       : SchlumbergerSema
//DATE CREATED : 10/05/2003
//DESCRIPTION  : 
//             : 
//DESIGN DOC   : DD034 Reatil Pricing
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/RetailBusinessObjects/ValidateSpecificFareRequest.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:46:28   mturner
//Initial revision.
//
//   Rev 1.7   Mar 22 2005 16:09:20   RPhilpott
//Addition of cost-based search for Del 7.
//
//   Rev 1.6   Apr 15 2004 17:06:16   CHosegood
//Was adding unnecessary padding to BO call
//Resolution for 663: Rail fares not being displayed
//
//   Rev 1.5   Jan 13 2004 13:13:18   CHosegood
//Now checks to see if a negative length output size has been supplied and throws an excpetion if this is the case.
//Resolution for 594: BO request do not check for a negative length output buffer.
//
//   Rev 1.4   Nov 07 2003 10:19:54   CHosegood
//Changed IgnoreSingles flag to Y
//
//   Rev 1.3   Oct 17 2003 10:15:10   CHosegood
//Ticket type is now a JourneyType
//
//   Rev 1.2   Oct 15 2003 20:10:12   CHosegood
//Not notifies the RBO if it is an outgoing or a return train
//
//   Rev 1.1   Oct 14 2003 12:26:44   CHosegood
//Intermediate version.  Not ready for build/test
//
//   Rev 1.0   Oct 13 2003 15:25:58   CHosegood
//Initial Revision

#region using
using System;
using System.Diagnostics;
using System.Text;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.UserPortal.PricingMessages;
#endregion

namespace TransportDirect.UserPortal.RetailBusinessObjects
{
	/// <summary>
	/// Summary description for ValidateSpecificFareRequest.
	/// </summary>
    public class ValidateSpecificFareRequest : BusinessObjectInput
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="interfaceVersion"></param>
        /// <param name="outputLength"></param>
        /// <param name="request"></param>
        /// <param name="fare"></param>
        public ValidateSpecificFareRequest(string interfaceVersion,
            int outputLength, PricingRequestDto request, Fare fare )
            : base("RE", "GB", interfaceVersion )
        {

            if ( outputLength < 0 ) 
            {
                string msg = "Output Lenght must be a non-negative number.  Output Length supplied::" + outputLength.ToString();
                TDException tde = new TDException(msg, true, TDExceptionIdentifier.PRHOutputLengthMustBeNonNegative );
                Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Error, msg, tde ) );
                throw tde;
            }

            HeaderInputParameter inputParameter = new HeaderInputParameter( this.BuildValidateSpecificFareRequest( request, fare ) );
            this.AddInputParameter( inputParameter, 0 );

            HeaderOutputParameter outputParameter = new HeaderOutputParameter( outputLength );
            this.AddOutputParameter( outputParameter, 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="fare"></param>
        /// <returns></returns>
        protected string BuildValidateSpecificFareRequest
            ( PricingRequestDto request, Fare fare) 
        {

            StringBuilder sb = new StringBuilder();
            sb.Append( request.Origin.Nlc );        //Origin NLC
            sb.Append( request.Origin.Crs );        //Origin CRS
            sb.Append( " " );                       //Origin country
            sb.Append( request.Destination.Nlc );   //Destination NLC
            sb.Append( request.Destination.Crs );   //destination CRS
            sb.Append( request.OutwardDate.ToString("yyyyMMdd") );       //outward date
            if ( request.ReturnDate == null ) 
            {
                sb.Append( "        " );            //return date
                sb.Append( "        " );            //alt return date
            } 
            else 
            {
                sb.Append( request.ReturnDate.ToString("yyyyMMdd") );    //return date
                sb.Append( request.ReturnDate.ToString("yyyyMMdd") );    //alt return date
            }
            sb.Append( "O" );                       //out-ret-ind   :: NOT USED
            sb.Append( fare.TicketType.Code );           //ticket type
            sb.Append( fare.Route.Code );            //route code
            sb.Append( "     " );                   //alt route code
            sb.Append( fare.TicketValidityCode );   //ticket validity code
            //ticket-repeat ( single or return )
            if ( fare.TicketType.Type.Equals(JourneyType.OutwardSingle) ) 
            {
                sb.Append( "S" );
            }
            else 
            {
                sb.Append( "R" );
            }
            sb.Append( fare.RestrictionCode );      //restriction code
            sb.Append( fare.RailcardCode );         //railcard code
            sb.Append( "T" );                       //TOC-OR-SECTOR-IND
            sb.Append( "Y" );                       //Ignore Singles
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
                    TrainDto train = request.Trains[i] as TrainDto;
                    //outbound or return indicator
                    if ( Enum.Parse( typeof(ReturnIndicator), train.ReturnIndicator.ToString(), true).Equals(ReturnIndicator.Outbound) ) 
                    {
                        sb.Append( "O");
                    } 
                    else if ( Enum.Parse( typeof(ReturnIndicator), train.ReturnIndicator.ToString(), true).Equals(ReturnIndicator.Return) ) 
                    {
                        sb.Append( "R" );
                    }
                    else 
                    {
                        sb.Append( " " );
                    }
                    //Train unique identifier
                    sb.Append( train.Uid.PadLeft(6, '0') );
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
                    sb.Append( train.TrainClass );
                    //Sleeper accommodation code of train
                    sb.Append( train.Sleeper );
                    //TOC code(s) of train operator
                    sb.Append( train.Tocs[0].Code );
                    sb.Append( train.Tocs[1].Code );
                    //sector : using TOC instead
                    sb.Append( " " );
                }   //if-else
            }   //for

            return sb.ToString();
        }   //build
    }   //class
}   //namespace
