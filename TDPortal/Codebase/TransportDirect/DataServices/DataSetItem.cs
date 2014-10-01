// *********************************************** 
// NAME			: DataSetItem.cs
// AUTHOR		: Paul Cross
// DATE CREATED	: 05/09/2005 
// DESCRIPTION	: Implementation of the DataSetItem class. Each instance of the class holds a data item from the
//                DataSet table, which represents a DataSet definition for a particular microsite.
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/DataServices/DataSetItem.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:20:48   mturner
//Initial revision.
//
//   Rev 1.1   Sep 29 2005 11:09:54   pcross
//Changes associated with PartnerId becoming integer
//Resolution for 2807: DEL8 White labelling Phase 3
//Resolution for 2810: Del 8 White Labelling Phase 3 - Changes to Properties and Data services Components
//
//   Rev 1.0   Sep 06 2005 17:42:12   pcross
//Initial revision.
//

using System;

namespace TransportDirect.UserPortal.DataServices
{
	/// <summary>
	/// Defines and holds all data associated with a data item from the
	/// DataSet table, which represents a DataSet definition for a particular microsite.
	/// </summary>
	public class DataSetItem
	{
		#region Private Members
		
		private string dataSet;
		private int partnerId;
		
		#endregion
		
		#region Constructors

		/// <summary>
		/// Empty object constructor
		/// </summary>
		public DataSetItem()
		{}

		
		/// <summary>
		/// Constructor with all properties passed in
		/// </summary>
		public DataSetItem(string dataSet, int partnerId)
		{
			// Populate the instance variables
			this.dataSet = dataSet;
			this.partnerId = partnerId;
		}

		#endregion

		#region Public Properties

		public string DataSet
		{
			get {return dataSet;}
			set {dataSet = value;}
		}

		public int PartnerId
		{
			get {return partnerId;}
			set {partnerId = value;}
		}

		#endregion
	}
}
