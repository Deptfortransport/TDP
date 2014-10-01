// *********************************************** 
// NAME                 : MobileDeviceTypesDropList.cs
// AUTHOR               : Sanjeev Chand
// DATE CREATED         : //2005 
// DESCRIPTION  		: Dropdown list for Device types
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/DepartureBoardService/MobileBookmark/MobileDeviceTypesDropList.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:21:32   mturner
//Initial revision.
//
//   Rev 1.0   Jun 23 2005 12:11:40   schand
//Initial revision.

using System;
using System.Xml; 

namespace TransportDirect.UserPortal.DepartureBoardService.MobileBookmark
{
	/// <summary>
	/// Summary description for MobileDeviceTypes.
	/// </summary>	
	[Serializable()] 
	public class MobileDeviceTypesDropList
	{
		private string mobileDeviceTypeText=string.Empty;
		private string mobileDeviceTypeValue = string.Empty;
		private bool isSelected;

		public MobileDeviceTypesDropList()
		{
		}

		public MobileDeviceTypesDropList(string deviceTypeText, string deviceTypeValue, bool isSelected)
		{
			this.mobileDeviceTypeText = deviceTypeText;
			this.mobileDeviceTypeValue = deviceTypeValue;
			this.isSelected = isSelected; 
		}
		public string MobileDeviceTypeText
		{
			get{return mobileDeviceTypeText;}
			set {mobileDeviceTypeText=value;}
		}

		public string MobileDeviceTypeValue
		{
			get {return mobileDeviceTypeValue;}
			set {mobileDeviceTypeValue=value;}
		}

		public bool IsSelected
		{
			get{return this.isSelected;}
			set {this.isSelected = value;}
		}

	}
}
