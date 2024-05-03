using Cobalt.GetIMG;
using Cobalt.Graphics;
using Cobalt.System;
using Cosmos.System.Graphics;
using CosmosTTF;

namespace Cobalt.Apps
{
	public class TTFBox : Process // Here you can see a STATIC text drawing example
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
				GUI.MainCanvas.DrawCenteredCachedTTFString("Welcome to Cosmos Optimization Kit!\nCobaltOS is a sample system that will help you add all the things to your system!\nCheck out how fast its running while rendering TTF Text and alpha images!\nhttps://github.com/SzymekkYT/Cosmos-Optimization-Kit\n\nCreated by Szymekk", sizeX, x, y + 30, 20, TTFManager.CachedFont.KMB18);
				CachedWindow = TakeBitmap.GetImage(x, y + Window.TopSize, sizeX, sizeY - Window.TopSize);
			}
			else
				GUI.MainCanvas.DrawImage(CachedWindow, x, y + Window.TopSize);
		}
	}
}
