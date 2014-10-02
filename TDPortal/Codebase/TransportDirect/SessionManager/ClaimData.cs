using System;
using TransportDirect.Common;

namespace TransportDirect.UserPortal.SessionManager
{
	/// <summary>
	/// Summary description for ClaimData.
	/// </summary>
	[Serializable()]
	public class ClaimData
	{
		private bool twoWeekFlag;
		private bool transportDirectProofFlag;
		private bool travelProofFlag;
		private TDDateTime obtainedPlan;
		private string firstName;
		private string lastName;
		private string address1;
		private string address2;
		private string address3;
		private string postCode;
		private string emailAddress;
		private string confirmEmailAddress;
		private string travellingFrom;
		private string travellingTo;
		private TDDateTime problemDate;
		private bool train;
		private bool tram;
		private bool bus;
		private bool underground;
		private bool ferry;
		private bool plane;
		private string problemOccured;
		private string details;

		private bool readPolicy;
		private bool emailToUser;

		public ClaimData()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public bool TwoWeekFlag
		{
			get { return twoWeekFlag; }
			set { twoWeekFlag = value; }
		}

		public bool TransportDirectProofFlag
		{
			get { return transportDirectProofFlag; }
			set { transportDirectProofFlag = value; }
		}

		public bool TravelProofFlag
		{
			get { return travelProofFlag; }
			set { travelProofFlag = value; }
		}

		public TDDateTime ObtainedPlan
		{
			get { return obtainedPlan; }
			set { obtainedPlan = value; }
		}

		public string FirstName
		{
			get { return firstName; }
			set { firstName = value; }
		}

		public string LastName
		{
			get { return lastName; }
			set { lastName = value; }
		}

		public string Address1
		{
			get { return address1; }
			set { address1 = value; }
		}

		public string Address2
		{
			get { return address2; }
			set { address2 = value; }
		}

		public string Address3
		{
			get { return address3; }
			set { address3 = value; }
		}

		public string PostCode
		{
			get { return postCode; }
			set { postCode = value; }
		}

		public string EmailAddress
		{
			get { return emailAddress; }
			set { emailAddress = value; }
		}

		public string ConfirmEmailAddress
		{
			get { return confirmEmailAddress; }
			set { confirmEmailAddress = value; }
		}

		public string TravellingFrom
		{
			get { return travellingFrom; }
			set { travellingFrom = value; }
		}

		public string TravellingTo
		{
			get { return travellingTo; }
			set { travellingTo = value; }
		}

		public TDDateTime ProblemDate
		{
			get { return problemDate; }
			set { problemDate = value; }
		}

		public bool Train
		{
			get { return train; }
			set { train = value; }
		}

		public bool Tram
		{
			get { return tram; }
			set { tram = value; }
		}

		public bool Bus
		{
			get { return bus; }
			set { bus = value; }
		}

		public bool Underground
		{
			get { return underground; }
			set { underground = value; }
		}

		public bool Ferry
		{
			get { return ferry; }
			set { ferry = value; }
		}

		public bool Plane
		{
			get { return plane; }
			set { plane = value; }
		}

		public string ProblemOccured
		{
			get { return problemOccured; }
			set { problemOccured = value; }
		}

		public string Details
		{
			get { return details; }
			set { details = value; }
		}

		public bool ReadPolicy
		{
			get { return readPolicy; }
			set { readPolicy = value; }
		}

		public bool EmailToUser
		{
			get { return emailToUser; }
			set { emailToUser = value; }
		}
	}
}
