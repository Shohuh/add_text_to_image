using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace AddTextToImageTgBot.Servces
{
    public class AddText : IAddText
    {
   
        // width and high are begginning point of rectangle, next is width and high of rectangle 
        void IAddText.AddText(string imageName,  int y, string newImageName, string nameOfUser,int statusColor)
        {

            StringFormat format1 = new StringFormat();
            format1.Alignment = StringAlignment.Center;


            FontFamily fontFamily = new FontFamily("Times New Roman");
            Font font = new Font(
               fontFamily,
               70,
               FontStyle.Regular,
               GraphicsUnit.Pixel);
            SolidBrush solidBrush = new SolidBrush(Color.FromArgb(255, 0, 0, 255));


            var bmp = Bitmap.FromFile($"{imageName}");
            var newImage = new Bitmap(bmp.Width, bmp.Height);

            var gr = Graphics.FromImage(newImage);
            gr.DrawImageUnscaled(bmp, 0, 0);
            if (statusColor == 1)
            {
                gr.DrawString($"{nameOfUser}", font, Brushes.White,
                    new RectangleF(0, y, bmp.Width, 80), format1); // size x and y 
            }
            if (statusColor == 2)
            {
                gr.DrawString($"{nameOfUser}", font, Brushes.Yellow,
                    new RectangleF(0, y, bmp.Width, 80), format1); // size x and y 
            }
            else
            {
                gr.DrawString($"{nameOfUser}", font, Brushes.White,
                  new RectangleF(0, y, bmp.Width, 80), format1); // size x and y 
            }
            newImage.Save($"{newImageName}");
        }

             public void DeleteImage()
        {
            throw new NotImplementedException();
        }
    }
}
