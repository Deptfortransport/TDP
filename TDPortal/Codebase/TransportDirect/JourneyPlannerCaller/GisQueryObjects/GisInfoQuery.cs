using System;
using System.Collections.Generic;
using System.Text;
using TransportDirect.Presentation.InteractiveMapping;

namespace JourneyPlannerCaller.GisQueryObjects
{
    public class GisInfoQuery : IGisQuery
    {
        private GisQueryFunction queryFunction;
        private string resultPath;
        string postcode;
        double x;
        double y;
        Point[] points;
        bool sameAreaOnly;
        string naptanId;
        string localityId;
        string[] toids;

        public string Postcode
        {
            get { return postcode; }
            set { postcode = value; }
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

        public Point[] Points
        {
            get { return points; }
            set { points = value; }
        }

        public bool SameAreaOnly
        {
            get { return sameAreaOnly; }
            set { sameAreaOnly = value; }
        }

        public string NaptanId
        {
            get { return naptanId; }
            set { naptanId = value; }
        }

        public string LocalityId
        {
            get { return localityId; }
            set { localityId = value; }
        }

        public string[] Toids
        {
            get { return toids; }
            set { toids = value; }
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
