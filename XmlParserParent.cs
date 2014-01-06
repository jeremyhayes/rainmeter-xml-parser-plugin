namespace RainmeterXmlParserPlugin
{
    using System;
    using System.Xml.XPath;
    using RM = Rainmeter;

    class XmlParserParent : XmlParserBase
    {
        private string _uri;
        private int _updateRate;
        private DateTime _lastUpdated;

        internal XPathDocument Document { get; private set; }

        internal override void Reload(RM.API rm, ref double maxValue)
        {
            base.Reload(rm, ref maxValue);

            _updateRate = rm.ReadInt("UpdateRate", 3600);
            RM.API.Log(RM.API.LogType.Debug, GetLogString("UpdateRate=[{0}]", _updateRate));

            _uri = rm.ReadString("Uri", null);
            RM.API.Log(RM.API.LogType.Debug, GetLogString("Uri=[{0}]", _uri));
        }

        protected override XPathDocument GetDocument()
        {
            if (Document == null || (DateTime.Now - _lastUpdated).TotalSeconds > _updateRate)
            {
                RM.API.Log(RM.API.LogType.Debug, GetLogString("Fetching: {0}", _uri));
                Document = new XPathDocument(_uri);

                _lastUpdated = DateTime.Now;
            }

            return Document;
        }

        internal override void Cleanup()
        {
            base.Cleanup();

            Document = null;
        }
    }
}
