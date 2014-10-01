// *********************************************** 
// NAME                 : CodeServiceRequest.cs
// AUTHOR               : Sanjeev Chand
// DATE CREATED         : 20/01/2006 
// DESCRIPTION  		: DTO class implementation to take input parameters for consuming code service. 
// ************************************************ 
// $Log

using System;
namespace TransportDirect.EnhancedExposedServices.DataTransfer.CodeHandler.V1
{
	/// <summary>
	/// DTO implementation to take input parameters for consumeing code service.
	/// </summary>
	[System.Serializable]
	public class CodeServiceRequest
	{
		#region Private Members
		private string placeText; 
		private bool fuzzy;       
		private CodeServiceModeType[]  modeTypes;
		#endregion

		#region Constructor
		public CodeServiceRequest()
		{
			placeText = string.Empty;
			modeTypes = new CodeServiceModeType[0]; 
		}
		#endregion

		#region Public properties
		/// <summary>
		/// Read-Write property for place text
		/// </summary>
		public string PlaceText
		{
			get {return placeText;}
			set {placeText=value;}
		}

		/// <summary>
		/// Read-Write property for fuzzy logic
		/// </summary>
		public bool Fuzzy
		{
			get {return fuzzy;}
			set {fuzzy=value;}
		}

		/// <summary>
		/// Read-Write property for mode types
		/// </summary>
		public CodeServiceModeType[] ModeTypes
		{
			get {return modeTypes;}
			set {modeTypes=value;}
		}
		#endregion


	}// END CLASS DEFINITION CodeServiceRequest

} // TransportDirect.EnhancedExposedServices.DataTransfer.CodeHandler.V1
