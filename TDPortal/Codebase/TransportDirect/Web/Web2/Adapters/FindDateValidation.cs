// *********************************************** 
// NAME                 : FindDateValidation.cs 
// AUTHOR               : Esther Severn
// DATE CREATED         : 12/07/2004 
// DESCRIPTION          : Class providing methods   
//                        for date validation. 
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Adapters/FindDateValidation.cs-arc  $ 
//
//   Rev 1.2   Mar 31 2008 12:59:00   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:11:18   mturner
//Initial revision.
//
//   Rev 1.7   Feb 23 2006 19:16:08   build
//Automatically merged from branch for stream3129
//
//   Rev 1.6.1.1   Jan 30 2006 12:15:20   mdambrine
//add TDCultureInfo is now in the common namespace
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.6.1.0   Jan 10 2006 15:17:34   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.6   Nov 01 2005 15:12:04   build
//Automatically merged from branch for stream2638
//
//   Rev 1.5.1.2   Oct 26 2005 18:49:10   asinclair
//Updated comments
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.5.1.1   Oct 20 2005 10:41:06   asinclair
//Added UpdateJourneyParameters
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.5.1.0   Oct 11 2005 09:30:12   asinclair
//Added IsAnyOutwardError and IsAnyReturnError
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.5   Aug 17 2004 17:58:04   RHopkins
//IR1324  Change Extension date (stopover) error checks so that they check for standard date errors as well as Extension date errors, because when the stopover control is submited it updates the standard dates, which then generate standard date errors.
//
//   Rev 1.4   Aug 02 2004 17:52:20   JHaydock
//The ReturnBeforeOutward method was returning the opposite value than expected
//
//   Rev 1.3   Jul 27 2004 17:06:20   esevern
//added further date validation methods required for JourneyPlannerAmbiguity
//
//   Rev 1.2   Jul 15 2004 12:06:02   esevern
//altered method name (consistent with JPAmbiguity page and FindFlightInput)
//
//   Rev 1.1   Jul 13 2004 12:55:02   esevern
//interim check-in to allow controls to be added to pages
//
//   Rev 1.0   Jul 12 2004 10:21:28   esevern
//Initial revision.

using System;using TransportDirect.Common.ResourceManager;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.Common;
using TransportDirect.UserPortal.Web.Controls;

namespace TransportDirect.UserPortal.Web.Adapters
{
	/// <summary>
	/// Summary description for FindDateValidation.
	/// </summary>
	public class FindDateValidation
	{
		
		/// <summary>
		///  Read only property returning true if outward datetime is
		///  valid, false otherwise
		/// </summary>
		public static bool IsValidOutward
		{
			get 
			{ 
			
				return !TDSessionManager.Current.ValidationError.Contains(
					ValidationErrorID.OutwardDateTimeInvalid);
			}
		}
        

		/// <summary>
		///  Read only property returning true if return datetime is
		///  valid, false otherwise
		/// </summary>
		public static bool IsValidReturn
		{
			get 
			{ 
				return !TDSessionManager.Current.ValidationError.Contains(
					ValidationErrorID.ReturnDateTimeInvalid); 
			}
			
		}

		/// <summary>
		///  Read only property returning true if outward and return 
		///  datetimes are valid, false otherwise
		/// </summary>
		public static bool AreDatesValid
		{
			get 
			{ 
				return !TDSessionManager.Current.ValidationError.Contains(
					ValidationErrorID.OutwardAndReturnDateTimeInvalid); 
			}
		}

		/// <summary>
		/// Read only property, returning true if the outward
		/// date is in the past
		/// </summary>
		public static bool OutwardDateInPast 
		{
			get 
			{
				return TDSessionManager.Current.ValidationError.Contains(
					ValidationErrorID.OutwardDateTimeNotLaterThanNow);
			}
		}

		/// <summary>
		/// Read only property, returning true if the return
		/// date is in the past
		/// </summary>
		public static bool ReturnDateInPast		
		{
			get 
			{
				return TDSessionManager.Current.ValidationError.Contains(
					ValidationErrorID.ReturnDateTimeNotLaterThanNow);
			}
		}

		/// <summary>
		/// Read only property, returning true if the outward
		/// and return dates are in the past
		/// </summary>
		public static bool AreDatesPast
		{
			get 
			{
				return TDSessionManager.Current.ValidationError.Contains(
					ValidationErrorID.OutwardAndReturnDateTimeNotLaterThanNow);
			}
		}

		/// <summary>
		/// Read only property, returning true if the return date selected 
		/// is after the outward date selected
		/// </summary>
		public static bool ReturnBeforeOutward 
		{
			get 
			{
				return TDSessionManager.Current.ValidationError.Contains(
					ValidationErrorID.ReturnDateTimeNotLaterThanOutwardDateTime);
			}
		}

		/// <summary>
		/// Read only property, returning true if the return
		/// month is missing
		/// </summary>
		public static bool ReturnMonthMissing 
		{
			get 
			{
				return (TDSessionManager.Current.ValidationError.Contains(
					ValidationErrorID.ReturnMonthMissing));
			}
		}

		/// <summary>
		/// Read only property, returning true if the return
		/// time is missing
		/// </summary>
		public static bool ReturnTimeMissing 
		{
			get 
			{
				return (TDSessionManager.Current.ValidationError.Contains(
					ValidationErrorID.ReturnTimeMissing));
			}
		}

		/// <summary>
		/// Read only property, returning true if the return
		/// date is missing
		/// </summary>
		public static bool ReturnDateMissing 
		{
			get 
			{
				return (TDSessionManager.Current.ValidationError.Contains(
					ValidationErrorID.ReturnDateMissing));
			}
		}

		/// <summary>
		/// Read only property, returning true if the extend to  
		/// start outward arrive/leave by time is in the past
		/// </summary>
		public static bool OutwardExtensionToStartInPast
		{
			get
			{
				return TDSessionManager.Current.ValidationError.Contains(
						ValidationErrorID.ExtendToStartOutwardInPast) ||
					TDSessionManager.Current.ValidationError.Contains(
						ValidationErrorID.OutwardDateTimeNotLaterThanNow) ||
					TDSessionManager.Current.ValidationError.Contains(
						ValidationErrorID.OutwardAndReturnDateTimeNotLaterThanNow);
			}
		}

		/// <summary>
		/// Read only property, returning true if the extend from 
		/// end outward arrive/leave by time is in the past
		/// </summary>
		public static bool OutwardExtensionFromEndInPast 
		{
			get
			{
				return TDSessionManager.Current.ValidationError.Contains(
						ValidationErrorID.ExtendFromEndOutwardInPast) ||
					TDSessionManager.Current.ValidationError.Contains(
						ValidationErrorID.OutwardDateTimeNotLaterThanNow) ||
					TDSessionManager.Current.ValidationError.Contains(
						ValidationErrorID.OutwardAndReturnDateTimeNotLaterThanNow);
			}
		}

		/// <summary>
		/// Read only property, returning true if the extend to  
		/// start return arrive/leave by time is in the past
		/// </summary>
		public static bool ReturnExtensionToStartInPast
		{   
			get
			{
				return TDSessionManager.Current.ValidationError.Contains(
						ValidationErrorID.ExtendToStartReturnInPast) ||
					TDSessionManager.Current.ValidationError.Contains(
						ValidationErrorID.ReturnDateTimeNotLaterThanNow) ||
					TDSessionManager.Current.ValidationError.Contains(
						ValidationErrorID.OutwardAndReturnDateTimeNotLaterThanNow);
			}
		}

		/// <summary>
		/// Read only property, returning true if the extend from  
		/// end return arrive/leave by time is in the past
		/// </summary>
		public static bool ReturnExtensionFromEndInPast 
		{
			get 
			{
				return TDSessionManager.Current.ValidationError.Contains(
						ValidationErrorID.ExtendFromEndReturnInPast) ||
					TDSessionManager.Current.ValidationError.Contains(
						ValidationErrorID.ReturnDateTimeNotLaterThanNow) ||
					TDSessionManager.Current.ValidationError.Contains(
						ValidationErrorID.OutwardAndReturnDateTimeNotLaterThanNow);
			}
		}

		/// <summary>
		/// Read only property, returning true if an extension  
		/// outward or return arrive/leave by time is in the past
		/// </summary>
		public static bool ExtendIntoPast
		{   
			get
			{
				ValidationError errors = TDSessionManager.Current.ValidationError;
				return 
					errors.Contains( ValidationErrorID.ExtendFromEndOutwardInPast ) || 
					errors.Contains( ValidationErrorID.ExtendFromEndReturnInPast ) || 
					errors.Contains( ValidationErrorID.ExtendToStartOutwardInPast ) || 
					errors.Contains( ValidationErrorID.ExtendToStartReturnInPast );
			}
		}

		/// <summary>
		/// Read only property, returning true if an extension  
		/// (either outward or return) to start date is in the past
		/// </summary>
		public static bool ExtendStartInPast 
		{
			get 
			{
				ValidationError errors = TDSessionManager.Current.ValidationError;
				return 
					errors.Contains( ValidationErrorID.ExtendToStartOutwardInPast ) ||
					errors.Contains( ValidationErrorID.ExtendToStartReturnInPast ) || 
					errors.Contains( ValidationErrorID.OutwardAndReturnDateTimeNotLaterThanNow );
			}
		}

		/// <summary>
		/// Read only property, returning true if no return journey requested
		/// </summary>
		public static bool NoReturn 
		{
			get 
			{
				return ( TDSessionManager.Current.JourneyParameters.ReturnMonthYear 
					== Enum.GetName(typeof (ReturnType), ReturnType.NoReturn) )
					|| ( TDSessionManager.Current.JourneyParameters.ReturnMonthYear
					== Enum.GetName(typeof (ReturnType), ReturnType.OpenReturn) ) ;
			}
		}

		/// <summary>
		/// Read only property, returning true if any validation errors exist
		/// </summary>
		public static bool ErrorsExist 
		{
			get 
			{
				if(TDSessionManager.Current.ValidationError.MessageIDs.Count > 0)
					return true;
				else
					return false;
			}
		}

		/// <summary>
		/// Read only property, returning true if outward date errors 
		/// (date invalid, return before outward, date in past) exist
		/// </summary>
		public static bool OutwardDateErrors 
		{
			get 
			{
				ValidationError errors = TDSessionManager.Current.ValidationError;
				if(errors.Contains(ValidationErrorID.ReturnDateTimeNotLaterThanOutwardDateTime)
					|| errors.Contains(ValidationErrorID.OutwardDateTimeNotLaterThanNow )
					|| errors.Contains(ValidationErrorID.OutwardAndReturnDateTimeNotLaterThanNow ) 
					|| errors.Contains(ValidationErrorID.OutwardAndReturnDateTimeInvalid ) 
					|| errors.Contains(ValidationErrorID.OutwardDateTimeInvalid ) )
				{
					return true;
				}
				else 
				{
					return false;
				}
			}  
		}


		/// <summary>
		/// Read only property, returning true if return date errors 
		/// (date invalid, return before outward, date in past) exist
		/// </summary>
		public static bool ReturnDateErrors 
		{
			get 
			{
				ValidationError errors = TDSessionManager.Current.ValidationError;
				if(errors.Contains(ValidationErrorID.ReturnDateTimeNotLaterThanOutwardDateTime)
					|| errors.Contains(ValidationErrorID.OutwardAndReturnDateTimeNotLaterThanNow )
 					|| errors.Contains(ValidationErrorID.ReturnDateTimeNotLaterThanNow )
					|| errors.Contains(ValidationErrorID.ReturnDateMissing )
					|| errors.Contains(ValidationErrorID.ReturnMonthMissing )
					|| errors.Contains(ValidationErrorID.ReturnTimeMissing )
					|| errors.Contains(ValidationErrorID.OutwardAndReturnDateTimeInvalid ) 
					|| errors.Contains(ValidationErrorID.ReturnDateTimeInvalid ) )
				{
					return true;
				}
				else 
				{
					return false;
				}
			}
		} 

		/// <summary>
		/// Read only property, returning true if the outward arrive/leave by time 
		/// or return arrive/leave by time is in the past
		/// </summary>
        public static bool IsOutwardAndReturnExtensionStartInPast
        {   
            get
            {
                return TDSessionManager.Current.ValidationError.Contains(
					ValidationErrorID.OutwardAndReturnDateTimeNotLaterThanNow);
            }
        }

		/// <summary>
		/// Read only property, returning true if the outward and return 
		/// arrive/leave by times overlap 
		/// </summary>
		public static bool IsExtensionReturnOverlap
        {   
            get
            {
                ValidationError errors = TDSessionManager.Current.ValidationError;
				return (errors.Contains(ValidationErrorID.ReturnDateTimeNotLaterThanOutwardDateTime) || 
                    errors.Contains(ValidationErrorID.OutwardAndReturnDateTimeInvalid));
            }
        }

		/// <summary>
		/// Read only property, returning true if there is an error in the outward date 
		/// </summary>
		public static bool IsAnyOutwardError
		{   
			get 
			{
				
				if(TDSessionManager.Current.ValidationError.Contains(
					ValidationErrorID.OutwardDateTimeInvalid)
					|| TDSessionManager.Current.ValidationError.Contains(
					ValidationErrorID.OutwardAndReturnDateTimeInvalid)
					|| TDSessionManager.Current.ValidationError.Contains(
					ValidationErrorID.ReturnDateTimeNotLaterThanOutwardDateTime)
					|| TDSessionManager.Current.ValidationError.Contains(
					ValidationErrorID.OutwardDateTimeNotLaterThanNow)
					|| TDSessionManager.Current.ValidationError.Contains(
					ValidationErrorID.OutwardAndReturnDateTimeNotLaterThanNow))
				{
					return true;
				}
				else 
				{
					return false;
				}
			}
		}

		/// <summary>
		/// Read only property, returning true if there is an error in the return date 
		/// </summary>
		public static bool IsAnyReturnError
		{   
			get 
			{
				
				if(TDSessionManager.Current.ValidationError.Contains(
					ValidationErrorID.ReturnDateTimeInvalid)
					|| TDSessionManager.Current.ValidationError.Contains(
					ValidationErrorID.OutwardAndReturnDateTimeInvalid)
					|| TDSessionManager.Current.ValidationError.Contains(
					ValidationErrorID.ReturnDateMissing)
					|| TDSessionManager.Current.ValidationError.Contains(
					ValidationErrorID.ReturnMonthMissing)
					|| TDSessionManager.Current.ValidationError.Contains(
					ValidationErrorID.ReturnTimeMissing)
					|| TDSessionManager.Current.ValidationError.Contains(
					ValidationErrorID.ReturnDateTimeNotLaterThanNow)
					||TDSessionManager.Current.ValidationError.Contains(
					ValidationErrorID.ReturnDateTimeNotLaterThanOutwardDateTime)
					|| TDSessionManager.Current.ValidationError.Contains(
					ValidationErrorID.OutwardAndReturnDateTimeNotLaterThanNow))
				{
					return true;
				}
				else 
				{
					return false;
				}
			}
		}

		/// <summary>
		///  Update journey parameters from the AmbiguousDateSelectControl
		/// </summary>
		/// <param name="journeyParams">TDJourneyParameters</param>
		 /// <param name="control">AmbiguousDateSelectControl</param>
		public void UpdateJourneyParameters(TDJourneyParameters journeyParams, AmbiguousDateSelectControl control)  
		{
			journeyParams.OutwardDayOfMonth =	control.Day;
			journeyParams.OutwardMonthYear =	control.MonthYear;
			journeyParams.OutwardAnyTime = control.ControlHours.SelectedItem.Value == Adapters.FindInputAdapter.AnyTimeValue;
			journeyParams.OutwardHour = control.Hour;
			journeyParams.OutwardMinute = control.Minute;

		}
																												
	}
}
