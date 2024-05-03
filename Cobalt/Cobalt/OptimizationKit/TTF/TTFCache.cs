using Cobalt.GetIMG;
using Cobalt.Graphics;
using Cosmos.System.Graphics;
using CosmosTTF;

namespace Cobalt.TTF
{
	/// <summary>
	/// Here we can cache TTF fonts to be rendered as DrawImage, not DrawImageAlpha.
	/// Text must have a static background!!
	/// </summary>
	public static class TTFCache
	{
		public static CharData[] KMB18Def = new CharData[255];
		public static CharData[] KMB18Dark = new CharData[255];
		public static CharData[] KMB18VVDark = new CharData[255];
		public static void CacheKMB18Default()
		{
			for (int i = 32; i < 127; i++) //Only normal characters
			{
				GUI.MainCanvas.DrawFilledRectangle(GUI.colors.Main, 0, 0, 14, 28); //Background = GUI.colors.Main
				char character = (char)i;
				GlyphResult g = TTFManager.RenderGlyphAsBitmap("KMB", character, GUI.colors.Text, 18);
				KMB18Def[i] = new CharData();
				KMB18Def[i].offY = g.offY;
				KMB18Def[i].offX = g.offX;
				GUI.MainCanvas.DrawImageAlpha(g.bmp, 0, 18 + (int)g.offY);
				KMB18Def[i].bitmap = TakeBitmap.GetImage(0, 4, 9, 18);
			}
			for (int i = 0; i < 32; i++) //Rest = '?'
			{
				KMB18Def[i] = KMB18Def[63];
			}
			for (int i = 128; i < 255; i++)
			{
				KMB18Def[i] = KMB18Def[63];
			}
		}
		public static void CacheKMB18Dark()
		{
			for (int i = 32; i < 127; i++) //Only normal characters
			{
				GUI.MainCanvas.DrawFilledRectangle(GUI.colors.Dark, 0, 0, 14, 28); // Background = GUI.colors.Dark
				char character = (char)i;
				GlyphResult g = TTFManager.RenderGlyphAsBitmap("KMB", character, GUI.colors.Text, 18);
				KMB18Dark[i] = new CharData();
				KMB18Dark[i].offY = g.offY;
				KMB18Dark[i].offX = g.offX;
				GUI.MainCanvas.DrawImageAlpha(g.bmp, 0, 18 + (int)g.offY);
				KMB18Dark[i].bitmap = TakeBitmap.GetImage(0, 4, 9, 18);
			}
			for (int i = 0; i < 32; i++) //Rest = '?'
			{
				KMB18Dark[i] = KMB18Dark[63];
			}
			for (int i = 128; i < 255; i++)
			{
				KMB18Dark[i] = KMB18Dark[63];
			}
		}

		public static void CacheKMB18VVDark()
		{
			for (int i = 32; i < 127; i++) //Only normal characters
			{
				GUI.MainCanvas.DrawFilledRectangle(GUI.colors.ColorVeryVeryDark, 0, 0, 14, 28); // Background = GUI.colors.Dark
				char character = (char)i;
				GlyphResult g = TTFManager.RenderGlyphAsBitmap("KMB", character, GUI.colors.Text, 18);
				KMB18VVDark[i] = new CharData();
				KMB18VVDark[i].offY = g.offY;
				KMB18VVDark[i].offX = g.offX;
				GUI.MainCanvas.DrawImageAlpha(g.bmp, 0, 18 + (int)g.offY);
				KMB18VVDark[i].bitmap = TakeBitmap.GetImage(0, 4, 9, 18);
			}
			for (int i = 0; i < 32; i++) //Rest = '?'
			{
				KMB18VVDark[i] = KMB18VVDark[63];
			}
			for (int i = 128; i < 255; i++)
			{
				KMB18VVDark[i] = KMB18VVDark[63];
			}
		}

		public static void CacheAllFonts()
		{
			CacheKMB18Default();
			CacheKMB18Dark();
			CacheKMB18VVDark();
		}

	}
	public class CharData
	{
		public Bitmap bitmap;
		public int offX;
		public int offY;
	}
}
