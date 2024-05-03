using Cobalt.GetIMG;
using Cobalt.Graphics;
using CosmosTTF;

namespace Cobalt.System
{
	public static class Window
	{
		public static int TopSize = 25;
		public static void DrawTop(Process proc)
		{
			if(GUI.currentProcess != proc) //If not clicked
			{
				CustomDrawing.DrawTopRoundedRectangle(proc.WindowData.WinPos.X, proc.WindowData.WinPos.Y, proc.WindowData.WinPos.Width, TopSize, TopSize / 2, GUI.colors.Dark);
				if (proc.WindowData.CachedTop == null) //Lets cache our *already cached* ttf for even better performance!
				{
					GUI.MainCanvas.DrawStringTTFCached(proc.Name, TTFManager.CachedFont.KMB18Dark, proc.WindowData.WinPos.X + 12, proc.WindowData.WinPos.Y + 6);
					proc.WindowData.CachedTop = TakeBitmap.GetImage(proc.WindowData.WinPos.X + 12, proc.WindowData.WinPos.Y + 6, TTFManager.GetTTFWidth(proc.Name, "KMB", 18), 18);
				}
				else
					GUI.MainCanvas.DrawImage(proc.WindowData.CachedTop, proc.WindowData.WinPos.X + 12, proc.WindowData.WinPos.Y + 6);
			}
			else
			{
				CustomDrawing.DrawTopRoundedRectangle(proc.WindowData.WinPos.X, proc.WindowData.WinPos.Y, proc.WindowData.WinPos.Width, TopSize, TopSize / 2, GUI.colors.ColorVeryVeryDark);
				if (proc.WindowData.CachedTopDark == null) //Lets cache our *already cached* ttf for even better performance!
				{
					GUI.MainCanvas.DrawStringTTFCached(proc.Name, TTFManager.CachedFont.KMB18VeryVeryDark, proc.WindowData.WinPos.X + 12, proc.WindowData.WinPos.Y + 6);
					proc.WindowData.CachedTopDark = TakeBitmap.GetImage(proc.WindowData.WinPos.X + 12, proc.WindowData.WinPos.Y + 6, TTFManager.GetTTFWidth(proc.Name, "KMB", 18), 18);
				}
				else
					GUI.MainCanvas.DrawImage(proc.WindowData.CachedTopDark, proc.WindowData.WinPos.X + 12, proc.WindowData.WinPos.Y + 6);
			}
		}
	}
}
