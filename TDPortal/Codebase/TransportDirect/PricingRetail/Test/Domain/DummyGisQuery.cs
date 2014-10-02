// *********************************************** 
// NAME                 : DummyGisQueryr.cs 
// AUTHOR               : Alistair Caunt
// DATE CREATED         : 13/10/03
// DESCRIPTION			: Implementation of the DummyGisQuery class
// ************************************************ 

using System;
using System.Collections;
using TransportDirect.Common;
using TransportDirect.Presentation.InteractiveMapping;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.UserPortal.LocationService;

namespace  TransportDirect.UserPortal.PricingRetail.Domain
{
	/// <summary>
	/// Class which provides a very thin wrapper around the ArcIMS Query object provided by ESRI.
	/// </summary>
	[Serializable()]
	public class DummyGisQuery : IGisQuery
	{
		/*
		class DummyStopsRow : QuerySchema.StopsRow
		{
			public DummyStopsRow(string naptan) : base(new System.Data.DataRowBuilder(null, null))
			{
				atcocode = naptan;
			}
		}
		*/

		class DummyQuerySchema : QuerySchema
		{
			public DummyQuerySchema(string[] naptans)
			{
				foreach(string naptan in naptans)
				{
					StopsRow dr = Stops.NewStopsRow();
					dr.atcocode = naptan;
					dr.natgazid = "somewhere";
					dr.X = 1;
					dr.Y = 1;
					Stops.Rows.Add(dr);
				}
			}
		}

		// Dummy NaPTAN groups. Completely arbitary values
		private string[] naptans1 = {"AAA", "AAB"};
		private string[] naptans2 = {"BAA", "BAB"};
		private string[] naptans3 = {"CAA", "CAB"};
		private string[] naptans4 = {"DAA", "DAB"};

		private ArrayList group1 = new ArrayList();
		private ArrayList group2 = new ArrayList();
		private ArrayList group3 = new ArrayList();
		private ArrayList group4 = new ArrayList();

		private QuerySchema qs1;
		private QuerySchema qs2;
		private QuerySchema qs3;
		private QuerySchema qs4;

		public DummyGisQuery()
		{	
			// Set up our DummyQuerySchemas as search groups of NaPTANs
			qs1 = new DummyQuerySchema(naptans1);
			qs2 = new DummyQuerySchema(naptans2);
			qs3 = new DummyQuerySchema(naptans3);
			qs4 = new DummyQuerySchema(naptans4);

			group1.Add(naptans1[0]);
			group1.Add(naptans1[1]);
			group2.Add(naptans2[0]);
			group2.Add(naptans2[1]);
			group3.Add(naptans3[0]);
			group3.Add(naptans3[1]);
			group4.Add(naptans4[0]);
			group4.Add(naptans4[1]);
		}



		/// <summary>
		/// Wrapper for the Gis Query FindNearestStops
		/// </summary>
		/// <param name="x">x coordonate</param>
		/// <param name="y">y coordonate</param>
		/// <param name="maxDistance">max Walking distance</param>
		/// <returns>returns a QuerySchema representing the GIS query result</returns>
		public QuerySchema FindNearestStops(double x, double y, int maxDistance)
		{
			return new QuerySchema();
		
		}

		/// <summary>
		/// Wrapper for the Gis Query FindNearestITNs
		/// </summary>
		/// <param name="x">x coordonate</param>
		/// <param name="y">y coordonate</param>
		/// <returns>returns a QuerySchema representing the GIS query result</returns>
		public QuerySchema FindNearestITNs(double x, double y)
		{
			return new QuerySchema();
		}

		/// <summary>
		/// Wrapper for the Gis Query FindNearestStopsAndITNs
		/// </summary>
		/// <param name="x">x coordonate</param>
		/// <param name="y">y coordonate</param>
		/// <param name="maxDistance">max Walking distance</param>
		/// <returns>returns a QuerySchema representing the GIS query result</returns>
		public QuerySchema FindNearestStopsAndITNs(double x, double y, int maxDistance)
		{
			return new QuerySchema();
		}

		/// <summary>
		/// Wrapper for the Gis Query FindStopsInGroupForStops
		/// </summary>
		/// <param name="naptanIDs">naptan IDs</param>
		/// <returns>returns a QuerySchema representing the GIS query result</returns>
		public QuerySchema FindStopsInGroupForStops(string[] naptanIDs)
		{
			if (group1.Contains(naptanIDs[0]))
			{
				return qs1;
			} 
			else if (group2.Contains(naptanIDs[0]))
			{
				return qs2;
			}
			else if (group3.Contains(naptanIDs[0]))
			{
				return qs3;
			}
			else 
			{
				return qs4;
			}

		}

		
		/// <summary>
		/// Wrapper for the Gis Query FindStopsInGroupForStops
		/// </summary>
		/// <param name="schema">An existing QuerySchema to be augmented with Naptan Groups</param>
		/// <returns>returns a QuerySchema representing the GIS query result</returns>
		public QuerySchema FindStopsInGroupForStops(QuerySchema schema)
		{
			return new QuerySchema();
		}

	
	}
}
