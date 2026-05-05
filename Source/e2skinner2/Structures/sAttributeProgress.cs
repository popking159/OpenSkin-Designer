using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Xml;
using System.ComponentModel;
using OpenSkinDesigner.Logic;
using System.Diagnostics;

namespace OpenSkinDesigner.Structures
{
    class sAttributeProgress : sAttribute
    {
        private const String entryName = "Progress";

        public sColor pBackgroundColor;
        public sColor pForegroundColor;
        public String pForegroundColorRaw;

        // #####################################
        public sGradient pForegroundGradient;
        public string ppixmap;
        // #####################################

        [Editor(typeof(OpenSkinDesigner.Structures.cProperty.GradeEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [TypeConverter(typeof(OpenSkinDesigner.Structures.cProperty.sColorConverter)),
        CategoryAttribute(entryName),
        DisplayName("Progress Background Color")]
        public String BackgroundColor
        {
            get { return pBackgroundColor != null ? pBackgroundColor.pName : "(none)"; }
            set
            {
                Logger.LogMessage("%%%%%%%%%%%%%%% sAttributeProgress - BackgroundColor setter");

                if (value != null && value != "(none)")
                    pBackgroundColor = (sColor)cDataBase.pColors.get(value);
                else
                    pBackgroundColor = null;

                if (pBackgroundColor != null && pBackgroundColor != (sColor)((sWindowStyle)cDataBase.pWindowstyles.get()).pColors["Background"])
                {
                    if (myNode.Attributes["backgroundColor"] != null)
                        myNode.Attributes["backgroundColor"].Value = pBackgroundColor.pName;
                    else
                    {
                        myNode.Attributes.Append(myNode.OwnerDocument.CreateAttribute("backgroundColor"));
                        myNode.Attributes["backgroundColor"].Value = pBackgroundColor.pName;
                    }
                }
                else
                {
                    if (myNode.Attributes["backgroundColor"] != null)
                        myNode.Attributes.RemoveNamedItem("backgroundColor");
                }
            }
        }

        [Editor(typeof(OpenSkinDesigner.Structures.cProperty.GradeEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [TypeConverter(typeof(OpenSkinDesigner.Structures.cProperty.sColorOrGradientConverter)),
        CategoryAttribute(entryName),
        DisplayName("Progress Foreground Color")]
        public String ForegroundColor
        {
            get
            {
                if (!String.IsNullOrEmpty(pForegroundColorRaw))
                    return pForegroundColorRaw;
                return pForegroundColor != null ? pForegroundColor.pName : "(none)";
            }
            set
            {
                Logger.LogMessage("%%%%%%%%%%%%%%% sAttributeProgress - ForegroundColor setter: " + value);

                if (String.IsNullOrEmpty(value) || value == "(none)")
                {
                    pForegroundColorRaw = null;
                    pForegroundColor = null;
                    pForegroundGradient = null;
                    if (myNode.Attributes["foregroundColor"] != null)
                        myNode.Attributes.RemoveNamedItem("foregroundColor");
                    return;
                }

                if (sGradient.isGradient(value))
                {
                    pForegroundColorRaw = value;
                    pForegroundGradient = sGradient.parse(value);
                    pForegroundColor = (sColor)cDataBase.pColors.get(value.Split(',')[0].Trim());
                    SetOrCreateAttribute("foregroundColor", value);
                    return;
                }

                pForegroundColorRaw = null;
                pForegroundColor = (sColor)cDataBase.pColors.get(value);
                if (pForegroundColor != null)
                {
                    SetOrCreateAttribute("foregroundColor", pForegroundColor.pName);
                    pForegroundGradient = CreateSolidForegroundGradient(pForegroundColor.pName);
                }
                else
                {
                    if (myNode.Attributes["foregroundColor"] != null)
                        myNode.Attributes.RemoveNamedItem("foregroundColor");
                    pForegroundGradient = null;
                }
            }
        }

        private void SetOrCreateAttribute(String name, String value)
        {
            if (myNode.Attributes[name] != null)
                myNode.Attributes[name].Value = value;
            else
            {
                myNode.Attributes.Append(myNode.OwnerDocument.CreateAttribute(name));
                myNode.Attributes[name].Value = value;
            }
        }

        private sGradient CreateSolidForegroundGradient(String colorName)
        {
            if (String.IsNullOrEmpty(colorName) || colorName == "(none)")
                return null;
            return sGradient.parse(colorName + "," + colorName + "," + colorName + ",horizontal");
        }



        [Editor(typeof(OpenSkinDesigner.Structures.cProperty.GradeEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [TypeConverter(typeof(OpenSkinDesigner.Structures.cProperty.sColorConverter)),
         CategoryAttribute("Progress Foreground Gradient"),
         DisplayName("Progress Foreground Start Color")]
        public String ForegroundGradientStartColor
        {
            get { return GetForegroundGradientPart(0); }
            set { SetForegroundGradientPart(0, value); }
        }

        [Editor(typeof(OpenSkinDesigner.Structures.cProperty.GradeEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [TypeConverter(typeof(OpenSkinDesigner.Structures.cProperty.sColorConverter)),
         CategoryAttribute("Progress Foreground Gradient"),
         DisplayName("Progress Foreground Middle Color")]
        public String ForegroundGradientMiddleColor
        {
            get { return GetForegroundGradientPart(1); }
            set { SetForegroundGradientPart(1, value); }
        }

        [Editor(typeof(OpenSkinDesigner.Structures.cProperty.GradeEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [TypeConverter(typeof(OpenSkinDesigner.Structures.cProperty.sColorConverter)),
         CategoryAttribute("Progress Foreground Gradient"),
         DisplayName("Progress Foreground End Color")]
        public String ForegroundGradientEndColor
        {
            get { return GetForegroundGradientPart(2); }
            set { SetForegroundGradientPart(2, value); }
        }

        [TypeConverter(typeof(cProperty.GradientDirectionConverter)),
         CategoryAttribute("Progress Foreground Gradient"),
         DisplayName("Progress Foreground Direction")]
        public String ForegroundGradientDirection
        {
            get { return GetForegroundGradientDirection(); }
            set { SetForegroundGradientDirection(value); }
        }

        private String[] GetForegroundGradientParts()
        {
            String raw = !String.IsNullOrEmpty(pForegroundColorRaw) ? pForegroundColorRaw : ForegroundColor;
            if (!sGradient.isGradient(raw))
            {
                String baseColor = pForegroundColor != null ? pForegroundColor.pName : "transparent";
                return new String[] { baseColor, baseColor, baseColor, "horizontal" };
            }

            String[] parts = raw.Split(new char[] { ',' });
            for (int i = 0; i < parts.Length; i++) parts[i] = parts[i].Trim();

            if (parts.Length == 3)
                return new String[] { parts[0], parts[1], parts[1], parts[2] };

            return new String[] { parts[0], parts[1], parts[2], parts[3] };
        }

        private String GetForegroundGradientPart(int index)
        {
            return GetForegroundGradientParts()[index];
        }

        private String GetForegroundGradientDirection()
        {
            return GetForegroundGradientParts()[3];
        }

        private void SetForegroundGradientPart(int index, String value)
        {
            String[] parts = GetForegroundGradientParts();
            if (String.IsNullOrEmpty(value) || value == "(none)") value = "transparent";
            parts[index] = value;
            ApplyForegroundGradientParts(parts);
        }

        private void SetForegroundGradientDirection(String value)
        {
            String[] parts = GetForegroundGradientParts();
            if (String.IsNullOrEmpty(value)) value = "horizontal";
            parts[3] = value.Trim().ToLowerInvariant();
            ApplyForegroundGradientParts(parts);
        }

        private void ApplyForegroundGradientParts(String[] parts)
        {
            // Always write the progress foreground gradient as four parts when
            // edited through the dedicated fields: start,mid,end,direction.
            ForegroundColor = parts[0] + "," + parts[1] + "," + parts[2] + "," + parts[3];
        }

        public sAttributeProgress(sAttribute parent, XmlNode node)
            : base(parent, node)
        {
            Logger.LogMessage("%%%%%%%%%%%%%%% sAttributeProgress - constructor");

            if (node.Attributes["pixmap"] != null)
            {
                string value = myNode.Attributes["pixmap"].Value;
                Logger.LogMessage("%%%%%%%%%%%%%%% cAttributeProgress.cs - pixmap value : " + value);
                ppixmap = value;
            }

            if (myNode.Attributes["cornerRadius"] != null)
            {
                string value = myNode.Attributes["cornerRadius"].Value;
                Logger.LogMessage("%%%%%%%%%%%%%%% cAttributeProgress.cs - cornerRadius ist: " + value);
                float.TryParse(value.Split(';')[0].Trim(), out pCornerRadius);
            }

            if (node.Attributes["backgroundColor"] != null)
                pBackgroundColor = (sColor)cDataBase.pColors.get(node.Attributes["backgroundColor"].Value);
            else
                pBackgroundColor = (sColor)((sWindowStyle)cDataBase.pWindowstyles.get()).pColors["Background"];

            // Enigma2 progress supports foregroundColor. It may be a normal color or a gradient:
            // foregroundColor="green,yellow,red,horizontal"
            if (node.Attributes["foregroundColor"] != null)
            {
                string value = myNode.Attributes["foregroundColor"].Value;
                Logger.LogMessage("%%%%%%%%%%%%%%% cAttributeProgress.cs - foregroundColor: " + value);

                if (sGradient.isGradient(value))
                {
                    pForegroundColorRaw = value;
                    pForegroundGradient = sGradient.parse(value);
                    pForegroundColor = (sColor)cDataBase.pColors.get(value.Split(',')[0].Trim());
                }
                else
                {
                    pForegroundColor = (sColor)cDataBase.pColors.get(value);
                    pForegroundGradient = CreateSolidForegroundGradient(pForegroundColor != null ? pForegroundColor.pName : value);
                }
            }
            else if (node.Attributes["foregroundGradient"] != null)
            {
                string value = myNode.Attributes["foregroundGradient"].Value;
                Logger.LogMessage("%%%%%%%%%%%%%%% cAttributeProgress.cs - foregroundGradient: " + value);
                pForegroundColorRaw = value;
                pForegroundGradient = sGradient.parse(value);
            }
        }
    }
}
