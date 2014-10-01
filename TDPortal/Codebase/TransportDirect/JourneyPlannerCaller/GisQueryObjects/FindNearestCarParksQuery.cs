using System;
using System.Collections.Generic;
using System.Text;

namespace JourneyPlannerCaller.GisQueryObjects
{
    [Serializable]
    public class FindNearestCarParksQuery : IGisQuery
    {
        private GisQueryFunction queryFunction;
        private string resultPath;
        private double easting = 0;
        private double northing = 0;
        private int initialRadius = 0;
        private int maxRadius = 0;
        private int maxNoCarParks = 0;

        public FindNearestCarParksQuery()
        {
        }

        public double Easting
        {
            get { return easting; }
            set { easting = value; }
        }

        public double Northing
        {
            get { return northing; }
            set { northing = value; }
        }

        public int InitialRadius
        {
            get { return initialRadius; }
            set { initialRadius = value; }
        }

        public int MaxRadius
        {
            get { return maxRadius; }
            set { maxRadius = value; }
        }

        public int MaxNoCarParks
        {
            get { return maxNoCarParks; }
            set { maxNoCarParks = value; }
        }

        #region IGisQuery Members

        public GisQueryFunction QueryFunction
        {
            get { return queryFunction; }
            set { queryFunction = value; }
        }

        public string GisResultPath
        {
            get { return resultPath; }
            set { resultPath = value; }
        }

        #endregion
    }
}
