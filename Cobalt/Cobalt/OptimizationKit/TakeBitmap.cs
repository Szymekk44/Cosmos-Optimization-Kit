using Cosmos.System.Graphics;
using Cobalt.Graphics;
using Cobalt.Cobalt.Apps;

namespace Cobalt.GetIMG
{
	public static class TakeBitmap
	{
		/// <summary>
		/// With this function you can cache pixels from your canvas!
		/// </summary>
		public static Bitmap GetImage(int X, int Y, int Width, int Height)
		{
			#region Delete this part! - It is for debugging purposes only!
			if (!OptimizationTest.Optimized)
				return null;
			#endregion
			Bitmap bitmap = new Bitmap((uint)Width, (uint)Height, ColorDepth.ColorDepth32);

			for (int y = Y, destY = 0; y < Y + Height; y++, destY++)
				for (int x = X, destX = 0; x < X + Width; x++, destX++)
					bitmap.RawData[destY * Width + destX] = GUI.MainCanvas.GetPointColor(x, y).ToArgb();

			return bitmap;
		}
	}
}
