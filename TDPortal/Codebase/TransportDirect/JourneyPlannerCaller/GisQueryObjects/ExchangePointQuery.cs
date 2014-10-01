using System;
using System.Collections.Generic;
using System.Text;
using TransportDirect.UserPortal.LocationService;
using System.Xml.Serialization;

namespace JourneyPlannerCaller.GisQueryObjects
{
    [Serializable]
    public class ExchangePointQuery : IGisQuery
    {
        private GisQueryFunction queryFunction;
        private string resultPath;
        private int x;
        private int y;
        private int radius;
        private string mode;
        private int maximum;

        public int X
        {
            get { return x; }
            set { x = value; }
        }

        public int Y
        {
            get { return y; }
            set { y = value; }
        }

        public int Radius
        {
            get { return radius; }
            set { radius = value; }
        }

        public string Mode
        {
            get { return mode; }
            set { mode = value; }
        }

        public int Maximum
        {
            get { return maximum; }
            set { maximum = value; }
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
