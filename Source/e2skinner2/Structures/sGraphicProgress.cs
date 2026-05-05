using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using OpenSkinDesigner.Logic;
using System.Drawing;
using System.Diagnostics;

namespace OpenSkinDesigner.Structures
{
    class sGraphicProgress : sGraphicElement
    {
        //protected sAttributeProgress pAttr;
        

        public sGraphicProgress(sAttributeProgress attr)
            : base(attr)
        {
            pAttr = attr;
        }

        // ########################################
        public int Width_org { get; private set; }
        // ########################################
        public override void paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {

            // Hole den Namen der aufrufenden Methode
            string callerName = new StackTrace().GetFrame(1).GetMethod().Name;
            // Log-Nachricht erstellen
            string logMessage = $"============= sGraphicProgress - paint () wurde von {callerName} aufgerufen .";
            // Loggen
            Logger.LogMessage(logMessage);
            // Weiter mit der eigentlichen Funktion



            if (!cProperties.getPropertyBool("skinned"))
            {
                Logger.LogMessage("============= sGraphicProgress - ruft cGraphicRectangel.cs auf 37  = 4 ");
                new sGraphicRectangel(pAttr, false, (float)1.0, new sColor(Color.Green))
                    .withCornerRadius(pAttr.pCornerRadiusRaw)
                    .paint(sender, e);
            }
            else
            {
                // ------------------------------- erst malen wir den Background --------------------------------
                if (pAttr.pTransparent)
                {
                    Logger.LogMessage("============= sGraphicProgress - Transparent true - es wird nichts gemalt ");
                    //new sGraphicRectangel().paint(sender, e);
                }
                else
                {
                    Logger.LogMessage("============= sGraphicProgress - Transparent false - ruft cGraphicRectangel.cs auf 49  = 4 ");
                    new sGraphicRectangel(pAttr, true, 1.0F, ((sAttributeProgress)pAttr).pBackgroundColor)
                        .withCornerRadius(pAttr.pCornerRadiusRaw)
                        .paint(sender, e);

                }
                // ------------------- erst pixmap - dann Gradient - sonst Foreground -----------------------------
                if (((sAttributeProgress)pAttr).ppixmap != null)

                {
                    // ------------------- Pixmap malen -------------------------
                    Width_org = pAttr.pWidth;
                    pAttr.pWidth = pAttr.pWidth / 4 * 3;
                    Logger.LogMessage("============= sGraphicProgress - PixmapName - ruft cGraphicImage.cs auf 66  = 7 ");
                    new sGraphicImage(null, ((sAttributeProgress)pAttr).ppixmap, pAttr.pAbsolutX, pAttr.pAbsolutY, pAttr.pWidth, pAttr.pHeight)
                         

                        .paint(sender, e);
                    pAttr.pWidth = Width_org;
                }
               
                else
                {
                    // Draw the progress foreground as 75% width for preview.
                    // pForegroundGradient is created for both a plain foregroundColor and
                    // a gradient such as: green,yellow,red,horizontal.
                    if (((sAttributeProgress)pAttr).pForegroundGradient != null)
                    {
                        int originalWidth = pAttr.pWidth;
                        try
                        {
                            pAttr.pWidth = pAttr.pWidth / 4 * 3;
                            Logger.LogMessage("============= sGraphicProgress - draw foreground color/gradient");
                            new sGraphicRectangel(pAttr, ((sAttributeProgress)pAttr).pForegroundGradient)
                                .withCornerRadius(pAttr.pCornerRadiusRaw)
                                .paint(sender, e);
                            Logger.LogMessage("============= cGraphicProgress.cs - ForegroundGradient ist vorhanden");
                        }
                        finally
                        {
                            pAttr.pWidth = originalWidth;
                        }
                    }
                }

         

            if (pAttr.pBorder)
            {
                Logger.LogMessage("============= sGraphicProgress - Border - ruft cGraphicRectangel.cs auf 95  = 4 ");
                new sGraphicRectangel(pAttr, false, (float)pAttr.pBorderWidth, pAttr.pBorderColor)
                    .withCornerRadius(pAttr.pCornerRadiusRaw)
                    .paint(sender, e);
            }

            }
        }
    }
}
