// *********************************************** 
// NAME                 : ImageMapRectangleRegion.cs 
// AUTHOR               : Jonathan George
// DATE CREATED         : 11/07/2005
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/ImageMapRectangleRegion.cs-arc  $
//
//   Rev 1.2   Mar 31 2008 13:21:08   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:15:02   mturner
//Initial revision.
//
//   Rev 1.2   Feb 23 2006 19:16:46   build
//Automatically merged from branch for stream3129
//
//   Rev 1.1.1.1   Jan 30 2006 14:41:10   mdambrine
//moved TDCultureInfo to the common project
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.1.1.0   Jan 10 2006 15:25:26   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.1   Aug 03 2005 14:02:22   jgeorge
//FxCop recommended changes
//Resolution for 2558: Del 8 Stream: Incident mapping
//
//   Rev 1.0   Jul 11 2005 17:15:36   jgeorge
//Initial revision.

using System;
using System.Drawing;
using System.Globalization;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ResourceManager;

namespace TransportDirect.UserPortal.Web.Controls
{
	/// <summary>
	/// Server side representation of an HTML imagemap AREA element with SHAPE=RECTANGLE.
	/// </summary>
	public class ImageMapRectangleRegion : ImageMapRegion
	{
		#region Private fields

		/// <summary>
		/// Backing field for the TopLeft property
		/// </summary>
		private readonly Point topLeft;

		/// <summary>
		/// Backing field for the BottomRight property
		/// </summary>
		private readonly Point bottomRight;

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor. Initialises the object with the supplied data
		/// </summary>
		/// <param name="id">ID of the new region</param>
		/// <param name="toolTip">Tooltip to display when the user points at the region</param>
		/// <param name="highlightImageUrl">Image to use to "highlight" the region</param>
		/// <param name="selectedImageUrl">Image to use when the region is selected.</param>
		/// <param name="topLeft">Co-ordinates of the top left corner of the rectangle</param>
		/// <param name="bottomRight">Co-ordinates of the bottom right corner of the rectangle</param>
		public ImageMapRectangleRegion( string id, string toolTip, string highlightImageUrl, string selectedImageUrl, Point topLeft, Point bottomRight) :
			base(id, toolTip, highlightImageUrl, selectedImageUrl)
		{
			this.topLeft = topLeft;
			this.bottomRight = bottomRight;
		}

		/// <summary>
		/// Constructor. Loads the region data from the properties service.
		/// </summary>
		/// <param name="baseKey">The key which forms the base for all relevent properties</param>
		/// <param name="id">The id of the region to load the data for</param>
		/// <param name="p">The property provider to use to retrieve data</param>
		/// <param name="rm">The resource manager to use when converting resource ids from the 
		/// properties service into the text values and image urls to use.</param>
		public ImageMapRectangleRegion( string baseKey, string id, IPropertyProvider properties, TDResourceManager resourceManager ) : 
			base(baseKey, id, properties, resourceManager)
		{
			int tx = Convert.ToInt32( properties[ string.Format( CultureInfo.InvariantCulture, ImageMapPropertyKeys.RegionRectangleTopLeftX, new object[] { baseKey, id }) ], CultureInfo.InvariantCulture );
			int ty = Convert.ToInt32( properties[ string.Format( CultureInfo.InvariantCulture, ImageMapPropertyKeys.RegionRectangleTopLeftY, new object[] { baseKey, id }) ], CultureInfo.InvariantCulture );
			int bx = Convert.ToInt32( properties[ string.Format( CultureInfo.InvariantCulture, ImageMapPropertyKeys.RegionRectangleBottomRightX, new object[] { baseKey, id }) ], CultureInfo.InvariantCulture );
			int by = Convert.ToInt32( properties[ string.Format( CultureInfo.InvariantCulture, ImageMapPropertyKeys.RegionRectangleBottomRightY, new object[] { baseKey, id }) ], CultureInfo.InvariantCulture );

			topLeft = new Point(tx, ty);
			bottomRight = new Point(bx, by);
		}

		#endregion

		#region Public properties

		/// <summary>
		/// Read only property returning the co-ordinates of the top left point of the rectangle
		/// </summary>
		public Point TopLeft
		{
			get { return topLeft; }
		}

		/// <summary>
		/// Read only property returning the co-ordinates of the bottom right point of the rectangle
		/// </summary>
		public Point BottomRight
		{
			get { return bottomRight; }
		}

		/// <summary>
		/// Read only property returning the value for the SHAPE attribute of the MAP element.
		/// </summary>
		public override string Shape 
		{ 
			get { return "rectangle"; }
		}

		#endregion

		#region Methods

		/// <summary>
		/// Returns the value for the COORDS attribute of the MAP element.
		/// </summary>
		/// <returns></returns>
		public override string GetCoordsAttributeValue()
		{
			return string.Format(CultureInfo.InvariantCulture, "{0},{1},{2},{3}", new object[] { topLeft.X, topLeft.Y, bottomRight.X, bottomRight.Y });
		}

		#endregion
	}

}
