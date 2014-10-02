// *********************************************** 
// NAME                 : LocationQueryResult.cs
// AUTHOR               : Patrick ASSUIED
// DATE CREATED         : 29/08/2003 
// DESCRIPTION  : Class used in LocationSearch to store the query results
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/LocationQueryResult.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:25:10   mturner
//Initial revision.
//
//   Rev 1.6   Apr 13 2005 09:10:12   rscott
//DEL 7 Additional Tasks - IR1978 enhancements added - reject single word address.
//
//   Rev 1.5   Apr 07 2005 16:23:52   rscott
//Updated with DEL7 additional task outlined in IR1977.
//
//   Rev 1.4   Mar 03 2004 15:24:50   COwczarek
//Added ParentChoice property. For hierarchic searches, this is
//the drop down location choice that created the query result
//Resolution for 649: Changes to the way ambiguous locations are resolved
//
//   Rev 1.3   Sep 22 2003 17:31:22   passuied
//made all objects serializable
//
//   Rev 1.2   Sep 22 2003 13:47:30   passuied
//updated
//
//   Rev 1.1   Sep 09 2003 17:23:50   passuied
//Clean up all files to pass code review!
//
//   Rev 1.0   Sep 05 2003 15:30:32   passuied
//Initial Revision

using System;

namespace TransportDirect.UserPortal.LocationService
{
    /// <summary>
    /// Class used in LocationSearch to store the query results
    /// </summary>
    [Serializable()]
    public class LocationQueryResult
    {
        private string stringPickListUsed = string.Empty;
        private LocationChoiceList choiceList;
        private string stringQueryReference = string.Empty;
        private LocationChoice parentChoice;
		private bool isVague = false;
		private bool isSingleWordAddress = false;

        public LocationQueryResult(string pickListUsed)
        {
            choiceList = new LocationChoiceList();
            stringPickListUsed = pickListUsed;
        }

		/// <summary>
		/// Bool that indicates the return from the gazetteer location search is vague
		/// i.e. too many results
		/// </summary>
		public bool IsVague
		{
			get{return isVague;}
			set{isVague = value;}
		}

		/// <summary>
		/// Bool that indicates the address was a single word search
		/// </summary>
		public bool IsSingleWordAddress
		{
			get{return isSingleWordAddress;}
			set{isSingleWordAddress = value;}
		}


        /// <summary>
        /// Read-Write property. get/set the query reference
        /// </summary>
        public string QueryReference
        {
            get
            {
                return stringQueryReference;
            }

            set
            {
                stringQueryReference = value;
            }
        }

        /// <summary>
        /// Choice used to create this result (for hierarchic searches)
        /// </summary>
        public LocationChoice ParentChoice {
            get {
                return parentChoice;
            }

            set {
                parentChoice = value;
            }
        }

        /// <summary>
        /// Read-Write property. get/set the locationChoice list
        /// </summary>
        public LocationChoiceList LocationChoiceList
        {
            get
            {
                return choiceList;
            }

            set
            {
                choiceList = value;
            }
        }

        /// <summary>
        /// Read-only property. get pickList string used 
        /// </summary>
        public string PickListUsed
        {
            get
            {
                return stringPickListUsed;
            }
        }

        

    }
}
