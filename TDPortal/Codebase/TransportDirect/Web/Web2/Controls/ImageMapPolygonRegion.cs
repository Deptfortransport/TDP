// *********************************************** 
// NAME                 : ImageMapPolygonRegion.cs 
// AUTHOR               : Jonathan George
// DATE CREATED         : 11/07/2005
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/ImageMapPolygonRegion.cs-arc  $
//
//   Rev 1.3   Dec 17 2008 11:26:48   apatel
//XHTML Compliance Changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.2   Mar 31 2008 13:21:06   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:15:00   mturner
//Initial revision.
//
//   Rev 1.3   Feb 23 2006 19:16:44   build
//Automatically merged from branch for stream3129
//
//   Rev 1.2.1.1   Jan 30 2006 14:41:10   mdambrine
//moved TDCultureInfo to the common project
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.2.1.0   Jan 10 2006 15:25:26   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.2   Aug 03 2005 14:02:12   jgeorge
//FxCop recommended changes
//Resolution for 2558: Del 8 Stream: Incident mapping
//
//   Rev 1.1   Jul 28 2005 12:04:22   jgeorge
//Bug fix
//Resolution for 2558: Del 8 Stream: Incident mapping
//
//   Rev 1.0   Jul 11 2005 17:15:36   jgeorge
//Initial revision.

using System;
using System.Drawing;
using System.Text;
using System.Globalization;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ResourceManager;

namespace TransportDirect.UserPortal.Web.Controls
{
	/// <summary>
	/// Server side representation of an HTML imagemap AREA element with SHAPE=POLYGON.
	/// </summary>
	public class ImageMapPolygonRegion : ImageMapRegion
	{
		#region Private fields

		/// <summary>
		/// Backing field for the Points property
		/// </summary>
		private readonly Point[] points;

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor. Initialises the object with the supplied data
		/// </summary>
		/// <param name="id">ID of the new region</param>
		/// <param name="toolTip">Tooltip to display when the user points at the region</param>
		/// <param name="highlightImageUrl">Image to use to "highlight" the region</param>
		/// <param name="selectedImageUrl">Image to use when the region is selected.</param>
		/// <param name="points">Array of Point objects which define the perimeter of the area</param>
		public ImageMapPolygonRegion( string id, string toolTip, string highlightImageUrl, string selectedImageUrl, Point[] points) :
			base(id, toolTip, highlightImageUrl, selectedImageUrl)
		{
			this.points = (Point[])points.Clone();
		}

		/// <summary>
		/// Constructor. Loads the region data from the properties service.
		/// </summary>
		/// <param name="baseKey">The key which forms the base for all relevent properties</param>
		/// <param name="id">The id of the region to load the data for</param>
		/// <param name="p">The property provider to use to retrieve data</param>
		/// <param name="rm">The resource manager to use when converting resource ids from the 
		/// properties service into the text values and image urls to use.</param>
		public ImageMapPolygonRegion( string baseKey, string id, IPropertyProvider properties, TDResourceManager resourceManager ) : 
			base(baseKey, id, properties, resourceManager)
		{
			int noOfPoints = Convert.ToInt32( properties[ string.Format( CultureInfo.InvariantCulture, ImageMapPropertyKeys.RegionPolygonPoints, new object[] { baseKey, id }) ], CultureInfo.InvariantCulture );
			points = new Point[noOfPoints];

			for (int i = 1; i <= noOfPoints; i++)
			{
				int x = Convert.ToInt32( properties[ string.Format( CultureInfo.InvariantCulture, ImageMapPropertyKeys.RegionPolygonPointX, new object[] { baseKey, id, i }) ], CultureInfo.InvariantCulture );
				int y = Convert.ToInt32( properties[ string.Format( CultureInfo.InvariantCulture, ImageMapPropertyKeys.RegionPolygonPointY, new object[] { baseKey, id, i }) ], CultureInfo.InvariantCulture );
				points[i - 1] = new Point(x, y);
			}
		}

		#endregion

		#region Public properties


		/// <summary>
		/// Read only property returning the value for the SHAPE attribute of the MAP element.
		/// </summary>
		public override string Shape 
		{ 
			get { return "poly"; }
		}

		#endregion

		#region Methods

		/// <summary>
		/// Returns the array of Point objects which define the perimeter of the area. 
		/// Updating this array has no effect on the points defined by the control.
		/// </summary>
		public Point[] GetPoints()
		{
			return (Point[])points.Clone();
		}	

		/// <summary>
		/// Returns the value for the COORDS attribute of the MAP element.
		/// </summary>
		/// <returns></returns>
		public override string GetCoordsAttributeValue()
		{
			if (points.Length == 0)
				return string.Empty;

			StringBuilder sb = new StringBuilder();
			
			foreach(Point p in points)
			{
				sb.Append(p.X);
				sb.Append(",");
				sb.Append(p.Y);
				sb.Append(",");
			}
			//Remove the trailing comma that we now have
			sb.Remove(sb.Length - 1, 1);			
			return sb.ToString();
		}

		#endregion
	}
}
