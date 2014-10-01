// *********************************************** 
// NAME                 : TaxiInformationOperator.cs
// AUTHOR               : Sanjeev Chand
// DATE CREATED         : 20/01/2006 
// DESCRIPTION  		: DTO class implementation of TaxiOperator. For Information refer to TaxiOperator.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/TaxiInformation/V1/TaxiInformationOperator.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:22:48   mturner
//Initial revision.
//
//   Rev 1.1   Jan 20 2006 19:38:42   schand
//Added comment and serialisation attribute
//Resolution for 3454: DEL 8.1 Stream: IR for Module assocaitions for Mobile Service TD111


namespace TransportDirect.EnhancedExposedServices.DataTransfer.TaxiInformation.V1
{
	/// <summary>
	/// DTO class implementation of TaxiOperator. For Information refer to TaxiOperator.
	/// </summary>
	[System.Serializable]
	public class TaxiInformationOperator
	{
		#region Private members
		private string name;
		private string phoneNumber;
		private bool accessible;
		#endregion

		#region Constructor
		public TaxiInformationOperator()
		{
			name = string.Empty;
			phoneNumber = string.Empty;			 
		}
		#endregion

		#region Public properties
		/// <summary>
		/// Read-Write property for Name
		/// </summary>
		public string Name
		{
			get 
			{
				return name;
			}
			set 
			{  
				name = value;
			}
		}

		/// <summary>
		/// Read-Write property for PhoneNumber
		/// </summary>
		public string PhoneNumber
		{
			get 
			{
				return   phoneNumber;
			}
			set 
			{
			  phoneNumber = value;
			}
		}

		/// <summary>
		/// Read-Write property for Accessible
		/// </summary>
		public bool Accessible
		{
			get 
			{
				return  accessible;
			}
			set 
			{
			   accessible = value;
			}
		}

		#endregion

	}// END CLASS DEFINITION TaxiInformationOperator

} // TransportDirect.EnhancedExposedServices.DataTransfer.TaxiInformation.V1
