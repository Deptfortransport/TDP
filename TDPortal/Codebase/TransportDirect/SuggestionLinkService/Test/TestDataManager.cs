// *********************************************** 
// NAME                 : TestDataManager.cs
// AUTHOR               : Ken Josling
// DATE CREATED         : 18/08/2005 
// DESCRIPTION			: Test harness for loading test data
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SuggestionLinkService/Test/TestDataManager.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:50:16   mturner
//Initial revision.
//
//   Rev 1.1   Feb 16 2006 15:54:32   build
//Automatically merged from branch for stream0002
//
//   Rev 1.0.1.0   Dec 16 2005 11:52:18   kjosling
//Updated
//Resolution for 2: DEL 8.1 Workstream - Zonal Services Phase 1
//
using System;
using System.IO;
using System.Xml;
using System.Collections;
using System.Diagnostics;
using System.Text;
using TransportDirect.Common.DatabaseInfrastructure;
using System.Data.SqlClient;
using TransportDirect.Common.PropertyService.Properties;

namespace TransportDirect.UserPortal.SuggestionLinkService.Test
{
	public class TestDataManager
	{
		#region Private Properties

		private string dataFile;
		private string setupScript;
		private string conString;
		private const string INSERT_COMMAND = "INSERT INTO {0} VALUES (";
		private const string DELETE_COMMAND = "DELETE FROM {0}";

		#endregion

		#region Constructor

		public TestDataManager(string DataFile, string SetupScript, string connectionString)
		{
			if(File.Exists(DataFile))
			{
				dataFile = DataFile;
			}
			else
			{
				throw new FileNotFoundException("Data file '" + DataFile + "' could not be found");
			}
			if(File.Exists(SetupScript))
			{
				setupScript = SetupScript;
			}
			else
			{
				throw new FileNotFoundException("Setup script '" + SetupScript + "' could not be found");
			}
			conString = connectionString;
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
		/// Gets or sets the data file containing the cleardown data
		/// </summary>
		public string SetupScript
		{
			get{	return setupScript;	}
			set
			{
				if(File.Exists(value))
				{
					setupScript = value;
				}
				else
				{
					throw new FileNotFoundException("Setup script '" + value + "' could not be found");
				}
			}
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Loads test data into the target database ready for test
		/// </summary>
		/// <returns>True if the operation completed successfully, false otherwise</returns>
		public bool LoadData()
		{
			ResetData();

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

		private void ResetData() 
		{ 
			SqlConnection connection = new SqlConnection(conString);
			connection.Open();
			StreamReader reader = new StreamReader(setupScript);
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
			ResetData();
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
					parms[i] = "'" + parms[i] + "'";
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
				helper.ConnOpen(SqlHelperDatabase.TransientPortalDB);
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
