namespace RainmeterXmlParserPlugin
{
    using System;
    using System.Xml.XPath;
    using RM = Rainmeter;

    abstract class XmlParserBase
    {
        private string _xpath;
        private string _value;

        internal IntPtr MeasureSkin { get; private set; }
        internal string MeasureName { get; private set; }

        internal virtual void Reload(RM.API rm, ref double maxValue)
        {
            MeasureSkin = rm.GetSkin();
            MeasureName = rm.GetMeasureName();

            _xpath = rm.ReadString("XPath", null);
            RM.API.Log(RM.API.LogType.Debug, GetLogString("XPath=[{0}]", _xpath));
        }

        internal double Update()
        {
            try
            {
                XPathDocument document = GetDocument();

                if (document != null)
                {
                    var navigator = document
                        .CreateNavigator()
                        .SelectSingleNode(_xpath);

                    if (navigator != null)
                    {
                        _value = navigator.InnerXml;
                        //RM.API.Log(RM.API.LogType.Debug, GetLogString("Value=[{0}]", _value));
                    }
                    else
                    {
                        RM.API.Log(RM.API.LogType.Error, GetLogString("Invalid XPath [{0}]", _xpath));
                    }
                }
            }
            catch (Exception ex)
            {
                RM.API.Log(RM.API.LogType.Error, GetLogString("Exception: {0}", ex.ToString()));
            }

            return default(double);
        }

        protected abstract XPathDocument GetDocument();

        internal string GetString()
        {
            return _value;
        }

        internal virtual void Cleanup()
        {
        }

        protected string GetLogString(string format, params object[] args)
        {
            return string.Format(
                "RainmeterXmlParserPlugin.dll: Skin=[{0}], Measure=[{1}]. {2}",
                MeasureSkin,
                MeasureName,
                string.Format(format, args));
        }
    }
}
