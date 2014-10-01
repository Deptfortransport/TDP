// ********************************************************************* 
// NAME                 : TaxiOperator.cs 
// AUTHOR               : Ken Josling
// DATE CREATED         : 2005-27-07
// DESCRIPTION			: Holds data for a specific taxi company. Linked to StopTaxiInformation
// ********************************************************************** 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/AdditionalDataModule/TaxiOperator.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:18:10   mturner
//Initial revision.
//
//   Rev 1.6   Sep 07 2005 10:33:34   kjosling
//Updated following code review
//Resolution for 2625: DEL 8 Stream: TrainTaxi Amendments
//
//   Rev 1.5   Sep 01 2005 11:45:04   kjosling
//Updated following code review
//
//   Rev 1.4   Aug 15 2005 16:34:46   kjosling
//Changed properties to r/w
//
//   Rev 1.3   Aug 15 2005 09:41:06   kjosling
//Added code to constructor to handle additional reference data permutation
//
//   Rev 1.2   Aug 12 2005 15:01:06   kjosling
//Added Serializable attribute
//
//   Rev 1.1   Aug 11 2005 17:37:52   kjosling
//Minor amendments following testing
//
//   Rev 1.0   Aug 10 2005 10:44:34   kjosling
//Initial revision.

using System;

namespace TransportDirect.UserPortal.AdditionalDataModule
{
	/// <summary>
	/// Holds data for a specific taxi company. Linked to StopTaxiInfomration
	/// </summary>
	[Serializable]
	public class TaxiOperator
	{
		#region Private Properties

		private string name;
		private string phoneNumber;
		private bool accessible;

		#endregion

		#region Constructor

		/// <summary>
		/// Creates a new instance of TaxiOperator
		/// </summary>
		/// <param name="name">The name of the taxi operator</param>
		/// <param name="phoneNumber">The phone number for the taxi operator</param>
		/// <param name="accessible">"Y" if the taxi operator provides accessible taxis, empty otherwise</param>
		public TaxiOperator(string name, string phoneNumber, string isAccessible)
		{
			this.name = name;
			this.phoneNumber = phoneNumber;
			if(isAccessible.Length > 0)
			{
				accessible = true;
			}
		}

		/// <summary>
		/// Default public constructor for serialisation
		/// </summary>
		public TaxiOperator(){}

		#endregion

		#region Public Properties

		/// <summary>
		/// (Read-Write) Gets or sets the name of the taxi operator
		/// </summary>
		public string Name
		{
			get{	return name;	}
			set{	name = value;	}
		}

		/// <summary>
		/// (Read-Write) Gets or sets the telephone number of the taxi operator
		/// </summary>
		public string PhoneNumber
		{
			get{	return phoneNumber;		}
			set{	phoneNumber = value;	}
		}

		/// <summary>
		/// (Read-Write) True if the taxi operator provides accessible taxis, false otherwise
		/// </summary>
		public bool Accessible
		{
			get{	return accessible;	}
			set{	accessible = value;	}
		}

		#endregion
	}
}
