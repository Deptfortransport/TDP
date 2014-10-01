// *********************************************** 
// NAME                 : Naptan.cs
// AUTHOR               : Manuel Dambrine
// DATE CREATED         : 04/01/2006
// DESCRIPTION  		: Naptan Class.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/Common/V1/Naptan.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:22:18   mturner
//Initial revision.
//
//   Rev 1.6   Mar 03 2006 15:42:54   mtillett
//Add Read/write to properties to pass code review CR056
//
//   Rev 1.5   Feb 02 2006 14:43:16   mdambrine
//rework on the journey planner enhanced exposed services see CR053_IR_3407 Journey Planner Service Component.doc 
//
//   Rev 1.4   Feb 01 2006 18:50:56   schand
//Code Review changes
//Resolution for 3454: DEL 8.1 Stream: IR for Module assocaitions for Mobile Service TD111
//
//   Rev 1.3   Jan 25 2006 16:18:20   mdambrine
//Adding serialization attribute
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.2   Jan 16 2006 10:44:06   mdambrine
//adding setters to the properties
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.1   Jan 06 2006 15:59:14   halkatib
//Applied changes required by wsdl documents for IR3407
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.0   Jan 04 2006 11:05:20   mdambrine
//Initial revision.
//Resolution for 3407:  DEL 8.1 Stream: IR for Module associations for Lauren  TD103

using System;

namespace TransportDirect.EnhancedExposedServices.DataTransfer.Common.V1
{
	[System.Serializable]
	public class Naptan
	{

		private OSGridReference osgr;
		private string naptan = string.Empty;
		private string stringName = string.Empty;

		/// <summary>
		/// constructor
		/// </summary>
		public Naptan()
		{
			
		}													

		/// <summary>
		/// Read/write Grid reference of NaPTAN.
		/// </summary>
		public OSGridReference GridReference
		{
			get
			{
				return osgr;
			}
			set
			{
				osgr = value;
			}
		}

		/// <summary>
		/// Read/write Id of NaPTAN.
		/// </summary>
		public string NaptanId
		{
			get
			{
				return naptan;
			}
			set
			{
				naptan = value;
			}
		}

		/// <summary>
		/// Read/write Name of NaPTAN.
		/// </summary>
		public string Name
		{
			get
			{
				return stringName;
			}
			set
			{
				stringName = value;
			}
		}
	}// END CLASS DEFINITION Naptan

} // TransportDirect.EnhancedExposedServices.DataTransfer.Common.V1
