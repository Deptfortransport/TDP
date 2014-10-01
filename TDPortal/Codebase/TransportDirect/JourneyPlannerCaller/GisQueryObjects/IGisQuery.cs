using System;
using System.Collections.Generic;
using System.Text;

namespace JourneyPlannerCaller.GisQueryObjects
{
    public interface IGisQuery
    {

        GisQueryFunction QueryFunction { get; set; }

        string GisResultPath { get; set; }

    }
}
