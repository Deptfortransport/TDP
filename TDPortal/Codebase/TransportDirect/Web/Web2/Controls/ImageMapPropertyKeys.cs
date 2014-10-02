// *********************************************** 
// NAME                 : ImageMapPropertyKeys.cs 
// AUTHOR               : Jonathan George
// DATE CREATED         : 11/07/2005
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/ImageMapPropertyKeys.cs-arc  $
//
//   Rev 1.2   Mar 31 2008 13:21:08   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:15:00   mturner
//Initial revision.
//
//   Rev 1.2   Feb 23 2006 19:16:46   build
//Automatically merged from branch for stream3129
//
//   Rev 1.1.1.0   Jan 10 2006 15:25:26   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.1   Aug 03 2005 14:02:18   jgeorge
//FxCop recommended changes
//Resolution for 2558: Del 8 Stream: Incident mapping
//
//   Rev 1.0   Jul 11 2005 17:15:36   jgeorge
//Initial revision.

using System;using TransportDirect.Common.ResourceManager;

namespace TransportDirect.UserPortal.Web.Controls
{
	/// <summary>
	/// Keys used for loading ImageMap initialisation info from the properties service
	/// </summary>
	public sealed class ImageMapPropertyKeys
	{
		/// <summary>
		/// Private default constructor to prevent instantiation
		/// </summary>
		private ImageMapPropertyKeys()
		{
		}

		public const string ImageUrlResourceId = "{0}.ImageUrlResourceId";
		public const string RegionIds = "{0}.RegionIds";
		public const string RegionToolTip = "{0}.{1}.ToolTip";
		public const string RegionHighlightImageResourceId = "{0}.{1}.HighlightImageResourceId";
		public const string RegionSelectedImageResourceId = "{0}.{1}.SelectedImageResourceId";
		public const string RegionType = "{0}.{1}.RegionType";

		public const string RegionCircleCentreX = "{0}.{1}.centre.x";
		public const string RegionCircleCentreY = "{0}.{1}.centre.y";
		public const string RegionCircleCentreRadius = "{0}.{1}.radius";

		public const string RegionRectangleTopLeftX = "{0}.{1}.topleft.x";
		public const string RegionRectangleTopLeftY = "{0}.{1}.topleft.y";
		public const string RegionRectangleBottomRightX = "{0}.{1}.bottomright.x";
		public const string RegionRectangleBottomRightY = "{0}.{1}.bottomright.y";

		public const string RegionPolygonPoints = "{0}.{1}.points";
		public const string RegionPolygonPointX = "{0}.{1}.point.{2}.x";
		public const string RegionPolygonPointY = "{0}.{1}.point.{2}.y";
	}
}
