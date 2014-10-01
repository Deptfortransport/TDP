// *********************************************** 
// NAME                 : TaxiInformationAssembler.cs
// AUTHOR               : Sanjeev Chand
// DATE CREATED         : 16/01/2006 
// DESCRIPTION  		: Assembler class for converting taxi information domain type to DTO type 
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/TaxiInformation/V1/TaxiInformationAssembler.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:22:48   mturner
//Initial revision.
//
//   Rev 1.2   Jan 20 2006 19:38:40   schand
//Added comment and serialisation attribute
//Resolution for 3454: DEL 8.1 Stream: IR for Module assocaitions for Mobile Service TD111
//
//   Rev 1.1   Jan 16 2006 19:27:54   schand
//Added some try catch block for nullreference
//Resolution for 3454: DEL 8.1 Stream: IR for Module assocaitions for Mobile Service TD111
//
//   Rev 1.0   Jan 16 2006 13:47:58   schand
//Initial revision.
//Resolution for 3454: DEL 8.1 Stream: IR for Module assocaitions for Mobile Service TD111

using System;
using TransportDirect.UserPortal.AdditionalDataModule;
using TransportDirect.EnhancedExposedServices;
using TransportDirect.EnhancedExposedServices.DataTransfer.Common.V1; 

namespace TransportDirect.EnhancedExposedServices.DataTransfer.TaxiInformation.V1
{
	/// <summary>
	/// Assembler class for converting taxi information domain type to DTO type
	/// </summary>
	public sealed class TaxiInformationAssembler
	{
		#region Public Methods
		/// <summary>
		/// Static assembler method to convert StopTaxiInformation (domain object) to TaxiInformationStopDetail (DTO)
		/// </summary>
		/// <param name="stopTaxiInformation">StopTaxiInformation as Domain object</param>
		/// <returns>TaxiInformationStopDetail as DTO object</returns>
		public static TaxiInformationStopDetail CreateTaxiInformationStopDetailDT(StopTaxiInformation stopTaxiInformation)
		{
			if (stopTaxiInformation==null)
				return null;

			TaxiInformationStopDetail taxiInformationStopDetail = new TaxiInformationStopDetail(); 
			taxiInformationStopDetail.AccessibleOperatorPresent = stopTaxiInformation.AccessibleOperatorPresent;
			taxiInformationStopDetail.AccessibleText = stopTaxiInformation.AccessibleText;
			try
			{
				if (stopTaxiInformation.AlternativeStops != null)
					taxiInformationStopDetail.AlternativeStops =  CreateTaxiInformationStopDetailArrayDT(stopTaxiInformation.AlternativeStops);			
			}
			catch (NullReferenceException)
			{
				// this object is throwing null as other object as there is no AlternativeStops available
				taxiInformationStopDetail.AlternativeStops = new TaxiInformationStopDetail[0];
			}

			taxiInformationStopDetail.Comment = stopTaxiInformation.Comment;
			
			try
			{
				taxiInformationStopDetail.InformationAvailable = stopTaxiInformation.InformationAvailable;
			}
			catch(NullReferenceException)
			{
			   taxiInformationStopDetail.InformationAvailable = false;
			}

			try
			{
				taxiInformationStopDetail.Operators = CreateTaxiInformationOperatorArrayDT(stopTaxiInformation.Operators); 
			}
			catch(NullReferenceException)
			{
			   taxiInformationStopDetail.Operators = new TaxiInformationOperator[0]; 
			}
			taxiInformationStopDetail.StopName = stopTaxiInformation.StopName;
			taxiInformationStopDetail.StopNaptan = stopTaxiInformation.StopNaptan;
			
			return	taxiInformationStopDetail; 
		}


		/// <summary>
		/// Static assembler method to convert an array of StopTaxiInformation (domain object) to an array TaxiInformationStopDetail (DTO)
		/// </summary>
		/// <param name="stopTaxiInformations">An array of StopTaxiInformation as Domain object array</param>
		/// <returns>An array of TaxiInformationStopDetail as DTO object array</returns>
		public static TaxiInformationStopDetail[] CreateTaxiInformationStopDetailArrayDT(StopTaxiInformation[] stopTaxiInformations)
		{
			if (stopTaxiInformations.Length ==0)
				return new TaxiInformationStopDetail[0];

			int objectCount=0;
			TaxiInformationStopDetail[] taxiInformationStopDetails = new TaxiInformationStopDetail[stopTaxiInformations.Length];

			foreach(StopTaxiInformation stopTaxiInformation in stopTaxiInformations)
			{
			   taxiInformationStopDetails[objectCount] = new TaxiInformationStopDetail();
			   taxiInformationStopDetails[objectCount] =  CreateTaxiInformationStopDetailDT(stopTaxiInformation);
			   objectCount++; 
			}
			return taxiInformationStopDetails;
		}

		/// <summary>
		/// Static assembler method to convert an array of TaxiOperator (domain object) to an array TaxiInformationOperator (DTO)
		/// </summary>
		/// <param name="taxiOperators">An array of TaxiOperator as domain object array</param>
		/// <returns>An array of TaxiInformationOperator as DTO object array</returns>
		public static TaxiInformationOperator[] CreateTaxiInformationOperatorArrayDT(TaxiOperator[] taxiOperators)
		{
			if (taxiOperators.Length ==0)
				return new TaxiInformationOperator[0];

			int objectCount=0;
			TaxiInformationOperator[] taxiInformationOperators = new TaxiInformationOperator[taxiOperators.Length];

			foreach(TaxiOperator taxiOperator in taxiOperators)
			{
				taxiInformationOperators[objectCount] = new TaxiInformationOperator();
				if (taxiOperator==null)
				{
					taxiInformationOperators[objectCount] = null;					
				}
				else
				{
					taxiInformationOperators[objectCount].Accessible =  taxiOperator.Accessible;
					taxiInformationOperators[objectCount].Name =  taxiOperator.Name;
					taxiInformationOperators[objectCount].PhoneNumber =  taxiOperator.PhoneNumber;				
				}
				objectCount++; 
			}
			return taxiInformationOperators;
		}
		#endregion
	}
}
