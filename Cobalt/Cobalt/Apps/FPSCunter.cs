using Cobalt.Graphics;
using Cobalt.System;
using CosmosTTF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cobalt.Cobalt.Apps
{
	public class FPSCunter : Process // Here you can see a changing text drawing (FPS counter) example
	{
		public override void Run()
		{
			Window.DrawTop(this);
			int x = WindowData.WinPos.X;
			int y = WindowData.WinPos.Y;
			int sizeX = WindowData.WinPos.Width;
			int sizeY = WindowData.WinPos.Height;
			GUI.MainCanvas.DrawFilledRectangle(GUI.colors.Main, x, y + Window.TopSize, sizeX, sizeY - Window.TopSize);
			GUI.MainCanvas.DrawCenteredCachedTTFString("FPS: " + CobaltCore._fps, sizeX, x, y + 30, 20, TTFManager.CachedFont.KMB18);
		}
	}
}
