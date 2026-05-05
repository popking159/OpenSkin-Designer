using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Drawing.Design;
using OpenSkinDesigner.Logic;
using System.Drawing;
using System.Collections;

namespace OpenSkinDesigner.Structures
{
    class cProperty
    {
        // Color
        public class sColorConverter : StringConverter
        {
            public override bool GetStandardValuesSupported(
                           ITypeDescriptorContext context)
            {
                return true;
            }

            public override StandardValuesCollection
                     GetStandardValues(ITypeDescriptorContext context)
            {
                sColor[] colors = (sColor[])cDataBase.pColors.getArray();
                ArrayList list = new ArrayList();
                foreach (sColor color in colors)
                    list.Add(color.pName);
                return new StandardValuesCollection(list.ToArray());
            }

            public override bool GetStandardValuesExclusive(
                           ITypeDescriptorContext context)
            {
                return true;
            }
        }

        // Color or comma-separated gradient string.
        // The dropdown still lists known colors, but manual text entry is allowed
        // for values such as: green,yellow,red,horizontal
        public class sColorOrGradientConverter : sColorConverter
        {
            public override bool GetStandardValuesExclusive(
                           ITypeDescriptorContext context)
            {
                return false;
            }
        }

        public class GradeEditor : UITypeEditor
        {
            public override bool GetPaintValueSupported(
                  ITypeDescriptorContext context)
            {
                // let the property browser know we'd like
                // to do custom painting.
                return true;
            }

            public override void PaintValue(PaintValueEventArgs pe)
            {
                // choose the right bitmap based on the value
                String g = (String)pe.Value;
                sColor color = (sColor)cDataBase.pColors.get(g);

                // draw that bitmap onto the surface provided.
                if(color != null)
                    pe.Graphics.FillRectangle(new SolidBrush(color.Color), pe.Bounds);
                else
                    pe.Graphics.DrawRectangle(new Pen(Color.Black, 1.0F), pe.Bounds);
            }
        }

        // Font
        public class sFontConverter : StringConverter
        {
            public override bool GetStandardValuesSupported(
                           ITypeDescriptorContext context)
            {
                return true;
            }

            public override StandardValuesCollection
                     GetStandardValues(ITypeDescriptorContext context)
            {
                sFont[] fonts = cDataBase.getFonts();
                ArrayList list = new ArrayList();
                foreach (sFont font in fonts)
                    list.Add(font.Name);
                return new StandardValuesCollection(list.ToArray());
            }

            public override bool GetStandardValuesExclusive(
                           ITypeDescriptorContext context)
            {
                return true;
            }
        }

        // Alphatest
        public enum eAlphatest { on, off, blend };
        public class AlphatestConverter : StringConverter
        {
            public override bool GetStandardValuesSupported(
                           ITypeDescriptorContext context)
            {
                return true;
            }

            public override StandardValuesCollection
                     GetStandardValues(ITypeDescriptorContext context)
            {
                return new StandardValuesCollection(new string[]{eAlphatest.off.ToString(), 
                                                     eAlphatest.on.ToString(), 
                                                     eAlphatest.blend.ToString()});
            }

            public override bool GetStandardValuesExclusive(
                           ITypeDescriptorContext context)
            {
                return true;
            }
        }



        public enum eVAlign { Top, Center, Bottom };
        public enum eHAlign { Left, Center, Right };
        public class VAlignConverter : StringConverter
        {
            public override bool GetStandardValuesSupported(
                           ITypeDescriptorContext context)
            {
                return true;
            }

            public override StandardValuesCollection
                     GetStandardValues(ITypeDescriptorContext context)
            {
                return new StandardValuesCollection(new string[]{eVAlign.Top.ToString(), 
                                                     eVAlign.Center.ToString(), 
                                                     eVAlign.Bottom.ToString()});
            }

            public override bool GetStandardValuesExclusive(
                           ITypeDescriptorContext context)
            {
                return true;
            }
        }
        public class HAlignConverter : StringConverter
        {
            public override bool GetStandardValuesSupported(
                           ITypeDescriptorContext context)
            {
                return true;
            }

            public override StandardValuesCollection
                     GetStandardValues(ITypeDescriptorContext context)
            {
                return new StandardValuesCollection(new string[]{eHAlign.Left.ToString(), 
                                                     eHAlign.Center.ToString(), 
                                                     eHAlign.Right.ToString()});
            }

            public override bool GetStandardValuesExclusive(
                           ITypeDescriptorContext context)
            {
                return true;
            }
        }

        public enum eScrollbarMode { showOnDemand, showAlways, showNever };
        public class ScrollbarModeConverter : StringConverter
        {
            public override bool GetStandardValuesSupported(
                           ITypeDescriptorContext context)
            {
                return true;
            }

            public override StandardValuesCollection
                     GetStandardValues(ITypeDescriptorContext context)
            {
                return new StandardValuesCollection(new string[]{eScrollbarMode.showNever.ToString(), 
                                                     eScrollbarMode.showOnDemand.ToString(), 
                                                     eScrollbarMode.showAlways.ToString()});
            }

            public override bool GetStandardValuesExclusive(
                           ITypeDescriptorContext context)
            {
                return true;
            }
        }


        // Corner radius direction presets used by the property grid.
        public class CornerRadiusCornersConverter : StringConverter
        {
            public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
            {
                return true;
            }

            public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
            {
                return new StandardValuesCollection(new string[]{
                    "all",
                    "top",
                    "bottom",
                    "left",
                    "right",
                    "topLeft",
                    "topRight",
                    "bottomLeft",
                    "bottomRight",
                    "topLeft,topRight",
                    "bottomLeft,bottomRight",
                    "topLeft,bottomLeft",
                    "topRight,bottomRight",
                    "topLeft,topRight,bottomLeft,bottomRight",
                    "none"
                });
            }

            public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
            {
                return false;
            }
        }

        // Gradient direction presets used by the property grid.
        public class GradientDirectionConverter : StringConverter
        {
            public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
            {
                return true;
            }

            public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
            {
                return new StandardValuesCollection(new string[]{"vertical", "horizontal"});
            }

            public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
            {
                return true;
            }
        }


        // Listbox orientation presets used by the property grid.
        public class ListOrientationConverter : StringConverter
        {
            public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
            {
                return true;
            }

            public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
            {
                return new StandardValuesCollection(new string[]{"vertical", "grid"});
            }

            public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
            {
                return true;
            }
        }

    }
}
