// *********************************************** 
// NAME                 : TestDataManager.cs
// AUTHOR               : Ken Josling
// DATE CREATED         : 18/08/2005 
// DESCRIPTION			: Test harness for loading test data
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/DatabaseInfrastructure/TestDataManager.cs-arc  $
//
//   Rev 1.2   Dec 05 2012 14:18:46   mmodi
//Updated to only throw errors when necessary
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.1   Feb 14 2010 13:42:24   mmodi
//Updates to allow a seperate setup and clearup files to be run
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.0   Nov 08 2007 12:19:54   mturner
//Initial revision.
//
//   Rev 1.0   Jan 03 2006 11:55:48   kjosling
//Initial revision.
//Resolution for 2: DEL 8.1 Workstream - Zonal Services Phase 1
//
using System;
using System.IO;
using System.Xml;
using System.Collections;
using System.Diagnostics;
using System.Text;
using System.Data.SqlClient;
using TransportDirect.Common.PropertyService.Properties;

namespace TransportDirect.Common.DatabaseInfrastructure
{
	public class TestDataManager
	{
		#region Private Properties

		private string dataFile;
        private string clearDownScript;
		private string setupScript;
		private string conString;
		private const string INSERT_COMMAND = "INSERT INTO {0} VALUES (";
		private const string DELETE_COMMAND = "DELETE FROM {0}";

        private SqlHelperDatabase database = SqlHelperDatabase.TransientPortalDB;

		#endregion

		#region Constructor

		public TestDataManager(string sourceData, string clearDownScript, string connectionString)
		{
			if(File.Exists(sourceData))
			{
				dataFile = sourceData;
			}
			else
			{
				throw new FileNotFoundException("Data file '" + DataFile + "' could not be found");
			}
			if(File.Exists(clearDownScript))
			{
				this.clearDownScript = clearDownScript;
			}
			else
			{
				throw new FileNotFoundException("Cleardown script '" + clearDownScript + "' could not be found");
			}
			conString = connectionString;
		}

        /// <summary>
        /// Overloaded constructor
        /// </summary>
        /// <param name="sourceData"></param>
        /// <param name="clearDownScript"></param>
        /// <param name="connectionString"></param>
        public TestDataManager(string sourceData, string setupScript, string clearDownScript, string connectionString, SqlHelperDatabase database)
        {
            if (File.Exists(sourceData))
            {
                dataFile = sourceData;
            }
            else
            {
                if (!string.IsNullOrEmpty(sourceData))
                    throw new FileNotFoundException("Data file '" + DataFile + "' could not be found");
            }
            if (File.Exists(setupScript))
            {
                this.setupScript = setupScript;
            }
            else
            {
                if (!string.IsNullOrEmpty(setupScript))
                    throw new FileNotFoundException("Setup script '" + setupScript + "' could not be found");
            }
            if (File.Exists(clearDownScript))
            {
                this.clearDownScript = clearDownScript;
            }
            else
            {
                if (!string.IsNullOrEmpty(clearDownScript))
                    throw new FileNotFoundException("Cleardown script '" + clearDownScript + "' could not be found");
            }
            conString = connectionString;
            this.database = database;
        }

		#endregion

		#region Public Properties

		/// <summary>
		/// Gets or sets the file containing the test data
		/// </summary>
		public string DataFile
		{
			get{	return dataFile;	}
			set
			{
				if(File.Exists(value))
				{
					dataFile = value;
				}
				else
				{
					throw new FileNotFoundException("Data File '" + value + "' could not be found");
				}
			}
		}

        /// <summary>
        /// Gets or sets the file to be run (optionally) for setting up the database before Loading the data
        /// </summary>
        public string SetupScript
        {
            get { return setupScript; }
            set
            {
                if (File.Exists(value))
                {
                    setupScript = value;
                }
                else
                {
                    throw new FileNotFoundException("Setup script '" + value + "' could not be found");
                }
            }
        }

		/// <summary>
		/// Gets or sets the data file containing the cleardown data
		/// </summary>
		public string ClearDownScript
		{
			get{	return clearDownScript;	}
			set
			{
				if(File.Exists(value))
				{
					clearDownScript = value;
				}
				else
				{
					throw new FileNotFoundException("Cleardown script '" + value + "' could not be found");
				}
			}
		}

		#endregion

		#region Public Methods

        /// <summary>
        /// Method to run the (optional) setup script before data is loaded
        /// </summary>
        /// <returns></returns>
        public bool Setup()
        {
            try
            {
                RunScript(setupScript);

                return true;    
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return false;
            }
        }

        /// <summary>
		/// Loads test data into the target database ready for test, will first run the cleardown script
		/// </summary>
		/// <returns>True if the operation completed successfully, false otherwise</returns>
        public bool LoadData()
        {
            return LoadData(true);
        }

		/// <summary>
		/// Loads test data into the target database ready for test, first running the cleardown script if specified
		/// </summary>
		/// <returns>True if the operation completed successfully, false otherwise</returns>
		public bool LoadData(bool runClearDownScript)
		{
            if (runClearDownScript)
            {
                RunScript(clearDownScript);
            }

			XmlDocument xmlData = new XmlDocument();
			try
			{
				xmlData.Load(dataFile);
			}
			catch(XmlException e)
			{
				Debug.WriteLine(e.ToString());
				return false;
			}

			XmlNodeList tables;
			tables = xmlData.DocumentElement.ChildNodes;
			ArrayList parms = new ArrayList();
			foreach(XmlNode table in tables)
			{
				XmlNodeList records = table.ChildNodes;
				foreach(XmlNode record in records)
				{
					parms.Add(table.Name);
					XmlAttributeCollection data = record.Attributes;
					foreach(XmlAttribute field in data)
					{
						parms.Add(field.Value);
					}
					string command = FormatInsertCommand((string[])parms.ToArray(typeof(string)));
					//EXECUTE THE COMMAND
					ExecuteCommand(command);
					parms.Clear();
				}
			}	
			return true;			
		}

		public bool ExecuteUpdateCommand(string Command)
		{
			try
			{
				ExecuteCommand(Command);
				return true;
			}
			catch(SqlException e)
			{
				Debug.WriteLine(e.ToString());
				return false;
			}
		}

		#endregion

		#region Private Methods

		private void RunScript(string script) 
		{ 
			SqlConnection connection = new SqlConnection(conString);
			connection.Open();
			StreamReader reader = new StreamReader(script);
			SqlCommand command = new SqlCommand(reader.ReadToEnd());
			command.Connection = connection;
			command.ExecuteNonQuery();
			connection.Close();
		} 

		/// <summary>
		/// Executes the clear down script to remove the test data from the database
		/// </summary>
		public void ClearData()
		{
			Debug.WriteLine("Resetting Database");
			RunScript(clearDownScript);
		}

		private string FormatInsertCommand(string[] parms)
		{
			StringBuilder command = new StringBuilder(INSERT_COMMAND);
			for(int i = 1; i < parms.Length; i ++)
			{
				try
				{
					int.Parse(parms[i]);
				}
				catch
				{
					if(parms[i] != "null")
					{	
						try
						{
							DateTime date = DateTime.Parse(parms[i]);
							parms[i] = "CONVERT(datetime, '" + parms[i] + "', 121)";
						}
						catch
						{
							parms[i] = "'" + parms[i] + "'";
						}				
					}
				}
				AddParameter(command, i);
				if(i != parms.GetUpperBound(0))
				{
					command.Append(',');
				}
				else
				{
					command.Append(')');
				}
			}
			return String.Format(command.ToString(), parms);
		}

		private void AddParameter(StringBuilder s, int i)
		{
			s.Append('{');
			s.Append(i.ToString());
			s.Append('}');
		}

		private void ExecuteCommand(string command)
		{
			Debug.WriteLine("Executing Command: " + command);
			SqlHelper helper = new SqlHelper();

			try
			{
				helper.ConnOpen(database);
				helper.Execute(command);
			}
			finally
			{
				helper.ConnClose();
			}
		}

		#endregion
	}
}
