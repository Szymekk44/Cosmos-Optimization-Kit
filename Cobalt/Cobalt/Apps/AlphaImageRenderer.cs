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
	public class AlphaImageRenderer : Process
	{
		public static Bitmap CachedWindow;
		public override void Run()
		{
			Window.DrawTop(this);
			int x = WindowData.WinPos.X;
			int y = WindowData.WinPos.Y;
			int sizeX = WindowData.WinPos.Width;
			int sizeY = WindowData.WinPos.Height;
			GUI.MainCanvas.DrawFilledRectangle(GUI.colors.Main, x, y + Window.TopSize, sizeX, sizeY - Window.TopSize);
			if (CachedWindow == null)
			{
				Bitmap alphaImage = new Bitmap(Resources.ROS7Phoenix);
				GUI.MainCanvas.DrawImageAlpha(alphaImage, (sizeX - (int)alphaImage.Width) / 2 + x, y + 30);
				CachedWindow = TakeBitmap.GetImage(x, y + Window.TopSize, sizeX, sizeY - Window.TopSize);
			}
			else
				GUI.MainCanvas.DrawImage(CachedWindow, x, y + Window.TopSize);
		}
	}
}
