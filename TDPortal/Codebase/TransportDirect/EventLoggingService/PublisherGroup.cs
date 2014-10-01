// *********************************************** 
// NAME                 : PublisherGroup.cs 
// AUTHOR               : Gary Eaton
// DATE CREATED         : 11/07/2003 
// DESCRIPTION  : Factory class for creating 
// publishing groups.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EventLoggingService/PublisherGroup.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:23:08   mturner
//Initial revision.
//
//   Rev 1.4   Jul 25 2003 14:14:40   geaton
//Changes resulting from code review 2003-07-22. (These were mainly concerned with adding comments and running through FXCop).
//
//   Rev 1.3   Jul 24 2003 18:27:50   geaton
//Added/updated comments

using System;
using System.Collections;

using TransportDirect.Common.PropertyService.Properties;

namespace TransportDirect.Common.Logging
{
	/// <summary>
	/// Abstract factory class for creating publishing groups.
	/// </summary>
	abstract class PublisherGroup
	{
		protected ArrayList publishers;
		protected IPropertyProvider properties;
		protected LoggingPropertyValidator validator;

		/// <summary>
		/// Constructor based on a property provider.</c>
		/// </summary>
		/// <param name="properties">Properties to use to create publisher group.</param>
		public PublisherGroup(IPropertyProvider properties)
		{
			this.properties = properties;
			this.validator = new LoggingPropertyValidator(properties);
			publishers = new ArrayList();
		}

		/// <summary>
		/// Gets the array list of publishers.
		/// </summary>
		public ArrayList Publishers
		{
			get {return publishers;}
		}

		/// <summary>
		/// Creates the publisher group.
		/// </summary>
		/// <param name="errors">Holds errors occurring during publisher creation.</param>
		abstract public void CreatePublishers(ArrayList errors);

	}
}
