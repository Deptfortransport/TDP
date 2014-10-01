using System;
using System.Collections.Generic;
using System.Text;
using TransportDirect.Presentation.InteractiveMapping;

namespace JourneyPlannerCaller.GisQueryObjects
{
    [Serializable]
    public class GisResult
    {
        private QuerySchema querySchemaResult;
        private string outputResult;
        private Point resultAsPoint;
        private string[] outputResults;
        private ExchangePointSchema exchangePointSchemaResult;

        public GisResult()
        {
        }

        public QuerySchema QuerySchemaResult
        {
            get { return querySchemaResult; }
            set { querySchemaResult = value; }
        }

        public string OutputResult
        {
            get { return outputResult; }
            set { outputResult = value; }
        }

        public string[] OutputResults
        {
            get { return outputResults; }
            set { outputResults = value; }
        }

        public Point ResultAsPoint
        {
            get { return resultAsPoint; }
            set { resultAsPoint = value; }
        }

        public ExchangePointSchema ExchangePointSchemaResult
        {
            get { return exchangePointSchemaResult; }
            set { exchangePointSchemaResult = value; }
        }

    }
}
