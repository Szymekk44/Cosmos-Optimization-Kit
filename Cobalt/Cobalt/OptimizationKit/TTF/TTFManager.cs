using Cosmos.System.Graphics;
using LunarLabs.Fonts;
using System.Collections.Generic;
using System;
using System.Drawing;
using Cosmos.System;
using System.Linq;
using Cobalt.TTF;
using Cobalt.Graphics;

namespace CosmosTTF
{
	public static class TTFManager
	{
		private static CustomDictString<Font> fonts = new();
		private static CustomDictString<GlyphResult> glyphCache = new();
		private static List<string> glyphCacheKeys = new();

		public static int GlyphCacheSize { get; set; } = 512;
		private static Canvas prevCanv;

		public static void RegisterFont(string name, byte[] byteArray)
		{
			fonts.Add(name, new Font(byteArray));
		}

		/// <summary>
		/// Render a glyph
		/// </summary>
		/// <param name="font">Registered font name</param>
		/// <param name="glyph">The character to render</param>
		/// <param name="color">The color to make the text (ignores alpha)</param>
		/// <param name="x">The scale in pixels</param>
		/// <returns></returns>
		/// <exception cref="Exception"></exception>
		public static GlyphResult RenderGlyphAsBitmap(string font, char glyph, Color color, float scalePx = 16)
		{
			var rgbOffset = ((color.R & 0xFF) << 16) + ((color.G & 0xFF) << 8) + color.B;

			if (!fonts.TryGet(font, out Font f))
			{
				throw new Exception("Font is not registered");
			}

			string key = font + glyph + scalePx + color;

			if (glyphCache.TryGet(key, out GlyphResult cached))
			{
				return cached;
			}

			float scale = f.ScaleInPixels(scalePx);
			var glyphRendered = f.RenderGlyph(glyph, scale);
			var image = glyphRendered.Image;

			var bmp = new Bitmap((uint)image.Width, (uint)image.Height, ColorDepth.ColorDepth32);

			for (int j = 0; j < image.Height; j++)
			{
				for (int i = 0; i < image.Width; i++)
				{
					byte alpha = image.Pixels[i + j * image.Width];

					if (alpha > 0)
					{
						bmp.RawData[i + j * image.Width] = ((int)alpha << 24) + rgbOffset;
					}
				}
			}

			glyphCache[key] = new GlyphResult(bmp, glyphRendered.xAdvance, glyphRendered.yOfs);
			return new GlyphResult(bmp, glyphRendered.xAdvance, glyphRendered.yOfs);
		}



		/// <summary>
		/// Draws a string using the registered TTF font provided under the font parameter.
		/// </summary>
		/// <param name="cv"></param>
		/// <param name="pen"></param>
		/// <param name="text"></param>
		/// <param name="font"></param>
		/// <param name="px"></param>
		/// <param name="point"></param>
		public static void DrawStringTTF(this Canvas cv, string text, string font, Color color, float px, int x, int y, float spacingMultiplier = 1f)
		{
			float offx = 0;
			float offY = 0;

			foreach (char c in text)
			{
				char curr = c;
				GlyphResult g = RenderGlyphAsBitmap(font, curr, color, px);
				GUI.MainCanvas.DrawImageAlpha(g.bmp, x + (int)offx, y + g.offY + (int)px);
				offx += g.offX;
			}
		}

		public enum CachedFont { KMB18, KMB18Dark, KMB18VeryVeryDark }; //Add your cached fonts here
		public static void DrawStringTTFCached(this Canvas cv, string text, CachedFont font, int x, int y, float spacingMultiplier = 1f)
		{
			prevCanv = cv;
			float offx = 0;
			foreach (char c in text)
			{				char curre = c;
				int px = 24;
				Bitmap currB = TTFCache.KMB18Def[31].bitmap;
				char curr = c;
				switch (font)
				{
					case CachedFont.KMB18:
						px = 18;
						currB = TTFCache.KMB18Def[(int)curr].bitmap;
						break;
					case CachedFont.KMB18Dark:
						px = 18;
						currB = TTFCache.KMB18Dark[(int)curr].bitmap;
						break;
					case CachedFont.KMB18VeryVeryDark:
						px = 18;
						currB = TTFCache.KMB18VVDark[(int)curr].bitmap;
						break;
				}
				GUI.MainCanvas.DrawImage(currB, x + (int)offx, y);
				switch (font)
				{
					default:
						offx += TTFCache.KMB18Def[(int)c].offX;
						break;
				}
			}
		}

		public static void DrawCenteredTTFString(this Canvas cv, string myString, int WinLength, int WinPosx, int WinPosY, int space, Color color, string fontName, int fontSize)
		{
			string[] strings = myString.Split(new string[] { "\n" }, StringSplitOptions.None).Select(s => s.Trim()).ToArray();
			for (int i = 0; i < strings.Length; i++)
			{
				int length = TTFManager.GetTTFWidth(strings[i], fontName, fontSize-1);

				int posx = (WinLength - length) / 2;
				cv.DrawStringTTF(strings[i], fontName, color, fontSize, posx + WinPosx, WinPosY + i * space);
			}
		}

		public static void DrawCenteredCachedTTFString(this Canvas cv, string myString, int WinLength, int WinPosx, int WinPosY, int space, CachedFont font)
		{
			string[] strings = myString.Split(new string[] { "\n" }, StringSplitOptions.None).Select(s => s.Trim()).ToArray();
			for (int i = 0; i < strings.Length; i++)
			{
				string fontName = "KMB";
				int fontSize = 18;
				switch (font)
				{
					case CachedFont.KMB18:
						fontSize = 18;
						fontName = "KMB";
						break;
				}
				int length = TTFManager.GetTTFWidth(strings[i], fontName, fontSize - 1);

				int posx = (WinLength - length) / 2;
				cv.DrawStringTTFCached(strings[i], font, posx + WinPosx, WinPosY + i * space);
			}
		}

		public static int GetTTFWidth(this string text, string font, float px)
		{
			if (!fonts.TryGet(font, out Font f))
			{
				throw new Exception("Font is not registered");
			}

			float scale = f.ScaleInPixels(px);
			int totalWidth = 0;

			foreach (char c in text)
			{
				f.GetCodepointHMetrics(c, out int advWidth, out int lsb);
				totalWidth += advWidth;
			}

			return (int)(totalWidth * scale);
		}

		internal static void DebugUIPrint(string txt, int offY = 0)
		{
			prevCanv.DrawFilledRectangle(Color.Black, 0, offY, 1000, 16);
			prevCanv.DrawString(txt, Cosmos.System.Graphics.Fonts.PCScreenFont.Default, Color.White, 16, offY);
			prevCanv.Display();
		}
	}

	public struct GlyphResult
	{
		public Bitmap bmp;
		public int offY = 0;
		public int offX = 0;

		public GlyphResult(Bitmap bmp, int offX, int offY)
		{
			this.bmp = bmp;
			this.offX = offX;
			this.offY = offY;
		}
	}
}