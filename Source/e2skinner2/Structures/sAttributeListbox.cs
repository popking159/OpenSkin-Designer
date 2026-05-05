using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;
//using System.Linq;
using System.Text;
using System.Xml;
using System.ComponentModel;
using OpenSkinDesigner.Logic;
using System.Drawing;
using System.IO;

namespace OpenSkinDesigner.Structures
{
    class sAttributeListbox : sAttribute
    {
        private const String entryName = "7 Listbox";

        //Listbox 20 regular http://ufs910.dontexist.com/trac/browser/cvs/apps/enigma2_01052009/lib/gui/elistboxcontent.cpp#L140
        //http://ufs910.dontexist.com/trac/browser/cvs/apps/enigma2_01052009/lib/gui/elistboxcontent.cpp#L273

        /*ListboxBackground" color="#450b1b1c"/>
		<color name="ListboxForeground" color="#ffffff"/>
		<color name="ListboxSelectedBackground" color="#25587b80"/>
		<color name="ListboxSelectedForeground" color="#ffffff"/>
		<color name="ListboxMarkedBackground" color="#ff0000"/>
		<color name="ListboxMarkedForeground" color="#ffffff"/>
		<color name="ListboxMarkedAndSelectedBackground" color="#800000"/>
		<color name="ListboxMarkedAndSelectedForeground" color="#ffffff"/>
		 * backgroundPixmap //shown for the hole listbox
         selectionPixmap //shown under an selected entry
         ScrollbarMode
		 * the following are only valid for the graphical multi epg
         EntryBorderColor="#071930" EntryBackgroundColor="#1f294b" EntryBackgroundColorSelected="#225b7395"
		 * the following are only valid for the servicelist
         serviceInfoFont="Regular;22" serviceNameFont="Regular;24" serviceNumberFont="Regular;24"*/

        //note this are the pngs which will be shown around the complete list, not an entry
        public Size pbpTopLeft;
        public Size pbpTop;
        public Size pbpTopRight;
        public Size pbpLeft;
        public Size pbpRight;
        public Size pbpBottomLeft;
        public Size pbpBottom;
        public Size pbpBottomRight;

        public String pbpTopLeftName;
        public String pbpTopName;
        public String pbpTopRightName;
        public String pbpLeftName;
        public String pbpRightName;
        public String pbpBottomLeftName;
        public String pbpBottomName;
        public String pbpBottomRightName;

        public sColor pListboxBackgroundColor;
        public sColor pListboxForegroundColor;
        public sColor pListboxSelectedBackgroundColor;
        public sColor pListboxSelectedForegroundColor;
        public sColor pListboxMarkedBackgroundColor;
        public sColor pListboxMarkedForegroundColor;
        public sColor pListboxMarkedAndSelectedBackgroundColor;
        public sColor pListboxMarkedAndSelectedForegroundColor;

        public String pBackgroundPixmapName;
        public Image pBackgroundPixmap;
        public String pSelectionPixmapName;
        public Image pSelectionPixmap;

        public List<string> pPreviewEntries = new List<string>();

        public cProperty.eScrollbarMode pScrollbarMode;

        // #########################################
        public sGradient pItemGradientSelected;
        public String pItemGradientSelectedRaw;
        public sGradient pItemGradient;
        public String pItemGradientRaw;
        public int pItemCornerRadius;
        public int pItemCornerRadiusSelected;
        public int pSelection;
        public bool pEnableWrapAround;
        public String pListOrientation = "vertical";
        public int pItemWidth = 0;
        public int pItemSpacingX = 0;
        public int pItemSpacingY = 0;
        public int pSelectionZoom = 0;
        public sColor pMoveBackgroundColor;
        public sColor pMoveFontColor;
        //public float pCornerRadius;

        public int pscrollbarWidth;
        public int pscrollbarSliderBorderWidth;
        public int pscrollbarOffset;
        public int pscrollbarRadius;

        public sColor pscrollbarSliderBorderColor;
        public sColor pscrollbarSliderForegroundColor;
        public sColor pscrollbarSliderBackgroundColor;
        public sGradient pScrollbarForegroundGradient;

        public String pScrollbarBackgroundPictureName;
        public Image pScrollbarBackgroundPicture;


        //public Image scrollbarSliderPicture;

        // #########################################

        [CategoryAttribute(entryName)]
        public String BackgroundPixmap
        {
            get { return pBackgroundPixmapName; }
            set
            {
                pBackgroundPixmapName = value;

                if (pBackgroundPixmapName != null && pBackgroundPixmapName.Length > 0)
                {
                    if (myNode.Attributes["backgroundPixmap"] != null)
                        myNode.Attributes["backgroundPixmap"].Value = pBackgroundPixmapName;
                    else
                    {
                        myNode.Attributes.Append(myNode.OwnerDocument.CreateAttribute("backgroundPixmap"));
                        myNode.Attributes["backgroundPixmap"].Value = pBackgroundPixmapName;
                    }
                }
                else
                    if (myNode.Attributes["backgroundPixmap"] != null)
                    myNode.Attributes.RemoveNamedItem("backgroundPixmap");
            }
        }

        [CategoryAttribute(entryName)]
        public String SelectionPixmap
        {
            get { return pSelectionPixmapName; }
            set
            {
                pSelectionPixmapName = value;

                if (pSelectionPixmapName != null && pSelectionPixmapName.Length > 0)
                {
                    if (myNode.Attributes["selectionPixmap"] != null)
                        myNode.Attributes["selectionPixmap"].Value = pSelectionPixmapName;
                    else
                    {
                        myNode.Attributes.Append(myNode.OwnerDocument.CreateAttribute("selectionPixmap"));
                        myNode.Attributes["selectionPixmap"].Value = pSelectionPixmapName;
                    }
                }
                else
                    if (myNode.Attributes["selectionPixmap"] != null)
                    myNode.Attributes.RemoveNamedItem("selectionPixmap");

            }
        }

        [TypeConverter(typeof(cProperty.ScrollbarModeConverter)),
         CategoryAttribute(entryName)]
        public String ScrollbarMode
        {
            get { return pScrollbarMode.ToString(); }
            set
            {
                if (value != null && value == cProperty.eScrollbarMode.showAlways.ToString()) pScrollbarMode = cProperty.eScrollbarMode.showAlways;
                else if (value != null && value == cProperty.eScrollbarMode.showOnDemand.ToString()) pScrollbarMode = cProperty.eScrollbarMode.showOnDemand;
                else pScrollbarMode = cProperty.eScrollbarMode.showNever;

                if (pScrollbarMode == cProperty.eScrollbarMode.showAlways) myNode.Attributes["scrollbarMode"].Value = "showAlways";
                else if (pScrollbarMode == cProperty.eScrollbarMode.showOnDemand) myNode.Attributes["scrollbarMode"].Value = "showOnDemand";
                else myNode.Attributes["scrollbarMode"].Value = "showNever";
            }
        }




        [CategoryAttribute(entryName)]
        public Int32 ScrollbarRadius
        {
            get { return pscrollbarRadius; }
            set
            {
                pscrollbarRadius = value < 0 ? 0 : value;
                if (myNode.Attributes["scrollbarRadius"] != null)
                    myNode.Attributes["scrollbarRadius"].Value = pscrollbarRadius.ToString();
                else
                {
                    myNode.Attributes.Append(myNode.OwnerDocument.CreateAttribute("scrollbarRadius"));
                    myNode.Attributes["scrollbarRadius"].Value = pscrollbarRadius.ToString();
                }
            }
        }


        private void SetAttributeValue(String name, String value)
        {
            if (String.IsNullOrEmpty(value) || value == "(none)")
            {
                if (myNode.Attributes[name] != null)
                    myNode.Attributes.RemoveNamedItem(name);
                return;
            }

            if (myNode.Attributes[name] != null)
                myNode.Attributes[name].Value = value;
            else
            {
                myNode.Attributes.Append(myNode.OwnerDocument.CreateAttribute(name));
                myNode.Attributes[name].Value = value;
            }
        }

        private static String ColorName(sColor color)
        {
            return color != null ? color.pName : "(none)";
        }

        private String[] GetItemGradientParts()
        {
            String raw = pItemGradientSelectedRaw;
            if (String.IsNullOrEmpty(raw) && myNode.Attributes["itemGradientSelected"] != null)
                raw = myNode.Attributes["itemGradientSelected"].Value;

            String[] parts = new String[] { "(none)", "(none)", "(none)", "vertical" };
            if (String.IsNullOrEmpty(raw))
                return parts;

            String[] split = raw.Split(',');
            if (split.Length >= 3)
            {
                parts[0] = split[0].Trim();
                if (split.Length == 3)
                {
                    parts[1] = split[1].Trim();
                    parts[2] = split[1].Trim();
                    parts[3] = split[2].Trim().ToLower() == "horizontal" ? "horizontal" : "vertical";
                }
                else
                {
                    parts[1] = split[1].Trim();
                    parts[2] = split[2].Trim();
                    parts[3] = split[3].Trim().ToLower() == "horizontal" ? "horizontal" : "vertical";
                }
            }
            return parts;
        }

        private void UpdateItemGradientPart(int index, String value)
        {
            String[] parts = GetItemGradientParts();
            parts[index] = String.IsNullOrEmpty(value) ? "(none)" : value.Trim();
            ItemGradientSelected = parts[0] + "," + parts[1] + "," + parts[2] + "," + parts[3];
        }


        private String[] GetNormalItemGradientParts()
        {
            String raw = pItemGradientRaw;
            if (String.IsNullOrEmpty(raw) || raw == "(none)")
                raw = "mcolor6,mcolor5,mcolor6,vertical";
            String[] parts = raw.Split(new char[] { ',' });
            if (parts.Length == 3)
                return new String[] { parts[0].Trim(), parts[1].Trim(), parts[1].Trim(), parts[2].Trim().ToLower() };
            if (parts.Length >= 4)
                return new String[] { parts[0].Trim(), parts[1].Trim(), parts[2].Trim(), parts[3].Trim().ToLower() };
            return new String[] { "mcolor6", "mcolor5", "mcolor6", "vertical" };
        }

        private void UpdateNormalItemGradientPart(int index, String value)
        {
            String[] parts = GetNormalItemGradientParts();
            parts[index] = String.IsNullOrEmpty(value) ? "(none)" : value.Trim();
            ItemGradient = parts[0] + "," + parts[1] + "," + parts[2] + "," + parts[3];
        }

        [Editor(typeof(OpenSkinDesigner.Structures.cProperty.GradeEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [TypeConverter(typeof(OpenSkinDesigner.Structures.cProperty.sColorConverter)),
         CategoryAttribute(entryName),
         DisplayName("Listbox Background Color")]
        public String BackgroundColor
        {
            get { return ColorName(pListboxBackgroundColor); }
            set
            {
                pListboxBackgroundColor = value != null && value != "(none)" ? (sColor)cDataBase.pColors.get(value) : null;
                SetAttributeValue("backgroundColor", value);
            }
        }

        [Editor(typeof(OpenSkinDesigner.Structures.cProperty.GradeEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [TypeConverter(typeof(OpenSkinDesigner.Structures.cProperty.sColorConverter)),
         CategoryAttribute(entryName),
         DisplayName("Listbox Foreground Color")]
        public String ForegroundColor
        {
            get { return ColorName(pListboxForegroundColor); }
            set
            {
                pListboxForegroundColor = value != null && value != "(none)" ? (sColor)cDataBase.pColors.get(value) : null;
                SetAttributeValue("foregroundColor", value);
            }
        }

        [Editor(typeof(OpenSkinDesigner.Structures.cProperty.GradeEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [TypeConverter(typeof(OpenSkinDesigner.Structures.cProperty.sColorConverter)),
         CategoryAttribute(entryName),
         DisplayName("Listbox Selected Background Color")]
        public String BackgroundColorSelected
        {
            get { return ColorName(pListboxSelectedBackgroundColor); }
            set
            {
                pListboxSelectedBackgroundColor = value != null && value != "(none)" ? (sColor)cDataBase.pColors.get(value) : null;
                SetAttributeValue("backgroundColorSelected", value);
            }
        }

        [Editor(typeof(OpenSkinDesigner.Structures.cProperty.GradeEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [TypeConverter(typeof(OpenSkinDesigner.Structures.cProperty.sColorConverter)),
         CategoryAttribute(entryName),
         DisplayName("Listbox Selected Foreground Color")]
        public String ForegroundColorSelected
        {
            get { return ColorName(pListboxSelectedForegroundColor); }
            set
            {
                pListboxSelectedForegroundColor = value != null && value != "(none)" ? (sColor)cDataBase.pColors.get(value) : null;
                SetAttributeValue("foregroundColorSelected", value);
            }
        }

        [TypeConverter(typeof(BooleanConverter)),
         CategoryAttribute(entryName),
         DisplayName("Enable Wrap Around")]
        public bool EnableWrapAround
        {
            get { return pEnableWrapAround; }
            set
            {
                pEnableWrapAround = value;
                SetAttributeValue("enableWrapAround", value ? "1" : "0");
            }
        }

        [CategoryAttribute(entryName),
         DisplayName("Selection")]
        public Int32 Selection
        {
            get { return pSelection; }
            set
            {
                pSelection = value < 0 ? 0 : value;
                SetAttributeValue("selection", pSelection.ToString());
            }
        }

        [CategoryAttribute(entryName),
         DisplayName("Item Selected Corner Radius")]
        public Int32 ItemCornerRadiusSelected
        {
            get { return pItemCornerRadiusSelected; }
            set
            {
                pItemCornerRadiusSelected = value < 0 ? 0 : value;
                SetAttributeValue("itemCornerRadiusSelected", pItemCornerRadiusSelected.ToString());
            }
        }

        [Editor(typeof(OpenSkinDesigner.Structures.cProperty.GradeEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [TypeConverter(typeof(OpenSkinDesigner.Structures.cProperty.sColorOrGradientConverter)),
         CategoryAttribute(entryName),
         DisplayName("Item Selected Gradient")]
        public String ItemGradientSelected
        {
            get { return String.IsNullOrEmpty(pItemGradientSelectedRaw) ? "(none)" : pItemGradientSelectedRaw; }
            set
            {
                if (String.IsNullOrEmpty(value) || value == "(none)")
                {
                    pItemGradientSelectedRaw = null;
                    pItemGradientSelected = null;
                    SetAttributeValue("itemGradientSelected", null);
                    return;
                }
                pItemGradientSelectedRaw = value.Trim();
                pItemGradientSelected = sGradient.parse(pItemGradientSelectedRaw);
                SetAttributeValue("itemGradientSelected", pItemGradientSelectedRaw);
            }
        }

        [Editor(typeof(OpenSkinDesigner.Structures.cProperty.GradeEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [TypeConverter(typeof(OpenSkinDesigner.Structures.cProperty.sColorConverter)),
         CategoryAttribute("7 Listbox Selected Gradient"),
         DisplayName("Item Selected Start Color")]
        public String ItemGradientSelectedStartColor
        {
            get { return GetItemGradientParts()[0]; }
            set { UpdateItemGradientPart(0, value); }
        }

        [Editor(typeof(OpenSkinDesigner.Structures.cProperty.GradeEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [TypeConverter(typeof(OpenSkinDesigner.Structures.cProperty.sColorConverter)),
         CategoryAttribute("7 Listbox Selected Gradient"),
         DisplayName("Item Selected Middle Color")]
        public String ItemGradientSelectedMiddleColor
        {
            get { return GetItemGradientParts()[1]; }
            set { UpdateItemGradientPart(1, value); }
        }

        [Editor(typeof(OpenSkinDesigner.Structures.cProperty.GradeEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [TypeConverter(typeof(OpenSkinDesigner.Structures.cProperty.sColorConverter)),
         CategoryAttribute("7 Listbox Selected Gradient"),
         DisplayName("Item Selected End Color")]
        public String ItemGradientSelectedEndColor
        {
            get { return GetItemGradientParts()[2]; }
            set { UpdateItemGradientPart(2, value); }
        }

        [TypeConverter(typeof(cProperty.GradientDirectionConverter)),
         CategoryAttribute("7 Listbox Selected Gradient"),
         DisplayName("Item Selected Direction")]
        public String ItemGradientSelectedDirection
        {
            get { return GetItemGradientParts()[3]; }
            set { UpdateItemGradientPart(3, value); }
        }


        [TypeConverter(typeof(cProperty.ListOrientationConverter)),
         CategoryAttribute(entryName),
         DisplayName("List Orientation")]
        public String ListOrientation
        {
            get { return String.IsNullOrEmpty(pListOrientation) ? "vertical" : pListOrientation; }
            set
            {
                pListOrientation = (value != null && value.ToLower() == "grid") ? "grid" : "vertical";
                SetAttributeValue("listOrientation", pListOrientation);
            }
        }

        [CategoryAttribute(entryName),
         DisplayName("Item Width")]
        public Int32 ItemWidth
        {
            get { return pItemWidth; }
            set
            {
                pItemWidth = value < 0 ? 0 : value;
                if (pItemWidth > 0)
                    SetAttributeValue("itemWidth", pItemWidth.ToString());
                else
                    SetAttributeValue("itemWidth", null);
            }
        }

        [CategoryAttribute(entryName),
         DisplayName("Item Spacing X")]
        public Int32 ItemSpacingX
        {
            get { return pItemSpacingX; }
            set
            {
                pItemSpacingX = value < 0 ? 0 : value;
                SetAttributeValue("itemSpacing", pItemSpacingX.ToString() + "," + pItemSpacingY.ToString());
            }
        }

        [CategoryAttribute(entryName),
         DisplayName("Item Spacing Y")]
        public Int32 ItemSpacingY
        {
            get { return pItemSpacingY; }
            set
            {
                pItemSpacingY = value < 0 ? 0 : value;
                SetAttributeValue("itemSpacing", pItemSpacingX.ToString() + "," + pItemSpacingY.ToString());
            }
        }

        [CategoryAttribute(entryName),
         DisplayName("Selection Zoom")]
        public Int32 SelectionZoom
        {
            get { return pSelectionZoom; }
            set
            {
                pSelectionZoom = value < 0 ? 0 : value;
                SetAttributeValue("selectionZoom", pSelectionZoom > 0 ? pSelectionZoom.ToString() : null);
            }
        }

        [Editor(typeof(OpenSkinDesigner.Structures.cProperty.GradeEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [TypeConverter(typeof(OpenSkinDesigner.Structures.cProperty.sColorConverter)),
         CategoryAttribute(entryName),
         DisplayName("Move Background Color")]
        public String MoveBackgroundColor
        {
            get { return ColorName(pMoveBackgroundColor); }
            set
            {
                pMoveBackgroundColor = value != null && value != "(none)" ? (sColor)cDataBase.pColors.get(value) : null;
                SetAttributeValue("moveBackgroundColor", value);
            }
        }

        [Editor(typeof(OpenSkinDesigner.Structures.cProperty.GradeEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [TypeConverter(typeof(OpenSkinDesigner.Structures.cProperty.sColorConverter)),
         CategoryAttribute(entryName),
         DisplayName("Move Font Color")]
        public String MoveFontColor
        {
            get { return ColorName(pMoveFontColor); }
            set
            {
                pMoveFontColor = value != null && value != "(none)" ? (sColor)cDataBase.pColors.get(value) : null;
                SetAttributeValue("moveFontColor", value);
            }
        }

        [CategoryAttribute(entryName),
         DisplayName("Item Corner Radius")]
        public Int32 ItemCornerRadius
        {
            get { return pItemCornerRadius; }
            set
            {
                pItemCornerRadius = value < 0 ? 0 : value;
                SetAttributeValue("itemCornerRadius", pItemCornerRadius > 0 ? pItemCornerRadius.ToString() : null);
            }
        }

        [Editor(typeof(OpenSkinDesigner.Structures.cProperty.GradeEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [TypeConverter(typeof(OpenSkinDesigner.Structures.cProperty.sColorOrGradientConverter)),
         CategoryAttribute(entryName),
         DisplayName("Item Gradient")]
        public String ItemGradient
        {
            get { return String.IsNullOrEmpty(pItemGradientRaw) ? "(none)" : pItemGradientRaw; }
            set
            {
                if (String.IsNullOrEmpty(value) || value == "(none)")
                {
                    pItemGradientRaw = null;
                    pItemGradient = null;
                    SetAttributeValue("itemGradient", null);
                    return;
                }
                pItemGradientRaw = value.Trim();
                pItemGradient = sGradient.parse(pItemGradientRaw);
                SetAttributeValue("itemGradient", pItemGradientRaw);
            }
        }

        [Editor(typeof(OpenSkinDesigner.Structures.cProperty.GradeEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [TypeConverter(typeof(OpenSkinDesigner.Structures.cProperty.sColorConverter)),
         CategoryAttribute("7 Listbox Item Gradient"),
         DisplayName("Item Start Color")]
        public String ItemGradientStartColor
        {
            get { return GetNormalItemGradientParts()[0]; }
            set { UpdateNormalItemGradientPart(0, value); }
        }

        [Editor(typeof(OpenSkinDesigner.Structures.cProperty.GradeEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [TypeConverter(typeof(OpenSkinDesigner.Structures.cProperty.sColorConverter)),
         CategoryAttribute("7 Listbox Item Gradient"),
         DisplayName("Item Middle Color")]
        public String ItemGradientMiddleColor
        {
            get { return GetNormalItemGradientParts()[1]; }
            set { UpdateNormalItemGradientPart(1, value); }
        }

        [Editor(typeof(OpenSkinDesigner.Structures.cProperty.GradeEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [TypeConverter(typeof(OpenSkinDesigner.Structures.cProperty.sColorConverter)),
         CategoryAttribute("7 Listbox Item Gradient"),
         DisplayName("Item End Color")]
        public String ItemGradientEndColor
        {
            get { return GetNormalItemGradientParts()[2]; }
            set { UpdateNormalItemGradientPart(2, value); }
        }

        [TypeConverter(typeof(cProperty.GradientDirectionConverter)),
         CategoryAttribute("7 Listbox Item Gradient"),
         DisplayName("Item Direction")]
        public String ItemGradientDirection
        {
            get { return GetNormalItemGradientParts()[3]; }
            set { UpdateNormalItemGradientPart(3, value); }
        }

        public Int32 pItemHeight = 0;

        [CategoryAttribute(entryName)]
        public Int32 ItemHeight
        {
            get { return pItemHeight; }
            set
            {
                pItemHeight = value;

                //if (pItemHeight != null)
                {
                    if (myNode.Attributes["itemHeight"] != null)
                        myNode.Attributes["itemHeight"].Value = pItemHeight.ToString();
                    else
                    {
                        myNode.Attributes.Append(myNode.OwnerDocument.CreateAttribute("itemHeight"));
                        myNode.Attributes["itemHeight"].Value = pItemHeight.ToString();
                    }
                }
                //else
                //    if (myNode.Attributes["itemHeight"] != null)
                //        myNode.Attributes.RemoveNamedItem("itemHeight");

            }
        }

        public sFont pFont;

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

                        if (!pFont.isAlias)
                            myNode.Attributes["font"].Value = pFont.Name + "; " + pFontSize;
                        else
                            myNode.Attributes["font"].Value = pFont.Name;
                    else
                    {
                        myNode.Attributes.Append(myNode.OwnerDocument.CreateAttribute("font"));
                        if (!pFont.isAlias)
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

        public float pFontSize;

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
                    if (!pFont.isAlias)
                        myNode.Attributes["font"].Value = pFont.Name + "; " + pFontSize;
                    else
                        myNode.Attributes["font"].Value = pFont.Name;
                else
                {
                    myNode.Attributes.Append(myNode.OwnerDocument.CreateAttribute("font"));
                    if (!pFont.isAlias)
                        myNode.Attributes["font"].Value = pFont.Name + "; " + pFontSize;
                    else
                        myNode.Attributes["font"].Value = pFont.Name;
                }
            }
        }



        public sFont pFontSecond;

        [TypeConverter(typeof(OpenSkinDesigner.Structures.cProperty.sFontConverter)),
         CategoryAttribute(entryName)]
        public String FontSecond
        {
            get
            {
                if (pFontSecond != null) return pFontSecond.Name;
                else return "(none)";
            }
            set
            {
                if (value != null && !value.Equals("(none)"))
                {
                    pFontSecond = cDataBase.getFont(value);

                    if (myNode.Attributes["secondfont"] != null)

                        if (!pFontSecond.isAlias)
                            myNode.Attributes["secondfont"].Value = pFontSecond.Name + "; " + pFontSecondSize;
                        else
                            myNode.Attributes["secondfont"].Value = pFontSecond.Name;
                    else
                    {
                        myNode.Attributes.Append(myNode.OwnerDocument.CreateAttribute("secondfont"));
                        if (!pFontSecond.isAlias)
                            myNode.Attributes["secondfont"].Value = pFontSecond.Name + "; " + pFontSecondSize;
                        else
                            myNode.Attributes["secondfont"].Value = pFontSecond.Name;
                    }
                }
                else
                {
                    pFontSecond = null;
                }
            }
        }

        public float pFontSecondSize;

        [CategoryAttribute(entryName)]
        public float FontSecondSize
        {
            get { return pFontSecondSize; }
            set
            {
                pFontSecondSize = value;

                if (myNode.Attributes["secondfont"] == null)
                {
                    myNode.Attributes.Append(myNode.OwnerDocument.CreateAttribute("secondfont"));
                    //myNode.Attributes["font"].Value = "1";
                }

                if (myNode.Attributes["secondfont"] != null)
                    if (!pFontSecond.isAlias)
                        myNode.Attributes["secondfont"].Value = pFontSecond.Name + "; " + pFontSecondSize;
                    else
                        myNode.Attributes["secondfont"].Value = pFontSecond.Name;
                else
                {
                    myNode.Attributes.Append(myNode.OwnerDocument.CreateAttribute("font"));
                    if (!pFontSecond.isAlias)
                        myNode.Attributes["secondfont"].Value = pFontSecond.Name + "; " + pFontSecondSize;
                    else
                        myNode.Attributes["secondfont"].Value = pFontSecond.Name;
                }
            }
        }

        public sAttributeListbox(sAttribute parent, XmlNode node)
            : base(parent, node)
        {
            // -----------------------------------------------------------------------------------------------------
            if (myNode.Attributes["cornerRadius"] != null)
            {
                string value = myNode.Attributes["cornerRadius"].Value;

                Logger.LogMessage("%%%%%%%%%%%%%%% cAttributeLabel.cs - cornerRadius ist: " + value);

                float.TryParse(value, out pCornerRadius);
            }


            if (node.Attributes["backgroundColorSelected"] != null)
                // ------------------ hier wird die farbe geholt wenn sie in der Zeile drin ist ---------------------------------------
                pListboxSelectedBackgroundColor = (sColor)cDataBase.pColors.get(node.Attributes["backgroundColorSelected"].Value);


            else
                // ------------------ das sollte die Farbe sein die in Windowssytle als default festgelegt ist ------------------------
                // ------------------ die variablen sind ganz am anfang auch hier festgelegt wenn kein windowsstyle da  ------------------------
                if (pListboxSelectedBackgroundColor == null)
                pListboxSelectedBackgroundColor = (sColor)((sWindowStyle)cDataBase.pWindowstyles.get()).pColors["ListboxSelectedBackground"];

            // ------------------------------ schauen ist Gradient vorhanden --------------------------------------------------
            if (myNode.Attributes["itemGradientSelected"] != null)
            {
                // hier Gradient einlesen und in variablen setzen und Gradient aktivieren

                string value = myNode.Attributes["itemGradientSelected"].Value;

                Logger.LogMessage("///////////////// cAttributeListbox.cs - Die Hintergrundfarbe ItemGradientSelected aus dem XML-Attribut ist: " + value);

                pItemGradientSelectedRaw = value;
                pItemGradientSelected = sGradient.parse(value);
            }
            // ________________________________________________________________________________________________________________

            if (myNode.Attributes["itemCornerRadiusSelected"] != null)
            {
                int.TryParse(myNode.Attributes["itemCornerRadiusSelected"].Value.Split(';')[0].Trim(), out pItemCornerRadiusSelected);
                if (pItemCornerRadiusSelected < 0)
                    pItemCornerRadiusSelected = 0;
            }



            if (myNode.Attributes["itemGradient"] != null)
            {
                string value = myNode.Attributes["itemGradient"].Value;
                pItemGradientRaw = value;
                pItemGradient = sGradient.parse(value);
            }

            if (myNode.Attributes["itemCornerRadius"] != null)
            {
                int.TryParse(myNode.Attributes["itemCornerRadius"].Value.Split(';')[0].Trim(), out pItemCornerRadius);
                if (pItemCornerRadius < 0)
                    pItemCornerRadius = 0;
            }

            if (myNode.Attributes["listOrientation"] != null)
                pListOrientation = myNode.Attributes["listOrientation"].Value.ToLower() == "grid" ? "grid" : "vertical";

            if (myNode.Attributes["itemWidth"] != null)
                int.TryParse(myNode.Attributes["itemWidth"].Value.Trim(), out pItemWidth);

            if (myNode.Attributes["itemSpacing"] != null)
            {
                String[] spacing = myNode.Attributes["itemSpacing"].Value.Split(new char[] { ',' });
                if (spacing.Length > 0) int.TryParse(spacing[0].Trim(), out pItemSpacingX);
                if (spacing.Length > 1) int.TryParse(spacing[1].Trim(), out pItemSpacingY);
            }

            if (myNode.Attributes["selectionZoom"] != null)
                int.TryParse(myNode.Attributes["selectionZoom"].Value.Trim(), out pSelectionZoom);

            if (myNode.Attributes["moveBackgroundColor"] != null)
                pMoveBackgroundColor = (sColor)cDataBase.pColors.get(myNode.Attributes["moveBackgroundColor"].Value);

            if (myNode.Attributes["moveFontColor"] != null)
                pMoveFontColor = (sColor)cDataBase.pColors.get(myNode.Attributes["moveFontColor"].Value);

            if (myNode.Attributes["selection"] != null)
            {
                int.TryParse(myNode.Attributes["selection"].Value.Trim(), out pSelection);
                if (pSelection < 0)
                    pSelection = 0;
            }

            if (myNode.Attributes["enableWrapAround"] != null)
            {
                String wrap = myNode.Attributes["enableWrapAround"].Value.Trim().ToLower();
                pEnableWrapAround = wrap == "1" || wrap == "true" || wrap == "yes";
            }



            if (node.Attributes["foregroundColorSelected"] != null)

                pListboxSelectedForegroundColor = (sColor)cDataBase.pColors.get(node.Attributes["foregroundColorSelected"].Value);
            else
                if (pListboxSelectedForegroundColor == null)
                pListboxSelectedForegroundColor = (sColor)((sWindowStyle)cDataBase.pWindowstyles.get()).pColors["ListboxSelectedForeground"];


            // -------------------------------------------------- Scrollbar ---------------------------------------------------------------------
            Logger.LogMessage(" ------------------------------- scrollbar ------------------------------------ ");


            if (myNode.Attributes["scrollbarMode"] != null)
            {
                pScrollbarMode = myNode.Attributes["scrollbarMode"].Value.ToLower() == "showAlways".ToLower() ? cProperty.eScrollbarMode.showAlways :
                    myNode.Attributes["scrollbarMode"].Value.ToLower() == "showOnDemand".ToLower() ? cProperty.eScrollbarMode.showOnDemand :
                    cProperty.eScrollbarMode.showNever;
                Logger.LogMessage("///////////////// cAttributeListbox.cs - pScrollbarMode: " + pScrollbarMode);

            }
            else
            {
                // wenn keine ScrollbarMode in der zeile , auf showNever setzen

                // Wenn kein ScrollbarMode vorhanden ist, auf showNever setzen
                pScrollbarMode = cProperty.eScrollbarMode.showNever;

                // Optional: Du kannst auch direkt das Attribut in myNode setzen, falls es nicht existiert.
                var scrollbarModeAttribute = myNode.OwnerDocument.CreateAttribute("scrollbarMode");
                scrollbarModeAttribute.Value = "showNever";
                myNode.Attributes.Append(scrollbarModeAttribute);

            }


            if (myNode.Attributes["scrollbarWidth"] != null)
            {
                string value = myNode.Attributes["scrollbarWidth"].Value;

                Logger.LogMessage("///////////////// cAttributeListbox.cs - scrollbarWidth : " + value);

                int.TryParse(value, out pscrollbarWidth);
            }

            if (myNode.Attributes["scrollbarOffset"] != null)
            {

                string value = myNode.Attributes["scrollbarOffset"].Value;

                Logger.LogMessage("///////////////// cAttributeListbox.cs - scrollbarOffset: " + value);

                int.TryParse(value, out pscrollbarOffset);
            }


            if (myNode.Attributes["scrollbarRadius"] != null)
            {
                string value = myNode.Attributes["scrollbarRadius"].Value;
                Logger.LogMessage("///////////////// cAttributeListbox.cs - scrollbarRadius: " + value);
                int.TryParse(value.Split(';')[0].Trim(), out pscrollbarRadius);
                if (pscrollbarRadius < 0)
                    pscrollbarRadius = 0;
            }

            if (myNode.Attributes["scrollbarSliderForegroundColor"] != null)
            {
                string value = myNode.Attributes["scrollbarSliderForegroundColor"].Value;
                Logger.LogMessage("///////////////// cAttributeListbox.cs - scrollbarSliderForegroundColor : " + value);
                pscrollbarSliderForegroundColor = (sColor)cDataBase.pColors.get(myNode.Attributes["scrollbarSliderForegroundColor"].Value);

            }

            if (myNode.Attributes["scrollbarForegroundColor"] != null && pscrollbarSliderForegroundColor == null)
            {
                string value = myNode.Attributes["scrollbarForegroundColor"].Value;
                Logger.LogMessage("///////////////// cAttributeListbox.cs - scrollbarForegroundColor : " + value);
                pscrollbarSliderForegroundColor = (sColor)cDataBase.pColors.get(value);
            }

            if (myNode.Attributes["scrollbarSliderBackgroundColor"] != null)
            {
                string value = myNode.Attributes["scrollbarSliderBackgroundColor"].Value;
                Logger.LogMessage("///////////////// cAttributeListbox.cs - scrollbarSliderBackgroundColor : " + value);

                pscrollbarSliderBackgroundColor = (sColor)cDataBase.pColors.get(myNode.Attributes["scrollbarSliderBackgroundColor"].Value);

            }

            if (myNode.Attributes["scrollbarBackgroundColor"] != null && pscrollbarSliderBackgroundColor == null)
            {
                string value = myNode.Attributes["scrollbarBackgroundColor"].Value;
                Logger.LogMessage("///////////////// cAttributeListbox.cs - scrollbarBackgroundColor : " + value);
                pscrollbarSliderBackgroundColor = (sColor)cDataBase.pColors.get(value);
            }

            if (myNode.Attributes["scrollbarSliderBorderColor"] != null)
            {
                string value = myNode.Attributes["scrollbarSliderBorderColor"].Value;
                Logger.LogMessage("///////////////// cAttributeListbox.cs - scrollbarSliderBorderColor : " + value);

                pscrollbarSliderBorderColor = (sColor)cDataBase.pColors.get(myNode.Attributes["scrollbarSliderBorderColor"].Value);

            }

            if (myNode.Attributes["scrollbarBorderColor"] != null && pscrollbarSliderBorderColor == null)
            {
                string value = myNode.Attributes["scrollbarBorderColor"].Value;
                Logger.LogMessage("///////////////// cAttributeListbox.cs - scrollbarBorderColor : " + value);
                pscrollbarSliderBorderColor = (sColor)cDataBase.pColors.get(value);
            }

            if (myNode.Attributes["scrollbarSliderBorderWidth"] != null)
            {
                string value = myNode.Attributes["scrollbarSliderBorderWidth"].Value;

                Logger.LogMessage("///////////////// cAttributeListbox.cs - scrollbarSliderBorderWidth: " + value);

                int.TryParse(value, out pscrollbarSliderBorderWidth);
            }


            if (myNode.Attributes["scrollbarBorderWidth"] != null && pscrollbarSliderBorderWidth == 0)
            {
                string value = myNode.Attributes["scrollbarBorderWidth"].Value;
                Logger.LogMessage("///////////////// cAttributeListbox.cs - scrollbarBorderWidth: " + value);
                int.TryParse(value, out pscrollbarSliderBorderWidth);
            }

            if (myNode.Attributes["scrollbarForegroundGradient"] != null)
            {
                string value = myNode.Attributes["scrollbarForegroundGradient"].Value;

                Logger.LogMessage("///////////////// cAttributeListbox.cs - scrollbarForegroundGradient: " + value);

                pScrollbarForegroundGradient = sGradient.parse(value);
            }

            if (node.Attributes["scrollbarBackgroundPicture"] != null)
            {
                pScrollbarBackgroundPictureName = node.Attributes["scrollbarBackgroundPicture"].Value;
                try
                {
                    pScrollbarBackgroundPicture = Image.FromFile(cDataBase.getPath(pScrollbarBackgroundPictureName));
                }
                catch (FileNotFoundException)
                {
                    pScrollbarBackgroundPicture = null;
                }
            }
            Logger.LogMessage(" ------------------------------- scrollbar ------------------------------------ ");

            // -------------------------------------------------------------------------------------------------------------------------------------












            if (node.Attributes["backgroundColor"] != null)
                pListboxBackgroundColor = (sColor)cDataBase.pColors.get(node.Attributes["backgroundColor"].Value);
            else
                pListboxBackgroundColor = (sColor)((sWindowStyle)cDataBase.pWindowstyles.get()).pColors["ListboxBackground"];

            if (node.Attributes["foregroundColor"] != null)
                pListboxForegroundColor = (sColor)cDataBase.pColors.get(node.Attributes["foregroundColor"].Value);
            else
                pListboxForegroundColor = (sColor)((sWindowStyle)cDataBase.pWindowstyles.get()).pColors["ListboxForeground"];

            if (myNode.Attributes["font"] != null)
            {
                pFont = cDataBase.getFont(myNode.Attributes["font"].Value);
                pFontSize = pFont.Size;
            }

            if (myNode.Attributes["secondfont"] != null)
            {
                pFontSecond = cDataBase.getFont(myNode.Attributes["secondfont"].Value);
                pFontSecondSize = pFontSecond.Size;
            }

            //if (node.HasChildNodes && node.FirstChild.Attributes["type"] != null && node.FirstChild.Attributes["type"].Value.ToLower() == "templatedmulticontent")
            //{
            //    String input = node.FirstChild.InnerText;
            //    Match match = Regex.Match(input, @"""itemHeight"": (\d+)");
            //    if (match.Success)
            //        pItemHeight = Int16.Parse(match.Groups[1].ToString());

            //    //MessageBox.Show(match.Groups[1].ToString());

            //}

            // Keep explicitly parsed selected colors. Use windowstyle defaults only when not set.
            if (pListboxSelectedBackgroundColor == null)
                pListboxSelectedBackgroundColor = (sColor)((sWindowStyle)cDataBase.pWindowstyles.get()).pColors["ListboxSelectedBackground"];

            if (pListboxSelectedForegroundColor == null)
                pListboxSelectedForegroundColor = (sColor)((sWindowStyle)cDataBase.pWindowstyles.get()).pColors["ListboxSelectedForeground"];


            //if (node.Attributes["backgroundColor"] != null)
            //    pListboxMarkedBackgroundColor = (sColor)cDataBase.pColors.get(node.Attributes["backgroundColor"].Value);
            //else
            pListboxMarkedBackgroundColor = (sColor)((sWindowStyle)cDataBase.pWindowstyles.get()).pColors["ListboxMarkedBackground"];

            //if (node.Attributes["backgroundColor"] != null)
            //    pListboxMarkedForegroundColor = (sColor)cDataBase.pColors.get(node.Attributes["backgroundColor"].Value);
            //else
            pListboxMarkedForegroundColor = (sColor)((sWindowStyle)cDataBase.pWindowstyles.get()).pColors["ListboxMarkedForeground"];


            //if (node.Attributes["backgroundColor"] != null)
            //    pListboxMarkedAndSelectedBackgroundColor = (sColor)cDataBase.pColors.get(node.Attributes["backgroundColor"].Value);
            //else
            pListboxMarkedAndSelectedBackgroundColor = (sColor)((sWindowStyle)cDataBase.pWindowstyles.get()).pColors["ListboxMarkedAndSelectedBackground"];

            //if (node.Attributes["backgroundColor"] != null)
            //    pListboxMarkedAndSelectedForegroundColor = (sColor)cDataBase.pColors.get(node.Attributes["backgroundColor"].Value);
            //else
            pListboxMarkedAndSelectedForegroundColor = (sColor)((sWindowStyle)cDataBase.pWindowstyles.get()).pColors["ListboxMarkedAndSelectedForeground"];


            if (node.Attributes["backgroundPixmap"] != null)
            {
                pBackgroundPixmapName = node.Attributes["backgroundPixmap"].Value;
                try
                {
                    pBackgroundPixmap = Image.FromFile(cDataBase.getPath(pBackgroundPixmapName));
                }
                catch (FileNotFoundException)
                {
                    pBackgroundPixmap = null;
                }
            }
            if (node.Attributes["selectionPixmap"] != null)
            {
                pSelectionPixmapName = node.Attributes["selectionPixmap"].Value;
                try
                {
                    pSelectionPixmap = Image.FromFile(cDataBase.getPath(pSelectionPixmapName));
                }
                catch (FileNotFoundException)
                {
                    pSelectionPixmap = null;
                }
            }

            if (myNode.Attributes["scrollbarMode"] != null)
                pScrollbarMode = myNode.Attributes["scrollbarMode"].Value.ToLower() == "showAlways".ToLower() ? cProperty.eScrollbarMode.showAlways :
                    myNode.Attributes["scrollbarMode"].Value.ToLower() == "showOnDemand".ToLower() ? cProperty.eScrollbarMode.showOnDemand :
                    cProperty.eScrollbarMode.showNever;

            sWindowStyle style = (sWindowStyle)cDataBase.pWindowstyles.get();
            sWindowStyle.sBorderSet borderset = (sWindowStyle.sBorderSet)style.pBorderSets["bsListboxEntry"];

            if (borderset != null)
            {

                if (borderset.pbpTopLeftName.Length > 0)
                {
                    pbpTopLeftName = borderset.pbpTopLeftName;
                    pbpTopLeft = borderset.pbpTopLeft;
                }
                if (borderset.pbpTopName.Length > 0)
                {
                    pbpTopName = borderset.pbpTopName;
                    pbpTop = borderset.pbpTop;
                }
                if (borderset.pbpTopRightName.Length > 0)
                {
                    pbpTopRightName = borderset.pbpTopRightName;
                    pbpTopRight = borderset.pbpTopRight;
                }


                if (borderset.pbpLeftName.Length > 0)
                {
                    pbpLeftName = borderset.pbpLeftName;
                    pbpLeft = borderset.pbpLeft;
                }
                if (borderset.pbpRightName.Length > 0)
                {
                    pbpRightName = borderset.pbpRightName;
                    pbpRight = borderset.pbpRight;
                }


                if (borderset.pbpBottomLeftName.Length > 0)
                {
                    pbpBottomLeftName = borderset.pbpBottomLeftName;
                    pbpBottomLeft = borderset.pbpBottomLeft;
                }
                if (borderset.pbpBottomName.Length > 0)
                {
                    pbpBottomName = borderset.pbpBottomName;
                    pbpBottom = borderset.pbpBottom;
                }
                if (borderset.pbpBottomRightName.Length > 0)
                {
                    pbpBottomRightName = borderset.pbpBottomRightName;
                    pbpBottomRight = borderset.pbpBottomRight;
                }
            }


            if (node.Attributes["itemHeight"] != null)
                pItemHeight = Convert.ToInt32(node.Attributes["itemHeight"].Value.Trim());
            else
                if (pItemHeight == 0)
                pItemHeight = 20;


            String entries = cPreviewText.getText(parent.Name, Name);
            if (entries.Length > 0)
                pPreviewEntries = new List<string>(entries.Split('|'));
            else
                for (int i = 1; i < 50; i++)
                {
                    pPreviewEntries.Add("List entry " + i);
                }
        }
    }
}