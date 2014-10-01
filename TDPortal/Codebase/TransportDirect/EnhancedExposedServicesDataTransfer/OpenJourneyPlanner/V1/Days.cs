// *********************************************** 
// NAME                 : Days.cs
// AUTHOR               : C.M. Owczarek
// DATE CREATED         : 28.03.06
// DESCRIPTION  		: Enumerator that represents travel days
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/OpenJourneyPlanner/V1/Days.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:22:38   mturner
//Initial revision.
//
//   Rev 1.0   Apr 06 2006 16:20:28   COwczarek
//Initial revision.
//

using System;

namespace TransportDirect.EnhancedExposedServices.DataTransfer.OpenJourneyPlanner.V1 
{
    /// <summary>
    /// Enumerator that represents travel days
    /// </summary>
    [Serializable]
    public enum Days
    {
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday,
        Sunday,
        MondayToFriday,
        MondayToSaturday,
        BankHoliday,
        NotBankHoliday,
        SchoolHoliday,
        NotSchoolHoliday
    }
 
}