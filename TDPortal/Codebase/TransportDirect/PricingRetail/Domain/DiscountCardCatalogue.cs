// *********************************************** 
// NAME			: DiscountCardCatalogue.cs
// AUTHOR		: J George
// DATE CREATED	: 26/01/2005
// DESCRIPTION	: Implements a catalogue that holds details discount cards
// ************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/Domain/DiscountCardCatalogue.cs-arc  $
//
//   Rev 1.2   Feb 02 2009 16:48:08   mmodi
//Place theme calling code in try catch to prevent error when there is no HTTPContext
//Resolution for 5223: CCN0385 - TTBO Routeing Guide
//
//   Rev 1.1   Mar 10 2008 15:22:18   mturner
//Initial Del10 Codebase from Dev Factory
//
//   Rev 1.0   Nov 08 2007 12:36:46   mturner
//Initial revision.
//
//   Rev 1.0   Jan 27 2005 11:39:32   jgeorge
//Initial revision.

using System;
using System.Collections;
using System.Data.SqlClient;
using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.Common.DatabaseInfrastructure.Content;
using TD.ThemeInfrastructure;

namespace TransportDirect.UserPortal.PricingRetail.Domain
{
	/// <summary>
	/// Implements a catalogue that holds details discount cards
	/// </summary>
	public class DiscountCardCatalogue : IServiceFactory
	{
		#region Private members

		/// <summary>
		/// Stored proc that will be used to load the cards
		/// </summary>
		private const string storedProcName = "GetDiscountCards";

		/// <summary>
		/// Hashtable holding a hashtable of cards for each mode. Keys are
		/// values of type ModeType
		/// </summary>
		private Hashtable cardsByMode;

		/// <summary>
		/// Hashtable holding discount cards. Keys are values of type int.
		/// </summary>
		private Hashtable cardsById;

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="sourceDB">The database containing the Discount card table/stored proc</param>
		public DiscountCardCatalogue(SqlHelperDatabase sourceDB)
		{
			LoadData(sourceDB);
        }

		#endregion

		#region IServiceFactory implementation

		/// <summary>
		/// Implementation of IServiceFactory.Get. Returns a reference to this object
		/// </summary>
		/// <returns></returns>
		public object Get()
		{
			return this;
		}

		#endregion

		#region Public methods

		/// <summary>
		/// Retrieves a discount card with the given mode and code
		/// </summary>
		/// <param name="mode">Mode of travel to which the card applies</param>
		/// <param name="code">Code used for the discount card</param>
		/// <returns>Null if no matching card is found</returns>
		public DiscountCard GetDiscountCard(ModeType mode, string code)
		{
			if (!cardsByMode.ContainsKey(mode))
				return null;

			Hashtable cards = (Hashtable)cardsByMode[mode];

			if (!cards.ContainsKey(code))
				return null;

			return (DiscountCard)cards[code];
		}

		/// <summary>
		/// Retrieves a discount card
		/// </summary>
		/// <param name="id">Unique id for the card</param>
		/// <returns>Null if no matching card is found</returns>
		public DiscountCard GetDiscountCard(int id)
		{
			if (!cardsById.ContainsKey(id))
				return null;

			return (DiscountCard)cardsById[id];
		}

		#endregion

		#region Internal methods for testing

		/// <summary>
		/// Returns the total number of cards held
		/// </summary>
		/// <returns></returns>
		internal int Count()
		{
			return cardsById.Count;
		}

		/// <summary>
		/// Returns the total number of cards held for the specified mode
		/// </summary>
		/// <param name="mode"></param>
		/// <returns>-1 of there are no cards for the given mode</returns>
		internal int Count(ModeType mode)
		{
			if (!cardsByMode.ContainsKey(mode))
				return -1;
			else
				return ((Hashtable)cardsByMode[mode]).Count;
		}

		#endregion

		#region Private methods

		/// <summary>
		/// Loads all of the discount card data into the hashtables
		/// </summary>
		/// <param name="sourceDB"></param>
		private void LoadData(SqlHelperDatabase sourceDB)
		{
			// Lock the object whilst loading
			lock (this)
			{
                // Place get theme in a try because exception can be thrown if attempting to get Retailers
                // for a non HTTPContext app, e.g. Nunit
                string themeName;
                try
                {
                    themeName = TD.ThemeInfrastructure.ThemeProvider.Instance.GetTheme().Name;
                }
                catch
                {
                    // No need to log error, this should not happen for the web app
                    themeName = TD.ThemeInfrastructure.ThemeProvider.Instance.GetDefaultTheme().Name;
                }

				// Create the helper and initialise objects
				SqlHelper helper = new SqlHelper();
				cardsById = new Hashtable();
				cardsByMode = new Hashtable();
                Hashtable htParameters = new Hashtable();
                htParameters.Add("@ThemeName", themeName);

				// Open connection and get a DataReader
				helper.ConnOpen(sourceDB);

                SqlDataReader reader = helper.GetReader(storedProcName, htParameters);

				// Vars to hold the data from the datareader
				int id;
				string code;
				ModeType mode;
				int maxAdults;
				int maxChildren;

				// Loop through the data and create discount card object for each record
				while (reader.Read())
				{
					id = reader.GetInt32(0);
					code = reader.GetString(1);
					mode = (ModeType)Enum.Parse(typeof(ModeType), reader.GetString(2), true);
					maxAdults = reader.GetInt32(3);
					maxChildren = reader.GetInt32(4);

					DiscountCard newCard = new DiscountCard(id, code, mode, maxAdults, maxChildren);

					GetOrCreateModeHashtable(mode).Add(newCard.Code, newCard);
					cardsById.Add(newCard.Id, newCard);
				}

				reader.Close();
				helper.ConnClose();
			}
		}

		/// <summary>
		/// Retrieves the hashtable for the given mode. If it doesn't exist, it
		/// is created
		/// </summary>
		/// <param name="mode"></param>
		/// <returns></returns>
		private Hashtable GetOrCreateModeHashtable(ModeType mode)
		{
			if (!cardsByMode.ContainsKey(mode))
				cardsByMode.Add(mode, new Hashtable());

			return (Hashtable)cardsByMode[mode];
		}

		#endregion

	}
}
