// ***********************************************
// NAME 		: IDataChangeNotificationService.cs
// AUTHOR 		: Rob Greenwood
// DATE CREATED : 16-Jun-2004
// DESCRIPTION 	: Interface for the DataChangeNotification class
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/DataServices/IDataChangeNotification.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:20:48   mturner
//Initial revision.
//
//   Rev 1.0   Jun 16 2004 17:44:10   rgreenwood
//Initial revision.

using System;

namespace TransportDirect.UserPortal.DataServices
{
	/// <summary>
	/// 
	/// </summary>
	public delegate void ChangedEventHandler(object sender, ChangedEventArgs e);

	#region ChangedEventArgs class
	/// <summary>
	/// Class to store data for the Changed event.
	/// </summary>
	public class ChangedEventArgs : EventArgs
	{
		/// <summary>
		/// 
		/// </summary>
		public string GroupId;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="groupId"></param>
		public ChangedEventArgs(string groupId)
		{
			GroupId = groupId;
		}
	}
    #endregion

	/// <summary>
	/// Summary description for IDataChangeNotification.
	/// </summary>
	public interface IDataChangeNotification
	{
		event ChangedEventHandler Changed;

	}
}
