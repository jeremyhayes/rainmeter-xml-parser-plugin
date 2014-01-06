namespace RainmeterXmlParserPlugin
{
    using System;
    using System.Collections.Generic;
    using RM = Rainmeter;

    public static class Plugin
    {
        internal static Dictionary<uint, XmlParserBase> Measures = new Dictionary<uint, XmlParserBase>();

        [RM.DllExport]
        public unsafe static void Initialize(void** data, void* rm)
        {
            uint id = (uint)((void*)*data);

            RM.API api = new RM.API((IntPtr)rm);

            string parentName = api.ReadString("Parent", null);
            if (string.IsNullOrEmpty(parentName))
                Measures.Add(id, new XmlParserParent());
            else
                Measures.Add(id, new XmlParserChild());
        }

        [RM.DllExport]
        public unsafe static void Finalize(void* data)
        {
            uint id = (uint)data;
            Measures[id].Cleanup();
            Measures.Remove(id);
        }

        [RM.DllExport]
        public unsafe static void Reload(void* data, void* rm, double* maxValue)
        {
            uint id = (uint)data;
            Measures[id].Reload(new RM.API((IntPtr)rm), ref *maxValue);
        }

        [RM.DllExport]
        public unsafe static double Update(void* data)
        {
            uint id = (uint)data;
            return Measures[id].Update();
        }

        [RM.DllExport]
        public unsafe static char* GetString(void* data)
        {
            uint id = (uint)data;
            fixed (char* s = Measures[id].GetString())
                return s;
        }
    }
}
