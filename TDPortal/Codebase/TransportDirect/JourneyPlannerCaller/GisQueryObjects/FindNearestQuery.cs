using System;
using System.Collections.Generic;
using System.Text;
using TransportDirect.Presentation.InteractiveMapping;

namespace JourneyPlannerCaller.GisQueryObjects
{
    [Serializable]
    public class FindNearestQuery : IGisQuery
    {
        private GisQueryFunction queryFunction;
        private string resultPath;
        private double x;
        private double y;
        private int maxDistance;
        private int searchDistance;
        private string[] naptanIds;
        private string[] stopTypes;
        private QuerySchema querySchema;
        private string address;
        private bool ignoreMotorways;
        private string toid;

        public FindNearestQuery()
        {
        }

        public double X
        {
            get { return x; }
            set { x = value; }
        }

        public double Y
        {
            get { return y; }
            set { y = value; }
        }

        public int MaxDistance
        {
            get { return maxDistance; }
            set { maxDistance = value; }
        }

        public int SearchDistance
        {
            get { return searchDistance; }
            set { searchDistance = value; }
        }

        public string[] NaptanIds
        {
            get { return naptanIds; }
            set { naptanIds = value; }
        }

        public string[] StopTypes
        {
            get { return stopTypes; }
            set { stopTypes = value; }
        }

        public QuerySchema Query
        {
            get { return querySchema; }
            set { querySchema = value; }
        }

        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        public bool IgnoreMotorways
        {
            get { return ignoreMotorways; }
            set { ignoreMotorways = value; }
        }

        public string ToId
        {
            get { return toid; }
            set { toid = value; }
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
