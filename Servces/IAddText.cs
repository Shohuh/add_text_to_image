using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace AddTextToImageTgBot.Servces
{
    interface IAddText
    {
        public void AddText(string imageName, int y, string newImageName, string nameOfUser, int statusColor);

        public void DeleteImage();
    }
}
