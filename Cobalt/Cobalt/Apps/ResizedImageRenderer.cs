using Cobalt.GetIMG;
using Cobalt.Graphics;
using Cobalt.System;
using Cosmos.System.Graphics;
using CosmosTTF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cobalt.Cobalt.Apps
{
	public class ResizedImageRenderer : Process
	{
		public static Bitmap CachedImage;
		public override void Run()
		{
			Window.DrawTop(this);
			int x = WindowData.WinPos.X;
			int y = WindowData.WinPos.Y;
			int sizeX = WindowData.WinPos.Width;
			int sizeY = WindowData.WinPos.Height;
			GUI.MainCanvas.DrawFilledRectangle(GUI.colors.Main, x, y + Window.TopSize, sizeX, sizeY - Window.TopSize);
			if (CachedImage == null)
			{
				GUI.MainCanvas.DrawImage(GUI.Wallpaper, x + 15, y + Window.TopSize + 15, (int)GUI.Wallpaper.Width / 6, (int)GUI.Wallpaper.Height / 6);
				CachedImage = TakeBitmap.GetImage(x + 15, y + Window.TopSize + 15, (int)GUI.Wallpaper.Width / 6, (int)GUI.Wallpaper.Height / 6);
			}
			else
				GUI.MainCanvas.DrawImage(CachedImage, x + 15, y + Window.TopSize + 15);
		}
	}
}
