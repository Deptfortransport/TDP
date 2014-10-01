// *********************************************** 
// NAME			: EBCCountry.cs
// AUTHOR		: Mitesh Modi
// DATE CREATED	: 21/09/2009
// DESCRIPTION	: Enum identifying the different types of countries for an EBC calculation
// ************************************************ 
//
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnvironmentalBenefits/EBCCountry.cs-arc  $
//
//   Rev 1.0   Oct 06 2009 13:58:42   mmodi
//Initial revision.
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//

using System;
using System.Collections.Generic;
using System.Text;

namespace TransportDirect.UserPortal.EnvironmentalBenefits
{
    public enum EBCCountry
    {
        None,
        England,
        Scotland,
        Wales
    }
}
