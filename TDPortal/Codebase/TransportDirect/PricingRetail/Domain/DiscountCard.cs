// *********************************************** 
// NAME			: DiscountCard.cs
// AUTHOR		: Jonathan George
// DATE CREATED	: 26/01/2005
// DESCRIPTION	: Implementation of the DiscountCard class
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/Domain/DiscountCard.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:36:46   mturner
//Initial revision.
//
//   Rev 1.5   Jan 27 2005 11:39:02   jgeorge
//Updated for Del 7

using System;
using TransportDirect.JourneyPlanning.CJPInterface;

namespace TransportDirect.UserPortal.PricingRetail.Domain
{
	/// <summary>
	/// Class representing a DiscountCard entity
	/// </summary>
	[Serializable]
	public class DiscountCard
	{
		#region Private members

		private readonly int id;
		private readonly string code;
		private readonly ModeType mode;
		private readonly int maxAdults;
		private readonly int maxChildren;

		#endregion

		#region Constructor

		/// <summary>
		/// Standard constructor. Object is immutable so all property values are supplied
		/// at object creation.
		/// </summary>
		/// <param name="id">Unique identifier for the card</param>
		/// <param name="code">Code for the card - must be unique for the mode</param>
		/// <param name="mode">Mode of transport that the card applies to</param>
		/// <param name="maxAdults">Maximum number of adults that can travel on this card</param>
		/// <param name="minAdults">Maximum number of children that can travel on this card</param>
		internal DiscountCard(int id, string code, ModeType mode, int maxAdults, int maxChildren)
		{
			this.id = id;
			this.code = code;
			this.mode = mode;
			this.maxAdults = maxAdults;
			this.maxChildren = maxChildren;
		}

		#endregion

		#region Public properties

		/// <summary>
		/// Unique identifier for the card
		/// </summary>
		public int Id
		{
			get { return id; }
		}

		/// <summary>
		/// Code for the card - must be unique for the mode
		/// </summary>
		public string Code
		{
			get { return code; }
		}

		/// <summary>
		/// Mode of transport that the card applies to
		/// </summary>
		public ModeType Mode
		{
			get { return mode; }
		}

		/// <summary>
		/// Maximum number of adults that can travel on this card
		/// </summary>
		public int MaxAdults
		{
			get { return maxAdults; }
		}

		/// <summary>
		/// Maximum number of children that can travel on this card
		/// </summary>
		public int MaxChildren
		{
			get { return maxChildren; }
		}

		#endregion

	}
}
