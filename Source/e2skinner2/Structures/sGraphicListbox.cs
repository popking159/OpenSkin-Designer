using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using OpenSkinDesigner.Logic;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace OpenSkinDesigner.Structures
{
    class sGraphicListbox : sGraphicElement
    {
        public sGraphicListbox(sAttributeListbox attr)
            : base(attr)
        {
            pAttr = attr;
        }

        private static int ClampRadius(int radius, int width, int height)
        {
            if (radius <= 0 || width <= 0 || height <= 0)
                return 0;
            int diameter = radius * 2;
            int maxDiameter = Math.Min(width, height);
            return diameter > maxDiameter ? maxDiameter : diameter;
        }

        private static GraphicsPath CreateRoundedRectanglePath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int d = ClampRadius(radius, rect.Width, rect.Height);
            if (d <= 0)
            {
                path.AddRectangle(rect);
                return path;
            }
            path.AddArc(rect.Left, rect.Top, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Top, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.Left, rect.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }

        private static Color ResolveColor(sColor color, Color fallback)
        {
            if (color == null)
                return fallback;
            return Logic.cProperties.getPropertyBool("enable_alpha") ? color.ColorAlpha : color.Color;
        }

        private static void FillRect(Graphics g, Rectangle rect, sColor color, int radius, Color fallback)
        {
            if (rect.Width <= 0 || rect.Height <= 0)
                return;
            using (SolidBrush brush = new SolidBrush(ResolveColor(color, fallback)))
            using (GraphicsPath path = CreateRoundedRectanglePath(rect, radius))
                g.FillPath(brush, path);
        }

        private static void DrawRect(Graphics g, Rectangle rect, sColor color, int radius, int borderWidth, Color fallback)
        {
            if (rect.Width <= 0 || rect.Height <= 0 || borderWidth <= 0 || color == null)
                return;
            Rectangle drawRect = rect;
            drawRect.Width = Math.Max(1, drawRect.Width - 1);
            drawRect.Height = Math.Max(1, drawRect.Height - 1);
            using (Pen pen = new Pen(ResolveColor(color, fallback), borderWidth))
            using (GraphicsPath path = CreateRoundedRectanglePath(drawRect, radius))
            {
                pen.LineJoin = LineJoin.Round;
                g.DrawPath(pen, path);
            }
        }

        private static bool IsUsableGradient(sGradient gradient)
        {
            return gradient != null && gradient.ColorStart != null && gradient.ColorEnd != null;
        }

        private static void FillGradient(Graphics g, Rectangle rect, sGradient gradient, int radius)
        {
            if (!IsUsableGradient(gradient) || rect.Width <= 0 || rect.Height <= 0)
                return;

            Color startColor = ResolveColor(gradient.ColorStart, Color.Transparent);
            Color endColor = ResolveColor(gradient.ColorEnd, startColor);
            Color middleColor = gradient.ColorMid != null ? ResolveColor(gradient.ColorMid, startColor) : startColor;

            bool horizontal = gradient.Direction == eGradientDirection.Horizontal;
            PointF start = new PointF(rect.Left, rect.Top);
            PointF end = horizontal ? new PointF(rect.Right, rect.Top) : new PointF(rect.Left, rect.Bottom);
            if (start == end)
                end = new PointF(rect.Left + 1, rect.Top);

            using (LinearGradientBrush brush = new LinearGradientBrush(start, end, startColor, endColor))
            using (GraphicsPath path = CreateRoundedRectanglePath(rect, radius))
            {
                brush.InterpolationColors = new ColorBlend
                {
                    Positions = new float[] { 0f, 0.5f, 1f },
                    Colors = new Color[] { startColor, middleColor, endColor }
                };
                g.FillPath(brush, path);
            }
        }

        private static List<string> PreviewEntries(sAttributeListbox attr)
        {
            if (attr.pPreviewEntries != null && attr.pPreviewEntries.Count > 0)
                return attr.pPreviewEntries;
            return new List<string> { "Plugin Browser", "Plugin Manager", "Skin Setup", "Network", "System", "Information", "Extensions", "Settings" };
        }

        private static Font PreviewFont(sAttributeListbox attr, float sizeOffset)
        {
            float size = attr.pFontSize > 0 ? attr.pFontSize : 24f;
            size += sizeOffset;
            if (size < 6f) size = 6f;
            string family = "Arial";
            try
            {
                if (attr.pFont != null && !String.IsNullOrEmpty(attr.pFont.Name))
                    family = attr.pFont.Name;
            }
            catch { }
            try { return new Font(family, size, GraphicsUnit.Pixel); }
            catch { return new Font("Arial", size, GraphicsUnit.Pixel); }
        }

        private void DrawItems(Graphics g, sAttributeListbox attr)
        {
            List<string> entries = PreviewEntries(attr);
            bool grid = (attr.pListOrientation != null && attr.pListOrientation.ToLower() == "grid");
            int itemHeight = attr.pItemHeight > 0 ? attr.pItemHeight : (grid ? 170 : 50);
            int itemWidth = grid ? (attr.pItemWidth > 0 ? attr.pItemWidth : Math.Max(1, itemHeight * 2)) : attr.pWidth;
            int spacingX = grid ? attr.pItemSpacingX : 0;
            int spacingY = grid ? attr.pItemSpacingY : 0;
            int selectedIndex = Math.Max(0, Math.Min(attr.pSelection, entries.Count - 1));
            int contentWidth = attr.pWidth;
            if (attr.pScrollbarMode != cProperty.eScrollbarMode.showNever)
            {
                int sw = attr.pscrollbarWidth > 0 ? attr.pscrollbarWidth : 10;
                contentWidth -= sw + Math.Max(0, attr.pscrollbarOffset);
            }
            if (!grid)
                itemWidth = Math.Max(1, contentWidth);

            int columns = 1;
            if (grid)
                columns = Math.Max(1, contentWidth / Math.Max(1, itemWidth + spacingX));

            using (Font normalFont = PreviewFont(attr, 0))
            using (Font selectedFont = PreviewFont(attr, attr.pSelectionZoom))
            {
                for (int i = 0; i < entries.Count; i++)
                {
                    int row = grid ? i / columns : i;
                    int col = grid ? i % columns : 0;
                    int x = attr.pAbsolutX + col * (itemWidth + spacingX);
                    int y = attr.pAbsolutY + row * (itemHeight + spacingY);
                    if (y >= attr.pAbsolutY + attr.pHeight)
                        break;
                    Rectangle itemRect = new Rectangle(x, y, Math.Min(itemWidth, attr.pAbsolutX + contentWidth - x), itemHeight);
                    if (itemRect.Width <= 0 || itemRect.Height <= 0)
                        continue;

                    bool selected = i == selectedIndex;
                    int radius = selected ? (attr.pItemCornerRadiusSelected > 0 ? attr.pItemCornerRadiusSelected : attr.pItemCornerRadius) : attr.pItemCornerRadius;
                    sGradient gradient = selected ? attr.pItemGradientSelected : attr.pItemGradient;
                    sColor background = selected ? attr.pListboxSelectedBackgroundColor : attr.pListboxBackgroundColor;
                    sColor foreground = selected ? attr.pListboxSelectedForegroundColor : attr.pListboxForegroundColor;

                    if (selected && attr.pSelectionPixmapName != null)
                        new sGraphicImage(null, attr.pSelectionPixmapName, itemRect.X, itemRect.Y, itemRect.Width, itemRect.Height).paint(null, new PaintEventArgs(g, itemRect));
                    else if (IsUsableGradient(gradient))
                        FillGradient(g, itemRect, gradient, radius);
                    else if (!attr.pTransparent || selected)
                        FillRect(g, itemRect, background, radius, selected ? Color.DimGray : Color.Transparent);

                    Rectangle textRect = itemRect;
                    textRect.Inflate(-8, -4);
                    using (SolidBrush brush = new SolidBrush(ResolveColor(foreground, selected ? Color.White : Color.Gainsboro)))
                    using (StringFormat format = new StringFormat())
                    {
                        format.Alignment = grid ? StringAlignment.Center : StringAlignment.Near;
                        format.LineAlignment = StringAlignment.Center;
                        format.Trimming = StringTrimming.EllipsisCharacter;
                        format.FormatFlags = 0;
                        g.DrawString(entries[i], selected ? selectedFont : normalFont, brush, textRect, format);
                    }
                }
            }
        }

        private void DrawScrollbar(Graphics g, sAttributeListbox attr)
        {
            if (attr.pScrollbarMode == cProperty.eScrollbarMode.showNever)
                return;
            int scrollbarWidth = attr.pscrollbarWidth > 0 ? attr.pscrollbarWidth : 10;
            int scrollbarOffset = attr.pscrollbarOffset >= 0 ? attr.pscrollbarOffset : 0;
            int radius = attr.pscrollbarRadius > 0 ? attr.pscrollbarRadius : 0;
            int trackX = attr.pAbsolutX + attr.pWidth - scrollbarWidth - scrollbarOffset;
            if (trackX < attr.pAbsolutX)
                trackX = attr.pAbsolutX + Math.Max(0, attr.pWidth - scrollbarWidth);
            Rectangle trackRect = new Rectangle(trackX, attr.pAbsolutY, scrollbarWidth, attr.pHeight);
            if (trackRect.Width <= 0 || trackRect.Height <= 0)
                return;

            sColor trackColor = attr.pscrollbarSliderBackgroundColor ?? attr.pListboxBackgroundColor;
            if (!attr.pTransparent)
                FillRect(g, trackRect, trackColor, radius, Color.FromArgb(80, Color.Gray));

            int thumbHeight = Math.Max(1, (trackRect.Height * 3) / 4);
            Rectangle thumbRect = new Rectangle(trackRect.X, trackRect.Y, trackRect.Width, thumbHeight);
            if (IsUsableGradient(attr.pScrollbarForegroundGradient))
                FillGradient(g, thumbRect, attr.pScrollbarForegroundGradient, radius);
            else
                FillRect(g, thumbRect, attr.pscrollbarSliderForegroundColor ?? attr.pListboxForegroundColor, radius, Color.LightGray);
            DrawRect(g, thumbRect, attr.pscrollbarSliderBorderColor, radius, attr.pscrollbarSliderBorderWidth, Color.Black);
        }

        public override void paint(object sender, PaintEventArgs e)
        {
            sAttributeListbox attr = (sAttributeListbox)pAttr;
            Graphics g = e.Graphics;
            SmoothingMode oldSmoothing = g.SmoothingMode;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            if (!attr.pTransparent)
            {
                Rectangle bg = new Rectangle(attr.pAbsolutX, attr.pAbsolutY, attr.pWidth, attr.pHeight);
                if (attr.pBackgroundPixmap != null)
                    new sGraphicImage(attr, attr.pBackgroundPixmapName).paint(sender, e);
                else
                    FillRect(g, bg, attr.pListboxBackgroundColor, (int)attr.pCornerRadius, Color.Transparent);
            }

            DrawItems(g, attr);
            DrawScrollbar(g, attr);
            g.SmoothingMode = oldSmoothing;
        }
    }
}
