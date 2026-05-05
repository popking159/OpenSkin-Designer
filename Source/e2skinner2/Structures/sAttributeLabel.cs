using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Xml;
using OpenSkinDesigner.Logic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Collections;
using System.Windows.Forms;
using System.Runtime.InteropServices.WindowsRuntime;

namespace OpenSkinDesigner.Structures
{
	class sAttributeLabel : sAttribute
	{
		private const String entryName = "Label";

		private sWindowStyle pWindowStyle;

		public String pText = null;
		public String pPreviewText = null;
		public sFont pFont;
		public float pFontSize;

		public sColor pBackgroundColor;
        public String pBackgroundColorRaw;
		public sColor pForegroundColor;

		public cProperty.eVAlign pValign = cProperty.eVAlign.Center;
		public cProperty.eHAlign pHalign = cProperty.eHAlign.Left;

		public bool pNoWrap = false; /* DONT KNOW IF THIS IS THE CORRECT DEFAULT VALUE */

        // ########################################################
        //public float pCornerRadius;
        public sGradient pBackgroundGradient;


        // ########################################################

        [CategoryAttribute(entryName),
		 DefaultValueAttribute("")]
		public String Text
		{
			get { return pText; }
			set
			{
				pText = value;
				if (pText != null && pText.Length > 0)
				{
					if (myNode.Attributes["text"] != null)
						myNode.Attributes["text"].Value = pText;
					else
					{
						myNode.Attributes.Append(myNode.OwnerDocument.CreateAttribute("text"));
						myNode.Attributes["text"].Value = pText;
					}
				}
				else
					if (myNode.Attributes["text"] != null)
						myNode.Attributes.RemoveNamedItem("text");
			}
		}

		//[CategoryAttribute(entryName),
		//ReadOnlyAttribute(true)]

		[TypeConverter(typeof(OpenSkinDesigner.Structures.cProperty.sFontConverter)),
		 CategoryAttribute(entryName)]
		public String Font
		{
			get
			{
				if (pFont != null) return pFont.Name;
				else return "(none)";
			}
			set
			{
				if (value != null && !value.Equals("(none)"))
				{
					pFont = cDataBase.getFont(value);

					if (myNode.Attributes["font"] != null)
						
						if(!pFont.isAlias)
							myNode.Attributes["font"].Value = pFont.Name + "; " + pFontSize;
						else
							myNode.Attributes["font"].Value = pFont.Name;
						else
					{
						myNode.Attributes.Append(myNode.OwnerDocument.CreateAttribute("font"));
						if(!pFont.isAlias)
							myNode.Attributes["font"].Value = pFont.Name + "; " + pFontSize;
						else
							myNode.Attributes["font"].Value = pFont.Name;
					}
				}
				else
				{
					pFont = null;
				}
			}
		}

		[CategoryAttribute(entryName)]
		public float FontSize
		{
			get { return pFontSize; }
			set
			{
				pFontSize = value;

				if (myNode.Attributes["font"] == null)
				{
					myNode.Attributes.Append(myNode.OwnerDocument.CreateAttribute("font"));
					//myNode.Attributes["font"].Value = "1";
				}

				if (myNode.Attributes["font"] != null)
					if(!pFont.isAlias)
						myNode.Attributes["font"].Value = pFont.Name + "; " + pFontSize;
					else
						myNode.Attributes["font"].Value = pFont.Name;
					else
				{
					myNode.Attributes.Append(myNode.OwnerDocument.CreateAttribute("font"));
					if(!pFont.isAlias)
						myNode.Attributes["font"].Value = pFont.Name + "; " + pFontSize;
					else
						myNode.Attributes["font"].Value = pFont.Name;
				}
			}
		}

		[Editor(typeof(OpenSkinDesigner.Structures.cProperty.GradeEditor), typeof(System.Drawing.Design.UITypeEditor))]
		[TypeConverter(typeof(OpenSkinDesigner.Structures.cProperty.sColorConverter)),
		 CategoryAttribute(entryName)]
		public String ForegroundColor
		{
			get 
			{
				if (pForegroundColor != null)
					return pForegroundColor.pName;
				else
					return "(none)";
			}
			set
			{
				if (value != null)
					pForegroundColor = (sColor)cDataBase.pColors.get(value);
				else
					pForegroundColor = null;

				if (pForegroundColor != null && pForegroundColor != (sColor)pWindowStyle.pColors["LabelForeground"])
				{
					if (myNode.Attributes["foregroundColor"] != null)
						myNode.Attributes["foregroundColor"].Value = pForegroundColor.pName;
					else
					{
						myNode.Attributes.Append(myNode.OwnerDocument.CreateAttribute("foregroundColor"));
						myNode.Attributes["foregroundColor"].Value = pForegroundColor.pName;
					}
				}
				else
					if (myNode.Attributes["foregroundColor"] != null)
						myNode.Attributes.RemoveNamedItem("foregroundColor");
			}
		}

		[Editor(typeof(OpenSkinDesigner.Structures.cProperty.GradeEditor), typeof(System.Drawing.Design.UITypeEditor))]
		[TypeConverter(typeof(OpenSkinDesigner.Structures.cProperty.sColorConverter)),
		 CategoryAttribute(entryName)]
		public String BackgroundColor
        {
            get
            {
                if (!String.IsNullOrEmpty(pBackgroundColorRaw)) return pBackgroundColorRaw;
                return pBackgroundColor != null ? pBackgroundColor.pName : "(none)";
            }
            set
            {
                pBackgroundColorRaw = value;

                if (sGradient.isGradient(value))
                {
                    pBackgroundGradient = sGradient.parse(value);
                    pBackgroundColor = null;
                    if (myNode.Attributes["backgroundColor"] != null)
                        myNode.Attributes["backgroundColor"].Value = value;
                    else
                    {
                        myNode.Attributes.Append(myNode.OwnerDocument.CreateAttribute("backgroundColor"));
                        myNode.Attributes["backgroundColor"].Value = value;
                    }
                    return;
                }

                if (value != null)
                    pBackgroundColor = (sColor)cDataBase.pColors.get(value);
                else
                    pBackgroundColor = null;
                pBackgroundColorRaw = null;

				if (pBackgroundColor != null && pBackgroundColor != (sColor)pWindowStyle.pColors["LabelBackground"])
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
					if (myNode.Attributes["backgroundColor"] != null)
						myNode.Attributes.RemoveNamedItem("backgroundColor");
			}
		}


        [Editor(typeof(OpenSkinDesigner.Structures.cProperty.GradeEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [TypeConverter(typeof(OpenSkinDesigner.Structures.cProperty.sColorConverter)),
         CategoryAttribute("Label Gradient"),
         DisplayName("Gradient Start Color")]
        public String BackgroundGradientStartColor
        {
            get { return GetBackgroundGradientPart(0); }
            set { SetBackgroundGradientPart(0, value); }
        }

        [Editor(typeof(OpenSkinDesigner.Structures.cProperty.GradeEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [TypeConverter(typeof(OpenSkinDesigner.Structures.cProperty.sColorConverter)),
         CategoryAttribute("Label Gradient"),
         DisplayName("Gradient Middle Color")]
        public String BackgroundGradientMiddleColor
        {
            get { return GetBackgroundGradientPart(1); }
            set { SetBackgroundGradientPart(1, value); }
        }

        [Editor(typeof(OpenSkinDesigner.Structures.cProperty.GradeEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [TypeConverter(typeof(OpenSkinDesigner.Structures.cProperty.sColorConverter)),
         CategoryAttribute("Label Gradient"),
         DisplayName("Gradient End Color")]
        public String BackgroundGradientEndColor
        {
            get { return GetBackgroundGradientPart(2); }
            set { SetBackgroundGradientPart(2, value); }
        }

        [TypeConverter(typeof(cProperty.GradientDirectionConverter)),
         CategoryAttribute("Label Gradient"),
         DisplayName("Gradient Direction")]
        public String BackgroundGradientDirection
        {
            get { return GetBackgroundGradientDirection(); }
            set { SetBackgroundGradientDirection(value); }
        }


		[TypeConverter(typeof(cProperty.VAlignConverter)),
		 CategoryAttribute(entryName)]
		public String Valign
		{
			get { return pValign.ToString(); }
			set
			{
				if (value == cProperty.eVAlign.Top.ToString()) pValign = cProperty.eVAlign.Top;
				else if (value == cProperty.eVAlign.Center.ToString()) pValign = cProperty.eVAlign.Center;
				else pValign = cProperty.eVAlign.Bottom;

				if (myNode.Attributes["valign"] == null)
				{
					myNode.Attributes.Append(myNode.OwnerDocument.CreateAttribute("valign"));
					myNode.Attributes["valign"].Value = "top";
				}

				if (pValign == cProperty.eVAlign.Top) myNode.Attributes["valign"].Value = "top";
				else if (pValign == cProperty.eVAlign.Center) myNode.Attributes["valign"].Value = "center";
				else myNode.Attributes["valign"].Value = "bottom";
			}
		}

		[TypeConverter(typeof(cProperty.HAlignConverter)),
		 CategoryAttribute(entryName)]
		public String Halign
		{
			get { return pHalign.ToString(); }
			set
			{
				if (value == cProperty.eHAlign.Left.ToString()) pHalign = cProperty.eHAlign.Left;
				else if (value == cProperty.eHAlign.Center.ToString()) pHalign = cProperty.eHAlign.Center;
				else pHalign = cProperty.eHAlign.Right;

				if (myNode.Attributes["halign"] == null)
				{
					myNode.Attributes.Append(myNode.OwnerDocument.CreateAttribute("halign"));
					myNode.Attributes["halign"].Value = "left";
				}

				if (pHalign == cProperty.eHAlign.Left) myNode.Attributes["halign"].Value = "left";
				else if (pHalign == cProperty.eHAlign.Center) myNode.Attributes["halign"].Value = "center";
				else myNode.Attributes["halign"].Value = "right";
			}
		}

		[CategoryAttribute(entryName)]
		public bool noWrap
		{
			get { return pNoWrap; }
			set
			{
				pNoWrap = value;

				if (pNoWrap)
				{
					if (myNode.Attributes["noWrap"] == null)
					{
						myNode.Attributes.Append(myNode.OwnerDocument.CreateAttribute("noWrap"));
						myNode.Attributes["noWrap"].Value = "1";
					}


					if (myNode.Attributes["noWrap"] != null)
						myNode.Attributes["noWrap"].Value = pNoWrap ? "1" : "0";
					else
					{
						myNode.Attributes.Append(myNode.OwnerDocument.CreateAttribute("noWrap"));
						myNode.Attributes["noWrap"].Value = pNoWrap ? "1" : "0";
					}
				}
				else
					if (myNode.Attributes["noWrap"] != null)
						myNode.Attributes.RemoveNamedItem("noWrap");
			}
		}


        private String[] GetBackgroundGradientParts()
        {
            String raw = !String.IsNullOrEmpty(pBackgroundColorRaw) ? pBackgroundColorRaw : BackgroundColor;
            if (!sGradient.isGradient(raw))
            {
                String baseColor = pBackgroundColor != null ? pBackgroundColor.pName : "transparent";
                return new String[] { baseColor, baseColor, baseColor, "vertical" };
            }

            String[] parts = raw.Split(new char[] { ',' });
            for (int i = 0; i < parts.Length; i++) parts[i] = parts[i].Trim();

            if (parts.Length == 3)
                return new String[] { parts[0], parts[1], parts[1], parts[2] };

            return new String[] { parts[0], parts[1], parts[2], parts[3] };
        }

        private String GetBackgroundGradientPart(int index)
        {
            return GetBackgroundGradientParts()[index];
        }

        private String GetBackgroundGradientDirection()
        {
            return GetBackgroundGradientParts()[3];
        }

        private void SetBackgroundGradientPart(int index, String value)
        {
            String[] parts = GetBackgroundGradientParts();
            if (String.IsNullOrEmpty(value) || value == "(none)") value = "transparent";
            parts[index] = value;
            ApplyBackgroundGradientParts(parts);
        }

        private void SetBackgroundGradientDirection(String value)
        {
            String[] parts = GetBackgroundGradientParts();
            if (String.IsNullOrEmpty(value)) value = "vertical";
            parts[3] = value.Trim().ToLowerInvariant();
            ApplyBackgroundGradientParts(parts);
        }

        private void ApplyBackgroundGradientParts(String[] parts)
        {
            // Save as 3-part syntax when middle and end are equal: start,end,direction.
            // Save as 4-part syntax when a real middle color is selected: start,mid,end,direction.
            String raw;
            if (parts[1] == parts[2])
                raw = parts[0] + "," + parts[2] + "," + parts[3];
            else
                raw = parts[0] + "," + parts[1] + "," + parts[2] + "," + parts[3];

            BackgroundColor = raw;
        }

		/// <summary>
		/// ///////////////////////////////////////////////////////////////////
		/// </summary>
		/// <param name="parent"></param>
		/// <param name="node"></param>

		public sAttributeLabel(sAttribute parent, XmlNode node)
			: base(parent, node)
		{
            Logger.LogMessage("%%%%%%%%%%%%%%% cAttributeLabel.cs - Einlesen Anfang ");
            pWindowStyle = (sWindowStyle)cDataBase.pWindowstyles.get();

			if (myNode.Attributes["text"] != null)
				pText = myNode.Attributes["text"].Value;

			if (myNode.Attributes["font"] != null)
			{
				pFont = cDataBase.getFont(myNode.Attributes["font"].Value);
				pFontSize = pFont.Size;
			}
			else
			{
				pFont = cDataBase.getFont("Regular");
				pFontSize = 16;
			}

            // ------------------------------ schauen ist Gradient vorhanden --------------------------------------------------
            if (myNode.Attributes["backgroundGradient"] != null)
            {
                // hier Gradient einlesen und in variablen setzen und Gradient aktivieren

                string value = myNode.Attributes["backgroundGradient"].Value;

                Logger.LogMessage("%%%%%%%%%%%%%%% cAttributeLabel.cs - BackgroundGradient ist: " + value);

                pBackgroundGradient = sGradient.parse(value);
            }
            // ________________________________________________________________________________________________________________


            // cornerRadius is parsed in the base sAttribute class.
            // Keep the raw value (for example "30;topLeft,topRight") so the preview can
            // draw only the requested corners instead of rounding all four corners.
            if (myNode.Attributes["cornerRadius"] != null)
            {
                Logger.LogMessage("%%%%%%%%%%%%%%% cAttributeLabel.cs - cornerRadius ist: " + myNode.Attributes["cornerRadius"].Value);
            }

            // ------------------------ background -----------------------------------------------------------------------------------


            if (myNode.Attributes["backgroundColor"] != null)
            {
                String value = myNode.Attributes["backgroundColor"].Value;
                if (sGradient.isGradient(value))
                {
                    pBackgroundColorRaw = value;
                    pBackgroundGradient = sGradient.parse(value);
                    // Keep a normal color for preview/fallback paths that still expect pBackgroundColor.
                    pBackgroundColor = (sColor)cDataBase.pColors.get(value.Split(',')[0].Trim());
                }
                else
                    pBackgroundColor = (sColor)cDataBase.pColors.get(value);
            }
			else if ((sColor)pWindowStyle.pColors["LabelBackground"] != null)
				pBackgroundColor = (sColor)pWindowStyle.pColors["LabelBackground"];
			else
				pBackgroundColor = (sColor)pWindowStyle.pColors["Background"];




			if (myNode.Attributes["foregroundColor"] != null)
				pForegroundColor = (sColor)cDataBase.pColors.get(myNode.Attributes["foregroundColor"].Value);
			else
				pForegroundColor = (sColor)pWindowStyle.pColors["LabelForeground"];

			if (myNode.Attributes["valign"] != null)
				pValign = myNode.Attributes["valign"].Value.ToLower() == "top" ? cProperty.eVAlign.Top :
					myNode.Attributes["valign"].Value.ToLower() == "center" ? cProperty.eVAlign.Center :
					cProperty.eVAlign.Bottom;

			if (myNode.Attributes["halign"] != null)
				pHalign = myNode.Attributes["halign"].Value.ToLower() == "left" ? cProperty.eHAlign.Left :
					myNode.Attributes["halign"].Value.ToLower() == "center" ? cProperty.eHAlign.Center :
					cProperty.eHAlign.Right;

			if (myNode.Attributes["noWrap"] != null)
				pNoWrap = Convert.ToUInt32(myNode.Attributes["noWrap"].Value.ToLower()) != 0 ? true : false;

			if (pText == null || pText.Length == 0)
			{
				// Show text for elements without render attribute, if they have a font attribute
				if (myNode.Attributes["font"] != null)
				{
					if (myNode.Attributes["name"] != null && myNode.Attributes["source"] == null)
					{
						// show name
						pText = myNode.Attributes["name"].Value;
					}
				}
			}

			if (pText == null || pText.Length > 0)
				if (Name.Length > 0)
					pPreviewText = cPreviewText.getText(parent.Name, Name);

            Logger.LogMessage("%%%%%%%%%%%%%%% cAttributeLabel.cs - Einlesen Ende ");
        }
	}
}
