namespace RainmeterXmlParserPlugin
{
    using System;
    using System.Xml.XPath;
    using RM = Rainmeter;

    class XmlParserChild : XmlParserBase
    {
        private XmlParserParent _parent;

        internal override void Reload(RM.API rm, ref double maxValue)
        {
            base.Reload(rm, ref maxValue);

            string parentName = rm.ReadString("Parent", null);
            RM.API.Log(RM.API.LogType.Debug, GetLogString("Parent=[{0}]", parentName));

            RuntimeTypeHandle parentHandle = typeof(XmlParserParent).TypeHandle;
            foreach (var measure in Plugin.Measures.Values)
            {
                if (Type.GetTypeHandle(measure).Equals(parentHandle))
                {
                    XmlParserParent parent = (XmlParserParent)measure;
                    if (parent.MeasureName.Equals(parentName)
                        && parent.MeasureSkin.Equals(MeasureSkin))
                    {
                        _parent = parent;
                        return;
                    }
                }
            }

            RM.API.Log(RM.API.LogType.Error, GetLogString("Parent [{0}] not found.", parentName));
        }

        protected override XPathDocument GetDocument()
        {
            if (_parent == null)
                return null;

            return _parent.Document;
        }

        internal override void Cleanup()
        {
            base.Cleanup();

            _parent = null;
        }
    }
}
