// *********************************************** 
// NAME                 : MapImageProperties.ascx
// AUTHOR               : Annukka Viitanen
// DATE CREATED         : 03/02/2006 
// DESCRIPTION  		: Contains properties used in displaying high-resolution map image 
// on a printable page.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Adapters/MapImageProperties.cs-arc  $
//
//   Rev 1.2   Mar 31 2008 12:59:10   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:11:26   mturner
//Initial revision.
//
//   Rev 1.0   Feb 09 2006 19:19:36   aviitanen
//Initial revision.


namespace TransportDirect.UserPortal.Web.Adapters
{
	/// <summary>
	/// Contructor - sets image properties
	/// </summary>
	/// <param name="imageURL">map image URL as a string</param>
	/// <param name="imageHeight">height of map image as an integer</param>
	/// <param name="imageWidth">width of map image as integer</param>
	public class MapImageProperties
	{
		public MapImageProperties(string imageUrl, int imageHeight, int imageWidth)
		{
			this.imageUrl = imageUrl;
			this.imageHeight = imageHeight;
			this.imageWidth = imageWidth;
		}
		
		/// <summary>
		/// Map image URL
		/// </summary>
		private string imageUrl;

		/// <summary>
		/// Map image height
		/// </summary> 
		private int imageHeight;

		/// <summary>
		/// Map image width
		/// </summary>
		private int imageWidth;


		/// <summary>
		/// Gets image URL
		/// </summary>
		public string ImageUrl
		{
			get { return imageUrl; }
		}

		/// <summary>
		/// Gets image height
		/// </summary>
		public int ImageHeightInPixels
		{
			get { return imageHeight; }
		}

		/// <summary>
		/// Gets image width
		/// </summary>
		public int ImageWidthInPixels
		{
			get { return imageWidth; }
		}
	}
}
