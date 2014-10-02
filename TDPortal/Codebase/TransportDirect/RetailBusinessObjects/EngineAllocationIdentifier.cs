//********************************************************************************
//NAME         : EngineAllocationIdentifier.cs
//AUTHOR       : SchlumbergerSema
//DATE CREATED : 27/10/2003
//DESCRIPTION  : Identifier struct used to control allocation of engines to
//				 business objects.
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/RetailBusinessObjects/EngineAllocationIdentifier.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:46:04   mturner
//Initial revision.
//
//   Rev 1.0   Oct 28 2003 20:04:12   geaton
//Initial Revision

using System;

namespace TransportDirect.UserPortal.RetailBusinessObjects
{
	/// <summary>
	/// Struct defining the engine allocation identifier.
	/// Used as a mechanism for objects, that are allocated engines, 
	/// to check that the originally allocated engine is available for use.
	/// </summary>
	public struct EngineAllocationIdentifier : ICloneable
	{
		/// <summary>
		/// Implements clone interface.
		/// </summary>
		/// <returns>Cloned structure.</returns>
		public object Clone()
		{
			EngineAllocationIdentifier clone;
			clone.internalId = this.internalId;
			return (object)clone;
		}

		/// <summary>
		/// Id represented as a datetime.
		/// </summary>
		private DateTime internalId;
		
		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="dummy">
		/// Parameter is discarded. 
		/// Paremeter is required since struct constructors must take 
		/// at least one parameter. 
		/// </param>
		public EngineAllocationIdentifier(bool dummy)
		{
			this.internalId = DateTime.Now;
		}
		
		/// <summary>
		/// Compares object for equality.
		/// </summary>
		/// <param name="obj">Object to compare against.</param>
		/// <returns>True if object is equal, else false.</returns>
		public override bool Equals(Object obj) 
		{
			return ((obj is EngineAllocationIdentifier) &&
				(this == (EngineAllocationIdentifier)obj));
		}

		/// <summary>
		/// Overload for ==
		/// </summary>
		/// <param name="x">Object to compare.</param>
		/// <param name="y">Object to compare.</param>
		/// <returns>True if objects equal, else false.</returns>
		public static bool operator == (EngineAllocationIdentifier x, EngineAllocationIdentifier y) 
		{
			return x.internalId == y.internalId;
		}

		/// <summary>
		/// Overload for !=
		/// </summary>
		/// <param name="x">Object to compare.</param>
		/// <param name="y">Object to compare.</param>
		/// <returns>True if objects not equal, else false.</returns>
		public static bool operator != ( EngineAllocationIdentifier x, EngineAllocationIdentifier y ) 
		{
			return ! ( x == y );
		}

		/// <summary>
		/// Gets hash code for instance.
		/// </summary>
		/// <returns>Hash code.</returns>
		public override int GetHashCode()
		{ 
			return internalId.GetHashCode();
		}

		/// <summary>
		/// Converts instance to a string representation.
		/// </summary>
		/// <returns>String representation of instance.</returns>
		public override String ToString() 
		{
			return this.internalId.ToString("yyyy-MM-ddTHH:mm:ss.fff");
		}
	}
}