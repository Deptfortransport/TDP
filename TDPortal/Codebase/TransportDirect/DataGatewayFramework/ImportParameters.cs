// ***********************************************
// NAME 		: ImportParameters.cs
// AUTHOR 		: Mark Turner
// DATE CREATED : 12/08/2003
// DESCRIPTION 	: A class that retrieves all of the parameters associated with a data import
// from the SQL configuration database.
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/DataGatewayFramework/ImportParameters.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:20:12   mturner
//Initial revision.
//
//   Rev 1.7   Sep 16 2003 14:59:46   MTurner
//Changes after code review
//
//   Rev 1.6   Sep 01 2003 14:11:36   MTurner
//Changes after comments by A. Caunt
//
//   Rev 1.5   Aug 29 2003 12:39:04   mturner
//Added error handling for SQL read()
//
//   Rev 1.4   Aug 27 2003 17:46:26   MTurner
//Corrected bug in SQL query string
//
//   Rev 1.3   Aug 27 2003 17:24:10   MTurner
//Changes to remove IDataRow
//
//   Rev 1.2   Aug 15 2003 13:35:38   mturner
//Not fully functional - checked in while on Leave
//
//   Rev 1.1   Aug 13 2003 12:25:50   mturner
//Added IDataRow implemetation
//
//   Rev 1.0   Aug 12 2003 16:04:02   mturner
//Initial Revision

using System;
using System.Data;
using System.Data.SqlClient;
using TransportDirect.Common.DatabaseInfrastructure;

namespace TransportDirect.Datagateway.Framework
{
	/// <summary>
	/// Import Parameters retrieves the congig values for a data import and holds them within
	/// a number of properties.  These can then be accessed by the import controller to enable
	/// the correct processing to be carried out on a particular imported file.
	/// </summary>
	public class ImportParameters
	{
		private string importClassName;
		private string classArchiveName;
		private string importUtilityName;
		private string dataFeedName;
		private string parameters1Name;
		private string parameters2Name;
		private string processingDirName;
		
		public ImportParameters(string dataFeed)
		{
			this.DataFeed = dataFeed;
		}

		#region Properties
		/// <summary>
		/// ImportClass is the subclass of ImportTask that needs to be instantiated
		/// to perform the import
		/// </summary>
		public string ImportClass
		{
			get
			{
				return this.importClassName;
			}
			set
			{
				this.importClassName = value;
			}
		}

		/// <summary>
		/// ClassArchive is the name of the archive that the sub-class can be found in
		/// </summary>
		public string ClassArchive
		{
			get
			{
				return this.classArchiveName;
			}
			set
			{
				this.classArchiveName = value;
			}
		}

		/// <summary>
		/// ImportUtility is the full path of any import utility that needs to call
		/// </summary>
		public string ImportUtility
		{
			get
			{
				return this.importUtilityName;
			}
			set
			{
				this.importUtilityName = value;
			}
		}

		/// <summary>
		/// Parameters1 are parameters to pass to the underlying import utility before the filename 
		/// </summary>
		public string Parameters1
		{
			get
			{
				return this.parameters1Name;
			}
			set
			{
				this.parameters1Name = value;
			}
		}

		/// <summary>
		/// Parameters2 are parameters to pass to the underlying import utility after the filename 
		/// </summary>
		public string Parameters2
		{
			get
			{
				return this.parameters2Name;
			}
			set
			{
				this.parameters2Name = value;
			}
		}

		/// <summary>
		/// ProcessingDir is used to indicate any particular directory a file must be copied to
		/// for processing.  An empty field indicates that the current directory should be used.
		/// </summary>
		public string ProcessingDir
		{
			get
			{
				return this.processingDirName;
			}
			set
			{
				this.processingDirName = value;
			}
		}

		/// <summary>
		/// DataFeed is the unique data feed name that provides the key for obtaining
		/// other parameters from the data service.
		/// </summary>
		public string DataFeed
		{
			get
			{
				return this.dataFeedName;
			}
			set
			{
				this.dataFeedName = value;
			}
		}
		#endregion

		#region Methods
		/// <summary>
		/// The configuration data for this particular import is read from the SQL table
		/// by use of td.common.databaseinfrastructure.dll. The values retreived are then 
		/// assigned to the corresponding properties of this class.
		/// </summary>
		public bool SetData()
		{
			bool dataSetOK = false;
			SqlHelper sql = new SqlHelper();

			try
			{
				// Build the SQL query and then use it to retrieve the correct config data.
				string sqlQueryString;
				sqlQueryString = "SELECT data_feed,import_class,class_archive,"
					+ "import_utility,parameters1,parameters2,processing_dir "
					+ "FROM import_configuration "
					+ "WHERE data_feed = '" + this.DataFeed + "'";

				SqlDataReader configurationDataReader;
				sql.ConnOpen(SqlHelperDatabase.DefaultDB);
				configurationDataReader = sql.GetReader(sqlQueryString);
				if(configurationDataReader.Read())
				{
					// Assign retrieved parameters to the corresponding properties
					this.ImportClass   = configurationDataReader.GetString(1); 
					this.ClassArchive  = configurationDataReader.GetString(2);
					this.ImportUtility = configurationDataReader.GetString(3);
					this.Parameters1   = configurationDataReader.GetString(4); 
					this.Parameters2   = configurationDataReader.GetString(5);
					this.ProcessingDir = configurationDataReader.GetString(6);
					dataSetOK = true;
				}
				configurationDataReader.Close();
			}
			catch(Exception)
			{
				dataSetOK = false;
				return dataSetOK;
			}
			finally
			{
				sql.ConnClose();
			}
			return dataSetOK;
		}
		#endregion
	}
}
