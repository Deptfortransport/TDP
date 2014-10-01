// *********************************************************************** 
// NAME                 :	BusinessLinksTemplateCatalogue.cs
// AUTHOR               :	Tolu Olomolaiye
// DATE CREATED         :	22 Nov 2005 
// DESCRIPTION			:	Handles retrieval and cacheing of the link HTML
// ************************************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/DataServices/BusinessLinksTemplateCatalogue.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:20:42   mturner
//Initial revision.
//
//   Rev 1.5   Jan 05 2006 17:45:50   tolomolaiye
//Code review updates for Business Links
//Resolution for 3143: DEL 8 stream: Business Links Development
//
//   Rev 1.4   Dec 16 2005 12:09:20   jbroome
//Added GetDefault method
//Resolution for 3143: DEL 8 stream: Business Links Development
//
//   Rev 1.3   Nov 28 2005 09:57:00   tolomolaiye
//Updates following code review
//Resolution for 3143: DEL 8 stream: Business Links Development
//
//   Rev 1.2   Nov 23 2005 15:09:18   tolomolaiye
//Ensure objects are initialised
//Resolution for 3143: DEL 8 stream: Business Links Development
//
//   Rev 1.1   Nov 22 2005 16:51:06   tolomolaiye
//Added properties and methods
//Resolution for 3143: DEL 8 stream: Business Links Development
//
//   Rev 1.0   Nov 22 2005 11:23:08   tolomolaiye
//Initial revision.

using System;
using System.Collections;
using System.Data.SqlClient;
using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.Common.Logging;
using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.DataServices
{
	/// <summary>
	/// Summary description for BusinessLinksTemplateCatalogue.
	/// </summary>
	public class BusinessLinksTemplateCatalogue : IBusinessLinksTemplateCatalogue
	{
		private ArrayList businessLinkList = new ArrayList();

		private const string BusinessLinkProcedure = "GetBusinessLinkTemplates";

		/// <summary>
		/// Constructor class - read the business links data from the database and pout it in an collection
		/// </summary>
		public BusinessLinksTemplateCatalogue()
		{
			//define sql objects
			SqlHelper sqlHelper = new SqlHelper();
			SqlDataReader businessDataReader = null;

			//now try and read the data from the database via a stored procedure
			try
			{
				// Open DB connection 
				sqlHelper.ConnOpen(SqlHelperDatabase.DefaultDB);
				businessDataReader = sqlHelper.GetReader(BusinessLinkProcedure);

				//loop through all the rows in the reader object and add them to the collection
				if ( businessDataReader != null )
				{
					while (businessDataReader.Read())
					{
						BusinessLinkTemplate businessTemplate = new BusinessLinkTemplate(businessDataReader.GetInt32(0), 
							businessDataReader.GetString(1), businessDataReader.GetString(2), businessDataReader.GetString(3)); 
						businessLinkList.Add(businessTemplate);
					}
				}
			}
			catch(SqlException ex)
			{
				// Error has occured in stored procedure. Log it and return null
				Logger.Write( new OperationalEvent( TDEventCategory.Database, TDTraceLevel.Verbose, "Error in " + BusinessLinkProcedure + " stored procedure. Unable to return query results. " + ex.Message ));
				businessDataReader = null;
			}
			finally
			{
				// Close database objects
				if ((businessDataReader != null) && (!businessDataReader.IsClosed))
				{
					businessDataReader.Close();
				}
				sqlHelper.ConnClose();
			}
		}

		#region IBusinessLinksTemplateCatalogue Members

		/// <summary>
		/// Returns an array of BusinessLinkTemplate objects
		/// </summary>
		/// <returns>BusinessLinkTemplate collection</returns>
		public BusinessLinkTemplate[] GetAll()
		{
			return (BusinessLinkTemplate[]) businessLinkList.ToArray(typeof(BusinessLinkTemplate));
		}

		/// <summary>
		/// Returns a single BusinesslinkTemplate object with given id, or null 
		/// if it does not exist in the table
		/// </summary>
		/// <param name="templateID">the id of the BusinessLinkTemplate object</param>
		/// <returns>A single BusinessLinkTemplate object</returns>
		public BusinessLinkTemplate Get(int templateID)
		{
			foreach (BusinessLinkTemplate businessTemplate in businessLinkList)
			{
				if (businessTemplate.Id == templateID)
				{
					return businessTemplate;
				}
			}

			//return null if nothing is found
			return null;
		}

		/// <summary>
		/// Returns the first BusinesslinkTemplate object in the list
		/// </summary>
		/// <returns>A single BusinessLinkTemplate object</returns>
		public BusinessLinkTemplate GetDefault()
		{
			return (BusinessLinkTemplate)businessLinkList[0];
		}

		#endregion
	}
}
