// *********************************************** 
// NAME                 : ImageMapCircleRegion.cs 
// AUTHOR               : Jonathan George
// DATE CREATED         : 11/07/2005
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/ImageMapCircleRegion.cs-arc  $
//
//   Rev 1.2   Mar 31 2008 13:21:06   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:15:00   mturner
//Initial revision.
//
//   Rev 1.2   Feb 23 2006 19:16:44   build
//Automatically merged from branch for stream3129
//
//   Rev 1.1.1.1   Jan 30 2006 14:41:08   mdambrine
//moved TDCultureInfo to the common project
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.1.1.0   Jan 10 2006 15:25:24   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.1   Aug 03 2005 14:02:02   jgeorge
//FxCop recommended changes
//Resolution for 2558: Del 8 Stream: Incident mapping
//
//   Rev 1.0   Jul 11 2005 17:15:30   jgeorge
//Initial revision.

using System;
using System.Drawing;
using System.Globalization;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ResourceManager;

namespace TransportDirect.UserPortal.Web.Controls
{
	/// <summary>
	/// Server side representation of an HTML imagemap AREA element with SHAPE=CIRCLE.
	/// </summary>
	public class ImageMapCircleRegion : ImageMapRegion
	{
		#region Private fields
		
		/// <summary>
		/// Backing field for the Centre property
		/// </summary>
		private readonly Point centre;

		/// <summary>
		/// Backing field for the Radius property
		/// </summary>
		private readonly int radius;

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor. Initialises the object with the supplied data
		/// </summary>
		/// <param name="id">ID of the new region</param>
		/// <param name="toolTip">Tooltip to display when the user points at the region</param>
		/// <param name="highlightImageUrl">Image to use to "highlight" the region</param>
		/// <param name="selectedImageUrl">Image to use when the region is selected.</param>
		/// <param name="centre">Centre of the region</param>
		/// <param name="radius">Radius of the region, in pixels</param>
		public ImageMapCircleRegion( string id, string toolTip, string highlightImageUrl, string selectedImageUrl, Point centre, int radius) :
			base(id, toolTip, highlightImageUrl, selectedImageUrl)
		{
			this.centre = centre;
			this.radius = radius;
		}

		/// <summary>
		/// Constructor. Loads the region data from the properties service.
		/// </summary>
		/// <param name="baseKey">The key which forms the base for all relevent properties</param>
		/// <param name="id">The id of the region to load the data for</param>
		/// <param name="p">The property provider to use to retrieve data</param>
		/// <param name="rm">The resource manager to use when converting resource ids from the 
		/// properties service into the text values and image urls to use.</param>
		public ImageMapCircleRegion( string baseKey, string id, IPropertyProvider properties, TDResourceManager resourceManager ) : 
			base(baseKey, id, properties, resourceManager)
		{
			int x = Convert.ToInt32( properties[ string.Format( CultureInfo.InvariantCulture, ImageMapPropertyKeys.RegionCircleCentreX, new object[] { baseKey, id }) ], CultureInfo.InvariantCulture );
			int y = Convert.ToInt32( properties[ string.Format( CultureInfo.InvariantCulture, ImageMapPropertyKeys.RegionCircleCentreY, new object[] { baseKey, id }) ], CultureInfo.InvariantCulture );
			centre = new Point(x, y);
			radius = Convert.ToInt32( properties[ string.Format( CultureInfo.InvariantCulture, ImageMapPropertyKeys.RegionCircleCentreRadius, new object[] { baseKey, id }) ], CultureInfo.InvariantCulture );
		}

		#endregion

		#region Public properties

		/// <summary>
		/// Read only property returning the co-ordinates of the centre of the circle.
		/// </summary>
		public Point Centre
		{
			get { return centre; }
		}

		/// <summary>
		/// Read only property returning the radius of the circle, in pixels
		/// </summary>
		public int Radius
		{
			get { return radius; }
		}

		/// <summary>
		/// Read only property returning the value for the SHAPE attribute of the MAP element.
		/// </summary>
		public override string Shape 
		{ 
			get { return "circle"; }
		}

		#endregion

		#region Methods

		/// <summary>
		/// Returns the value for the COORDS attribute of the MAP element.
		/// </summary>
		/// <returns></returns>
		public override string GetCoordsAttributeValue()
		{
			return string.Format(CultureInfo.InvariantCulture, "{0},{1},{2}", new object[] { centre.X, centre.Y, radius });
		}

		#endregion
	
	}
}
