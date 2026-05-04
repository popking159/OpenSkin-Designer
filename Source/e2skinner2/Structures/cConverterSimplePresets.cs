using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace OpenSkinDesigner.Structures
{
    public class cConverterSimplePresets
    {
        const string fileName = "converterSimple.xml";

        private readonly string name;

        public string Name
        {
            get { return this.name; }
        }

        private readonly string value;

        public string Value
        {
            get { return this.value; }
        }

        public cConverterSimplePresets(String name, String value)
        {
            this.name = name;
            this.value = value;
        }

        public static List<cConverterSimplePresets> ListAll()
        {
            List<cConverterSimplePresets> result = new List<cConverterSimplePresets>();

            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("xml/" + fileName);
            XmlNodeList presets = xDoc.GetElementsByTagName("preset");

            foreach (XmlNode preset in presets)
            {
                // jump over Comments
                if (preset.NodeType == XmlNodeType.Comment)
                    continue;

                result.Add(new cConverterSimplePresets(preset.Attributes["name"].Value,
                    preset.Attributes["value"].Value));
            }

            return result;
        }
    }
}
