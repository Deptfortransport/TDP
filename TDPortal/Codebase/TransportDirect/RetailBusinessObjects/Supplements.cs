//********************************************************************************
//NAME         : Supplements.cs
//AUTHOR       : Richard Philpott
//DATE CREATED : 2005-03-02
//DESCRIPTION  : Encapsulates the collection of supplements applying 
//				 to a journey/fare combination. 
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/RetailBusinessObjects/Supplements.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:46:24   mturner
//Initial revision.
//
//   Rev 1.1   Apr 19 2005 16:34:12   RPhilpott
//Handle errors returned by SBO.
//Resolution for 2242: PT - Train - Clicking Tickets/Costs on Results Page causes error
//
//   Rev 1.0   Mar 22 2005 16:30:44   RPhilpott
//Initial revision.
//

using System;
using System.Collections;

namespace TransportDirect.UserPortal.RetailBusinessObjects
{
	/// <summary>
	/// Encapsulates the collection of supplements 
	///	 applying to a journey/fare combination.
	/// </summary>
	[Serializable]
	public class Supplements
	{
		private ArrayList supplementList = new ArrayList();
		private bool errorReturnDirection = false;
		private string errorSupplementCode = string.Empty;
		private int errorLeg = 0;
		private int errorReservation = 0;

		public ArrayList SupplementList
		{
			get { return supplementList; }
			set { this.supplementList = value; }
		}

		public Supplements(BusinessObjectOutput output) 
		{

			if	(output.ErrorCode.Length > 0)
			{
				// error has already been logged -- exiting here
				//  will just leave us wuth an empty supplement list 
				return;
			}

			int headerLength = output.RecordDetails[0].RecordLength;
			string header = output.OutputBody.Substring(0, headerLength);

			errorReturnDirection = (header.Substring(8, 1).Equals("R"));
			errorSupplementCode = header.Substring(9,3);
			errorLeg = Int32.Parse(header.Substring(12,2));
			errorReservation = Int32.Parse(header.Substring(14,1));

			int supplementsLength = output.RecordDetails[3].RecordLength;
			string allSupplementsString = output.OutputBody.Substring(headerLength, supplementsLength);
			
			int supplementCount = 0;
			
			try
			{
				supplementCount = Int32.Parse(allSupplementsString.Substring(8, 3));
			}
			catch (Exception)
			{
				// if not an integer, just leave supplement list empty
				supplementCount = 0;
			}

			for (int i = 0; i < supplementCount; i++) 
			{
				supplementList.Add(new Supplement(allSupplementsString.Substring((11 + (i * 8)), 8)));
			}
		}
	}
}