using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Collections;

using OpenSkinDesigner.Structures;
using System.Xml;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Linq;
using OpenSkinDesigner.Frames;

namespace OpenSkinDesigner.Logic
{
    static public class cDataBase
    {
        static private Hashtable pFonts = null;



        static public cDataBaseColor pColors;
        static public cDataBaseWindowstyle pWindowstyles;
        static public cDataBaseImage pImage;
        static public cDataBaseResolution pResolution;

        static public void init(cXMLHandler XmlHandler)
        {
            pColors = new cDataBaseColor(XmlHandler);
            initFonts(XmlHandler);
            pResolution = new cDataBaseResolution(XmlHandler);
            pImage = new cDataBaseImage(XmlHandler);
            pWindowstyles = new cDataBaseWindowstyle(XmlHandler);
        }

        static public void clear()
        {
            pColors = null;
            pFonts = null;
            pResolution = null;
            pImage = null;
            pWindowstyles = null;
        }

        //#################################################################

        public abstract class cDataBaseElement
        {
            public abstract Object[] getArray();
            public abstract Object get(String name);
            public abstract String add(Object element);
            public abstract bool remove(Object element);
            public abstract bool sync(cXMLHandler XmlHandler);
        }

        //#################################################################

        public class cDataBaseWindowstyle : cDataBaseElement
        {
            private sWindowStyle pWindowStyle = null;

            public cDataBaseWindowstyle(cXMLHandler XmlHandler)
            {
                Hashtable colors = new Hashtable();
                sFont titlefont = null;
                float titlesize = (float)0.0;
                Int32 xOff = 0, yOff = 0;
                ArrayList bordersets = new ArrayList();

                string[] path = { /*"skin", */"windowstyle" };
                XmlNode styleNode = XmlHandler.XmlGetRootNodeElement(path);
                if (styleNode == null)
                {
                    //<font filename= name= scale="90" />
                    sFont font = new sFont(
                        "Regular",
                        "nmsbd.ttf",
                        90,
                        30,
                        "nmsbd",
                        false
                    );
                    pFonts.Add(font.Name, font);

                    colors.Add("Background", pColors.get("#25062748"));
                    colors.Add("LabelForeground", pColors.get("#ffffff"));
                    colors.Add("ListboxBackground", pColors.get("#25062748"));
                    colors.Add("ListboxForeground", pColors.get("#ffffff"));
                    colors.Add("ListboxSelectedBackground", pColors.get("#254f7497"));
                    colors.Add("ListboxSelectedForeground", pColors.get("#ffffff"));
                    colors.Add("ListboxMarkedBackground", pColors.get("#ff0000"));
                    colors.Add("ListboxMarkedForeground", pColors.get("#ffffff"));
                    colors.Add("ListboxMarkedAndSelectedBackground", pColors.get("#800000"));
                    colors.Add("ListboxMarkedAndSelectedForeground", pColors.get("#ffffff"));
                    colors.Add("WindowTitleForeground", pColors.get("#ffffff"));
                    colors.Add("WindowTitleBackground", pColors.get("#25062748"));

                    sWindowStyle.sBorderSet borderset = new sWindowStyle.sBorderSet(
                                "bsWindow",
                                "skin_default/b_tl.png",
                                "skin_default/b_t.png",
                                "skin_default/b_tr.png",
                                "skin_default/b_l.png",
                                "skin_default/b_r.png",
                                "skin_default/b_bl.png",
                                "skin_default/b_b.png",
                                "skin_default/b_br.png");
                    bordersets.Add(borderset);

                    pWindowStyle = new sWindowStyle(getFont("Regular"), 20.0f, 33, 14, colors, (sWindowStyle.sBorderSet[])bordersets.ToArray(typeof(sWindowStyle.sBorderSet)));
                }
                else
                {
                    foreach (XmlNode myXmlNode in styleNode.ChildNodes)
                    {
                        if (myXmlNode.Name == "color")
                        {
                            if (colors[myXmlNode.Attributes["color"].Value] == null)
                                colors.Add(myXmlNode.Attributes["name"].Value, pColors.get(myXmlNode.Attributes["color"].Value));
                        }
                        else if (myXmlNode.Name == "title")
                        {
                            String font = myXmlNode.Attributes["font"].Value;
                            titlesize = Convert.ToSingle(font.Substring(font.IndexOf(';') + 1));
                            font = font.Substring(0, font.IndexOf(';'));
                            titlefont = getFont(font);
                            xOff = Convert.ToInt32(myXmlNode.Attributes["offset"].Value.Substring(0, myXmlNode.Attributes["offset"].Value.IndexOf(',')));
                            yOff = Convert.ToInt32(myXmlNode.Attributes["offset"].Value.Substring(myXmlNode.Attributes["offset"].Value.IndexOf(',') + 1));
                        }
                        else if (myXmlNode.Name == "borderset")
                        {
                            String pbpTopLeftName = "";
                            String pbpTopName = "";
                            String pbpTopRightName = "";
                            String pbpLeftName = "";
                            String pbpRightName = "";
                            String pbpBottomLeftName = "";
                            String pbpBottomName = "";
                            String pbpBottomRightName = "";

                            //string[] path2 = { "skin", "windowstyle", "borderset" };
                            //XmlNode fontNode2 = XmlHandler.XmlGetRootNodeElement(path2);
                            int anz = 0;
                            foreach (XmlNode myXmlNode2 in /*fontNode2*/myXmlNode.ChildNodes)
                            {
                                try
                                {
                                    if (myXmlNode2.Attributes["pos"] != null)
                                    {


                                        if (myXmlNode2.Attributes["pos"].Value == "bpTopLeft")
                                        {
                                            if (myXmlNode2.OuterXml.Contains("filename="))
                                                pbpTopLeftName = myXmlNode2.Attributes["filename"].Value;
                                            else
                                            {
                                                anz += 1;
                                            }
                                        }
                                        else if (myXmlNode2.Attributes["pos"].Value == "bpTop")
                                        {
                                            if (myXmlNode2.OuterXml.Contains("filename="))
                                                pbpTopName = myXmlNode2.Attributes["filename"].Value;
                                            else
                                            {
                                                anz += 1;
                                            }
                                        }
                                        else if (myXmlNode2.Attributes["pos"].Value == "bpTopRight")
                                        {
                                            if (myXmlNode2.OuterXml.Contains("filename="))
                                                pbpTopRightName = myXmlNode2.Attributes["filename"].Value;
                                            else
                                            {
                                                anz += 1;
                                            }
                                        }
                                        else if (myXmlNode2.Attributes["pos"].Value == "bpLeft")
                                        {
                                            if (myXmlNode2.OuterXml.Contains("filename="))
                                                pbpLeftName = myXmlNode2.Attributes["filename"].Value;
                                            else
                                            {
                                                anz += 1;
                                            }
                                        }
                                        else if (myXmlNode2.Attributes["pos"].Value == "bpRight")
                                        {
                                            if (myXmlNode2.OuterXml.Contains("filename="))
                                                pbpRightName = myXmlNode2.Attributes["filename"].Value;
                                            else
                                            {
                                                anz += 1;
                                            }
                                        }
                                        else if (myXmlNode2.Attributes["pos"].Value == "bpBottomLeft")
                                        {
                                            if (myXmlNode2.OuterXml.Contains("filename="))
                                                pbpBottomLeftName = myXmlNode2.Attributes["filename"].Value;
                                            else
                                            {
                                                anz += 1;
                                            }
                                        }
                                        else if (myXmlNode2.Attributes["pos"].Value == "bpBottom")
                                        {
                                            if (myXmlNode2.OuterXml.Contains("filename="))
                                                pbpBottomName = myXmlNode2.Attributes["filename"].Value;
                                            else
                                            {
                                                anz += 1;
                                            }
                                        }
                                        else if (myXmlNode2.Attributes["pos"].Value == "bpBottomRight")
                                        {
                                            if (myXmlNode2.OuterXml.Contains("filename="))
                                                pbpBottomRightName = myXmlNode2.Attributes["filename"].Value;
                                            else
                                            {
                                                anz += 1;
                                            }
                                        }

                                    }
                                    else
                                    { // [pos] = null

                                    }

                                }
                                catch (NullReferenceException err)
                                {
                                    MessageBox.Show("There is something wrong with your borderset" + "\n\n" + err.Message);
                                }
                                
                                    
                            }
                            if (anz > 0)
                            {
                                MessageBox.Show("The path of " + anz + " borderset-files is not specified!" + "\n\n",
                                "Path(s) not specified",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                            }
                            sWindowStyle.sBorderSet borderset = new sWindowStyle.sBorderSet(
                                myXmlNode.Attributes["name"].Value,
                                pbpTopLeftName,
                                pbpTopName,
                                pbpTopRightName,
                                pbpLeftName,
                                pbpRightName,
                                pbpBottomLeftName,
                                pbpBottomName,
                                pbpBottomRightName);
                            bordersets.Add(borderset);
                        }
                    }

                    pWindowStyle = new sWindowStyle(titlefont, titlesize, xOff, yOff, colors, (sWindowStyle.sBorderSet[])bordersets.ToArray(typeof(sWindowStyle.sBorderSet)));
                }
            }

            public Object get()
            {
                // Valerie workaround till font loading is supported by skins
                // At this point the font should be loaded
                if (pWindowStyle.pFont == null) pWindowStyle.pFont = getFont("Regular");
                return (Object)pWindowStyle;
            }

            public override Object get(String name)
            {
                return (Object)pWindowStyle;
            }

            public override Object[] getArray() { return null; }
            public override String add(Object element) { return null; }

            public override bool remove(Object element) { return false; }
            public override bool sync(cXMLHandler XmlHandler) { return false; }
        }

        //#################################################################

        public class cDataBaseImage
        {
            private ArrayList pImages = null;

            public cDataBaseImage(cXMLHandler XmlHandler)
            {
                string[] path2 = { "skin" };
                XmlNode Node = XmlHandler.XmlGetRootNodeElement(null/*path2*/);

                pImages = new ArrayList();

                if (Node.HasChildNodes)
                    checkImages(Node.ChildNodes);
            }

            private void checkImages(XmlNodeList nodes)
            {
                foreach (XmlNode myXmlNode in nodes)
                {
                    if (myXmlNode.HasChildNodes)
                        checkImages(myXmlNode.ChildNodes);

                    if (myXmlNode.Attributes != null)
                    {
                        if (myXmlNode.Attributes["pixmap"] != null)
                        {
                            if (!pImages.Contains(myXmlNode.Attributes["pixmap"].Value))
                                pImages.Add(myXmlNode.Attributes["pixmap"].Value);
                        }
                        //if (myXmlNode.Attributes["filename"] != null)
                        //{
                        //    if (!pImages.Contains(myXmlNode.Attributes["filename"].Value))
                        //        pImages.Add(myXmlNode.Attributes["filename"].Value);
                        //}
                    }
                }
            }

            public void rescale(int o_x, int o_y, int x, int y)
            {
                double scale_x = (x * 1.0) / o_x;
                double scale_y = (y * 1.0) / o_y;

                foreach (String image in pImages)
                {
                    Image pixmap;
                    try
                    {
                        pixmap = Image.FromFile(cDataBase.getPath(image));
                    }
                    catch (FileNotFoundException error)
                    {
                        String errormessage = fMain.GetTranslation("PNG not found") + " " + image + "!";
                        errormessage += cDataBase.getPath(image) + "\n\n";
                        errormessage += fMain.GetTranslation("Error:") + "\n";
                        errormessage += error.Message;

                        MessageBox.Show(errormessage,
                            error.Message,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information,
                            MessageBoxDefaultButton.Button1);
                        continue;
                    }
                    Image scaled = pixmap.GetThumbnailImage(Convert.ToInt32(pixmap.Width * scale_x), Convert.ToInt32(pixmap.Height * scale_y), null, IntPtr.Zero);
                    pixmap.Dispose();
                    pixmap = null;
                    String path = cDataBase.getPath(image);
                    scaled.Save(path);
                    scaled.Dispose();
                    pixmap = null;
                }

            }
        }

        //#################################################################

        public class cDataBaseColor : cDataBaseElement
        {
            private Hashtable pColors = null;

            public cDataBaseColor(cXMLHandler XmlHandler)
            {
                pColors = new Hashtable();

                string[] path = { /*"skin", */"colors" };
                XmlNode colorNode = XmlHandler.XmlGetRootNodeElement(path);
                foreach (XmlNode myXmlNode in colorNode.ChildNodes)
                {
                    if (myXmlNode.NodeType != XmlNodeType.Element)
                        continue;
                    String colorString = myXmlNode.Attributes["value"].Value;
                    sColor color;
                    if (colorString[0] == '#')
                    {
                        UInt32 colorValue = Convert.ToUInt32(colorString.Substring(1), 16);
                        color = new sColor(myXmlNode.Attributes["name"].Value, colorValue);
                    }
                    else
                        color = new sColor(myXmlNode.Attributes["name"].Value, colorString);

                    if (pColors[color.pName] == null)
                        pColors.Add(color.pName, color);
                    else
                    {
                        String errormessage = fMain.GetTranslation("More than one color defined with name") + " '" + color.pName + "'";
                        errormessage += "\n\n" + ((sColor)pColors[color.pName]).pName + "\t#" + Convert.ToString(((sColor)pColors[color.pName]).pValue, 16); ;
                        errormessage += "\n" + color.pName + "\t#" + Convert.ToString(color.pValue, 16);

                        errormessage += "\n";
                        //errormessage += "\n" + fMain.GetTranslation("The second definition will be deleted if you save manually.");
                        errormessage += "\n" + fMain.GetTranslation("The second definition will be deleted!");

                        // errormessage += "\n";
                        // errormessage += "\n" + fMain.GetTranslation("Until you save, the second definition is still shown in TreeView!");

                        // errormessage += "\n";
                        // errormessage += "\n" + fMain.GetTranslation("Do you want to save and reload the skin?");



                        // if (MessageBox.Show(errormessage,
                        //    fMain.GetTranslation("Error while parsing color table"),
                        //  MessageBoxButtons.YesNo,
                        //MessageBoxIcon.Information,
                        //MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                        // {
                        //     MyGlobaleVariables.Reload = true;
                        // }
                    }
                }

                string[] path2 = { "skin" };
                XmlNode Node = XmlHandler.XmlGetRootNodeElement(null/*path2*/);
                checkColor(Node.ChildNodes);

                sync(XmlHandler);
            }

            private String[] color_attrs = { "color", "foregroundColor", "backgroundColor", "borderColor" };

            private void checkColor(XmlNodeList nodes)
            {
                foreach (XmlNode myXmlNode in nodes)
                {
                    if (myXmlNode.Attributes == null)
                        continue;

                    if (myXmlNode.HasChildNodes)
                        checkColor(myXmlNode.ChildNodes);

                    foreach (String color in color_attrs)
                    {
                        if (myXmlNode.Attributes[color] != null)
                        {
                            // Do not treat gradient values as a single color.
                        // Valid Enigma2 syntax can be: backgroundColor="#00101010,#00303030,vertical".
                        if (myXmlNode.Attributes[color].Value.Contains(","))
                            continue;

                        if (myXmlNode.Attributes[color].Value[0] == '#')
                            {                                                        
                                if (Properties.Settings.Default.DontReplaceColors == false)                                   
                                {
                                    String colorString = myXmlNode.Attributes[color].Value.Substring(1);
                                    try
                                    {
                                        uint colorValue = Convert.ToUInt32(colorString, 16);
                                        myXmlNode.Attributes[color].Value = get(colorValue);
                                    }
                                    catch (Exception)
                                    {
                                        // If a color begun with '#' but was not able to convert then new colorname is without '#' to 
                                        // prevent an exception when trying to displaying the screen
                                        myXmlNode.Attributes[color].Value = colorString;

                                    }
                                }
                            }
                        }
                    }
                }
            }

            public void rename(cXMLHandler XmlHandler, String name, String to)
            {
                sColor color = (sColor)pColors[name];
                remove(color);
                color.pName = to;
                add(color);

                XmlNode Node = XmlHandler.XmlGetRootNodeElement(null);
                renameColor(Node.ChildNodes, name, to);
            }

            public void removeColor(cXMLHandler XmlHandler, String name, String to)
            {
                sColor color = (sColor)pColors[name];
                remove(color);
            }

            private void renameColor(XmlNodeList nodes, String name, String to)
            {
                foreach (XmlNode myXmlNode in nodes)
                {
                    if (myXmlNode.Attributes == null)
                        continue;

                    if (myXmlNode.HasChildNodes)
                        renameColor(myXmlNode.ChildNodes, name, to);

                    foreach (String attr in color_attrs)
                    {
                        if (myXmlNode.Attributes[attr] != null)
                        {
                            if (myXmlNode.Attributes[attr].Value == name)
                            {
                                myXmlNode.Attributes[attr].Value = to;
                            }
                        }
                    }
                }
            }

            public override Object[] getArray()
            {
                ArrayList colors = new ArrayList();
                colors.AddRange(pColors.Values);
                colors.Sort();
                Object[] Aobjects = (Object[])colors.ToArray();
                sColor[] Acolors = new sColor[Aobjects.Length];
                Aobjects.CopyTo(Acolors, 0);
                return (Object[])Acolors;
            }

            public override Object get(String name)
            {
                if (String.IsNullOrEmpty(name) || name == "(none)")
                    return null;

                // A comma-separated value is a gradient, not a color name/hex literal.
                // Let sGradient parse values like: #00101010,#00303030,vertical
                if (name.Contains(","))
                    return null;

                if (name[0] == '#')
                {                                       
                    // if (MyGlobaleVariables.AddUndefinedColor == "#")
                    if (Properties.Settings.Default.addUndefinedColor == true) // Add undifined color with '#'
                    {
                        try
                        {
                            uint colorValue = Convert.ToUInt32(name.Substring(1), 16);
                            add(new sColor(name, colorValue));
                            return new sColor(name, colorValue);
                        }
                        catch (Exception)
                        {
                            MessageBox.Show(fMain.GetTranslation("An invalid colour was specified!"));
                        }
                        
                    }
                    else // Add undifined color with 'un'
                    {
                        try
                        {
                            uint colorValue = Convert.ToUInt32(name.Substring(1), 16);
                            add(new sColor(name.Replace("#", "un"), colorValue));
                            return new sColor(name.Replace("#", "un"), colorValue);
                        }
                        catch (Exception)
                        {
                            MessageBox.Show(fMain.GetTranslation("An invalid colour was specified!"));
                        }
                        
                    } 
                      // add(new sColor(name, Convert.ToUInt32(name.Substring(1), 16))); //MOD
                      // return new sColor("undefined", Convert.ToUInt32(name.Substring(1), 16)); // Original
                      // return new sColor(name, Convert.ToUInt32(name.Substring(1), 16)); //MOD                    
                }
                Object col = (Object)pColors[name];
                if (col == null)
                {
                    col = new sColor(name, 0xFF00FF);
                    add(col);
                }
                return col;
            }

            public String get(UInt32 value)
            {
                foreach (sColor color in pColors.Values)
                {
                    if (color.pValue == value)
                        return color.pName;
                }
                //   return add(new sColor("un" + Convert.ToString(value, 16), value)); // Original
                if (Properties.Settings.Default.addUndefinedColor == false)
                    return add(new sColor("un" + Convert.ToString(value, 16), value));
                else
                    return add(new sColor("#" + Convert.ToString(value, 16), value));
            }

            public override String add(Object element)
            {
                sColor color = (sColor)element;
                pColors[color.pName] = color;
                return color.pName;
            }

            public override bool remove(Object element)
            {
                sColor color = (sColor)element;
                foreach (sColor tmpcolor in pColors.Values)
                {
                    if (tmpcolor.pName == color.pName)
                    {
                        pColors.Remove(tmpcolor.pName);
                        return true;
                    }
                }
                return false;
            }

            public override bool sync(cXMLHandler XmlHandler)
            {
                string[] path = { /*"skin", */"colors" };
                XmlNode colorNode = XmlHandler.XmlGetRootNodeElement(path);

                colorNode.RemoveAll();

                //Sort Names: an hashtable is not sortable, so convert it to an arraylist

                ArrayList sorter = new ArrayList();
                sorter.AddRange(pColors.Values);
                sorter.Sort();

                foreach (sColor color in sorter)
                {
                    if (color.isNamedColor)
                    {
                        String[] attributes = { "color",
                                            "name",  color.pName,
                                            "value", color.pValueName };
                        XmlHandler.XmlAppendNode(colorNode, attributes);
                    }
                    else
                    {
                        String value = Convert.ToString(color.pValue, 16);
                        while (value.Length < 8)
                            value = "0" + value;

                        String[] attributes = { "color",
                                            "name",  color.pName,
                                            "value", "#" + value };
                        XmlHandler.XmlAppendNode(colorNode, attributes);
                    }
                }

                return true;
            }
        }

        //#################################################################

        public class cDataBaseResolution
        {
            private sResolution pResolution = null;
            private XmlNode resolutionNode = null;

            public cDataBaseResolution(cXMLHandler XmlHandler)
            {
                string[] path = { /*"skin", */"output" };
                resolutionNode = XmlHandler.XmlGetRootNodeElement(path);
                foreach (XmlNode myXmlNode in resolutionNode.ChildNodes)
                {
                    if (myXmlNode.NodeType != XmlNodeType.Element)
                        continue;

                    pResolution = new sResolution(
                        Convert.ToUInt32(myXmlNode.Attributes["xres"].Value),
                        Convert.ToUInt32(myXmlNode.Attributes["yres"].Value),
                        Convert.ToUInt32(myXmlNode.Attributes["bpp"].Value)
                    );
                }
            }

            public sResolution getResolution()
            {
                return pResolution;
            }

            public void setResolution(uint x, uint y)
            {
                pResolution.Xres = x;
                pResolution.Yres = y;

                string[] path = { /*"skin", */"output" };
                foreach (XmlNode myXmlNode in resolutionNode.ChildNodes)
                {
                    if (myXmlNode.NodeType != XmlNodeType.Element)
                        continue;

                    myXmlNode.Attributes["xres"].Value = x.ToString();
                    myXmlNode.Attributes["yres"].Value = y.ToString();
                    //myXmlNode.Attributes["bpp"].Value = bpp.ToString();
                }
            }
        }

        //#################################################################

        static private void initFonts(cXMLHandler XmlHandler)
        {
            pFonts = new Hashtable();

            string[] path = { /*"skin", */"fonts" };
            XmlNode fontNode = XmlHandler.XmlGetRootNodeElement(path);
            if (fontNode == null)
            {
                //PVMC Workaround till proper fonts for plugins is implemented in enigma2
                //We expect that the fontloader pseudo screen is the first screen in the skin
                path[0] = "screen";
                fontNode = XmlHandler.XmlGetRootNodeElement(path);
                if (fontNode.Attributes["name"].Value == "PVMC_FontLoader")
                {
                    Hashtable elements = new Hashtable();
                    foreach (XmlNode myXmlNode in fontNode.ChildNodes)
                    {
                        if (myXmlNode.NodeType != XmlNodeType.Element)
                            continue;
                        elements.Add(myXmlNode.Attributes["name"].Value, myXmlNode.Attributes["text"].Value);
                    }

                    Int32 api = Convert.ToInt32(elements["API"] != null ? elements["API"] : "1");
                    if (api >= 2)
                    {
                        Int32 count = Convert.ToInt32(elements["COUNT"] != null ? elements["COUNT"] : "0");
                        for (int i = 0; i < count; i++)
                        {
                            string[] fontString = ((string)elements["FONT" + i.ToString()]).Split('|');

                            sFont font = new sFont(
                                fontString[1],
                                fontString[0],
                                Convert.ToInt32(fontString[2]),
                                12,
                                "",
                                fontString[3] != "False"
                            );
                            pFonts.Add(font.Name, font);
                        }
                    }
                }

            }
            else
            {
                foreach (XmlNode myXmlNode in fontNode.ChildNodes)
                {
                    if (myXmlNode.NodeType != XmlNodeType.Element || myXmlNode.Name != "font" && myXmlNode.Name != "alias") //MOD
                        continue;
                    if (myXmlNode.Name == "font")//MOD
                        try //MOD
                        {
                            if (pFonts[myXmlNode.Attributes["name"].Value] == null)
                            {
                                sFont font = new sFont(
                                    myXmlNode.Attributes["name"].Value,
                                    myXmlNode.Attributes["filename"].Value,
                                    Convert.ToInt32(myXmlNode.Attributes["scale"] != null ? myXmlNode.Attributes["scale"].Value : "100"),
                                    Convert.ToInt32(myXmlNode.Attributes["size"] != null),
                                    "",
                                    Convert.ToInt32(myXmlNode.Attributes["replacement"] != null ? myXmlNode.Attributes["replacement"].Value : "0") != 0);
                                pFonts.Add(font.Name, font);
                            }
                            else
                            {
                                String errormessage = fMain.GetTranslation("More than one font defined with name") + " '" + myXmlNode.Attributes["name"].Value + "'";

                                MessageBox.Show(errormessage,
                                     fMain.GetTranslation("Error while parsing font table"),
                                     MessageBoxButtons.OK,
                                     MessageBoxIcon.Information,
                                     MessageBoxDefaultButton.Button1);
                            }


                        }
                        catch 
                        {
                            String errorMessage = "";
                            errorMessage += fMain.GetTranslation("OpenSkinDesigner has tried to open the font") + " '" + myXmlNode.Attributes["name"].Value + "'.\n\n";
                            errorMessage += fMain.GetTranslation("Unfortunatly this was not successful!") + "\n";
                            errorMessage += fMain.GetTranslation("Either the font type is not supported by OpenSkinDesigner") + " ";
                            errorMessage += fMain.GetTranslation("or it's not a valid font.") + "\n\n";
                            errorMessage += "\n";
                            errorMessage += fMain.GetTranslation("Location") + ":\n";
                            errorMessage += myXmlNode.Attributes["filename"].Value;

                            MessageBox.Show(errorMessage,
                                fMain.GetTranslation("Error while loading fonts"),
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information,
                                MessageBoxDefaultButton.Button1);

                            return;
                        }
                    else if (myXmlNode.Name == "alias")//MOD
                        try
                        {
                            if (pFonts[myXmlNode.Attributes["name"].Value] == null)
                            {
                                sFont font = new sFont(
                                    myXmlNode.Attributes["name"].Value,
                                    getFont(myXmlNode.Attributes["font"].Value).Path,
                                    100,
                                    Convert.ToInt32(myXmlNode.Attributes["size"].Value),
                                    myXmlNode.Attributes["font"].Value,
                                    false,
                                    true);

                                pFonts.Add(font.Name, font);
                            }
                            else
                            {
                                String errormessage = fMain.GetTranslation("More than one font (alias) defined with name") + " '" + myXmlNode.Attributes["name"].Value + "'";

                                MessageBox.Show(errormessage,
                                     fMain.GetTranslation("Error while parsing font table"),
                                     MessageBoxButtons.OK,
                                     MessageBoxIcon.Information,
                                     MessageBoxDefaultButton.Button1);
                            }
                        }
                        catch
                        {
                        }
                }
            }
        }

        static public sFont getFont(String name)
        {
            try
            {
                //Font Attribut: 'Font;Size' 
                int size = Convert.ToInt16(name.Substring(name.IndexOf(';') + 1));
                string fontname = name.Substring(0, name.IndexOf(';'));

                sFont font = (sFont)pFonts[fontname];
                if (font == null)
                {
                    String fontPath = cProperties.getProperty("path_fonts");
                    fontPath = fontPath + "/" + "lcd.ttf";
                    font = new sFont("Regular", fontPath, 100, 25, "Fallback", false, false);
                    font.Size = size;
                    if (MyGlobaleVariables.ShowMsgFallbackFont==true)
                    {
                        DialogResult dr = new DialogResult();
                        dr = MessageBox.Show(String.Format(fMain.GetTranslation("Font") + " '{0}' " + fMain.GetTranslation("is not defined or exist!") + "\n" + fMain.GetTranslation("Using 'fallback font'") + " 'lcd.ttf; 25'" + Environment.NewLine + Environment.NewLine
                            + fMain.GetTranslation("Show this message again?"), name), fMain.GetTranslation("Error"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dr == DialogResult.No)
                            MyGlobaleVariables.ShowMsgFallbackFont = false;


                    }
                    return font;

                }
                else
                {
                    font.Size = size;
                    return (sFont)pFonts[fontname];
                }
                
            }
            catch
            {
                sFont font = (sFont)pFonts[name];

                if (font == null)
                {
                    String fontPath = cProperties.getProperty("path_fonts");
                    fontPath = fontPath + "/" + "lcd.ttf";
                    font = new sFont("Regular", fontPath, 100, 25, "Fallback", false, false);

                    if (MyGlobaleVariables.ShowMsgFallbackFont == true)
                    {
                        DialogResult dr = new DialogResult();
                        dr = MessageBox.Show(String.Format(fMain.GetTranslation("Font") + " '{0}' " + fMain.GetTranslation("is not defined or exist!") + "\n" + fMain.GetTranslation("Using 'fallback font'") + " 'lcd.ttf; 25'" + Environment.NewLine + Environment.NewLine
                            + fMain.GetTranslation("Show this message again?"), name), fMain.GetTranslation("Error"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dr == DialogResult.No)
                            MyGlobaleVariables.ShowMsgFallbackFont = false;


                    }
                }

                //Font Alias
                return font;
            }
        }

        static public sFont[] getFonts()
        {
            sFont[] fonts = new sFont[pFonts.Count];
            pFonts.Values.CopyTo(fonts, 0);

            fonts = fonts.OrderBy(a => a.Name).ToArray();

            return fonts;
        }

        //#################################################################
        static public String getPNGPath(String path)
        {
            string PNGpath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(path),System.IO.Path.GetFileNameWithoutExtension(path));
            PNGpath += ".png";
            return PNGpath;
        }

        static public String getPath(String path)
        {
            if (path[0] == '~')
                return cProperties.getProperty("path_skin") + path.Substring(1);
            else if (path[0] == '@')
                return cProperties.getProperty("path_skins") + path.Substring(1);
            else
            {
                string fpath = cProperties.getProperty("path") + "/" + path;
                if (!File.Exists(fpath))
                    fpath = cProperties.getProperty("path") + "/" + cProperties.getProperty("path_skin") + "/" + path;
                return fpath;
            }
        }

        //#################################################################
        //#################################################################
        //#################################################################

        private static double scale_x = 1;
        private static double scale_y = 1;

        public static void rescaleLocations(cXMLHandler XmlHandler, int o_x, int o_y, int x, int y)
        {
            scale_x = (x * 1.0) / o_x;
            scale_y = (y * 1.0) / o_y;

            string[] path2 = { "skin" };
            XmlNode Node = XmlHandler.XmlGetRootNodeElement(path2);

            
            if (Node != null)
            {
                if (Node.HasChildNodes)
                    checkLocations(Node.ChildNodes);
            }
                
        }

        private static void checkLocations(XmlNodeList nodes)
        {
            foreach (XmlNode node in nodes)
            {
                if (node.NodeType != XmlNodeType.Element)
                    continue;

                if (node.HasChildNodes)
                    checkLocations(node.ChildNodes);

                if (node.Attributes != null)
                {
                    int w = 0;
                    int h = 0;
                    if (node.Attributes["size"] != null)
                    {
                        {
                            double dw, dh;
                            try
                            {
                                dw = Convert.ToInt32(node.Attributes["size"].Value.Substring(0, node.Attributes["size"].Value.IndexOf(',')));
                            }
                            catch (OverflowException)
                            {
                                dw = 0;
                            }
                            dw *= scale_x;
                            try
                            {
                                dh = Convert.ToInt32(node.Attributes["size"].Value.Substring(node.Attributes["size"].Value.IndexOf(',') + 1));
                            }
                            catch (OverflowException)
                            {
                                dh = 0;
                            }
                            dh *= scale_y;

                            w = (int)dw;
                            h = (int)dh;

                            node.Attributes["size"].Value = w.ToString() + "," + h.ToString();
                        }
                    }
                    if (node.Attributes["position"] != null)
                    {
                        {
                            double dx, dy;
                            try
                            {
                                String sRelativeX = node.Attributes["position"].Value.Substring(0, node.Attributes["position"].Value.IndexOf(',')).Trim();
                                if (sRelativeX.Equals("center"))
                                    dx = (cDataBase.pResolution.getResolution().Xres - w) >> 1 /*1/2*/;
                                else
                                    dx = Convert.ToUInt32(sRelativeX);
                            }
                            catch (OverflowException)
                            {
                                dx = 0;
                            }
                            dx *= scale_x;
                            try
                            {
                                String sRelativeY = node.Attributes["position"].Value.Substring(node.Attributes["position"].Value.IndexOf(',') + 1).Trim();
                                if (sRelativeY.Equals("center"))
                                    dy = (cDataBase.pResolution.getResolution().Yres - h) >> 1 /*1/2*/;
                                else
                                    dy = Convert.ToUInt32(sRelativeY);
                            }
                            catch (OverflowException)
                            {
                                dy = 0;
                            }
                            dy *= scale_y;

                            int x = (int)dx;
                            int y = (int)dy;

                            node.Attributes["position"].Value = x.ToString() + "," + y.ToString();
                        }
                    }

                    if (node.Attributes["font"] != null)
                    { //font="Regular;20"
                        String tmpfont = node.Attributes["font"].Value;
                        double size = Convert.ToDouble(tmpfont.Substring(tmpfont.IndexOf(';') + 1));
                        String fontname = tmpfont.Substring(0, tmpfont.IndexOf(';'));

                        size *= scale_x;

                        node.Attributes["font"].Value = fontname + "; " + Convert.ToInt32(size);
                    }
                }
            }
        }
    }
}
