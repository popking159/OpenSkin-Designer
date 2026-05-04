using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace OpenSkinDesigner.Structures
{
    public class cConverterSimple
    {
        const string fileName = "converterSimple.xml";

        private readonly string source;

        public string Source
        {
            get { return this.source; }
        }

        private readonly string converterName;

        public string ConverterName
        {
            get { return this.converterName; }
        }

        private readonly string converterParameter;

        public string ConverterParameter
        {
            get { return this.converterParameter; }
        }

        private readonly string value;

        public string Value
        {
            get { return this.value; }
        }

        public cConverterSimple(String source, String converterName, String converterParameter, String value)
        {
            this.source = source;
            this.converterName = converterName;
            this.converterParameter = converterParameter;
            this.value = value;
        }

        public static List<cConverterSimple> ListAll()
        {
            List<cConverterSimplePresets> presets = cConverterSimplePresets.ListAll();

            List<cConverterSimple> result = new List<cConverterSimple>();

            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("xml/" + fileName);
            XmlNodeList entries = xDoc.GetElementsByTagName("entry");

            foreach (XmlNode entry in entries)
            {
                // jump over Comments
                if (entry.NodeType == XmlNodeType.Comment)
                    continue;

                String value = entry.Attributes["value"].Value;

                if(value.StartsWith("#") && value.EndsWith("#") && presets != null && presets.Count > 0)
                {
                    cConverterSimplePresets preset = presets.Find(p => p.Name.Equals(value.Replace("#", "")));

                    if(preset != null)
                    {
                        value = preset.Value;
                    }
                }

                result.Add(new cConverterSimple(entry.Attributes["source"].Value,
                    entry.Attributes["converterName"].Value,
                    entry.Attributes["converterParameter"].Value,
                    value));
            }

            return result;
        }
    }
}
