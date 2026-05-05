using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Drawing;
using System.Diagnostics;
using System.Xml;
using System.Drawing.Drawing2D;

namespace OpenSkinDesigner.Structures
{
    class sGraphicRectangel : sGraphicElement
    {
        protected bool pFilled;
        protected float pLineWidth;
        protected sColor pColor;
        protected sGradient pGradient;
        private float pCornerRadius;
        private bool pCornerTopLeft = true;
        private bool pCornerTopRight = true;
        private bool pCornerBottomRight = true;
        private bool pCornerBottomLeft = true;

        public sGraphicRectangel(sAttribute attr, bool filled, float linewidth, sColor color)
            : base(attr)
        {
            // Hole den Namen der aufrufenden Methode
            string callerName = new StackTrace().GetFrame(1).GetMethod().Name;
            // Log-Nachricht erstellen
            string logMessage = $"============= sGraphicRectangels - erster sollten 4 stueck sein -   () wurde von {callerName} aufgerufen .";
            // Loggen
            Logger.LogMessage(logMessage);
            // Weiter mit der eigentlichen Funktion

            pFilled = filled;
            pLineWidth = linewidth;
            pColor = color;
        }

        public sGraphicRectangel(Int32 x, Int32 y, Int32 width, Int32 height, bool filled, float linewidth, sColor color)
            : base(x, y, width, height)
        {
            //Console.WriteLine("sGraphicRectangel: " + x + ":" + y + " " + width + "x" + height);
            // Hole den Namen der aufrufenden Methode
            string callerName = new StackTrace().GetFrame(1).GetMethod().Name;
            // Log-Nachricht erstellen
            string logMessage = $"============= sGraphicRectangels  -zweiter sollten 7 stueck sein -  () wurde von {callerName} aufgerufen .";
            // Loggen
            Logger.LogMessage(logMessage);
            // Weiter mit der eigentlichen Funktion


            pFilled = filled;
            pLineWidth = linewidth;
            pColor = color;

            pZPosition = 1000;
        }

        public sGraphicRectangel(sAttribute attr, sGradient gradient)
            : base(attr)
        {
            pGradient = gradient;
        }

        public sGraphicRectangel(Int32 x, Int32 y, Int32 width, Int32 height, sGradient gradient)
            : base(x, y, width, height)
        {
            //Console.WriteLine("sGraphicRectangel: " + x + ":" + y + " " + width + "x" + height);
            // Hole den Namen der aufrufenden Methode
            string callerName = new StackTrace().GetFrame(1).GetMethod().Name;
            // Log-Nachricht erstellen
            string logMessage = $"============= sGraphicRectangels  -dritte sollten 5 stueck sein (Gradient) () wurde von {callerName} aufgerufen .";
            // Loggen
            Logger.LogMessage(logMessage);
            // Weiter mit der eigentlichen Funktion
            
            //this.cornerRadius = cornerRadius;
            pGradient = gradient;

            pZPosition = 1000;
        }
        // ##################################
        public sGraphicRectangel withCornerRadius(float cornerRadius)
        {
            pCornerRadius = cornerRadius * 2;
            SetCornerMask("all");
            Logger.LogMessage("============= sGraphicRectangels - cornerRadius uebergeben ist doppelt so groß: () " + cornerRadius);
            return this;
        }

        public sGraphicRectangel withCornerRadius(String cornerRadiusRaw)
        {
            float radius = 0;
            String mask = "all";
            if (!String.IsNullOrEmpty(cornerRadiusRaw))
            {
                String[] parts = cornerRadiusRaw.Split(new char[] { ';' }, 2);
                float.TryParse(parts[0].Trim(), System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out radius);
                if (parts.Length > 1)
                    mask = parts[1].Trim();
            }
            pCornerRadius = radius * 2;
            SetCornerMask(mask);
            Logger.LogMessage("============= sGraphicRectangels - cornerRadius raw: " + cornerRadiusRaw);
            return this;
        }

        private void SetCornerMask(String mask)
        {
            pCornerTopLeft = true;
            pCornerTopRight = true;
            pCornerBottomRight = true;
            pCornerBottomLeft = true;

            if (String.IsNullOrEmpty(mask) || mask == "all")
                return;

            pCornerTopLeft = false;
            pCornerTopRight = false;
            pCornerBottomRight = false;
            pCornerBottomLeft = false;

            String[] parts = mask.Split(new char[] { ',' });
            foreach (String raw in parts)
            {
                String part = raw.Trim();
                if (part == "top") { pCornerTopLeft = true; pCornerTopRight = true; }
                else if (part == "bottom") { pCornerBottomLeft = true; pCornerBottomRight = true; }
                else if (part == "left") { pCornerTopLeft = true; pCornerBottomLeft = true; }
                else if (part == "right") { pCornerTopRight = true; pCornerBottomRight = true; }
                else if (part == "topLeft") pCornerTopLeft = true;
                else if (part == "topRight") pCornerTopRight = true;
                else if (part == "bottomRight") pCornerBottomRight = true;
                else if (part == "bottomLeft") pCornerBottomLeft = true;
            }
        }

        private GraphicsPath CreateRoundedPath(float x, float y, float width, float height)
        {
            GraphicsPath path = new GraphicsPath();
            float d = pCornerRadius;
            if (d <= 0 || width <= 0 || height <= 0)
            {
                path.AddRectangle(new RectangleF(x, y, width, height));
                return path;
            }

            if (d > width) d = width;
            if (d > height) d = height;

            if (pCornerTopLeft) path.AddArc(x, y, d, d, 180, 90); else path.AddLine(x, y, x, y);
            if (pCornerTopRight) path.AddArc(x + width - d, y, d, d, 270, 90); else path.AddLine(x + width, y, x + width, y);
            if (pCornerBottomRight) path.AddArc(x + width - d, y + height - d, d, d, 0, 90); else path.AddLine(x + width, y + height, x + width, y + height);
            if (pCornerBottomLeft) path.AddArc(x, y + height - d, d, d, 90, 90); else path.AddLine(x, y + height, x, y + height);
            path.CloseFigure();
            return path;
        }

        public override void paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            // Hole den Namen der aufrufenden Methode
            string callerName = new StackTrace().GetFrame(1).GetMethod().Name;
            // Log-Nachricht erstellen
            string logMessage = $"============= sGraphicRectangels - malen () wurde von {callerName} aufgerufen .";
            Logger.LogMessage(logMessage);


            Logger.LogMessage("============= cGraphicRectangel.cs - ist was in Gradient drin = " + pGradient);
            Logger.LogMessage("============= cGraphicRectangel.cs - cornerRadius =  " + pCornerRadius);

            if (pGradient != null)  // ------------------------ backgroundGradient malen 
            {
                Logger.LogMessage("============= cGraphicRectangel.cs - BackgroundGradient malen ");

                // Rechteck-Koordinaten
                float x = pX;
                float y = pY;
                float width = pWidth;
                float height = pHeight;

                // Gradient-Richtung festlegen (horizontal oder vertikal)
                bool gradient_direction = pGradient.Direction == eGradientDirection.Horizontal;

                // Linearen Farbverlauf erstellen
                LinearGradientBrush brush = new LinearGradientBrush(
                    gradient_direction ? new PointF(x, y) : new PointF(x, y),
                    gradient_direction ? new PointF(x + width, y) : new PointF(x, y + height),
                    pGradient.ColorStart.Color,
                    pGradient.ColorEnd.Color);

                // Farbverlauf definieren
                brush.InterpolationColors = new ColorBlend
                {
                    Positions = new float[] { 0f, 0.5f, 1f },
                    Colors = new Color[] { pGradient.ColorStart.Color, pGradient.ColorMid.Color, pGradient.ColorEnd.Color }
                };
                
                // GraphicsPath für das Rechteck mit abgerundeten Ecken erstellen
                using (GraphicsPath path = CreateRoundedPath(x, y, width, height))
                {
                    // Pfad mit dem Farbverlauf füllen
                    g.FillPath(brush, path);
                }
                brush.Dispose();

            }
            // #########################################################################################################
            else
            {
                // ------------------------------------  nur backgroundColor malen
                Logger.LogMessage("============= cGraphicRectangel.cs - BackgroundColor malen ");
                Logger.LogMessage("============= cGraphicRectangel.cs - cornerRadius =  " + pCornerRadius);



                Color penColor = pColor.Color;
                if (Logic.cProperties.getPropertyBool("enable_alpha"))
                    penColor = pColor.ColorAlpha;

                Logger.LogMessage("penColor: " + penColor);
                Logger.LogMessage("pX      : " + pX);
                Logger.LogMessage("pY      : " + pY);
                Logger.LogMessage("pWidght : " + pWidth);
                Logger.LogMessage("pHeight : " + pHeight);
    

                using (GraphicsPath path = CreateRoundedPath(pX, pY, pWidth, pHeight))
                {
                    if (pFilled)
                    {
                        Logger.LogMessage("============= cGraphicRectangel.cs - BackgroundColor malen Filled ");
                        using (SolidBrush brush = new SolidBrush(penColor))
                        {
                            g.FillPath(brush, path);
                        }
                    }
                    else
                    {
                        Logger.LogMessage("============= cGraphicRectangel.cs - BackgroundColor malen DrawPath");
                        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                        using (Pen outlinePen = new Pen(penColor, pLineWidth))
                        {
                            outlinePen.LineJoin = System.Drawing.Drawing2D.LineJoin.Round;
                            g.DrawPath(outlinePen, path);
                        }
                    }
                }
            }
        }
    }
}
