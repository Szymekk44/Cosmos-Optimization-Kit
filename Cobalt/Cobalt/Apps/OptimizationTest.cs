using Cobalt.Apps;
using Cobalt.GetIMG;
using Cobalt.Graphics;
using Cobalt.System;
using Cosmos.System;
using Cosmos.System.Graphics;
using CosmosTTF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cobalt.Cobalt.Apps
{
	public class OptimizationTest : Process
	{
		public Bitmap CachedWindow;
		public static bool Optimized = true;
		public override void Run()
		{
			Window.DrawTop(this);
			int x = WindowData.WinPos.X;
			int y = WindowData.WinPos.Y;
			int sizeX = WindowData.WinPos.Width;
			int sizeY = WindowData.WinPos.Height;
			if (CachedWindow == null)
			{
				if (Optimized)
				{
					GUI.MainCanvas.DrawFilledRectangle(GUI.colors.Main, x, y + Window.TopSize, sizeX, sizeY - Window.TopSize);
					GUI.MainCanvas.DrawCenteredTTFString("Optimization: ON", sizeX, x, y + 30, 20, GUI.colors.Text, "KMB", 18);
					CachedWindow = TakeBitmap.GetImage(x, y + Window.TopSize, sizeX, sizeY - Window.TopSize);
				}
				else
				{
					GUI.MainCanvas.DrawFilledRectangle(GUI.colors.ColorVeryLightRed, x, y + Window.TopSize, sizeX, sizeY - Window.TopSize);
					GUI.MainCanvas.DrawCenteredTTFString("Optimization: OFF", sizeX, x, y + 30, 20, GUI.colors.Text, "KMB", 18);
					CachedWindow = TakeBitmap.GetImage(x, y + Window.TopSize, sizeX, sizeY - Window.TopSize);
				}
			}
			else
				GUI.MainCanvas.DrawImage(CachedWindow, x, y + Window.TopSize);

			if(GUI.MX > x && GUI.MX < x + sizeX && GUI.MY > y + 25 && GUI.MY < y + sizeY && MouseManager.MouseState == MouseState.Left && !GUI.Clicked)
			{
				Optimized = !Optimized;
			}
			if(!Optimized)
			{
				CachedWindow = null;
				AlphaImageRenderer.CachedWindow = null;
				ResizedImageRenderer.CachedImage = null;
				TTFBox.CachedWindow = null;
				foreach(var item in ProcessManager.ProcessList)
				{
					item.WindowData.CachedTop = null;
				}
			}
		}
	}
}
